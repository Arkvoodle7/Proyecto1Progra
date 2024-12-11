using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocios.Login_Reference1;

namespace Negocios
{
    public class NegociosLogin
    {
        public LoginAdmin ValidarUsuario(string usuario, string contraseña)
        {
            try
            {
                var servicio = new Login_Reference1.WSA1SoapClient("WSA1Soap");
                var resultado = servicio.Login_Administradores(usuario, contraseña);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al consumir el servicio web: {ex.Message}", ex);
            }
        }
    }
}
