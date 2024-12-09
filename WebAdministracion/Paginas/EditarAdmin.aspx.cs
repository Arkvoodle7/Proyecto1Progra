using System;
using Modelos;
using Negocios;

namespace WebAdministracion.Paginas
{
    public partial class EditarAdmin : System.Web.UI.Page
    {
        private readonly NegociosAdministrador _negociosAdministrador;

        public EditarAdmin()
        {
            _negociosAdministrador = new NegociosAdministrador();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string nombreUsuario = Request.QueryString["nombreUsuario"];
                if (!string.IsNullOrEmpty(nombreUsuario))
                {
                    lblNombreUsuario.Text = "Parámetro nombreUsuario: " + nombreUsuario;

                    try
                    {
                        var administrador = _negociosAdministrador.ObtenerAdministradorPorNombreUsuario(nombreUsuario);
                        if (administrador != null)
                        {
                            txt_nombreU.Text = administrador.NombreUsuario;
                            txt_nombreC.Text = administrador.NombreCompleto;
                        }
                        else
                        {
                            lblMensaje.Text = "Administrador no encontrado.";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensaje.Text = "Error al cargar el administrador: " + ex.Message;
                    }
                }
                else
                {
                    lblMensaje.Text = "Nombre de usuario no proporcionado.";
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                var administrador = new ModeloAdmins
                {
                    NombreUsuario = txt_nombreU.Text.Trim(),
                    NombreCompleto = txt_nombreC.Text.Trim()
                };

                string contrasena = txt_contra.Text.Trim();

                if (string.IsNullOrEmpty(contrasena))
                {
                    lblMensaje.Text = "La contraseña no puede estar vacía.";
                    return;
                }

                _negociosAdministrador.ActualizarAdministrador(administrador, contrasena);
                Response.Redirect("GestionAdministradores.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al actualizar el administrador: " + ex.Message;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionAdministradores.aspx");
        }
    }
}
