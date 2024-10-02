import threading
import socket
import configparser
import xml.etree.ElementTree as ET
from ValidarInscripcion import ValidadorInscripcion  

# URI de MongoDB
mongo_uri = "mongodb://localhost:27017" 

# Instancia del validador
validador = ValidadorInscripcion(mongo_uri)
import xml.etree.ElementTree as xml
from Validaciones_Transaccion import ValidadorTransaccion


# archivo de configuración
config = configparser.ConfigParser()
config.read('Config.ini')
puerto = int(config['Orquestador']['puerto'])
puerto_bancario = int(config['Banco']['puerto'])  # puerto del socket bancario
puerto_receptor_externo = int(config['ReceptorExterno']['puerto'])  # puerto del socket receptor externo

# trama de envio del saldo
def enviar_trama_saldo(identificacion, cuenta):
    try:
        bancario_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        bancario_socket.connect(('localhost', 8080))  # Cambiar el puerto si es necesario

        # crea la trama sobre el saldo para bancario
        trama_saldo = f"{identificacion}|{cuenta}"
        bancario_socket.send(trama_saldo.encode('utf-8'))

        # recibe la respuesta de bancario
        respuesta_banco = bancario_socket.recv(1024).decode('utf-8')
        bancario_socket.close()

        return respuesta_banco
    except socket.error as e:
        print(f"Error de socket al conectar con el banco: {e}")
        return None
    
def enviar_trama_bancaria(identificacion, cuenta, monto, tipo):
    try:
        #conexion al socket bancario
        bancario_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        bancario_socket.connect(('localhost', puerto_bancario))

        #crea la trama para enviar al socket bancario
        trama_banco = f"{identificacion}|{cuenta}|{monto}|{tipo}"
        bancario_socket.send(trama_banco.encode('utf-8'))

        #espera la respuesta del socket bancario
        respuesta_banco = bancario_socket.recv(1024).decode('utf-8')
        bancario_socket.close()

        return respuesta_banco
    except socket.error as e:
        print(f"Error de socket al conectar con el banco: {e}")
        return None


def enviar_trama_receptor_externo(telefono, monto, descripcion):
    try:
        # conexion al socket del receptor externo
        receptor_externo_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        receptor_externo_socket.connect(('localhost', puerto_receptor_externo))

        # crea la trama en formato JSON para el receptor externo
        trama_receptor = f'{{"telefono":"{telefono}", "monto":{monto}, "descripcion":"{descripcion}"}}'
        receptor_externo_socket.send(trama_receptor.encode('utf-8'))

        # espera la respuesta del receptor externo
        respuesta_receptor = receptor_externo_socket.recv(1024).decode('utf-8')
        receptor_externo_socket.close()

        return respuesta_receptor
    except socket.error as e:
        print(f"Error de socket al conectar con el receptor externo: {e}")
        return None

def recibe_consulta_saldo(orquestador_socket):
    data = orquestador_socket.recv(1024).decode('utf-8')
    print("Trama recibida en el orquestador para consulta de saldo:")
    print(data)
     
    try:
        consulta_saldo_xml = ET.fromstring(data)
        
        telefono = consulta_saldo_xml.find('telefono').text
        
        validador = ValidadorTransaccion()

        respuesta_error = validador.validar_transaccion(telefono, "0", "Consulta de saldo")
        if respuesta_error:
            print(respuesta_error)
            orquestador_socket.send(respuesta_error.encode('utf-8'))
            return
        
        # Obtener los datos del cliente (ya validado en ValidadorTransaccion)
        registro_cliente = validador.obtener_registro_cliente(telefono)
        identificacion = registro_cliente['identificacion']
        cuenta = registro_cliente['numero_cuenta']

        # trama para obtener el saldo
        respuesta_banco = enviar_trama_bancaria(identificacion, cuenta)

        # procesamiento de la respuesta de banco
        if respuesta_banco and respuesta_banco.startswith("OK|"):
            saldo_cliente = respuesta_banco.split('|')[1]  
            
            # respuesta de exito con xml
            respuesta_exito = f"<respuesta><codigo>0</codigo><saldo>{saldo_cliente}</saldo></respuesta>"
            print(f"Respuesta enviada: {respuesta_exito}")
            orquestador_socket.send(respuesta_exito.encode('utf-8'))
        else:
            # respuesta de error con xml
            respuesta_error_banco = validador.generar_respuesta_error(f"Error desde el banco: {respuesta_banco}")
            print(f"Error recibido del banco: {respuesta_banco}")
            orquestador_socket.send(respuesta_error_banco.encode('utf-8'))

    except xml.ParseError:
        respuesta_error = ValidadorTransaccion().generar_respuesta_error("Error al parsear la trama XML")
        print(respuesta_error)
    

