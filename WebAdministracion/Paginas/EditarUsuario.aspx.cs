using Modelos;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdministracion.Paginas
{
    public partial class EditarUsuario : System.Web.UI.Page
    {
        private readonly NegociosUsuario _userService;

        public EditarUsuario()
        {
            _userService = new NegociosUsuario();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //carga los datos del usuario
            if (!IsPostBack)
            {
                string identificacion = Request.QueryString["identificacion"];
                if (!string.IsNullOrEmpty(identificacion))
                {
                    CargarUsuario(identificacion);
                }
                else
                {
                    lblMensaje.Text = "No se proporcionó una identificación válida.";
                }
            }
        }

        private void CargarUsuario(string identificacion)
        {
            try
            {
                //carga los usuarios desde la capa de negocio
                var usuario = _userService.ObtenerUsuarioIdentificacion(identificacion);

                //precarga los datos en los campos del formulario
                txtIdentificacion.Text = usuario.Identificacion;
                txtNombreUsuario.Text = usuario.NombreUsuario;
                txtNombreCompleto.Text = usuario.NombreCompleto;
                txtTelefono.Text = usuario.Telefono;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar los datos del usuario: " + ex.Message;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                //crear un objeto con los datos actualizados
                var usuarioActualizado = new ModeloUsuario
                {
                    Identificacion = txtIdentificacion.Text,
                    NombreUsuario = txtNombreUsuario.Text,
                    NombreCompleto = txtNombreCompleto.Text,
                    Telefono = txtTelefono.Text,
                };

                _userService.ActualizarUsuario(usuarioActualizado, txtContrasena.Text);

                Response.Redirect("GestionUsuarios.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al actualizar el usuario: " + ex.Message;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionUsuarios.aspx");
        }
    }
}