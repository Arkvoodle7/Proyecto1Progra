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
            if (Session["Usuario"] != null) //administrador autenticado
            {
                OpcionesLogin.Visible = false;
                OpcionesAdministrador.Visible = true;
            }
            else //no autenticado
            {
                OpcionesLogin.Visible = true;
                OpcionesAdministrador.Visible = false;
            }
        }

    }
}