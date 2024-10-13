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
        
        self.puerto = int(config['Orquestador']['puerto'])
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
        Orquestador = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        Orquestador.bind(('localhost', self.puerto))
        Orquestador.listen(5)
        print(f"Orquestador en el puerto {self.puerto}...")

        while True:
            client_socket, client_address = Orquestador.accept()
            print(f"Conexión recibida de {client_address}")
            cliente_thread = threading.Thread(target=self.handle_client, args=(client_socket,))
            cliente_thread.start()

    def handle_client(self, client_socket):
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

                # Decidir la operación según el tag
                if root.tag == "transaccion":
                    print("Llamando a manejar_transaccion con los datos recibidos.")
                    
                    # Extraer los valores correctos del XML
                    telefono = root.find('telefono').text
                    monto = root.find('monto').text
                    descripcion = root.find('descripcion').text

                    # Llamar a manejar_transaccion con los datos extraídos
                    respuesta = self.manejar_transaccion(telefono, monto, descripcion)
                    
                    # Enviar la respuesta al cliente
                    client_socket.send(respuesta.encode('utf-8'))

                elif root.tag == "saldo":
                    self.recibe_consulta_saldo(client_socket)
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


    def manejar_transaccion(self, telefono, monto, descripcion):
        # Lógica para manejar la transacción
        print(f"Manejando transacción con teléfono: {telefono}, monto: {monto}, descripción: {descripcion}")

        # Validación de la transacción
        respuesta_error = self.validador.validarTransaccion(telefono, monto, descripcion, es_interno=False)
        if respuesta_error and self.es_transaccion_interna(telefono):
            print(respuesta_error)
            return respuesta_error

        # Obtener el cliente asociado
        registro_cliente = self.validador.obtenerCliente(telefono)
        identificacion = registro_cliente["identificacion"] if registro_cliente else "000000000"
        cuenta = registro_cliente["numero_cuenta"] if registro_cliente else "000000000"

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
            
            # Establecer un timeout de 30 segundos
            receptor_externo_socket.settimeout(30)  # Timeout de 30 segundos
            
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

if __name__ == "__main__":
    orquestador = OrquestadorSocket()
    orquestador.start()