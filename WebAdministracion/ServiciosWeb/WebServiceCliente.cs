using ServiciosWeb.WSUsuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos;

namespace ServiciosWeb
{
    public class WebServiceCliente
    {
        private readonly WebServiceAD_UsuariosSoapClient _client;

        public WebServiceCliente()
        {
            _client = new WebServiceAD_UsuariosSoapClient();
        }

        //metodo para llamar al listar usuario del webservice
        public List<Usuario> ListarUsuarios()
        {
            return _client.ListarUsuarios().ToList();
        }

        //metodo para llamar al crear usuario del webservice
        public void CrearUsuario(string identificacion, string nombreUsuario, string nombreCompleto, string telefono, string contrasena)
        {
            _client.CrearUsuario(identificacion, nombreUsuario, nombreCompleto, telefono, contrasena);
        }

        public ModeloUsuario ObtenerUsuario(string identificacion)
        {
            var usuarioWS = _client.ObtenerUsuario(identificacion);
            return new ModeloUsuario
            {
                Identificacion = usuarioWS.Identificacion,
                NombreUsuario = usuarioWS.NombreUsuario,
                NombreCompleto = usuarioWS.NombreCompleto,
                Telefono = usuarioWS.Telefono
            };
        }

        public void ActualizarUsuario(string identificacion, string nombreUsuario, string nombreCompleto, string telefono, string contrasena)
        {
            _client.ActualizarUsuario(identificacion, nombreUsuario, nombreCompleto, telefono, contrasena);
        }

        public void EliminarUsuario(string identificacion)
        {
            _client.EliminarUsuario(identificacion);
        }

    }
}
