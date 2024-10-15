using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using System.Xml;

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

        //imprimir la trama recibida del simulador
        Console.WriteLine($"Trama recibida del simulador: {tramaRecibida}");

        //procesar la transaccion
        string tramaRespuesta = RecibirTransaccion(tramaRecibida);

        //imprimir la trama enviada al Orquestador y la respuesta recibida
        if (!string.IsNullOrEmpty(tramaEnviadaAlOrquestador))
        {
            Console.WriteLine($"Trama enviada a Orquestador: {tramaEnviadaAlOrquestador}");
        }
        if (!string.IsNullOrEmpty(respuestaDelOrquestador))
        {
            Console.WriteLine($"Respuesta recibida de Orquestador: {respuestaDelOrquestador}");
        }

        //imprimir la respuesta enviada al simulador
        Console.WriteLine($"Respuesta enviada al simulador: {tramaRespuesta}");

        byte[] response = Encoding.ASCII.GetBytes(tramaRespuesta);
        stream.Write(response, 0, response.Length);

        //registrar en la bitacora
        Thread bitacoraThread = new Thread(() => BitacoraHandler.RegistrarBitacora(tramaRecibida, tramaRespuesta));
        bitacoraThread.Start();

        client.Close();
    }

    private string RecibirTransaccion(string trama)
    {
        //logica de validacion
        dynamic transaccion = JsonConvert.DeserializeObject(trama);

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
            //si el monto es nulo o vacío retorna un error
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
            var codigoYDescripcion = ExtraerCodigoYDescripcion(respuestaOrquestador);

            int codigo = codigoYDescripcion.Item1;
            string descripcionRespuesta = codigoYDescripcion.Item2;

            return $"{{\"codigo\": {codigo}, \"descripcion\": \"{descripcionRespuesta}\"}}";
        }
        catch (Exception ex)
        {
            return $"{{\"codigo\": -1, \"descripcion\": \"Error al conectar con el Orquestador: {ex.Message}\"}}";
        }
    }

    private (int, string) ExtraerCodigoYDescripcion(string respuestaOrquestador)
    {
        //intentar parsear como XML
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(respuestaOrquestador);

            XmlNode codigoNode = xmlDoc.SelectSingleNode("//codigo");
            XmlNode descripcionNode = xmlDoc.SelectSingleNode("//descripcion");

            int codigo = int.Parse(codigoNode.InnerText);
            string descripcion = descripcionNode.InnerText;

            return (codigo, descripcion);
        }
        catch
        {
            //si falla, intentar parsear como JSON
            try
            {
                dynamic respuestaJson = JsonConvert.DeserializeObject(respuestaOrquestador);
                int codigo = respuestaJson.codigo;
                string descripcion = respuestaJson.descripcion;

                return (codigo, descripcion);
            }
            catch
            {
                // Si falla, retornar código -1 y la respuesta original
                return (-1, "Respuesta inválida del Orquestador");
            }
        }
    }

    private string EnviarTramaOrquestador(string tramaXML)
    {
        //leer la IP y puerto del Orquestador desde la configuración
        var config = ConfigManager.LeerConfiguracion("Config.ini");
        string ipOrquestador = config["Orquestador.IP"];
        int portOrquestador = int.Parse(config["Orquestador.Port"]);

        this.tramaEnviadaAlOrquestador = tramaXML;

        using (TcpClient client = new TcpClient(ipOrquestador, portOrquestador))
        {
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.ASCII.GetBytes(tramaXML);
            stream.Write(data, 0, data.Length);

            //leer la respuesta del Orquestador
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string respuesta = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            this.respuestaDelOrquestador = respuesta;

            return respuesta;
        }
    }
}
