using Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WebUsuarios.Paginas
{
    public partial class ConsultarSaldo : System.Web.UI.Page
    {
        private readonly NegociosConsultarSaldo _negociosConsultarSaldo;

        public ConsultarSaldo()
        {
            _negociosConsultarSaldo = new NegociosConsultarSaldo();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtTelefono.Text = "12345678"; // Placeholder
            }
        }

        protected async void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                string telefono = txtTelefono.Text;
                var result = await _negociosConsultarSaldo.ConsultarSaldo(telefono);

                
                if (result.isSuccess)
                {
                    panelMensaje.Visible = false;
                    panelResultado.Visible = true;
                    saldoAmount.Text = $"<p>{result.message.Replace("Su saldo es: ", "")}</p>";
                    telefonoAsociado.Text = $"Teléfono asociado: {telefono}";
                    
                }
                else
                {
                    panelResultado.Visible = false;
                    // Errores provenientes de los serivicios (Entornos) 
                    MostrarMensaje(
                        tipo: "warning",
                        titulo: "Error",
                        descripcion: result.message,
                        icono: "fa-exclamation-circle" 
                    );
                   
                }
            }
            catch (Exception ex)
            {
                // Errores no controlados
                MostrarMensaje(
                    tipo: "danger",
                    titulo: "Error de Comunicación",
                    descripcion: "No se pudo procesar la solicitud. Intente nuevamente en unos minutos.",
                    icono: "fa-exclamation-triangle" // Ícono de error grave
                );

                
                Console.WriteLine(ex.Message);
            }
        }

        private void MostrarMensaje(string tipo, string titulo, string descripcion, string icono)
        {
            // Actualizar estilos del panel según el tipo (success, danger, etc.)
            panelMensaje.CssClass = $"alert alert-{tipo} mt-3 d-flex align-items-center";
            lblTitulo.Text = titulo;
            lblDescripcion.Text = descripcion;

            // Cambiar el ícono dinámicamente
            iconMensaje.Attributes["class"] = $"fas {icono}";

            panelMensaje.Visible = true;
            panelResultado.Visible = false; // Ocultar panel de saldo
        }
    }
}
