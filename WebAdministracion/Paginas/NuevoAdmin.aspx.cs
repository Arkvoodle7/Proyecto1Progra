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
    public partial class NuevoAdmin : System.Web.UI.Page
    {
        private readonly NegociosAdministrador _negociosAdministrador;

        public NuevoAdmin()
        {
            _negociosAdministrador = new NegociosAdministrador();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear un nuevo administrador
                var nuevoAdmin = new ModeloAdmins
                {
                    NombreUsuario = txt_nombreU.Text,
                    NombreCompleto = txt_nombreC.Text
                };

                
                _negociosAdministrador.CrearAdministrador(nuevoAdmin, txt_contra.Text);

                
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