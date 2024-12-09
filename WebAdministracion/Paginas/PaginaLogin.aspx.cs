using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdministracion.Paginas
{
    public partial class PaginaLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contraseña = txtPassword.Text;

            var negocioLogin = new NegociosLogin();
            var resultado = negocioLogin.ValidarUsuario(usuario, contraseña);
            try
            {
                if (resultado.resultado == 0)
                {
                    //redirigir
                    Response.Redirect("GestionAdministradores.aspx");
                }
                else 
                {
                    lblMensaje.Text = resultado.mensaje;
                }
            }
            catch(Exception ex)
            {
                lblMensaje.Text = "Ha ocurrido un error: " + ex.Message;
            }
        }
    }
}
