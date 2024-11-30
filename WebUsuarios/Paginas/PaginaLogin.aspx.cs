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
            // Registrar la tarea asíncrona
            Page.RegisterAsyncTask(new PageAsyncTask(ValidarLogin));
        }

        private async Task ValidarLogin()
        {
            // Capturar valores del formulario
            string nombreUsuario = txtUsuario.Text.Trim();
            string contrasena = txtPassword.Text.Trim();

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(contrasena))
            {
                lblMensaje.Text = "Por favor, ingrese sus credenciales.";
                return;
            }

            // Llamar al método de negocios para validar credenciales
            try
            {
                var respuesta = await negociosLogin.ValidarCredencialesAsync(nombreUsuario, contrasena);

                if (respuesta.Resultado == 0) // Credenciales correctas
                {
                    // Establecer la sesión del usuario autenticado
                    Session["Usuario"] = nombreUsuario;

                    // Redirigir a la página principal (la maestra se encargará de mostrar las opciones correctas)
                    Response.Redirect("ConsultarSaldo.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else // Credenciales incorrectas
                {
                    lblMensaje.Text = respuesta.Mensaje; // Mostrar mensaje de error
                }
            }
            catch (Exception ex)
            {
                // Manejar errores
                lblMensaje.Text = "Error al procesar la solicitud: " + ex.Message;
            }
        }
    }
}
