using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.Threading;

class TransaccionHandler
{
    private string tramaEnviadaAlOrquestador;
    private string respuestaDelOrquestador;

    public void ManejarCliente(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string tramaRecibida = Encoding.ASCII.GetString(buffer, 0, bytesRead);

        // Imprimir la trama recibida del simulador
        Console.WriteLine($"Trama recibida del simulador: {tramaRecibida}");

        // Procesar la transacción
        string tramaRespuesta = RecibirTransaccion(tramaRecibida);

        // Imprimir la trama enviada al Orquestador y la respuesta recibida
        if (!string.IsNullOrEmpty(tramaEnviadaAlOrquestador))
        {
            Console.WriteLine($"Trama enviada a Orquestador: {tramaEnviadaAlOrquestador}");
        }
        if (!string.IsNullOrEmpty(respuestaDelOrquestador))
        {
            Console.WriteLine($"Respuesta recibida de Orquestador: {respuestaDelOrquestador}");
        }

        // Imprimir la respuesta enviada al simulador
        Console.WriteLine($"Respuesta enviada al simulador: {tramaRespuesta}");

        byte[] response = Encoding.ASCII.GetBytes(tramaRespuesta);
        stream.Write(response, 0, response.Length);

        // Registrar en la bitácora
        Thread bitacoraThread = new Thread(() => BitacoraHandler.RegistrarBitacora(tramaRecibida, tramaRespuesta));
        bitacoraThread.Start();

        client.Close();
    }

    private string RecibirTransaccion(string trama)
    {
        // Lógica de validación
        dynamic transaccion = Newtonsoft.Json.JsonConvert.DeserializeObject(trama);

        // Validar teléfono
        string telefono = transaccion.telefono;
        if (telefono.Length != 8 || !telefono.All(char.IsDigit))
        {
            return "{\"codigo\":-1, \"descripcion\":\"Debe enviar los datos completos y válidos\"}";
        }

        // Obtener el valor del monto desde la trama
        decimal monto;

        if (transaccion.monto == null || string.IsNullOrEmpty(transaccion.monto.ToString()))
        {
            // Si el monto es nulo o vacío retorna un error
            return "{\"codigo\":-1, \"descripcion\":\"Debe enviar los datos completos y válidos\"}";
        }
        else
        {
            // Convertir el monto si no es nulo
            monto = (decimal)transaccion.monto;
        }

        // Validar el monto
        if (monto > 100000)
        {
            return "{\"codigo\":-1, \"descripcion\":\"El monto no debe ser superior a 100.000\"}";
        }

        // Validar descripción
        string descripcion = transaccion.descripcion;
        if (descripcion.Length > 25)
        {
            return "{\"codigo\":-1, \"descripcion\":\"La descripción no puede superar 25 caracteres\"}";
        }

        // Validar que todos los datos estén presentes
        if (string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(descripcion) || monto <= 0)
        {
            return "{\"codigo\":-1, \"descripcion\":\"Debe enviar los datos completos y válidos\"}";
        }

        string tramaXML = $"<transaccion><telefono>{telefono}</telefono><monto>{monto}</monto><descripcion>{descripcion}</descripcion></transaccion>";

        // Enviar trama al Orquestador
        try
        {
            string respuestaOrquestador = EnviarTramaOrquestador(tramaXML);

            // Procesar la respuesta del Orquestador
            if (respuestaOrquestador.Contains("<codigo>0</codigo>"))
            {
                return "{\"codigo\": 0, \"descripcion\": \"Transacción aplicada\"}";
            }
            else
            {
                return "{\"codigo\": -1, \"descripcion\": \"Error en el Orquestador: " + respuestaOrquestador + "\"}";
            }
        }
        catch (Exception ex)
        {
            return "{\"codigo\": -1, \"descripcion\": \"Error al conectar con el Orquestador: " + ex.Message + "\"}";
        }
    }

    private string EnviarTramaOrquestador(string tramaXML)
    {
        // Leer la IP y puerto del Orquestador desde la configuración
        var config = ConfigManager.LeerConfiguracion("Config.ini");
        string ipOrquestador = config["Orquestador.IP"];
        int portOrquestador = int.Parse(config["Orquestador.Port"]);

        this.tramaEnviadaAlOrquestador = tramaXML;

        using (TcpClient client = new TcpClient(ipOrquestador, portOrquestador))
        {
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(tramaXML);
            stream.Write(data, 0, data.Length);
            // Console.WriteLine($"Trama enviada al Orquestador: {tramaXML}");

            // Leer la respuesta del Orquestador
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string respuesta = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            this.respuestaDelOrquestador = respuesta;

            // Console.WriteLine($"Respuesta del Orquestador: {respuesta}");

            return respuesta;
        }
    }
}
