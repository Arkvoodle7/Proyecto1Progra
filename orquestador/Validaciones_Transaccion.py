import xml.etree.ElementTree as xml
from pymongo import MongoClient

class ValidadorTransaccion:

    def __init__(self):
        self.client = MongoClient('localhost', 27017)
        self.db = self.client['PagosMovilesOrquestador']
        self.collection = self.db['TelefonosXCuentas']
       
    #crea el xml con la respuesta de error si las validaciones son incorrectas
    def generar_respuesta_error(self, mensaje_error):
        respuesta = xml.Element('respuesta')
        codigo = xml.SubElement(respuesta, 'codigo')
        codigo.text = '-1'
        descripcion = xml.SubElement(respuesta, 'descripcion')
        descripcion.text = mensaje_error
        
        #convierte el XML a cadena de texto
        return xml.tostring(respuesta, encoding='unicode')
    
    #crea el xml con la respuesta de exito si las validaciones pasan
    def generar_respuesta_exito(self):
        respuesta = xml.Element('respuesta')
        codigo = xml.SubElement(respuesta, 'codigo')
        codigo.text = '0'
        descripcion = xml.SubElement(respuesta, 'descripcion')
        descripcion.text = "Transacción realizada"
        
        #convierte el XML a cadena de texto
        return xml.tostring(respuesta, encoding='unicode')
    
    #validaciones
    def validar_transaccion(self, telefono, monto, descripcion):
        
        #valida si el cliente esta registrado en
        registro = self.collection.find_one({"telefono": telefono})

        if not registro:
            return self.generar_respuesta_error("Cliente no asociado a pagos móviles.")

        if len(telefono) != 8 or not telefono.isdigit():
            return self.generar_respuesta_error("Debe enviar un número de teléfono válido.")

        if not telefono and not monto and not descripcion:
            return self.generar_respuesta_error("Debe enviar los datos completos y válidos")
         
        if float(monto) > 100000:
            return self.generar_respuesta_error("El monto no debe ser superior a 100.000.")
        
        if len(descripcion) > 25:
            return self.generar_respuesta_error("La descripción no puede superar 25 caracteres")

        return None 
    def obtener_registro_cliente(self, telefono):
        # retorna el registro del cliente si existe, de lo contrario None
        return self.collection.find_one({"telefono": telefono})

