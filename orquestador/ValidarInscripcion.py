from pymongo import MongoClient

class ValidadorInscripcion:

    def __init__(self, mongo_uri):
        # Conexión a MongoDB
        self.cliente = MongoClient(mongo_uri)
        self.db = self.cliente['Pagos_Movile']  
        self.cuentas = self.db['TelefonosXCuentas'] 
    
    def validar_datos(self, cuenta, identificacion, telefono):
        # Verifica que todos los campos están presentes
        if not cuenta or not identificacion or not telefono:
            return False, "<respuesta><codigo>-1</codigo><descripcion>Datos incorrectos</descripcion></respuesta>"
        
        # Verificación de la identificación (9 dígitos)
        if len(identificacion) != 9 or not identificacion.isdigit():
            return False, "<respuesta><codigo>-1</codigo><descripcion>Datos incorrectos</descripcion></respuesta>"

        # Verificación del teléfono (8 dígitos)
        if len(telefono) != 8 or not telefono.isdigit():
            return False, "<respuesta><codigo>-1</codigo><descripcion>Datos incorrectos</descripcion></respuesta>"
        
        # Verificación de que la cuenta no tenga ya un número de teléfono asociado
        cuenta_existente = self.cuentas.find_one({"numero_cuenta": cuenta, "estado": "activo"})
        if cuenta_existente:
            return False, "<respuesta><codigo>-1</codigo><descripcion>Esta cuenta ya tiene un número de teléfono asociado</descripcion></respuesta>"

        # Verifica si el teléfono ya está afiliado
        cuenta_existente = self.cuentas.find_one({"telefono": telefono})
        if cuenta_existente:
            estado = cuenta_existente.get("estado", "")
            if estado == "activo":
                # Si el teléfono ya está activo, retornar error
                return False, "<respuesta><codigo>-1</codigo><descripcion>Teléfono ya se encuentra afiliado, realice el proceso de desinscripción</descripcion></respuesta>"
            elif estado in ["deshabilitado", "desinscrito"]:
                # Si está deshabilitado o desinscrito, se reactiva la cuenta
                self.cuentas.update_one(
                    {"telefono": telefono},
                    {"$set": {"estado": "activo"}}
                )
                return True, "<respuesta><codigo>0</codigo><descripcion>Inscripción realizada</descripcion></respuesta>"
        
        # Si pasa las validaciones y no hay cuentas conflictivas
        return True, None
    
    def validar_datos_desinscripcion(self, cuenta, identificacion, telefono):
        # Verifica que todos los campos estén presentes
        if not cuenta or not identificacion or not telefono:
            return False, "<respuesta><codigo>-1</codigo><descripcion>Datos incorrectos</descripcion></respuesta>"
        
        # Verificación de la identificación (9 dígitos)
        if len(identificacion) != 9 or not identificacion.isdigit():
            return False, "<respuesta><codigo>-1</codigo><descripcion>Datos incorrectos</descripcion></respuesta>"

        # Verificación del teléfono (8 dígitos)
        if len(telefono) != 8 or not telefono.isdigit():
            return False, "<respuesta><codigo>-1</codigo><descripcion>Datos incorrectos</descripcion></respuesta>"
        
        # Si pasa las validaciones
        return True, None

    def registrar_asociacion(self, cuenta, identificacion, telefono):
        # Inserta un nuevo registro en la base de datos
        nueva_cuenta = {
            "identificacion": identificacion,
            "numero_cuenta": cuenta,
            "telefono": telefono,
            "estado": "activo"
        }
        self.cuentas.insert_one(nueva_cuenta)

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
