using System;
using System.Web.UI.WebControls;
using WebUsuarios.ServicioCuentas;

namespace WebUsuarios.Paginas
{
    public partial class Desinscribirse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PrecargarInformacion();
            }
        }

        private void PrecargarInformacion()
        {
            try
            {
                if (Session["Usuario"] != null)
                {
                    string nombreUsuario = Session["Usuario"].ToString();

                    // Instanciar el cliente del servicio
                    WebServiceAD_CuentasSoapClient cliente = new WebServiceAD_CuentasSoapClient();

                    // Llamar al método del servicio para listar cuentas del usuario
                    var cuentas = cliente.ListarCuentas(nombreUsuario);

                    // Llenar el DropDownList con las cuentas del usuario
                    ddlCuentas.DataSource = cuentas;
                    ddlCuentas.DataTextField = "NumeroCuenta";
                    ddlCuentas.DataValueField = "NumeroCuenta";
                    ddlCuentas.DataBind();

                    // Agregar un ítem inicial al DropDownList
                    ddlCuentas.Items.Insert(0, new ListItem("Seleccione una cuenta", ""));

                    // Precargar el número de teléfono asociado al usuario
                    var telefono = cliente.ObtenerTelefonoUsuario(nombreUsuario); // Método ficticio
                    if (!string.IsNullOrEmpty(telefono))
                    {
                        txtTelefono.Text = telefono;
                        txtTelefono.Enabled = false; // Deshabilitar el campo para que no se pueda modificar
                    }
                    else
                    {
                        lblMensaje.Text = "No se pudo cargar el teléfono del usuario.";
                    }
                }
                else
                {
                    lblMensaje.Text = "Debe iniciar sesión para ver sus cuentas.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al cargar la información: " + ex.Message;
            }
        }

        protected async void btnDesinscribir_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los valores ingresados por el usuario
                string cuenta = ddlCuentas.SelectedValue;
                string identificacion = txtIdentificacion.Text.Trim();
                string telefono = txtTelefono.Text.Trim();

                // Validar campos obligatorios
                if (string.IsNullOrEmpty(cuenta) || string.IsNullOrEmpty(identificacion) || string.IsNullOrEmpty(telefono))
                {
                    lblMensaje.Text = "Todos los campos son obligatorios.";
                    return;
                }

                // Crear el objeto de solicitud
                DesinscripcionRequestDto request = new DesinscripcionRequestDto
                {
                    Cuenta = cuenta,
                    Identificacion = identificacion,
                    Telefono = telefono
                };

                // Llamar al servicio
                DesinscripcionServiceClient serviceClient = new DesinscripcionServiceClient();
                DesinscripcionResponseDto response = await serviceClient.UnregisterUserAsync(request);

                // Manejar la respuesta
                if (response.Codigo == 0)
                {
                    lblMensaje.Text = "Operacion exitosa";
                    lblMensaje.CssClass = "text-success";
                }
                else
                {
                    lblMensaje.Text = "Error en la desinscripción: " + response.Mensaje;
                    lblMensaje.CssClass = "text-danger";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Error al procesar la desinscripción: " + ex.Message;
            }
        }
    }
}
