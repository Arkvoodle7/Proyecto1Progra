<<<<<<< HEAD
﻿using System;
=======
﻿using Modelos;
using ServiciosWeb;
using System;
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
<<<<<<< HEAD
    internal class DatosCuenta
    {
=======
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

>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
    }
}
