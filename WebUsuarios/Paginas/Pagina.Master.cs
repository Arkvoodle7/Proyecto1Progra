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
            if (Session["Usuario"] != null) //usuario autenticado
            {
                OpcionesLogin.Visible = false;
                OpcionesUsuario.Visible = true;
                IconoUsuario.Visible = true; //mostrar el menu del usuario
            }
            else //usuario no autenticado
            {
                OpcionesLogin.Visible = true;
                OpcionesUsuario.Visible = false;
                IconoUsuario.Visible = false; //esconder el menu del usuario
            }
        }

        protected void CerrarSesion_Click(object sender, EventArgs e)
        {
            //limpiar la sesion
            Session.Clear();
            Session.Abandon();

            //redirigir al login
            Response.Redirect("PaginaLogin.aspx");
        }
    }
}
