using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace WSAdministracion
{
    /// <summary>
    /// Descripción breve de WebServiceAD_Cuentas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceAD_Cuentas : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public XmlDocument CrearCuetna(string numero_cuenta, string nombre_usuario, string tipo_cuenta, decimal saldo)
        {
            XmlDocument response = new XmlDocument();

            try
            {
                AD_Cuentas cuentasService = new AD_Cuentas();
                cuentasService.CrearCuenta(numero_cuenta, nombre_usuario, tipo_cuenta, saldo);

                response.LoadXml("<Respuesta><Codigo>0</Codigo><Descripcion>Cuenta creada exitosamente</Descripcion></Respuesta>");
            }
            catch (Exception ex)
            {
                response.LoadXml($"<Respuesta><Codigo>-1</Codigo><Descripcion>{ex.Message}</Descripcion></Respuesta>");
            }

            return response;
        }

        [WebMethod]
        public List<Cuentas> ListarCuentas(string nombre_usuario)
        {
            AD_Cuentas cuentasService = new AD_Cuentas();
            return cuentasService.ListarCuentas(nombre_usuario);
        }
    }
}
