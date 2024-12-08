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
                // Obtener el parámetro "identificacion" de la URL
                string identificacion = Request.QueryString["identificacion"];

                if (!string.IsNullOrEmpty(identificacion))
                {
                    try
                    {
                        // Llamar a la capa de negocios para obtener el usuario
                        var usuario = _userService.ObtenerUsuarioIdentificacion(identificacion);

                        if (usuario != null)
                        {
                            // Rellenar los campos con los datos del usuario
                            txtIdentificacion.Text = usuario.Identificacion;
                            txtNombreUsuario.Text = usuario.NombreUsuario;
                            txtNombreCompleto.Text = usuario.NombreCompleto;
                            txtTelefono.Text = usuario.Telefono;
                        }
                        else
                        {
                            lblMensaje.Text = "Usuario no encontrado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensaje.Text = "Error al cargar el usuario: " + ex.Message;
                    }
                }
                else
                {
                    lblMensaje.Text = "Identificación no proporcionada.";
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
                // Crear el objeto usuario con los datos del formulario
                var usuario = new ModeloUsuario
                {
                    Identificacion = txtIdentificacion.Text,
                    NombreUsuario = txtNombreUsuario.Text,
                    NombreCompleto = txtNombreCompleto.Text,
                    Telefono = txtTelefono.Text
                };

                // Actualizar el usuario
                _userService.ActualizarUsuario(usuario, txtContrasena.Text);

                // Redirigir al listado de usuarios después de la actualización
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