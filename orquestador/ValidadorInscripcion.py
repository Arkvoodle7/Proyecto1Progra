class ValidadorInscripcion:
    
    @staticmethod
    def validar_datos(cuenta, identificacion, telefono):
        # Verifica que todos los campos están presentes
        if not cuenta or not identificacion or not telefono:
            return False, "<respuesta><codigo>-1</codigo><descripcion>Datos incorrectos</descripcion></respuesta>"

        # Verificación de la identificación (9 dígitos)
        if len(identificacion) != 9 or not identificacion.isdigit():
            return False, "<respuesta><codigo>-1</codigo><descripcion>Datos incorrectos</descripcion></respuesta>"

        # Verificación del teléfono (8 dígitos)
        if len(telefono) != 8 or not telefono.isdigit():
            return False, "<respuesta><codigo>-1</codigo><descripcion>Datos incorrectos</descripcion></respuesta>"

        # Si pasa todas las validaciones, retorna True
        return True, None

    @staticmethod
    def validar_datos_desinscripcion(cuenta, identificacion, telefono):
        # Reutilizamos la misma lógica de validación para desinscripción
        return ValidadorInscripcion.validar_datos(cuenta, identificacion, telefono)
