import threading
import socket
import configparser
import xml.etree.ElementTree as ET


#constructor 
from Validaciones_Transaccion import ValidadorTransaccion

class OrquestadorSocket:

    def __init__(self):
        # archivo de configuración
        config = configparser.ConfigParser()
        config.read('Config.ini')
        self.puerto = int(config['Orquestador']['puerto'])
        self.puerto_bancario = int(config['Banco']['puerto'])  # puerto del socket bancario
        self.puerto_receptor_externo = int(config['ReceptorExterno']['puerto'])  # puerto del socket receptor externo
        self.validador = ValidadorTransaccion()

    # trama de envio del saldo
    def enviar_trama_saldo(self, identificacion, cuenta):
        try:
            bancario_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            bancario_socket.connect(('localhost', self.puerto_bancario))  # Cambiar el puerto si es necesario

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

    def enviar_trama_bancaria(self, identificacion, cuenta, monto, tipo):
        try:
            # conexion al socket bancario
            bancario_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            bancario_socket.connect(('localhost', self.puerto_bancario))

            # crea la trama para enviar al socket bancario
            trama_banco = f"{identificacion}|{cuenta}|{monto}|{tipo}"
            bancario_socket.send(trama_banco.encode('utf-8'))

            # espera la respuesta del socket bancario
            respuesta_banco = bancario_socket.recv(1024).decode('utf-8')
            bancario_socket.close()

            return respuesta_banco
        except socket.error as e:
            print(f"Error de socket al conectar con el banco: {e}")
            return None

    def enviar_trama_receptor_externo(self, telefono, monto, descripcion):
        try:
            # conexion al socket del receptor externo
            receptor_externo_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            receptor_externo_socket.connect(('localhost', self.puerto_receptor_externo))

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

    def recibe_consulta_saldo(self, client_socket):
        data = client_socket.recv(1024).decode('utf-8')
        print("Trama recibida en el orquestador para consulta de saldo:")
        print(data)

        try:
            consulta_saldo_xml = ET.fromstring(data)

            telefono = consulta_saldo_xml.find('telefono').text

            respuesta_error = self.validador.validarTransaccion(telefono, "0", "Consulta de saldo")
            if respuesta_error:
                print(respuesta_error)
                client_socket.send(respuesta_error.encode('utf-8'))
                return

            # Obtener los datos del cliente (ya validado en ValidadorTransaccion)
            registro_cliente = self.validador.obtenerCliente(telefono)
            identificacion = registro_cliente['identificacion']
            cuenta = registro_cliente['numero_cuenta']

            # trama para obtener el saldo
            respuesta_banco = self.enviar_trama_saldo(identificacion, cuenta)

            # procesamiento de la respuesta de banco
            if respuesta_banco and respuesta_banco.startswith("OK|"):
                saldo_cliente = respuesta_banco.split('|')[1]

                # respuesta de exito con xml
                respuesta_exito = f"<respuesta><codigo>0</codigo><saldo>{saldo_cliente}</saldo></respuesta>"
                print(f"Respuesta enviada: {respuesta_exito}")
                client_socket.send(respuesta_exito.encode('utf-8'))
            else:
                # respuesta de error con xml
                respuesta_error_banco = self.validador.generarError(f"Error desde el banco: {respuesta_banco}")
                print(f"Error recibido del banco: {respuesta_banco}")
                client_socket.send(respuesta_error_banco.encode('utf-8'))

        except ET.ParseError:
            respuesta_error = self.validador.generarError("Error al parsear la trama XML")
            print(respuesta_error)

    def manejar_cliente(self, cliente_socket):
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
                es_valido, mensaje = self.validador.validar_datos(cuenta, identificacion, telefono)

                if es_valido:
                    if mensaje is not None:
                        # Si la cuenta fue reactivada, enviar la respuesta de éxito
                        cliente_socket.sendall(mensaje.encode('utf-8'))
                    else:
                        # Si es una nueva inscripción, registrar en la base de datos
                        self.validador.registrar_asociacion(cuenta, identificacion, telefono)
                        respuesta = "<respuesta><codigo>0</codigo><descripcion>Inscripción realizada</descripcion></respuesta>"
                        cliente_socket.sendall(respuesta.encode('utf-8'))
                else:
                    # Si los datos son inválidos, enviar el mensaje de error
                    cliente_socket.sendall(mensaje.encode('utf-8'))

            elif root.tag == "desinscripcion":
                # Lógica para desinscripción
                es_valido, mensaje = self.validador.validar_datos_desinscripcion(cuenta, identificacion, telefono)

                if es_valido:
                    # Si los datos son válidos, proceder con la desinscripción
                    respuesta = self.validador.desinscribir_telefono(cuenta, identificacion, telefono)
                else:
                    # Si los datos no son válidos, enviar el mensaje de error
                    respuesta = mensaje

                cliente_socket.sendall(respuesta.encode('utf-8'))

        except Exception as e:
            print(f"Error al manejar cliente: {e}")

        finally:
            cliente_socket.close()


    def recibir_trama(self, client_socket):
        # recibe la trama del receptor externo
        data = client_socket.recv(1024).decode('utf-8')  # recibe hasta 1024 bytes en UTF-8
        print("Trama recibida en el orquestador:")
        print(data)

        try:
            transaccion = ET.fromstring(data)

            # informacion de la transaccion
            telefono = transaccion.find('telefono').text
            monto = transaccion.find('monto').text
            descripcion = transaccion.find('descripcion').text

            # valida la transaccion
            respuesta_error = self.validador.validarTransaccion(telefono, monto, descripcion)
            if respuesta_error:
                print(respuesta_error)
                client_socket.send(respuesta_error.encode('utf-8'))
                return

            # si pasa las validaciones sabemos que el cliente esta registrado en pagos moviles
            registro_cliente = self.validador.obtenerCliente(telefono)
            identificacion = registro_cliente["identificacion"]
            cuenta = registro_cliente["numero_cuenta"]

            # enviar la trama al socket bancario
            tipo_transaccion = "CRE"
            respuesta_banco = self.enviar_trama_bancaria(identificacion, cuenta, monto, tipo_transaccion)

            if respuesta_banco == "OK|Transacción aplicada":
                respuesta_exito = self.validador.generarExito()
                print(respuesta_exito)
                client_socket.send(respuesta_exito.encode('utf-8'))
            else:
                respuesta_error_banco = self.validador.generarError(respuesta_banco)
                print(respuesta_error_banco)
                client_socket.send(respuesta_error_banco.encode('utf-8'))

        except ET.ParseError:
            respuesta_error = self.validador.generarError("Error al parsear la trama XML")
            print(respuesta_error)
            client_socket.send(respuesta_error.encode('utf-8'))

    def handle_client(self, client_socket):
        while True:
            try:
                # Aquí se decide qué operación realizar según la trama recibida
                data = client_socket.recv(1024)
                if not data:
                    break
                data = data.replace(b'\n', b'').replace(b'\r', b'')
                print(f"Datos recibidos: {data.decode('utf-8')}")
                trama = ET.fromstring(data.decode('utf-8'))

                validador = ValidadorTransaccion()

                # Identificar el tipo de trama y procesarla
                if trama.tag == "saldo":
                    # ORQ6 - Consulta de saldo
                    telefono = trama.find('telefono').text
                    registro_cliente = self.validador.obtenerCliente(telefono)

                    if registro_cliente:
                        identificacion = registro_cliente['identificacion']
                        cuenta = registro_cliente['numero_cuenta']

                        # Enviar la trama de saldo al sistema bancario
                        respuesta_banco = self.enviar_trama_saldo(identificacion, cuenta)

                        # Manejar la respuesta del banco
                        if respuesta_banco and respuesta_banco.startswith("OK|"):
                            saldo_cliente = respuesta_banco.split('|')[1]
                            respuesta_exito = f"<respuesta><codigo>0</codigo><saldo>{saldo_cliente}</saldo></respuesta>"
                            client_socket.send(respuesta_exito.encode('utf-8'))
                        else:
                            respuesta_error_banco = self.validador.generarError(f"Error del banco: {respuesta_banco}")
                            client_socket.send(respuesta_error_banco.encode('utf-8'))
                    else:
                        respuesta_error = self.validador.generarError("Cliente no asociado a pagos móviles")
                        client_socket.send(respuesta_error.encode('utf-8'))

                elif trama.tag == "transaccion":
                    # ORQ2 y ORQ5 - Procesamiento de transacciones
                    telefono = trama.find('telefono').text
                    monto = trama.find('monto').text
                    descripcion = trama.find('descripcion').text

                     # Validar la transacción
                    respuesta_error = self.validador.validarTransaccion(telefono, monto, descripcion)
                    if respuesta_error:
                        client_socket.send(respuesta_error.encode('utf-8'))
                        return

                    # Obtener los datos del cliente
                    registro_cliente = self.validador.obtenerCliente(telefono)
                    if registro_cliente:
                        identificacion = registro_cliente['identificacion']
                        cuenta = registro_cliente['numero_cuenta']

                        # Enviar la trama bancaria
                        tipo_transaccion = "CRE"
                        respuesta_banco = self.enviar_trama_bancaria(identificacion, cuenta, monto, tipo_transaccion)

                        # Manejar la respuesta del banco
                        if respuesta_banco and respuesta_banco.startswith("OK|"):
                            respuesta_exito = self.validador.generarExito()
                            client_socket.send(respuesta_exito.encode('utf-8'))
                        else:
                            respuesta_error_banco = self.validador.generarError(respuesta_banco)
                            client_socket.send(respuesta_error_banco.encode('utf-8'))
                    else:
                        # Si no está registrado en pagos móviles, enviar al receptor externo (ORQ5)
                        respuesta_receptor = self.enviar_trama_receptor_externo(telefono, monto, descripcion)

                    # Manejar la respuesta del receptor externo
                    if respuesta_receptor and respuesta_receptor.startswith('{"codigo": 0'):
                        client_socket.send(respuesta_receptor.encode('utf-8'))
                    else:
                        respuesta_error_receptor = self.validador.generarError(f"Error en receptor externo: {respuesta_receptor}")
                        client_socket.send(respuesta_error_receptor.encode('utf-8'))


                else:
                    # Manejar otras tramas como inscripción o desinscripción
                    if trama.tag in ["inscripcion", "desinscripcion"]:
                        self.manejar_cliente(client_socket)


            except ConnectionResetError:
                print("La conexión ha sido interrumpida por el host remoto.")
                client_socket.close()
            except OSError:
                print("El cliente ha cerrado la conexión.")
        client_socket.close()

    def start(self):
        # objeto socket para el servidor
        Orquestador = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        Orquestador.bind(('localhost', self.puerto))
        Orquestador.listen(5)
        print(f"Orquestador en el puerto {self.puerto}...")

        while True:
            client_socket, client_address = Orquestador.accept()
            print(f"Conexión recibida de {client_address}")
            cliente_thread = threading.Thread(target=self.handle_client, args=(client_socket,))
            cliente_thread.start()


if __name__ == "__main__":
    orquestador = OrquestadorSocket()
    orquestador.start()