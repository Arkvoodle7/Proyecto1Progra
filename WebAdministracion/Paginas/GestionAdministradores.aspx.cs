using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Modelos;
using Negocios;

namespace WebAdministracion.Paginas
{
    public partial class GestionAdministradores : System.Web.UI.Page
    {
        private readonly NegociosAdministrador _negociosAdministrador;

        public GestionAdministradores()
        {
            _negociosAdministrador = new NegociosAdministrador();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAdministradores();
            }
        }

        private void CargarAdministradores()
        {
            try
            {
              
                var administradores = _negociosAdministrador.ObtenerTodosLosAdministradores();

                // Cargar el GridView
                gvAdministradores.DataSource = administradores;
                gvAdministradores.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los administradores: " + ex.Message;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevoAdmin.aspx");
        }

        protected void gvAdministradores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                string nombreUsuario = e.CommandArgument.ToString();
                Response.Redirect($"EditarAdmin.aspx?nombreUsuario={nombreUsuario}");
            }
            else if (e.CommandName == "Eliminar")
            {
                string nombreUsuario = e.CommandArgument.ToString();

                try
                {
                    _negociosAdministrador.EliminarAdministrador(nombreUsuario);
                    CargarAdministradores(); // Actualiza la lista después de eliminar
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al eliminar el administrador: " + ex.Message;
                }
            }
        }
        // este render te va a servir para eliminar, si no te sirve este le decis a chat y te va a dar otro que se llama diferente
        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow row in gvAdministradores.Rows)
            {
                if (gvAdministradores.DataKeys[row.RowIndex].Value != null)
                {
                    string identificacion = gvAdministradores.DataKeys[row.RowIndex].Value.ToString();
                    ClientScript.RegisterForEventValidation(gvAdministradores.UniqueID, "Eliminar$" + identificacion);
                }
            }
            base.Render(writer);
        }
    }
}
