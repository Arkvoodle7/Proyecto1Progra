using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

class SocketExterno
{
    static void Main(string[] args)
    {
        //cargar la configuracion desde el archivo .ini
        var config = LeerConfiguracion("Config.ini");
        string ipReceptorExterno = config["IP"];
        int portReceptorExterno = int.Parse(config["Port"]);

        //iniciar el socket
        IPAddress ip = IPAddress.Parse(ipReceptorExterno);
        TcpListener server = new TcpListener(ip, portReceptorExterno);
        server.Start();
        Console.WriteLine($"Socket externo corriendo en {ipReceptorExterno}:{portReceptorExterno}");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Thread clientThread = new Thread(() => ManejarCliente(client));
            clientThread.Start();
        }

        static void ManejarCliente(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string tramaRecibida = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);

            //procesar la transaccion y generar la respuesta
            string tramaRespuesta = RecibirTransaccion(tramaRecibida);
            byte[] response = System.Text.Encoding.ASCII.GetBytes(tramaRespuesta);
            stream.Write(response, 0, response.Length);

            //imprimir la trama recibida y la respuesta
            Console.WriteLine($"Trama recibida: {tramaRecibida}");
            Console.WriteLine($"Respuesta enviada: {tramaRespuesta}");

            //registrar en la bitacora en un hilo independiente
            Thread bitacoraThread = new Thread(() => RegistrarBitacora(tramaRecibida, tramaRespuesta));
            bitacoraThread.Start();

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
    static string RecibirTransaccion(string trama)
    {
        //convertir la trama JSON a un objeto
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

        //si todo es correcto (cambiar cuando se conecte a orquestador)
        return "{\"codigo\":0, \"descripcion\":\"Transacción aplicada\"}";
    }

    static string GenerarXMLTransaccion(string telefono, decimal monto, string descripcion)
    {
        return $"<transaccion><telefono>{telefono}</telefono><monto>{monto}</monto><descripcion>{descripcion}</descripcion></transaccion>";
    }

    static void RegistrarBitacora(string tramaRecibida, string tramaRespuesta)
    {
        string connectionString = "server=localhost;user=root;password=Daniel2510*;database=PagosMovilesSocketExterno";

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();

            string query = "INSERT INTO Bitacora (fecha_hora, trama_recibida, trama_respuesta) VALUES (@fecha_hora, @trama_recibida, @trama_respuesta)";

            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@fecha_hora", DateTime.Now);
                cmd.Parameters.AddWithValue("@trama_recibida", tramaRecibida);
                cmd.Parameters.AddWithValue("@trama_respuesta", tramaRespuesta);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
