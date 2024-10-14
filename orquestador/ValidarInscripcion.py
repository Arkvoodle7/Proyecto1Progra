class ValidadorInscripcion:
    
    @staticmethod
    def validar_datos(cuenta, identificacion, telefono):
        #verifica que todos los campos estan presentes
        if not cuenta or not identificacion or not telefono:
            return False, "<respuesta><codigo>-1</codigo><descripcion>Datos incorrectos</descripcion></respuesta>"

        #verificacion de la identificacion (9 digitos)
        if len(identificacion) != 9 or not identificacion.isdigit():
            return False, "<respuesta><codigo>-1</codigo><descripcion>Datos incorrectos</descripcion></respuesta>"

        #verificacion del telefono (8 digitos)
        if len(telefono) != 8 or not telefono.isdigit():
            return False, "<respuesta><codigo>-1</codigo><descripcion>Datos incorrectos</descripcion></respuesta>"

        #si pasa todas las validaciones, retorna True
        return True, None

    @staticmethod
    def validar_datos_desinscripcion(cuenta, identificacion, telefono):
        #reutilizamos la misma logica de validación para desinscripcion
        return ValidadorInscripcion.validar_datos(cuenta, identificacion, telefono)