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
    public partial class CrearCuenta : System.Web.UI.Page
    {
        private readonly NegociosCuenta _negociosCuenta;

        public CrearCuenta()
        {
            _negociosCuenta = new NegociosCuenta();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void txtCedulaUsuario_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string cedula = txtCedulaUsuario.Text.Trim();
                string nombreUsuario = _negociosCuenta.ObtenerNombreUsuarioPorCedula(cedula);

                if (!string.IsNullOrEmpty(nombreUsuario))
                {
                    lblNombreUsuario.Text = "Nombre del Usuario: " + nombreUsuario;
                    lblNombreUsuario.CssClass = "text-success";
                }
                else
                {
                    lblNombreUsuario.Text = "No se encontró ningún usuario con esta cédula.";
                    lblNombreUsuario.CssClass = "text-danger";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al buscar el usuario: " + ex.Message;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener el nombre del usuario usando la cédula ingresada
                string cedula = txtCedulaUsuario.Text.Trim();
                string nombreUsuario = _negociosCuenta.ObtenerNombreUsuarioPorCedula(cedula);

                if (string.IsNullOrEmpty(nombreUsuario))
                {
                    lblMensaje.Text = "No se encontró un usuario con esta cédula.";
                    lblMensaje.CssClass = "text-danger";
                    return; // detiene el proceso si no se encuentra el usuario
                }

                var cuenta = new ModeloCuenta
                {
                    NumeroCuenta = txtNumeroCuenta.Text.Trim(),
                    NombreUsuario = nombreUsuario,
                    TipoCuenta = ddlTipoCuenta.SelectedValue,
                    Saldo = Convert.ToDecimal(txtSaldoInicial.Text.Trim())
                };

                _negociosCuenta.CrearCuenta(cuenta);
                Response.Redirect("GestionCuentas.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al crear la cuenta: " + ex.Message;
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionCuentas.aspx");
        }

        protected void cvSaldoInicial_ServerValidate(object source, ServerValidateEventArgs args)
        {
            decimal saldo;
            if (decimal.TryParse(args.Value, out saldo))
            {
                // valida que el saldo este dentro del rango permitido
                if (saldo >= 0.01m && saldo <= 9999999999.99m)
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                }
            }
            else
            {
                args.IsValid = false;
            }
        }
    }
}