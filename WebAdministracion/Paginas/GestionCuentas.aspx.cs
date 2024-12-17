using System;
<<<<<<< HEAD
=======
using Negocios;
using Modelos;
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdministracion.Paginas
{
    public partial class GestionCuentas : System.Web.UI.Page
    {
<<<<<<< HEAD
        protected void Page_Load(object sender, EventArgs e)
        {

=======
        private readonly NegociosCuenta _cuentaService;

        public GestionCuentas()
        {
            _cuentaService = new NegociosCuenta();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCuentas();
            }
        }

        private void CargarCuentas()
        {
            try
            {
                // Llama a la capa de negocios para obtener las cuentas
                var cuentas = _cuentaService.ObtenerTodasLasCuentas();

                // Enlaza los datos al GridView
                gvCuentas.DataSource = cuentas;
                gvCuentas.DataBind();
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar las cuentas: " + ex.Message;
            }
        }

        protected void btnAgregarCuenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("CrearCuenta.aspx");
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
        }
    }
}