def recibir_trama(orquestador_Socket):
    # recibe la trama del receptor externo
    data = orquestador_Socket.recv(1024).decode('utf-8')  # recibe hasta 1024 bytes en UTF-8
    print("Trama recibida en el orquestador:")
    print(data)

    try:
        transaccion = xml.fromstring(data)

        # informacion de la transaccion
        telefono = transaccion.find('telefono').text
        monto = transaccion.find('monto').text
        descripcion = transaccion.find('descripcion').text

        validador = ValidadorTransaccion()

        # valida la transaccion
        respuesta_error = validador.validar_transaccion(telefono, monto, descripcion)
        if respuesta_error:
            print(respuesta_error)
            orquestador_Socket.send(respuesta_error.encode('utf-8'))
            return

        # si pasa las validaciones sabemos que el cliente esta registrado en pagos moviles
        registro_cliente = validador.obtener_registro_cliente(telefono)
        identificacion = registro_cliente["identificacion"]
        cuenta = registro_cliente["numero_cuenta"]

        # enviar la trama al socket bancario
        tipo_transaccion = "CRE"
        respuesta_banco = enviar_trama_bancaria(identificacion, cuenta, monto, tipo_transaccion)

        if respuesta_banco == "OK|Transacción aplicada":
            respuesta_exito = validador.generar_respuesta_exito()
            print(respuesta_exito)
            orquestador_Socket.send(respuesta_exito.encode('utf-8'))
        else:
            respuesta_error_banco = validador.generar_respuesta_error(respuesta_banco)
            print(respuesta_error_banco)
            orquestador_Socket.send(respuesta_error_banco.encode('utf-8'))

    except xml.ParseError:
        respuesta_error = ValidadorTransaccion().generar_respuesta_error("Error al parsear la trama XML")
        print(respuesta_error)
        orquestador_Socket.send(respuesta_error.encode('utf-8'))

# def manejar_cliente(cliente_socket):
#     try:
#         data = cliente_socket.recv(4096).decode('utf-8')  # Recibe datos
#         if not data:
#             print("No se recibieron datos del cliente.")
#             return
        
#         print(f"Datos recibidos: {data}")
        
#         # Parsear el XML recibido
#         root = ET.fromstring(data)
#         cuenta = root.find('cuenta').text
#         identificacion = root.find('identificacion').text
#         telefono = root.find('telefono').text

#         if root.tag == "inscripcion":
#             # Validar los datos de inscripción
#             es_valido, mensaje = validador.validar_datos(cuenta, identificacion, telefono)
            
#             if es_valido:
#                 if mensaje is not None:
#                     # Si la cuenta fue reactivada, enviar la respuesta de éxito
#                     cliente_socket.sendall(mensaje.encode('utf-8'))
#                 else:
#                     # Si es una nueva inscripción, registrar en la base de datos
#                     validador.registrar_asociacion(cuenta, identificacion, telefono)
#                     respuesta = "<respuesta><codigo>0</codigo><descripcion>Inscripción realizada</descripcion></respuesta>"
#                     cliente_socket.sendall(respuesta.encode('utf-8'))
#             else:
#                 # Si los datos son inválidos, enviar el mensaje de error
#                 cliente_socket.sendall(mensaje.encode('utf-8'))

#         elif root.tag == "desinscripcion":
#             # Lógica para desinscripción
#             es_valido, mensaje = validador.validar_datos_desinscripcion(cuenta, identificacion, telefono)
            
#             if es_valido:
#                 # Si los datos son válidos, proceder con la desinscripción
#                 respuesta = validador.desinscribir_telefono(cuenta, identificacion, telefono)
#             else:
#                 # Si los datos no son válidos, enviar el mensaje de error
#                 respuesta = mensaje
            
#             cliente_socket.sendall(respuesta.encode('utf-8'))

#     except Exception as e:
#         print(f"Error al manejar cliente: {e}")
    
#     finally:
#         cliente_socket.close()

def orquestador():
    # objeto socket para el servidor
    orquestador_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    orquestador_socket.bind(('localhost', puerto))
    orquestador_socket.listen(5)
    print(f"Orquestador escuchando en el puerto {puerto}...")

    while True:
        receptor_externo_socket, receptor_direccion = orquestador_socket.accept()
        print(f"Conexión recibida de {receptor_direccion}")
        cliente_thread = threading.Thread(target=recibir_trama, args=(receptor_externo_socket,))
        cliente_thread.start()

if __name__ == "__main__":
    orquestador()
