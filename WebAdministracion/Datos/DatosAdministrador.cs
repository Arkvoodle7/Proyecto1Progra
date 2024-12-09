using System;
using System.Collections.Generic;
using Datos.Modelos;
using WebServiceAdmin;
using MongoDB.Bson;

namespace Datos
{
    public class DatosAdministrador
    {
        private readonly WSAdministracion _webServiceAdmin;

        public DatosAdministrador()
        {
            _webServiceAdmin = new WSAdministracion();
        }

        // Método para obtener todos los administradores
        public List<ModeloAdmin> ObtenerAdministradores()
        {
            var administradoresWS = _webServiceAdmin.ListarUsuarios();

            var administradores = new List<ModeloAdmin>();

            foreach (var administradorWS in administradoresWS)
            {
                    administradores.Add(new ModeloAdmin
                    {
                        NombreUsuario = administradorWS,
                        NombreCompleto = administradorWS
                    });
            }

            return administradores;
        }
        // Método para insertar un nuevo administrador
        public void InsertarAdministrador(ModeloAdmin administrador, string contrasena)
        {
            _webServiceAdmin.CrearUsuario(administrador.NombreUsuario, administrador.NombreCompleto, contrasena);
        }

        // Método para obtener un administrador por nombre de usuario
        public ModeloAdmin ObtenerAdministradorPorNombreUsuario(string nombreUsuario)
        {
            var administradorWS = _webServiceAdmin.ObtenerUsuario(nombreUsuario);  

            if (administradorWS == "Usuario no encontrado")
            {
                return null;
            }
            var usuario = BsonDocument.Parse(administradorWS);

            return new ModeloAdmin
            {
                NombreUsuario = usuario["nombre_usuario"].ToString(),
                NombreCompleto = usuario["nombre_completo"].ToString()
            };
        }

        // Método para actualizar la información de un administrador
        public void EliminarAdministrador(string nombreUsuario)
        {
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                throw new Exception("El nombre de usuario no puede estar vacío.");
            }

            try
            {
                _webServiceAdmin.EliminarUsuario(nombreUsuario); 
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el administrador: " + ex.Message);
            }
        }

        public void ActualizarAdministrador(ModeloAdmin administrador, string contrasena)
        {
            if (string.IsNullOrEmpty(administrador.NombreUsuario) ||
                string.IsNullOrEmpty(administrador.NombreCompleto) ||
                string.IsNullOrEmpty(contrasena))
            {
                throw new Exception("Todos los campos son requeridos.");
            }

            try
            {
                _webServiceAdmin.EditarUsuario(administrador.NombreUsuario, administrador.NombreCompleto, contrasena);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el administrador: " + ex.Message);
            }
        }

    }
}
