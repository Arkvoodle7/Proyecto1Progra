using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class ReceptorExterno
{
    static void Main(string[] args)
    {
        //cargar la configuracion desde el archivo .ini
        var config = LeerConfiguracion("Config.ini");
        string ipAddress = config["IP"];
        int port = int.Parse(config["Port"]);

        //iniciar el socket
        IPAddress ip = IPAddress.Parse(ipAddress);
        TcpListener server = new TcpListener(ip, port);
        server.Start();
        Console.WriteLine($"Socket Receptor externo corriendo en {ipAddress}:{port}");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            //leer la trama recibida
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string tramaRecibida = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Trama recibida: {tramaRecibida}");

            //realizar la validacion de los datos recibidos
            string tramaRespuesta = ProcesarTransaccion(tramaRecibida);

            //enviar la respuesta al cliente
            byte[] response = System.Text.Encoding.ASCII.GetBytes(tramaRespuesta);
            stream.Write(response, 0, response.Length);

            client.Close();
        }
    }

    //metodo para leer el archivo .ini manualmente
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

    //metodo para procesar la transaccion
    static string ProcesarTransaccion(string trama)
    {
        //validacion del teléfono, monto, descripción y para retornar la respuesta necesaria
        return "{\"codigo\":0, \"descripcion\":\"Transacción aplicada\"}";
    }
}
