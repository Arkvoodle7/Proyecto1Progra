using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using Negocio;
using Entidades;

namespace WSAdministracion
{
    /// <summary>
    /// Descripción breve de WebServiceAD_Usuarios
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceAD_Usuarios : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }
        [WebMethod]
        public XmlDocument CrearUsuario(string identificacion, string nombreUsuario, string nombreCompleto, string telefono, string contrasena)
        {
            XmlDocument response = new XmlDocument();

            try
            {
                AD_Usuarios usuarioService = new AD_Usuarios();
                usuarioService.CrearUsuario(identificacion, nombreUsuario, nombreCompleto, contrasena, telefono);

                response.LoadXml("<Respuesta><Codigo>0</Codigo><Descripcion>Usuario creado exitosamente</Descripcion></Respuesta>");
            }
            catch (Exception ex)
            {
                response.LoadXml($"<Respuesta><Codigo>-1</Codigo><Descripcion>{ex.Message}</Descripcion></Respuesta>");
            }

            return response;
        }

        [WebMethod]
        public string ActualizarUsuario(string identificacion, string nombreUsuario, string nombreCompleto, string telefono, string contrasena)
        {
            AD_Usuarios usuarioService = new AD_Usuarios();

            string resultado = usuarioService.ActualizarUsuario(identificacion, nombreUsuario, nombreCompleto, contrasena, telefono);

            return resultado;
        }
        [WebMethod]
        public XmlDocument EliminarUsuario(string identificacion)
        {
            XmlDocument response = new XmlDocument();

            try
            {
                AD_Usuarios usuarioService = new AD_Usuarios();

                usuarioService.EliminarUsuario(identificacion);
                response.LoadXml("<Respuesta><Codigo>0</Codigo><Descripcion>Usuario eliminado exitosamente</Descripcion></Respuesta>");
            }
            catch (Exception ex)
            {
                response.LoadXml($"<Respuesta><Codigo>-1</Codigo><Descripcion>{ex.Message}</Descripcion></Respuesta>");
            }

            return response;

        }

        [WebMethod]
        public List<Usuario> ListarUsuarios()
        {
            AD_Usuarios usuarioService = new AD_Usuarios();
            return usuarioService.ListarUsuarios();
        }

        [WebMethod]
        public Usuario ObtenerUsuario(string identificacion)
        {
            AD_Usuarios usuarioService = new AD_Usuarios();
            return usuarioService.ObtenerUsuario(identificacion);
        }
    }
}
