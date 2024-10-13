using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

class TransaccionHandler
{
    public void ManejarCliente(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string tramaRecibida = Encoding.ASCII.GetString(buffer, 0, bytesRead);

        //procesar la transaccion
        string tramaRespuesta = RecibirTransaccion(tramaRecibida);
        byte[] response = Encoding.ASCII.GetBytes(tramaRespuesta);
        stream.Write(response, 0, response.Length);

        //imprimir la trama recibida y la respuesta
        Console.WriteLine($"Trama recibida: {tramaRecibida}");
        Console.WriteLine($"Respuesta enviada: {tramaRespuesta}");

        //registrar en la bitacora
        BitacoraHandler.RegistrarBitacora(tramaRecibida, tramaRespuesta);

        client.Close();
    }

    private string RecibirTransaccion(string trama)
    {
        //logica de validacion
        dynamic transaccion = Newtonsoft.Json.JsonConvert.DeserializeObject(trama);

        //validar telefono
        string telefono = transaccion.telefono;
        if (telefono.Length != 8 || !telefono.All(char.IsDigit))
        {
            return "{\"codigo\":-1, \"descripcion\":\"Debe enviar los datos completos y válidos\"}";
        }

        //obtener el valor del monto desde la trama
        decimal monto;

        if (transaccion.monto == null || string.IsNullOrEmpty(transaccion.monto.ToString()))
        {
            //si el monto es nulo o vacio retorna un error
            return "{\"codigo\":-1, \"descripcion\":\"Debe enviar los datos completos y válidos\"}";
        }
        else
        {
            //convertir el monto si no es nulo
            monto = (decimal)transaccion.monto;
        }

        //validar el monto
        if (monto > 100000)
        {
            return "{\"codigo\":-1, \"descripcion\":\"El monto no debe ser superior a 100.000\"}";
        }

        //validar descripcion
        string descripcion = transaccion.descripcion;
        if (descripcion.Length > 25)
        {
            return "{\"codigo\":-1, \"descripcion\":\"La descripción no puede superar 25 caracteres\"}";
        }

        //validar que todos los datos esten presentes
        if (string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(descripcion) || monto <= 0)
        {
            return "{\"codigo\":-1, \"descripcion\":\"Debe enviar los datos completos y válidos\"}";
        }

        string tramaXML = $"<transaccion><telefono>{telefono}</telefono><monto>{monto}</monto><descripcion>{descripcion}</descripcion></transaccion>";

        //enviar trama al Orquestador
        try
        {
            string respuestaOrquestador = EnviarTramaOrquestador(tramaXML);

            //procesar la respuesta del Orquestador
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
        //leer la IP y puerto del Orquestador desde la configuracion
        var config = ConfigManager.LeerConfiguracion("Config.ini");
        string ipOrquestador = config["Orquestador.IP"];
        int portOrquestador = int.Parse(config["Orquestador.Port"]);

        using (TcpClient client = new TcpClient(ipOrquestador, portOrquestador))
        {
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(tramaXML);
            stream.Write(data, 0, data.Length);

            //leer la respuesta del Orquestador
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string respuesta = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            return respuesta;
        }
    }
}
