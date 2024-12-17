<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    internal class DatosAdministrador
    {
=======
using System;
using System.Collections.Generic;
using Modelos;
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
        public List<ModeloAdmins> ObtenerAdministradores()
        {
            var administradoresWS = _webServiceAdmin.ListarUsuarios();

            var administradores = new List<ModeloAdmins>();

            foreach (var administradorWS in administradoresWS)
            {
                    administradores.Add(new ModeloAdmins
                    {
                        NombreUsuario = administradorWS,
                        NombreCompleto = administradorWS
                    });
            }

            return administradores;
        }
        // Método para insertar un nuevo administrador
        public void InsertarAdministrador(ModeloAdmins administrador, string contrasena)
        {
            _webServiceAdmin.CrearUsuario(administrador.NombreUsuario, administrador.NombreCompleto, contrasena);
        }

        // Método para obtener un administrador por nombre de usuario
        public ModeloAdmins ObtenerAdministradorPorNombreUsuario(string nombreUsuario)
        {
            var administradorWS = _webServiceAdmin.ObtenerUsuario(nombreUsuario);  

            if (administradorWS == "Usuario no encontrado")
            {
                return null;
            }
            var usuario = BsonDocument.Parse(administradorWS);

            return new ModeloAdmins
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
                // Llamamos al servicio web para eliminar el usuario
                var resultado = _webServiceAdmin.EliminarUsuario(nombreUsuario);

                // Verifica si la eliminación fue exitosa
                if (resultado != "Usuario eliminado con éxito")
                {
                    throw new Exception("Error al eliminar el administrador: " + resultado);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el administrador: " + ex.Message);
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

            try
            {
                _webServiceAdmin.EditarUsuario(administrador.NombreUsuario, administrador.NombreCompleto, contrasena);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el administrador: " + ex.Message);
            }
        }

>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
    }
}
