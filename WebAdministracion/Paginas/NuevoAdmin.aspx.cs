using Datos.Modelos;
using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdministracion.Paginas
{
    public partial class NuevoAdmin : System.Web.UI.Page
    {
        private readonly NegociosAdministrador _negociosAdministrador;

        public NuevoAdmin()
        {
            _negociosAdministrador = new NegociosAdministrador(); // Instancia la capa de negocio
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear un nuevo administrador con los datos del formulario
                var nuevoAdmin = new ModeloAdmin
                {
                    NombreUsuario = txt_nombreU.Text,
                    NombreCompleto = txt_nombreC.Text
                };

                // Llama al servicio para crear el administrador
                _negociosAdministrador.CrearAdministrador(nuevoAdmin, txt_contra.Text);

                // Redirige al listado de administradores con un mensaje de éxito
                Response.Redirect("GestionAdministradores.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al crear el administrador: " + ex.Message;
            }
        }

        protected void btn_regresar_Click(object sender, EventArgs e)
        {
            // Redirige a la página de gestión de administradores
            Response.Redirect("GestionAdministradores.aspx");
        }
    }
}