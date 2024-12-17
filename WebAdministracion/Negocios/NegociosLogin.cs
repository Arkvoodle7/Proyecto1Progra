<<<<<<< HEAD
﻿using System;
=======
using System;
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD

namespace Negocios
{
    internal class NegociosLogin
    {
=======
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
                //Mensaje de error
                throw new Exception($"Error al consumir el servicio web: {ex.Message}", ex);
            }
        }
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
    }
}
