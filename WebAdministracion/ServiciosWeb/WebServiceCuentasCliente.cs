using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiciosWeb.WSCuentas;
using Modelos;

namespace ServiciosWeb
{
    public class WebServiceCuentasCliente
    {
        private readonly WebServiceAD_CuentasSoapClient _client;

        public WebServiceCuentasCliente()
        {
            _client = new WebServiceAD_CuentasSoapClient();
        }

        // metodo para obtener el listado de cuentas
        public List<ModeloCuenta> ListarTodasCuentas()
        {
            var cuentasWS = _client.ListarTodasCuentas(); // llama al metodo del web service

            // convierte las cuentas del servicio a ModeloCuenta
            return cuentasWS.Select(cuentaWS => new ModeloCuenta
            {
                NumeroCuenta = cuentaWS.NumeroCuenta,
                NombreUsuario = cuentaWS.NombreUsuario,
                TipoCuenta = cuentaWS.TipoCuenta,
                Saldo = cuentaWS.Saldo
            }).ToList();
        }

        public void CrearCuenta(string numeroCuenta, string nombreUsuario, string tipoCuenta, decimal saldo)
        {
            _client.CrearCuetna(numeroCuenta, nombreUsuario, tipoCuenta, saldo);
        }

        public string ObtenerNombreUsuarioPorCedula(string cedula)
        {
            return _client.ObtenerNombreUsuarioPorCedula(cedula);
        }

    }
}
