using System;
using Negocios;
using System.Threading.Tasks;
using System.Web.UI;

namespace WebUsuarios.Paginas
{
    public partial class PaginaRegistro : System.Web.UI.Page
    {
        private readonly NegociosRegistro negociosRegistro = new NegociosRegistro();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Registrar la tarea asíncrona
            Page.RegisterAsyncTask(new PageAsyncTask(RegistrarUsuario));
        }

        private async Task RegistrarUsuario()
        {
            // Capturar valores del formulario
            string identificacion = txtIdentificacion.Text.Trim();
            string nombreUsuario = txtNombreUsuario.Text.Trim();
            string nombreCompleto = txtNombreCompleto.Text.Trim();
            string contrasena = txtPassword.Text.Trim();
            string telefono = txtTelefono.Text.Trim();

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(identificacion) ||
                string.IsNullOrEmpty(nombreUsuario) ||
                string.IsNullOrEmpty(nombreCompleto) ||
                string.IsNullOrEmpty(contrasena) ||
                string.IsNullOrEmpty(telefono))
            {
                // No hacer nada si hay campos vacíos
                return;
            }

            // Crear objeto Usuario de Negocios
            var usuario = new Usuario
            {
                Identificacion = identificacion,
                NombreUsuario = nombreUsuario,
                NombreCompleto = nombreCompleto,
                Contrasena = contrasena,
                Telefono = telefono
            };

            // Llamar al método de Negocios para registrar el usuario
            bool registroExitoso = await negociosRegistro.RegistrarUsuarioAsync(usuario);

            if (registroExitoso)
            {
                // Registro exitoso, redirigir a PaginaLogin.aspx
                Response.Redirect("PaginaLogin.aspx");
            }
            else
            {
                // No hacer nada si el registro falla
                // Puedes agregar lógica adicional si lo deseas
            }
        }
    }
}
