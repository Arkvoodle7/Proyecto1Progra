using Newtonsoft.Json;
using System.Net.Sockets;
using System.IO;
using System.Runtime.InteropServices;

namespace SimuladorOtroBanco
{
    public partial class frmSimuladorOtroBanco : Form
    {
        [DllImport("kernel32.dll")]
        static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();

        public frmSimuladorOtroBanco()
        {
            InitializeComponent();
            InicializarConsola();
        }

        public void InicializarConsola()
        {
            AllocConsole();
            Console.Title = "Consola de SimuladorOtroBanco";
        }

        private void frmSimuladorOtroBanco_FormClosing(object sender, FormClosingEventArgs e)
        {
            FreeConsole();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            string telefono = txtTelefono.Text;
            string monto = txtMonto.Text;
            string descripcion = txtDescripcion.Text;

            //generar la trama JSON
            string tramaJSON = GenerarTramaJSON(telefono, monto, descripcion);

            //enviar la trama al socket del Receptor Externo
            EnviarTrama(tramaJSON);
        }

        private string GenerarTramaJSON(string telefono, string monto, string descripcion)
        {
            var transaccion = new
            {
                telefono = telefono,
                monto = string.IsNullOrWhiteSpace(monto) ? (decimal?)null : TryParseMonto(monto),
                descripcion = descripcion
            };

            //convertir el objeto a JSON
            return JsonConvert.SerializeObject(transaccion);
        }

        private decimal? TryParseMonto(string monto)
        {
            decimal result;
            if (decimal.TryParse(monto, out result))
            {
                return result;
            }
            else
            {
                //si no es un numero, retorna null
                return null;
            }
        }

        private void EnviarTrama(string trama)
        {
            try
            {
                //cargar la configuracion desde el archivo .ini
                var config = LeerConfiguracion("Config.ini");
                string ipReceptorExterno = config["IP"];
                int puertoReceptorExterno = int.Parse(config["Port"]);

                //crear conexion TCP al Receptor Externo
                TcpClient client = new TcpClient(ipReceptorExterno, puertoReceptorExterno);
                NetworkStream stream = client.GetStream();

                //enviar la trama
                byte[] dataToSend = System.Text.Encoding.ASCII.GetBytes(trama);
                stream.Write(dataToSend, 0, dataToSend.Length);

                //agregar timeout para evitar espera indefinida
                client.ReceiveTimeout = 30000;  // 30 segundos de timeout

                //recibir la respuesta del Receptor Externo
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string respuesta = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);

                //mostrar la respuesta en la consola
                Console.WriteLine($"Respuesta recibida del Receptor Externo: {respuesta}");

                //cerrar la conexion
                client.Close();
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static Dictionary<string, string> LeerConfiguracion(string filePath)
        {
            var config = new Dictionary<string, string>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("[") && !line.StartsWith("#") && line.Contains('='))
                {
                    var keyValue = line.Split('=');
                    config[keyValue[0].Trim()] = keyValue[1].Trim();
                }
            }

            return config;
        }
    }
}
