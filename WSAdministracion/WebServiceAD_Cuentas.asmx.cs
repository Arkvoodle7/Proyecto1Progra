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
        public void CrearCuetna(string numero_cuenta, string nombre_usuario, string tipo_cuenta, decimal saldo)
        {
            AD_Cuentas cuentasService = new AD_Cuentas();
            cuentasService.CrearCuenta(numero_cuenta, nombre_usuario, tipo_cuenta, saldo);
        }

        [WebMethod]
        public List<Cuentas> ListarCuentas(string nombre_usuario)
        {
            AD_Cuentas cuentasService = new AD_Cuentas();
            return cuentasService.ListarCuentas(nombre_usuario);
        }
    }
}
