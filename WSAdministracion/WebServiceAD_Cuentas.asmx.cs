using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
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
        public void CrearCuetna(string numero_cuenta, string nombre_usuario, string tipo_cuenta, decimal saldo)
        {
            AD_Cuentas cuentasService = new AD_Cuentas();
            cuentasService.CrearCuenta(numero_cuenta, nombre_usuario, tipo_cuenta, saldo);
        }

        [WebMethod]
        public List<Cuentas> ListarCuentas(string nombre_usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre_usuario))
                {
                    throw new Exception("El parámetro 'nombre_usuario' no puede ser nulo o vacío.");
                }

                AD_Cuentas cuentasService = new AD_Cuentas();
                return cuentasService.ListarCuentas(nombre_usuario);
            }
            catch (Exception ex)
            {
                // Lanzar una excepción detallada al cliente
                throw new SoapException("Error en el servicio: " + ex.Message, SoapException.ServerFaultCode, ex);
            }
        }
        [WebMethod]
        public string ObtenerTelefonoUsuario(string nombreUsuario)
        {
            try
            {
                AD_Cuentas cuentasService = new AD_Cuentas();
                return cuentasService.ObtenerTelefonoPorUsuario(nombreUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el teléfono: " + ex.Message);
            }
        }
    }
}