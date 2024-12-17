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
    public partial class CrearUsuario : System.Web.UI.Page
    {
        private readonly NegociosUsuario _userService;

        public CrearUsuario()
        {
            _userService = new NegociosUsuario();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear un nuevo usuario con los datos del formulario
                var nuevoUsuario = new ModeloUsuario
                {
                    Identificacion = txtIdentificacion.Text,
                    NombreUsuario = txtNombreUsuario.Text,
                    NombreCompleto = txtNombreCompleto.Text,
                    Telefono = txtTelefono.Text,
                };

                // Llama al servicio para crear el usuario
                _userService.CrearUsuario(nuevoUsuario, txtContrasena.Text);

                // Redirige al listado de usuarios con un mensaje de éxito
                Response.Redirect("GestionUsuarios.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al crear el usuario: " + ex.Message;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionUsuarios.aspx");
        }
    }
}