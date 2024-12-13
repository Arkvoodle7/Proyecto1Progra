using System;
using System.Threading.Tasks;
using Negocios;

namespace WebUsuarios.Paginas
{
    public partial class Transferencias : System.Web.UI.Page
    {
        private readonly NegociosTransferencias _negociosTransferencias = new NegociosTransferencias();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Validación de postback para mostrar mensajes en campos vacíos
            if (IsPostBack)
            {
                var eventArg = Request["__EVENTARGUMENT"];
                if (!string.IsNullOrEmpty(eventArg) && eventArg == "Campos vacíos")
                {
                    MostrarMensaje("Error", "Debe completar todos los campos del formulario.", "danger", "fa-exclamation-circle");
                }
            }
        }

        protected async void btnConfirmar_Click(object sender, EventArgs e)
        {
            string telefonoDestino = txtTelefonoDestino.Text.Trim();
            string montoTexto = txtMonto.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            if (string.IsNullOrEmpty(telefonoDestino) || string.IsNullOrEmpty(montoTexto) || string.IsNullOrEmpty(descripcion))
            {
                MostrarMensaje("Error", "Debe completar todos los campos del formulario.", "danger", "fa-exclamation-circle");
                return;
            }

            if (!decimal.TryParse(montoTexto, out decimal monto) || monto <= 0)
            {
                MostrarMensaje("Error", "Ingrese un monto válido.", "danger", "fa-exclamation-circle");
                return;
            }

            var (isSuccess, mensaje) = await _negociosTransferencias.RealizarTransferencia(telefonoDestino, monto, descripcion);

            if (isSuccess)
            {
                MostrarMensaje("Operación Exitosa", mensaje, "success", "fa-check-circle");
            }
            else
            {
                MostrarMensaje("Error", mensaje, "danger", "fa-times-circle");
            }
        }

        private void MostrarMensaje(string titulo, string descripcion, string tipo, string icono)
        {
            lblMensaje.Text = $@"
                <div class='alert alert-{tipo} alert-dismissible fade show' role='alert'>
                    <i class='alert-custom-icon fas {icono}'></i>
                    <strong>{titulo}</strong><br />{descripcion}
                    <button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button>
                </div>";
        }
    }
}
