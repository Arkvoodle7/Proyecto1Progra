import socket
import threading
import configparser

# archivo de configuracion
config = configparser.ConfigParser()
config.read('Config.ini')
puerto = int(config['Orquestador']['puerto'])
#

def orquestador():
    #objeto socket para el servidor
    orquestador_Socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    orquestador_Socket.bind(('localhost', puerto))
    #escucha las conexiones y permite tener 5 en cola
    orquestador_Socket.listen(5)
    print(f"Orquestador escuchando en el puerto {puerto}...")

    #hilo para manejar el cliente
    while True:
        #acepta nuevas conexiones
        receptorExterno_socket, receptor_direccion = orquestador_Socket.accept()
        #crear un nuevo hilo para manejar la conexion del cliente
        #cliente_thread = threading.Thread(target = Metodo_para_manejar_receptor, args = (receptorExterno_socket, receptor_direccion))
        #cliente_thread.start()

orquestador()