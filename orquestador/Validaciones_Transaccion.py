import xml.etree.ElementTree as ET
from pymongo import MongoClient

class ValidadorTransaccion:

    def __init__(self):
        self.client = MongoClient('localhost', 27017)
        self.db = self.client['PagosMovilesOrquestador']
        self.collection = self.db['TelefonosXCuentas']

    #metodo para generar un mensaje de error en formato XML
    def generarError(self, mensaje_error):
        respuesta = ET.Element('respuesta')
        codigo = ET.SubElement(respuesta, 'codigo')
        codigo.text = '-1'
        descripcion = ET.SubElement(respuesta, 'descripcion')
        descripcion.text = mensaje_error
        return ET.tostring(respuesta, encoding='unicode')

    #metodo para validar la transaccion
    def validarTransaccion(self, telefono, monto, descripcion, es_interno=True):
        #validar que el telefono tenga 8 dígitos y sea numerico
        if not telefono or len(telefono) != 8 or not telefono.isdigit():
            return self.generarError("Debe enviar los datos completos y válidos")

        #validar que monto y descripcion no sean nulos o vacios, excepto en la consulta de saldo
        if descripcion != "Consulta de saldo":
            if not monto or not descripcion:
                return self.generarError("Debe enviar los datos completos y válidos")

            #validar que la descripcion no supere 25 caracteres
            if len(descripcion) > 25:
                return self.generarError("La descripción no puede superar 25 caracteres")

            #validar que el monto sea un numero valido y no supere 100,000
            try:
                monto_float = float(monto)
                if monto_float > 100000:
                    return self.generarError("El monto no debe ser superior a 100.000")
                if monto_float <= 0:
                    return self.generarError("Debe enviar los datos completos y válidos")
            except ValueError:
                return self.generarError("Debe enviar los datos completos y válidos")

        #validar si el cliente esta registrado en la base de datos
        registro = self.collection.find_one({"telefono": telefono})

        if not registro and not es_interno:
            #si es una transaccion externa y el cliente no esta registrado, mostrar error
            return self.generarError("Cliente no asociado a pagos móviles.")

        return None

    #metodo para obtener el cliente
    def obtenerCliente(self, telefono):
        return self.collection.find_one({"telefono": telefono})
