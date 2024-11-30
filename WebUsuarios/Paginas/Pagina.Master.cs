using System;
using System.Web;

namespace WebUsuarios.Paginas
{
    public partial class Pagina : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ActualizarBarraLateral();
            }
        }

        private void ActualizarBarraLateral()
        {
            if (Session["Usuario"] != null) // Usuario autenticado
            {
                OpcionesLogin.Visible = false;
                OpcionesUsuario.Visible = true;
                IconoUsuario.Visible = true; // Mostrar el menú del usuario
            }
            else // Usuario no autenticado
            {
                OpcionesLogin.Visible = true;
                OpcionesUsuario.Visible = false;
                IconoUsuario.Visible = false; // Ocultar el menú del usuario
            }
        }

        protected void CerrarSesion_Click(object sender, EventArgs e)
        {
            // Limpiar la sesión
            Session.Clear();
            Session.Abandon();

            // Redirigir al login
            Response.Redirect("PaginaLogin.aspx");
        }
    }
}
