<<<<<<< HEAD
﻿using System;
=======
﻿using Datos;
using Modelos;
using System;
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios
{
<<<<<<< HEAD
    internal class NegociosCuenta
    {
=======
    public class NegociosCuenta
    {
        private readonly DatosCuenta _datosCuenta;

        public NegociosCuenta()
        {
            _datosCuenta = new DatosCuenta();
        }

        public List<ModeloCuenta> ObtenerTodasLasCuentas()
        {
            return _datosCuenta.ObtenerTodasLasCuentas();
        }

        public void CrearCuenta(ModeloCuenta cuenta)
        {
            if (string.IsNullOrEmpty(cuenta.NumeroCuenta) ||
                string.IsNullOrEmpty(cuenta.NombreUsuario) ||
                string.IsNullOrEmpty(cuenta.TipoCuenta) ||
                cuenta.Saldo <= 0)
            {
                throw new Exception("El saldo debe ser mayor a 0.");
            }

            _datosCuenta.CrearCuenta(cuenta);
        }

        public string ObtenerNombreUsuarioPorCedula(string cedula)
        {
            if (string.IsNullOrEmpty(cedula))
                throw new Exception("La cédula no puede estar vacía.");

            return _datosCuenta.ObtenerNombreUsuarioPorCedula(cedula);
        }


>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
    }
}
