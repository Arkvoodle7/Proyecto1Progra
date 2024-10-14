
from pymongo import MongoClient

class GestorInscripcion:
    
    def __init__(self, mongouri):
        #conexion a MongoDB
        self.cliente = MongoClient(mongouri)
        self.db = self.cliente['PagosMovilesOrquestador']
        self.cuentas = self.db['TelefonosXCuentas']

    def registrar_asociacion(self, cuenta, identificacion, telefono):
        #inserta un nuevo registro en la base de datos
        nueva_cuenta = {
            "identificacion": identificacion,
            "numero_cuenta": cuenta,
            "telefono": telefono,
            "estado": "activo"
        }
        self.cuentas.insert_one(nueva_cuenta)
        
        #retorna la respuesta de inscripcion realizada
        return "<respuesta><codigo>0</codigo><descripcion>Inscripción realizada</descripcion></respuesta>"


    def verificar_telefono_asociado(self, cuenta, telefono):
        #primero, verifica si el telefono ya esta asociado a alguna cuenta activa
        cuenta_existente_telefono = self.cuentas.find_one({"telefono": telefono})
        if cuenta_existente_telefono:
            estado = cuenta_existente_telefono.get("estado", "")
            if estado == "activo":
                return False, "<respuesta><codigo>-1</codigo><descripcion>Teléfono ya se encuentra afiliado, realice el proceso de desinscripción</descripcion></respuesta>"
            elif estado in ["deshabilitado", "desinscrito"]:
                #si esta deshabilitado o desinscrito, se reactiva el telefono
                self.cuentas.update_one(
                    {"telefono": telefono},
                    {"$set": {"estado": "activo", "numero_cuenta": cuenta}}
                )
                #aseguramos que se envie la respuesta cuando el telefono es reactivado
                return True, "<respuesta><codigo>0</codigo><descripcion>Inscripción realizada</descripcion></respuesta>"

        #despues de verificar el telefono, verifica si la cuenta tiene un numero de telefono activo asociado
        cuenta_existente = self.cuentas.find_one({"numero_cuenta": cuenta, "estado": "activo"})
        if cuenta_existente:
            return False, "<respuesta><codigo>-1</codigo><descripcion>Esta cuenta ya tiene un número de teléfono asociado</descripcion></respuesta>"

        #si pasa todas las validaciones y no hay cuentas conflictivas
        return True, None



    def desinscribir_telefono(self, cuenta, identificacion, telefono):
        #verifica si el telefono esta afiliado a una cuenta
        cuenta_existente = self.cuentas.find_one({"telefono": telefono, "numero_cuenta": cuenta, "identificacion": identificacion})

        if cuenta_existente:
            #actualiza el estado a "desinscrito"
            self.cuentas.update_one(
                {"telefono": telefono},
                {"$set": {"estado": "desinscrito"}}
            )
            return "<respuesta><codigo>0</codigo><descripcion>Desinscripción realizada</descripcion></respuesta>"
        else:
            return "<respuesta><codigo>-1</codigo><descripcion>Teléfono no se encuentra afiliado</descripcion></respuesta>"

    def cerrar_conexion(self):
        self.cliente.close()
