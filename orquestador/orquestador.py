import socket
import threading
import configparser
import xml.etree.ElementTree as xml
from Validaciones_Transaccion import ValidadorTransaccion

# archivo de configuracion
config = configparser.ConfigParser()
config.read('Config.ini')
puerto = int(config['Orquestador']['puerto'])
puerto_bancario = int(config['Banco']['puerto'])  # puerto del socket bancario
puerto_receptor_externo = int(config['ReceptorExterno']['puerto'])  # puerto del socket receptor externo


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

        # si pasa las validaciones, sabemos que el cliente esta registrado en pagos moviles
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


def orquestador():
    # objeto socket para el servidor
    orquestador_Socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    orquestador_Socket.bind(('localhost', puerto))
    # escucha las conexiones y permite tener 5 en cola
    orquestador_Socket.listen(5)
    print(f"Orquestador escuchando en el puerto {puerto}...")

    # hilo para manejar el cliente
    while True:
        # acepta nuevas conexiones
        receptorExterno_socket, receptor_direccion = orquestador_Socket.accept()
        # crear un nuevo hilo para manejar la conexion del cliente
        cliente_thread = threading.Thread(target=recibir_trama, args=(receptorExterno_socket,))
        cliente_thread.start()


if __name__ == "__main__":
    orquestador()