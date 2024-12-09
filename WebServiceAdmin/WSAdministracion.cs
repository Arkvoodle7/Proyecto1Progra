using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceAdmin.WSadmin;

namespace WebServiceAdmin
{
    public class WSAdministracion
    {

        private readonly WSA2SoapClient _client;

        public WSAdministracion()
        {
            _client = new WSA2SoapClient();
        }

        // Método para listar todos los usuarios
        public List<string> ListarUsuarios()
        {
            var usuarios = _client.listar_usuarios(); 
            return usuarios.Split('\n').ToList(); 
        }

        // Método para crear un usuario
        public string CrearUsuario(string nombre_usuario, string nombre_completo, string contra)
        {
            return _client.crear_usuario(nombre_usuario, nombre_completo, contra);
        }

        // Método para editar un usuario
        public string EditarUsuario(string nombre_usuario, string nombre_completo, string contra)
        {
            return _client.editar_usuario(nombre_usuario, nombre_completo, contra);
        }

        // Método para obtener un usuario por nombre de usuario
        public string ObtenerUsuario(string nombre_usuario)
        {
            return _client.listar_usuario(nombre_usuario);
        }

        // Método para eliminar un usuario
        public string EliminarUsuario(string nombre_usuario)
        {
            return _client.borrar_usuario(nombre_usuario);
        }

        public class AdminWS
        {
            public string NombreUsuario { get; set; }
            public string NombreCompleto { get; set; }
        }

    }
}