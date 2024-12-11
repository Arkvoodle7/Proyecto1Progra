using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdministracion.Paginas
{
    public partial class Pagina : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioAutenticado"] != null)
            {
                // Administrador autenticado 
                OpcionesLogin.Visible = false;  
                OpcionesCerrarSesion.Visible = true;
                OpcionesAdministrador.Visible = true; 
            }
            else
            {
                // Administrador no auntenticado
                OpcionesLogin.Visible = true; 
                OpcionesCerrarSesion.Visible = false;
                OpcionesAdministrador.Visible = false; 
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Cerrar sesión, deshabilitar todas las opciones 
            Session.Clear();
            Session.Abandon();
            Response.Redirect("PaginaLogin.aspx");
            OpcionesCerrarSesion.Visible = false;
            OpcionesAdministrador.Visible = false;
        }
    }
}