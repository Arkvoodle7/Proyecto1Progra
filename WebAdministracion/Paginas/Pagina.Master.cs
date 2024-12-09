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
                // Usuario autenticado, mostrar las opciones de administración
                OpcionesLogin.Visible = false;  // Ocultar opciones de login
                OpcionesAdministrador.Visible = true;  // Mostrar las opciones de administrador
            }
            else
            {
                // Usuario no autenticado, mostrar las opciones de login
                OpcionesLogin.Visible = true;   // Mostrar opciones de login
                OpcionesAdministrador.Visible = false;  // Ocultar las opciones de administrador
            }
        }
    }
}