import xml.etree.ElementTree as xml
from pymongo import MongoClient

class ValidadorTransaccion:

    def __init__(self):
        self.client = MongoClient('localhost', 27017)
        self.db = self.client['PagosMovilesOrquestador']
        self.collection = self.db['TelefonosXCuentas']
       
    #crea el xml con la respuesta de error si las validaciones son incorrectas
    def generarError(self, mensaje_error):
        respuesta = xml.Element('respuesta')
        codigo = xml.SubElement(respuesta, 'codigo')
        codigo.text = '-1'
        descripcion = xml.SubElement(respuesta, 'descripcion')
        descripcion.text = mensaje_error
        
        #convierte el XML a cadena de texto
        return xml.tostring(respuesta, encoding='unicode')
    
    #crea el xml con la respuesta de exito si las validaciones pasan
    def generarExito(self):
        respuesta = xml.Element('respuesta')
        codigo = xml.SubElement(respuesta, 'codigo')
        codigo.text = '0'
        descripcion = xml.SubElement(respuesta, 'descripcion')
        descripcion.text = "Transacción realizada"
        
        #convierte el XML a cadena de texto
        return xml.tostring(respuesta, encoding='unicode')
    


    #validaciones
    def validarTransaccion(self, telefono, monto, descripcion):
        
        if len(telefono) != 8 or not telefono.isdigit():
            return self.generarError("Debe enviar un número de teléfono válido.")
        
        if not telefono and not monto and not descripcion:
            return self.generarError("Debe enviar los datos completos y válidos")
        
        if len(descripcion) > 25:
            return self.generarError("La descripción no puede superar 25 caracteres")
        
        if float(monto) > 100000:
            return self.generarError("El monto no debe ser superior a 100.000.")

        #valida si el cliente esta registrado en
        registro = self.collection.find_one({"telefono": telefono})

        if not registro:
            return self.generarError("Cliente no asociado a pagos móviles.")

        return None 
    def obtenerCliente(self, telefono):
        # retorna el registro del cliente si existe, de lo contrario None
        return self.collection.find_one({"telefono": telefono})

