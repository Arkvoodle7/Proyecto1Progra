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
                // Llama a la capa de negocios para obtener la lista de usuarios
                var usuarios = _userService.ObtenerTodosLosUsuarios();

                // Enlaza los datos al GridView
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
                // Obtener la identificación del usuario desde CommandArgument
                string identificacion = e.CommandArgument.ToString();

                // Redirigir a la página de edición con la identificación como parámetro
                Response.Redirect($"EditarUsuario.aspx?identificacion={identificacion}");
            }

            if (e.CommandName == "Eliminar")
            {
                // Obtener la identificación del usuario desde CommandArgument
                string identificacion = e.CommandArgument.ToString();

                // Llamar al método de la capa de negocios para eliminar el usuario
                _userService.EliminarUsuario(identificacion);

                // Volver a cargar el listado de usuarios
                CargarUsuarios();

                
            }
        }
    }
}