using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAdministracion.Paginas
{
    public partial class GestionCuentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCuentas();
            }
        }
        private void CargarCuentas()
        {
            // Simulación de datos
            var cuentas = new List<object>
    {
        new { NumeroCuenta = "123456", Usuario = "jperez", TipoCuenta = "Ahorros", Saldo = 5000 },
        new { NumeroCuenta = "654321", Usuario = "mlopez", TipoCuenta = "Empresarial", Saldo = 15000 }
    };

            gvCuentas.DataSource = cuentas;
            gvCuentas.DataBind();
        }
    }
}