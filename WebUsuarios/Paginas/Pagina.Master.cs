using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUsuarios.Paginas
{
    public partial class Pagina : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null) //usuario autenticado
            {
                OpcionesLogin.Visible = false;
                OpcionesUsuario.Visible = true;
            }
            else //no autenticado
            {
                OpcionesLogin.Visible = true;
                OpcionesUsuario.Visible = false;
            }
        }

    }
}