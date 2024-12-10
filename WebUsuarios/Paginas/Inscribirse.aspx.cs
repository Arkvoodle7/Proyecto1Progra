using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI;
using WebUsuarios.ServicioCuentas;

namespace WebUsuarios.Paginas
{
    public partial class Inscribirse : System.Web.UI.Page
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
                    ddlCuentas.DataTextField = "NumeroCuenta"; // Nombre de la propiedad que se mostrará
                    ddlCuentas.DataValueField = "NumeroCuenta"; // Valor asociado a cada opción
                    ddlCuentas.DataBind();

                    // Agregar un ítem inicial al DropDownList
                    ddlCuentas.Items.Insert(0, new ListItem("Seleccione una cuenta", ""));

                    // Precargar el número de teléfono asociado al usuario
                    // Supongamos que el servicio también devuelve el teléfono asociado al usuario
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

        protected async void btnInscribir_Click(object sender, EventArgs e)
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
                RegistrationRequestDto request = new RegistrationRequestDto
                {
                    Cuenta = cuenta,
                    Identificacion = identificacion,
                    Telefono = telefono
                };

                // Llamar al servicio
                RegistrationServiceClient serviceClient = new RegistrationServiceClient();
                RegistrationResponseDto response = await serviceClient.RegisterUserAsync(request);

                // Manejar la respuesta
                if (response.Codigo == 0)
                {
                    lblMensaje.Text = "Operación exitosa";
                    lblMensaje.CssClass = "text-success";
                }
                else
                {
                    lblMensaje.Text = "Error en la inscripción: " + response.Mensaje;
                    lblMensaje.CssClass = "text-danger";
                }
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje detallado del error
                lblMensaje.Text = "Error al procesar la inscripción: " + ex.Message;
                Console.WriteLine("Error detallado: " + ex.ToString());
            }
        }
    }
}
