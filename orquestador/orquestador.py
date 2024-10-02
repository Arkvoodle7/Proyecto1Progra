import threading
import socket
import configparser
import xml.etree.ElementTree as ET
from ValidarInscripcion import ValidadorInscripcion  

# URI de MongoDB
mongo_uri = "mongodb://localhost:27017" 

# Instancia del validador
validador = ValidadorInscripcion(mongo_uri)


# archivo de configuración
config = configparser.ConfigParser()
config.read('D:/U/Programacion 4/Proyecto1Progra/orquestador/Config.ini')
puerto = int(config['Orquestador']['puerto'])

def manejar_cliente(cliente_socket):
    try:
        data = cliente_socket.recv(4096).decode('utf-8')  # Recibe datos
        if not data:
            print("No se recibieron datos del cliente.")
            return
        
        print(f"Datos recibidos: {data}")
        
        # Parsear el XML recibido
        root = ET.fromstring(data)
        cuenta = root.find('cuenta').text
        identificacion = root.find('identificacion').text
        telefono = root.find('telefono').text

        if root.tag == "inscripcion":
            # Validar los datos de inscripción
            es_valido, mensaje = validador.validar_datos(cuenta, identificacion, telefono)
            
            if es_valido:
                if mensaje is not None:
                    # Si la cuenta fue reactivada, enviar la respuesta de éxito
                    cliente_socket.sendall(mensaje.encode('utf-8'))
                else:
                    # Si es una nueva inscripción, registrar en la base de datos
                    validador.registrar_asociacion(cuenta, identificacion, telefono)
                    respuesta = "<respuesta><codigo>0</codigo><descripcion>Inscripción realizada</descripcion></respuesta>"
                    cliente_socket.sendall(respuesta.encode('utf-8'))
            else:
                # Si los datos son inválidos, enviar el mensaje de error
                cliente_socket.sendall(mensaje.encode('utf-8'))

        elif root.tag == "desinscripcion":
            # Lógica para desinscripción
            es_valido, mensaje = validador.validar_datos_desinscripcion(cuenta, identificacion, telefono)
            
            if es_valido:
                # Si los datos son válidos, proceder con la desinscripción
                respuesta = validador.desinscribir_telefono(cuenta, identificacion, telefono)
            else:
                # Si los datos no son válidos, enviar el mensaje de error
                respuesta = mensaje
            
            cliente_socket.sendall(respuesta.encode('utf-8'))

    except Exception as e:
        print(f"Error al manejar cliente: {e}")
    
    finally:
        cliente_socket.close()

def orquestador():
    # objeto socket para el servidor
    orquestador_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    orquestador_socket.bind(('localhost', puerto))
    orquestador_socket.listen(5)
    print(f"Orquestador escuchando en el puerto {puerto}...")

    while True:
        receptor_externo_socket, receptor_direccion = orquestador_socket.accept()
        print(f"Conexión recibida de {receptor_direccion}")
        cliente_thread = threading.Thread(target=manejar_cliente, args=(receptor_externo_socket,))
        cliente_thread.start()

orquestador()
