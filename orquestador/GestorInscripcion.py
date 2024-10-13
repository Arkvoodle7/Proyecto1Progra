
from pymongo import MongoClient

class GestorInscripcion:
    
    def __init__(self, mongouri):
        # Conexión a MongoDB
        self.cliente = MongoClient(mongouri)
        self.db = self.cliente['PagosMovilesOrquestador']
        self.cuentas = self.db['TelefonosXCuentas']

    def registrar_asociacion(self, cuenta, identificacion, telefono):
        # Inserta un nuevo registro en la base de datos
        nueva_cuenta = {
            "identificacion": identificacion,
            "numero_cuenta": cuenta,
            "telefono": telefono,
            "estado": "activo"
        }
        self.cuentas.insert_one(nueva_cuenta)
        
        # Retorna la respuesta de inscripción realizada
        return "<respuesta><codigo>0</codigo><descripcion>Inscripción realizada</descripcion></respuesta>"


    def verificar_telefono_asociado(self, cuenta, telefono):
        # Primero, verifica si el teléfono ya está asociado a alguna cuenta activa
        cuenta_existente_telefono = self.cuentas.find_one({"telefono": telefono})
        if cuenta_existente_telefono:
            estado = cuenta_existente_telefono.get("estado", "")
            if estado == "activo":
                return False, "<respuesta><codigo>-1</codigo><descripcion>Teléfono ya se encuentra afiliado, realice el proceso de desinscripción</descripcion></respuesta>"
            elif estado in ["deshabilitado", "desinscrito"]:
                # Si está deshabilitado o desinscrito, se reactiva el teléfono
                self.cuentas.update_one(
                    {"telefono": telefono},
                    {"$set": {"estado": "activo", "numero_cuenta": cuenta}}
                )
                # Aseguramos que se envíe la respuesta cuando el teléfono es reactivado
                return True, "<respuesta><codigo>0</codigo><descripcion>Inscripción realizada</descripcion></respuesta>"

        # Después de verificar el teléfono, verifica si la cuenta tiene un número de teléfono activo asociado
        cuenta_existente = self.cuentas.find_one({"numero_cuenta": cuenta, "estado": "activo"})
        if cuenta_existente:
            return False, "<respuesta><codigo>-1</codigo><descripcion>Esta cuenta ya tiene un número de teléfono asociado</descripcion></respuesta>"

        # Si pasa todas las validaciones y no hay cuentas conflictivas
        return True, None



    def desinscribir_telefono(self, cuenta, identificacion, telefono):
        # Verifica si el teléfono está afiliado a una cuenta
        cuenta_existente = self.cuentas.find_one({"telefono": telefono, "numero_cuenta": cuenta, "identificacion": identificacion})

        if cuenta_existente:
            # Actualiza el estado a "desinscrito"
            self.cuentas.update_one(
                {"telefono": telefono},
                {"$set": {"estado": "desinscrito"}}
            )
            return "<respuesta><codigo>0</codigo><descripcion>Desinscripción realizada</descripcion></respuesta>"
        else:
            return "<respuesta><codigo>-1</codigo><descripcion>Teléfono no se encuentra afiliado</descripcion></respuesta>"

    def cerrar_conexion(self):
        self.cliente.close()
