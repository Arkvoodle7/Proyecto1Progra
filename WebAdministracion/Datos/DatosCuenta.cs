using Modelos;
using ServiciosWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosCuenta
    {
        private readonly WebServiceCuentasCliente _webServiceCliente;

        public DatosCuenta()
        {
            _webServiceCliente = new WebServiceCuentasCliente();
        }

        public List<ModeloCuenta> ObtenerTodasLasCuentas()
        {
            return _webServiceCliente.ListarTodasCuentas();
        }

        public void CrearCuenta(ModeloCuenta cuenta)
        {
            _webServiceCliente.CrearCuenta(cuenta.NumeroCuenta, cuenta.NombreUsuario, cuenta.TipoCuenta, cuenta.Saldo);
        }

        public string ObtenerNombreUsuarioPorCedula(string cedula)
        {
            return _webServiceCliente.ObtenerNombreUsuarioPorCedula(cedula);
        }

    }
}
