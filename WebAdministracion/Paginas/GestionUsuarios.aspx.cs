using Modelos;
using Negocios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;


namespace WebAdministracion.Paginas
{
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        private readonly NegociosUsuario _userService;

        public GestionUsuarios()
        {
            _userService = new NegociosUsuario();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            try
            {
                var usuarios = _userService.ObtenerTodosLosUsuarios();

                //carga el grrid
                gvUsuarios.DataSource = usuarios;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los usuarios: " + ex.Message;
            }
        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("CrearUsuario.aspx");
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Editar")
            {
                string identificacion = e.CommandArgument.ToString();

                // redirige a la pagina de edicion con la identificacion como parametro
                Response.Redirect($"EditarUsuario.aspx?identificacion={identificacion}");
            }

            if (e.CommandName == "Eliminar")
            {
                //obtiene la identificación del usuario desde el CommandArgument
                string identificacion = e.CommandArgument.ToString();

                if (!string.IsNullOrEmpty(identificacion))
                {
                    try
                    {
                        _userService.EliminarUsuario(identificacion);

                        CargarUsuarios();

                    }
                    catch (Exception ex)
                    {
                        lblMensaje.Text = "Error al eliminar el usuario: " + ex.Message;
                    }
                }
                else
                {
                    lblMensaje.Text = "La identificación no puede estar vacía.";
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow row in gvUsuarios.Rows)
            {
                if (gvUsuarios.DataKeys[row.RowIndex].Value != null)
                {
                    string identificacion = gvUsuarios.DataKeys[row.RowIndex].Value.ToString();
                    ClientScript.RegisterForEventValidation(gvUsuarios.UniqueID, "Eliminar$" + identificacion);
                }
            }
            base.Render(writer);
        }

    }
}