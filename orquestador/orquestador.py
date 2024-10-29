import threading
import socket
import configparser
import xml.etree.ElementTree as ET
import json
import re
from pymongo import MongoClient

#constructor
from Validaciones_Transaccion import ValidadorTransaccion
from GestorInscripcion import GestorInscripcion
from ValidarInscripcion import ValidadorInscripcion

class OrquestadorSocket:

    def __init__(self):
        config = configparser.ConfigParser()
        config.read('Config.ini')
        
        self.puerto_interno = int(config['Orquestador']['puerto_interno'])
        self.puerto_externo = int(config['Orquestador']['puerto_externo'])
        self.puerto_bancario = int(config['Banco']['puerto'])  #puerto del socket bancario
        self.puerto_receptor_externo = int(config['ReceptorExterno']['puerto'])  #puerto del socket receptor externo
        
        self.validador = ValidadorTransaccion()
        self.validadori = ValidadorInscripcion

        #inicializar la conexion a MongoDB
        self.mongo_uri = config['MongoDB']['uri']
        self.mongo_client = MongoClient(self.mongo_uri)
        self.mongo_db = self.mongo_client.PagosMovilesOrquestador

        self.gestor = GestorInscripcion(self.mongo_uri)

    def start(self):
        Orquestador_interno = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        Orquestador_externo = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        Orquestador_interno.bind(('localhost', self.puerto_interno))
        Orquestador_externo.bind(('localhost', self.puerto_externo))
        Orquestador_interno.listen(5)
        Orquestador_externo.listen(5)
        print(f"Orquestador escuchando en {self.puerto_interno} y {self.puerto_externo}")

        threading.Thread(target=self.accept_connections, args=(Orquestador_interno, True)).start()
        threading.Thread(target=self.accept_connections, args=(Orquestador_externo, False)).start()

    def accept_connections(self, Orquestador_socket, es_interno):
        while True:
            client_socket, client_address = Orquestador_socket.accept()
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
                trama_recibida = data.decode('utf-8')

                #eliminar espacios en blanco innecesarios de la cadena XML
                trama_recibida_minificada = self.minify_xml(trama_recibida)

                #determinar el origen y imprimir la trama recibida sin espacios adicionales
                if es_interno:
                    print(f"Trama recibida del simulador: {trama_recibida_minificada}")
                else:
                    print(f"Trama recibida del Receptor Externo: {trama_recibida_minificada}")

                root = ET.fromstring(trama_recibida)

                if root.tag == "transaccion":
                    
                    #extraer los valores del XML
                    telefono = root.find('telefono').text
                    monto = root.find('monto').text
                    descripcion = root.find('descripcion').text

                    #llamar a manejar_transaccion con los datos extraídos
                    respuesta = self.manejar_transaccion(telefono, monto, descripcion, es_interno)

                    #enviar la respuesta al cliente y imprimirla
                    client_socket.send(respuesta.encode('utf-8'))
                    if es_interno:
                        print(f"Respuesta enviada al simulador: {respuesta}")
                    else:
                        print(f"Respuesta enviada al Receptor Externo: {respuesta}")

                elif root.tag == "saldo":
                    self.recibe_consulta_saldo(root, client_socket)
                elif root.tag in ["inscripcion", "desinscripcion"]:
                    self.manejar_cliente(client_socket, root)
                    break

            except ConnectionResetError as e:
                print(e)
                break
            except OSError as e:
                print(e)
                break
            except Exception as e:
                print(e)
                break

        client_socket.close()

    def minify_xml(self, xml_string):
        #eliminar espacios en blanco entre etiquetas
        xml_string = re.sub(r'>\s+<', '><', xml_string)
        #eliminar espacios al inicio y al final
        xml_string = xml_string.strip()
        return xml_string

    def manejar_transaccion(self, telefono, monto, descripcion, es_interno):
        #validacion de la transaccion
        respuesta_error = self.validador.validarTransaccion(telefono, monto, descripcion, es_interno)
        if respuesta_error:
            return respuesta_error

        #obtener el cliente asociado
        registro_cliente = self.validador.obtenerCliente(telefono)

        if es_interno:
            #si la trama proviene del simulador interno
            identificacion = registro_cliente.get("identificacion", "000000000") if registro_cliente else "000000000"
            cuenta = registro_cliente.get("numero_cuenta", "000000000") if registro_cliente else "000000000"

            #procesar la trama
            respuesta = self.procesar_trama(telefono, identificacion, cuenta, monto, descripcion, es_interno)
            return respuesta
        else:
            #si la trama proviene del Receptor Externo
            if registro_cliente is None:
                #cliente no asociado
                print("Cliente no asociado a pagos móviles.")
                respuesta_error = self.validador.generarError("Cliente no asociado a pagos móviles.")
                return respuesta_error
            else:
                identificacion = registro_cliente.get("identificacion", "000000000")
                cuenta = registro_cliente.get("numero_cuenta", "000000000")

                #procesar la trama
                respuesta = self.procesar_trama(telefono, identificacion, cuenta, monto, descripcion, es_interno)
                return respuesta

    def procesar_trama(self, telefono, identificacion, cuenta, monto, descripcion, es_interno):
        cuenta_existente = self.mongo_db.TelefonosXCuentas.find_one({"telefono": telefono})

        if cuenta_existente:
            #transaccion interna
            respuesta_bancaria = self.enviar_trama_bancaria(identificacion, cuenta, monto, "CRE", es_interno)
            return respuesta_bancaria
        else:
            #transaccion externa
            respuesta_externa = self.enviar_trama_receptor_externo(telefono, monto, descripcion, es_interno)
            return respuesta_externa

    def enviar_trama_bancaria(self, identificacion, cuenta, monto, tipo, es_interno):
        try:
            #intentar conectar al socket bancario
            bancario_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            bancario_socket.connect(('localhost', self.puerto_bancario))

            #enviar la trama al socket bancario
            trama_banco = f"{identificacion}|{cuenta}|{monto}|{tipo}"
            print(f"Trama enviada a Bancario: {trama_banco}")
            bancario_socket.send((trama_banco + "\n").encode('utf-8'))

            #recibir la respuesta del socket bancario
            respuesta_banco = bancario_socket.recv(1024).decode('utf-8').strip()
            print(f"Respuesta recibida de Bancario: {respuesta_banco}")
            bancario_socket.close()

            #procesar la respuesta del banco
            if respuesta_banco.startswith('OK|'):
                #respuesta exitosa
                codigo = '0'
                descripcion = respuesta_banco[3:]  #remover 'OK|' del inicio
            elif respuesta_banco.startswith('ERROR|'):
                #respuesta de error
                codigo = '-1'
                descripcion = respuesta_banco[6:]  #remover 'ERROR|' del inicio
            else:
                #respuesta inesperada
                codigo = '-1'
                descripcion = 'Respuesta inesperada del banco.'

            #crear la respuesta XML utilizando ElementTree
            respuesta_xml = ET.Element('respuesta')
            ET.SubElement(respuesta_xml, 'codigo').text = codigo
            ET.SubElement(respuesta_xml, 'descripcion').text = descripcion.strip()

            #convertir el arbol XML a una cadena
            respuesta_xml_str = ET.tostring(respuesta_xml, encoding='utf-8').decode('utf-8')

            #si la trama proviene del Receptor Externo, convertir a JSON
            if not es_interno:
                respuesta_json = {
                    "codigo": int(codigo),
                    "descripcion": descripcion.strip()
                }
                return json.dumps(respuesta_json)

            #si la trama proviene del SimuladorInterno, devolver respuesta en XML
            return respuesta_xml_str

        except socket.error as e:
            print(e)
            error_response_xml = ET.Element('respuesta')
            ET.SubElement(error_response_xml, 'codigo').text = '-1'
            ET.SubElement(error_response_xml, 'descripcion').text = f"Error de conexión con el banco: {str(e)}"
            error_response_str = ET.tostring(error_response_xml, encoding='utf-8').decode('utf-8')

            if not es_interno:
                return json.dumps({
                    "codigo": -1,
                    "descripcion": f"Error de conexión con el banco: {str(e)}"
                })
            print(error_response_str)
            return error_response_str

    def enviar_trama_receptor_externo(self, telefono, monto, descripcion, es_interno):
        try:
            #conexion al socket del receptor externo
            receptor_externo_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            
            #establecer un timeout de 20 segundos
            receptor_externo_socket.settimeout(20)  #timeout de 20 segundos
            
            receptor_externo_socket.connect(('localhost', self.puerto_receptor_externo))

            #crear la trama en formato JSON para el receptor externo
            trama_receptor = f'{{"telefono":"{telefono}", "monto":{monto}, "descripcion":"{descripcion}"}}'
            print(f"Trama enviada al Receptor Externo: {trama_receptor}")
            receptor_externo_socket.send(trama_receptor.encode('utf-8'))

            #espera la respuesta del receptor externo con timeout
            respuesta_receptor = receptor_externo_socket.recv(1024).decode('utf-8')
            print(f"Respuesta recibida del Receptor Externo: {respuesta_receptor}")
            #cerrar el socket
            receptor_externo_socket.close()

            #si la trama proviene del Simulador Interno, devolver la respuesta
            if es_interno:
                return respuesta_receptor
            else:
                #si proviene del Receptor Externo, procesar la respuesta
                return respuesta_receptor

        except socket.timeout as e:
            print(e)
            if es_interno:
                return self.validador.generarError("Timeout al comunicarse con el Receptor Externo.")
            else:
                return self.validador.generarError("Timeout al comunicarse con Bancario.")
        except socket.error as e:
            print(e)
            if es_interno:
                return self.validador.generarError(f"Error al comunicarse con el Receptor Externo: {str(e)}")
            else:
                return self.validador.generarError(f"Error al comunicarse con Bancario: {str(e)}")

    def manejar_cliente(self, cliente_socket, root):
        """
        Manejar la lógica de inscripción y desinscripción.
        """
        try:

            cuenta = root.find('cuenta').text
            identificacion = root.find('identificacion').text
            telefono = root.find('telefono').text

            #validar datos de inscripcion
            if root.tag == "inscripcion":
                es_valido, mensaje = self.validadori.validar_datos(cuenta, identificacion, telefono)
                if not es_valido:
                    print(f"Respuesta enviada al simulador: {mensaje}")
                    cliente_socket.sendall(mensaje.encode('utf-8'))
                    return  #retornar aqui si hay un error

                es_valido, respuesta = self.gestor.verificar_telefono_asociado(cuenta, telefono)
                if respuesta:
                    print(f"Respuesta enviada al simulador: {respuesta}")
                    cliente_socket.sendall(respuesta.encode('utf-8'))
                    return  #detener si hay error o reactivacion

                if es_valido and respuesta is None:
                    respuesta_inscripcion = self.gestor.registrar_asociacion(cuenta, identificacion, telefono)
                    print(f"Respuesta enviada al simulador: {respuesta_inscripcion}")
                    cliente_socket.sendall(respuesta_inscripcion.encode('utf-8'))                    

            elif root.tag == "desinscripcion":
                es_valido, mensaje = self.validadori.validar_datos_desinscripcion(cuenta, identificacion, telefono)
                if not es_valido:
                    print(f"Respuesta enviada al simulador: {mensaje}")
                    cliente_socket.sendall(mensaje.encode('utf-8'))
                    return

                respuesta = self.gestor.desinscribir_telefono(cuenta, identificacion, telefono)
                cliente_socket.sendall(respuesta.encode('utf-8'))
                print(f"Respuesta enviada al simulador: {respuesta}")

        except Exception as e:
            print(e)
        finally:
            cliente_socket.close()

    #trama de envio del saldo
    def enviar_trama_saldo(self, identificacion, cuenta):
        try:
            bancario_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            bancario_socket.connect(('localhost', self.puerto_bancario))

            #crea la trama sobre el saldo para bancario
            trama_saldo = f"{identificacion}|{cuenta}\n"  # Agregar '\n' al final
            print(f"Trama enviada a Bancario: {trama_saldo.strip()}")
            bancario_socket.send(trama_saldo.encode('utf-8'))

            #recibe la respuesta de bancario
            respuesta_banco = bancario_socket.recv(1024).decode('utf-8')
            print(f"Respuesta recibida de Bancario: {respuesta_banco.strip()}")
            bancario_socket.close()

            return respuesta_banco.strip()
        except socket.error as e:
            print(e)
            return None
        
    def recibe_consulta_saldo(self, root, client_socket):
        try:
            telefono = root.find('telefono').text

            respuesta_error = self.validador.validarTransaccion(telefono, "0", "Consulta de saldo")
            if respuesta_error:
                print(respuesta_error)
                client_socket.send(respuesta_error.encode('utf-8'))
                return

            #obtener los datos del cliente
            registro_cliente = self.validador.obtenerCliente(telefono)
            if not registro_cliente:
                respuesta_error = self.validador.generarError("Cliente no asociado a pagos móviles.")
                client_socket.send(respuesta_error.encode('utf-8'))
                return

            identificacion = registro_cliente['identificacion']
            cuenta = registro_cliente['numero_cuenta']

            #trama para obtener el saldo
            respuesta_banco = self.enviar_trama_saldo(identificacion, cuenta)

            #procesamiento de la respuesta del banco
            if respuesta_banco:
                if respuesta_banco.startswith("OK|"):
                    saldo_cliente = respuesta_banco.split('|', 1)[1]

                    #respuesta de exito en XML
                    respuesta_exito = f"<respuesta><codigo>0</codigo><saldo>{saldo_cliente}</saldo></respuesta>"
                    print(f"Respuesta enviada al Simulador: {respuesta_exito}")
                    client_socket.send(respuesta_exito.encode('utf-8'))
                elif respuesta_banco.startswith("ERROR|"):
                    #extraer el mensaje de error
                    mensaje_error = respuesta_banco.split('|', 1)[1]
                    respuesta_error_banco = self.validador.generarError(mensaje_error)
                    print(mensaje_error)
                    client_socket.send(respuesta_error_banco.encode('utf-8'))
                else:
                    #respuesta inesperada
                    respuesta_error_banco = self.validador.generarError("Respuesta inesperada del banco.")
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
