using Modelos;
using ServiciosWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosUsuario
    {
        private readonly WebServiceCliente _webServiceCliente;

        public DatosUsuario()
        {
            _webServiceCliente = new WebServiceCliente();
        }

        public List<ModeloUsuario> ObtenerUsuarios()
        {
            //llama al client del web service para obtener los usuarios
            var usuariosWS = _webServiceCliente.ListarUsuarios();

            var usuarios = new List<ModeloUsuario>();
            foreach (var usuarioWS in usuariosWS)
            {
                usuarios.Add(new ModeloUsuario
                {
                    Identificacion = usuarioWS.Identificacion,
                    NombreUsuario = usuarioWS.NombreUsuario,
                    NombreCompleto = usuarioWS.NombreCompleto,
                    Telefono = usuarioWS.Telefono
                });
            }

            return usuarios;
        }

        //hace llamada del metodo del web service para hacer el insert
        public void InsertarUsuario(ModeloUsuario usuario, string contrasena)
        {
            _webServiceCliente.CrearUsuario(usuario.Identificacion, usuario.NombreUsuario, usuario.NombreCompleto, usuario.Telefono, contrasena);
        }

        //obtiene el usuario de pagos moviles por la identificacion
        public ModeloUsuario ObtenerUsuarioPorIdentificacion(string identificacion)
        {
            return _webServiceCliente.ObtenerUsuario(identificacion);
        }

        //actualiza la informacion de un usuario
        public void ActualizarUsuario(ModeloUsuario usuario, string contrasena)
        {
            _webServiceCliente.ActualizarUsuario(usuario.Identificacion, usuario.NombreUsuario, usuario.NombreCompleto, usuario.Telefono, contrasena);
        }

        public void EliminarUsuario(string identificacion)
        {
            // Llama al método del servicio web para eliminar el usuario
            _webServiceCliente.EliminarUsuario(identificacion);
        }
    }
}
