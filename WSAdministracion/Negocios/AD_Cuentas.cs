using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Datos;
using Entidades;

namespace Negocio
{
    public class AD_Cuentas
    {
        public void CrearCuenta(string numero_cuenta, string nombre_usuario, string tipo_cuenta, decimal saldo)
        {
            AD_CuentasBD repo = new AD_CuentasBD();
            repo.InsertCuentas(numero_cuenta, nombre_usuario, tipo_cuenta, saldo);
        }

        public List<Cuentas> ListarCuentas(string nombre_usuario)
        {
            AD_CuentasBD cuentasBD = new AD_CuentasBD();

            return cuentasBD.MostrarCuentas(nombre_usuario);
        }
    }
}
