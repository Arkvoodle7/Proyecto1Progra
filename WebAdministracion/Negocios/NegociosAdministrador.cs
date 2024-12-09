using System;
using System.Collections.Generic;
using Datos;
using Modelos;

namespace Negocios
{
    public class NegociosAdministrador
    {
        private readonly DatosAdministrador _datosAdministrador;

        public NegociosAdministrador()
        {
            _datosAdministrador = new DatosAdministrador(); 
        }

        // Método para obtener todos los administradores
        public List<ModeloAdmins> ObtenerTodosLosAdministradores()
        {
            return _datosAdministrador.ObtenerAdministradores();
        }

        // Método para crear un nuevo administrador
        public void CrearAdministrador(ModeloAdmins administrador, string contrasena)
        {
            if (string.IsNullOrEmpty(administrador.NombreUsuario) ||
                string.IsNullOrEmpty(administrador.NombreCompleto) ||
                string.IsNullOrEmpty(contrasena))
            {
                throw new Exception("Todos los campos son requeridos.");
            }
            _datosAdministrador.InsertarAdministrador(administrador, contrasena);
        }

        // Método para obtener un administrador por nombre de usuario
        public ModeloAdmins ObtenerAdministradorPorNombreUsuario(string nombreUsuario)
        {
            return _datosAdministrador.ObtenerAdministradorPorNombreUsuario(nombreUsuario);
        }

        public void EliminarAdministrador(string nombreUsuario)
        {
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                throw new Exception("El nombre de usuario no puede estar vacío.");
            }

            try
            {
                _datosAdministrador.EliminarAdministrador(nombreUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar eliminar el administrador: " + ex.Message);
            }
        }

        public void ActualizarAdministrador(ModeloAdmins administrador, string contrasena)
        {
            if (string.IsNullOrEmpty(administrador.NombreUsuario) ||
                string.IsNullOrEmpty(administrador.NombreCompleto) ||
                string.IsNullOrEmpty(contrasena))
            {
                throw new Exception("Todos los campos son requeridos.");
            }

            _datosAdministrador.ActualizarAdministrador(administrador, contrasena);
        }

    }
}
