using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

class ComunicacionHandler
{
    public static void Enviar()
    {
        try
        {
            //leer la IP y el puerto del SocketExterno desde el Config.ini
            var config = ConfigManager.LeerConfiguracion("Config.ini");
            string ipSocketExterno = config["SocketExterno.IP"];
            int portSocketExterno = int.Parse(config["SocketExterno.Port"]);

            //trama de prueba para enviar (cambiar cuando se conecte a orquestador)
            string trama = "{\"telefono\":12345678, \"monto\":1000, \"descripcion\":\"Pago externo de prueba\"}";

            //crear conexion TCP al SocketExterno
            TcpClient client = new TcpClient(ipSocketExterno, portSocketExterno);
            NetworkStream stream = client.GetStream();

            //enviar la trama
            byte[] dataToSend = Encoding.ASCII.GetBytes(trama);
            stream.Write(dataToSend, 0, dataToSend.Length);

            //recibir la respuesta del SocketExterno
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string respuesta = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            //mostrar la respuesta en la consola
            Console.WriteLine($"Respuesta del SocketExterno: {respuesta}");

            //cerrar la conexion
            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al enviar la trama: {ex.Message}");
        }
    }
}
