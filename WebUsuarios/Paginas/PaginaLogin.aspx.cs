using System;
using Negocios;
using System.Threading.Tasks;
using System.Web.UI;

namespace WebUsuarios.Paginas
{
    public partial class PaginaLogin : System.Web.UI.Page
    {
        private readonly NegociosLogin negociosLogin = new NegociosLogin();

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Page.RegisterAsyncTask(new PageAsyncTask(ValidarLogin));
        }

        private async Task ValidarLogin()
        {
            //capturar valores del formulario
            string nombreUsuario = txtUsuario.Text.Trim();
            string contrasena = txtPassword.Text.Trim();

            //validar que los campos no esten vacios
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena))
            {
                lblMensaje.Text = "Por favor, ingrese sus credenciales.";
                return;
            }

            //llamar al metodo de negocios para validar credenciales
            try
            {
                var respuesta = await negociosLogin.ValidarCredencialesAsync(nombreUsuario, contrasena);

                if (respuesta.Resultado == 0) //credenciales correctas
                {
                    //establecer la sesion del usuario autenticado
                    Session["Usuario"] = nombreUsuario;

                    //redirigir a la pagina de consulta de saldo
                    Response.Redirect("ConsultarSaldo.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else //credenciales incorrectas
                {
                    lblMensaje.Text = respuesta.Mensaje;
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al procesar la solicitud: " + ex.Message;
            }
        }
    }
}
