import threading
import socket
import configparser
import xml.etree.ElementTree as ET
import json
from pymongo import MongoClient

#constructor 
from Validaciones_Transaccion import ValidadorTransaccion
from GestorInscripcion import GestorInscripcion
from ValidarInscripcion import ValidadorInscripcion


class OrquestadorSocket:

    def __init__(self):
        # archivo de configuración
        config = configparser.ConfigParser()
        config.read('C:/Users/alexl/source/repos/Proyecto1Progra/orquestador/Config.ini')
        
        self.puerto_interno = int(config['Orquestador']['puerto_interno'])
        self.puerto_externo = int(config['Orquestador']['puerto_externo'])
        self.puerto_bancario = int(config['Banco']['puerto'])  # puerto del socket bancario
        self.puerto_receptor_externo = int(config['ReceptorExterno']['puerto'])  # puerto del socket receptor externo
        
        self.validador = ValidadorTransaccion()
        self.validadori = ValidadorInscripcion

        # Inicializar la conexión a MongoDB
        self.mongo_uri = config['MongoDB']['uri']
        self.mongo_client = MongoClient(self.mongo_uri)
        self.mongo_db = self.mongo_client.PagosMovilesOrquestador  # Reemplaza por el nombre de la base de datos correcto

        self.gestor = GestorInscripcion(self.mongo_uri)

    def start(self):
     # objeto socket para el servidor
        Orquestador_interno = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        Orquestador_externo = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        Orquestador_interno.bind(('localhost', self.puerto_interno))
        Orquestador_externo.bind(('localhost', self.puerto_externo))
        Orquestador_interno.listen(5)
        Orquestador_externo.listen(5)
        print(f"Orquestador en el puerto {self.puerto_interno} y {self.puerto_externo}...")

        threading.Thread(target=self.accept_connections, args=(Orquestador_interno, True)).start()
        threading.Thread(target=self.accept_connections, args=(Orquestador_externo, False)).start()

    def accept_connections(self, Orquestador_socket, es_interno):
        while True:
            client_socket, client_address = Orquestador_socket.accept()
            print(f"Conexión recibida de {client_address}")
            cliente_thread = threading.Thread(target=self.handle_client, args=(client_socket, es_interno))
            cliente_thread.start()

    def handle_client(self, client_socket, es_interno):
        """
        Manejar las diferentes tramas recibidas.
        """
        while True:
            try:
                data = client_socket.recv(1024)
                if not data:
                    break
        
                data = data.replace(b'\n', b'').replace(b'\r', b'')
                print(f"Datos recibidos: {data.decode('utf-8')}")
                root = ET.fromstring(data.decode('utf-8'))
                print(f"Tag recibido: {root.tag}")
        
                if root.tag == "transaccion":
                    print("Llamando a manejar_transaccion con los datos recibidos.")
                    
                    # Extraer los valores correctos del XML
                    telefono = root.find('telefono').text
                    monto = root.find('monto').text
                    descripcion = root.find('descripcion').text

                    # Llamar a manejar_transaccion con los datos extraídos
                    respuesta = self.manejar_transaccion(telefono, monto, descripcion, es_interno)
                    
                    # Enviar la respuesta al cliente
                    client_socket.send(respuesta.encode('utf-8'))

                elif root.tag == "saldo":
                    self.recibe_consulta_saldo(root, client_socket)
                elif root.tag in ["inscripcion", "desinscripcion"]:
                    self.manejar_cliente(client_socket, root)

            except ConnectionResetError:
                print("La conexión ha sido interrumpida por el host remoto.")
                break
            except OSError:
                print("El cliente ha cerrado la conexión.")
                break
            except Exception as e:
                print(f"Error al procesar la trama: {e}")
                break

        client_socket.close()


    def manejar_transaccion(self, telefono, monto, descripcion, es_interno):
        # Lógica para manejar la transacción
        print(f"Manejando transacción con teléfono: {telefono}, monto: {monto}, descripción: {descripcion}")

        # Validación de la transacción
        respuesta_error = self.validador.validarTransaccion(telefono, monto, descripcion, es_interno)
        if respuesta_error:
            print(respuesta_error)
            return respuesta_error

        # Obtener el cliente asociado
        registro_cliente = self.validador.obtenerCliente(telefono)

        if registro_cliente is None:
            identificacion = "000000000"
            cuenta = "000000000"
        else:
            identificacion = registro_cliente.get("identificacion", "000000000")
            cuenta = registro_cliente.get("numero_cuenta", "000000000")

        # Procesar la trama
        respuesta = self.procesar_trama(telefono, identificacion, cuenta, monto, descripcion)

        # Retornar la respuesta al cliente (ya en formato XML o JSON)
        return respuesta

    def procesar_trama(self, telefono, identificacion, cuenta, monto, descripcion):
        print("Procesando trama...")
        cuenta_existente = self.mongo_db.TelefonosXCuentas.find_one({"telefono": telefono})

        if cuenta_existente:
            # Transacción interna
            print("Transacción interna. Enviando al socket bancario.")
            respuesta_bancaria = self.enviar_trama_bancaria(identificacion, cuenta, monto, "CRE", es_interno=True)
            return respuesta_bancaria
        else:
            # Transacción externa
            print("Transacción externa. Enviando al Receptor Externo.")
            respuesta_externa = self.enviar_trama_receptor_externo(telefono, monto, descripcion)
            return respuesta_externa

    def enviar_trama_bancaria(self, identificacion, cuenta, monto, tipo, es_interno):
        try:
            # Intentar conectar al socket bancario
            bancario_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            print(f"Intentando conectar al socket bancario en el puerto {self.puerto_bancario}")
            bancario_socket.connect(('localhost', self.puerto_bancario))
            print("Conexión con el socket bancario establecida")

            # Enviar la trama al socket bancario
            trama_banco = f"{identificacion}|{cuenta}|{monto}|{tipo}"
            print(f"Enviando trama al socket bancario: {trama_banco}")
            bancario_socket.send((trama_banco + "\n").encode('utf-8'))
            print("Trama enviada exitosamente al socket bancario.")

            # Recibir la respuesta del socket bancario
            respuesta_banco = bancario_socket.recv(1024).decode('utf-8')
            print(f"Respuesta recibida del socket bancario: {respuesta_banco}")
            bancario_socket.close()

            # Crear la respuesta XML utilizando ElementTree
            respuesta_xml = ET.Element('respuesta')

            if respuesta_banco.startswith('OK'):
                # Respuesta exitosa
                ET.SubElement(respuesta_xml, 'codigo').text = '0'
                ET.SubElement(respuesta_xml, 'descripcion').text = 'Transacción realizada'
            else:
                # Respuesta de error con el mensaje del banco
                ET.SubElement(respuesta_xml, 'codigo').text = '-1'
                ET.SubElement(respuesta_xml, 'descripcion').text = respuesta_banco

            # Convertir el árbol XML a una cadena
            respuesta_xml_str = ET.tostring(respuesta_xml, encoding='utf-8').decode('utf-8')

            # Si la trama proviene del Receptor Externo, convertir a JSON
            if not es_interno:
                respuesta_json = {
                    "codigo": -1 if respuesta_banco.startswith('ERROR') else 0,
                    "descripcion": respuesta_banco
                }
                print(f"Enviando respuesta en JSON: {respuesta_json}")
                return json.dumps(respuesta_json)

            # Si la trama proviene del SimuladorInterno, devolver respuesta en XML
            print(f"Enviando respuesta en XML: {respuesta_xml_str}")
            return respuesta_xml_str

        except socket.error as e:
            print(f"Error de conexión con el socket bancario: {e}")
            error_response_xml = ET.Element('respuesta')
            ET.SubElement(error_response_xml, 'codigo').text = '-1'
            ET.SubElement(error_response_xml, 'descripcion').text = f"Error de conexión con el banco: {str(e)}"
            error_response_str = ET.tostring(error_response_xml, encoding='utf-8').decode('utf-8')

            if not es_interno:
                return json.dumps({
                    "codigo": -1,
                    "descripcion": f"Error de conexión con el banco: {str(e)}"
                })
            print(f"Enviando respuesta de error en XML: {error_response_str}")
            return error_response_str

    def enviar_trama_receptor_externo(self, telefono, monto, descripcion):
        try:
            # Conexión al socket del receptor externo
            receptor_externo_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            
            # Establecer un timeout de 20 segundos
            receptor_externo_socket.settimeout(20)  # Timeout de 20 segundos
            
            receptor_externo_socket.connect(('localhost', self.puerto_receptor_externo))

            # Crear la trama en formato JSON para el receptor externo
            trama_receptor = f'{{"telefono":"{telefono}", "monto":{monto}, "descripcion":"{descripcion}"}}'
            receptor_externo_socket.send(trama_receptor.encode('utf-8'))

            # Espera la respuesta del receptor externo con timeout
            respuesta_receptor = receptor_externo_socket.recv(1024).decode('utf-8')
            
            # Cerrar el socket
            receptor_externo_socket.close()

            return respuesta_receptor
        except socket.timeout:
            print("Timeout al esperar respuesta del Receptor Externo")
            return '{"codigo":-1, "descripcion":"Timeout al conectar con el Receptor Externo"}'
        except socket.error as e:
            print(f"Error de socket al conectar con el Receptor Externo: {e}")
            return '{"codigo":-1, "descripcion":"Error de conexión con el Receptor Externo"}'
        
    def manejar_cliente(self, cliente_socket, root):
        """
        Manejar la lógica de inscripción y desinscripción.
        """
        try:
            print("Inicio del manejo del cliente...")

            cuenta = root.find('cuenta').text
            identificacion = root.find('identificacion').text
            telefono = root.find('telefono').text

            print(f"Datos recibidos - Cuenta: {cuenta}, Identificación: {identificacion}, Teléfono: {telefono}")

            # Validar datos de inscripción
            if root.tag == "inscripcion":
                print("Procesando inscripción...")
                es_valido, mensaje = self.validadori.validar_datos(cuenta, identificacion, telefono)
                if not es_valido:
                    print(f"Error en la validación de inscripción: {mensaje}")
                    cliente_socket.sendall(mensaje.encode('utf-8'))
                    return  # Retornar aquí si hay un error

                es_valido, respuesta = self.gestor.verificar_telefono_asociado(cuenta, telefono)
                print(f"Verificación de teléfono asociado - es_valido: {es_valido}, respuesta: {respuesta}")
                if respuesta:
                    print("Error en la verificación del teléfono asociado.")
                    cliente_socket.sendall(respuesta.encode('utf-8'))
                    return  # Detener si hay error o reactivación

                if es_valido and respuesta is None:
                    print("Registro de asociación válido, registrando...")
                    respuesta_inscripcion = self.gestor.registrar_asociacion(cuenta, identificacion, telefono)
                    cliente_socket.sendall(respuesta_inscripcion.encode('utf-8'))
                    print(f"Respuesta de inscripción enviada: {respuesta_inscripcion}")

            elif root.tag == "desinscripcion":
                print("Procesando desinscripción...")
                es_valido, mensaje = self.validadori.validar_datos_desinscripcion(cuenta, identificacion, telefono)
                if not es_valido:
                    print(f"Error en la validación de desinscripción: {mensaje}")
                    cliente_socket.sendall(mensaje.encode('utf-8'))
                    return  # Error

                respuesta = self.gestor.desinscribir_telefono(cuenta, identificacion, telefono)
                cliente_socket.sendall(respuesta.encode('utf-8'))
                print(f"Respuesta de desinscripción enviada: {respuesta}")

        except Exception as e:
            print(f"Error al manejar cliente: {e}")
        finally:
            print("Cerrando la conexión con el cliente.")
            cliente_socket.close()

    # trama de envio del saldo
    def enviar_trama_saldo(self, identificacion, cuenta):
        try:
            bancario_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            bancario_socket.connect(('localhost', self.puerto_bancario))

            # Crea la trama sobre el saldo para bancario
            trama_saldo = f"{identificacion}|{cuenta}\n"  # Agregar '\n' al final
            bancario_socket.send(trama_saldo.encode('utf-8'))

            # Recibe la respuesta de bancario
            respuesta_banco = bancario_socket.recv(1024).decode('utf-8')
            bancario_socket.close()

            return respuesta_banco.strip()
        except socket.error as e:
            print(f"Error de socket al conectar con el banco: {e}")
            return None
        
    def recibe_consulta_saldo(self, root, client_socket):
        try:
            # Ya tenemos el objeto root, extraemos el teléfono
            telefono = root.find('telefono').text

            respuesta_error = self.validador.validarTransaccion(telefono, "0", "Consulta de saldo")
            if respuesta_error:
                print(respuesta_error)
                client_socket.send(respuesta_error.encode('utf-8'))
                return

            # Obtener los datos del cliente
            registro_cliente = self.validador.obtenerCliente(telefono)
            if not registro_cliente:
                respuesta_error = self.validador.generarError("Cliente no asociado a pagos móviles.")
                client_socket.send(respuesta_error.encode('utf-8'))
                return

            identificacion = registro_cliente['identificacion']
            cuenta = registro_cliente['numero_cuenta']

            # Trama para obtener el saldo
            respuesta_banco = self.enviar_trama_saldo(identificacion, cuenta)

            # Procesamiento de la respuesta del banco
            if respuesta_banco:
                if respuesta_banco.startswith("OK|"):
                    saldo_cliente = respuesta_banco.split('|', 1)[1]

                    # Respuesta de éxito en XML
                    respuesta_exito = f"<respuesta><codigo>0</codigo><saldo>{saldo_cliente}</saldo></respuesta>"
                    print(f"Respuesta enviada: {respuesta_exito}")
                    client_socket.send(respuesta_exito.encode('utf-8'))
                elif respuesta_banco.startswith("ERROR|"):
                    # Extraer el mensaje de error
                    mensaje_error = respuesta_banco.split('|', 1)[1]
                    respuesta_error_banco = self.validador.generarError(mensaje_error)
                    print(f"Error recibido del banco: {mensaje_error}")
                    client_socket.send(respuesta_error_banco.encode('utf-8'))
                else:
                    # Respuesta inesperada
                    respuesta_error_banco = self.validador.generarError("Respuesta inesperada del banco.")
                    print("Respuesta inesperada del banco.")
                    client_socket.send(respuesta_error_banco.encode('utf-8'))
            else:
                respuesta_error_banco = self.validador.generarError("No se recibió respuesta del banco.")
                client_socket.send(respuesta_error_banco.encode('utf-8'))

        except ET.ParseError:
            respuesta_error = self.validador.generarError("Error al parsear la trama XML")
            print(respuesta_error)
            client_socket.send(respuesta_error.encode('utf-8'))

if __name__ == "__main__":
    orquestador = OrquestadorSocket()
    orquestador.start()