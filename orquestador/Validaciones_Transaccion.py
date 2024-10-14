import xml.etree.ElementTree as ET
from pymongo import MongoClient

class ValidadorTransaccion:

    def __init__(self):
        self.client = MongoClient('localhost', 27017)
        self.db = self.client['PagosMovilesOrquestador']
        self.collection = self.db['TelefonosXCuentas']

    # Método para generar un mensaje de error en formato XML
    def generarError(self, mensaje_error):
        respuesta = ET.Element('respuesta')
        codigo = ET.SubElement(respuesta, 'codigo')
        codigo.text = '-1'
        descripcion = ET.SubElement(respuesta, 'descripcion')
        descripcion.text = mensaje_error
        return ET.tostring(respuesta, encoding='unicode')

    # Método para validar la transacción
    def validarTransaccion(self, telefono, monto, descripcion, es_interno=True):
        # Validar que el teléfono tenga 8 dígitos y sea numérico
        if not telefono or len(telefono) != 8 or not telefono.isdigit():
            return self.generarError("Debe enviar los datos completos y válidos")

        # Validar que monto y descripción no sean nulos o vacíos, excepto en la consulta de saldo
        if descripcion != "Consulta de saldo":
            if not monto or not descripcion:
                return self.generarError("Debe enviar los datos completos y válidos")

            # Validar que la descripción no supere 25 caracteres
            if len(descripcion) > 25:
                return self.generarError("La descripción no puede superar 25 caracteres")

            # Validar que el monto sea un número válido y no supere 100,000
            try:
                monto_float = float(monto)
                if monto_float > 100000:
                    return self.generarError("El monto no debe ser superior a 100.000.")
                if monto_float <= 0:
                    return self.generarError("El monto debe ser mayor que cero.")
            except ValueError:
                return self.generarError("El monto debe ser un número válido")

        # Validar si el cliente está registrado en la base de datos
        registro = self.collection.find_one({"telefono": telefono})

        if not registro and not es_interno:
            # Si es una transacción externa y el cliente no está registrado, mostrar error
            return self.generarError("Cliente no asociado a pagos móviles.")

        return None

    # Método para obtener el cliente
    def obtenerCliente(self, telefono):
        return self.collection.find_one({"telefono": telefono})
