using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

class TransaccionHandlerSE
{
    public void ManejarCliente(TcpClient client)
    {
        try
        {
            //leer la trama recibida del Receptor Externo
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string tramaRecibida = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"Trama recibida del Receptor Externo: {tramaRecibida}");

            //respuesta despues de recibir la trama
            string tramaRespuesta = "{\"codigo\": 0, \"descripcion\": \"Transacción procesada en el Socket Externo\"}";

            //enviar la respuesta de vuelta al Receptor Externo
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(tramaRespuesta);
            stream.Write(respuestaBytes, 0, respuestaBytes.Length);

            //registrar en la bitacora
            BitacoraHandlerSE.RegistrarBitacora(tramaRecibida, tramaRespuesta);

            //cerrar la conexión
            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al procesar la trama del Receptor Externo: {ex.Message}");
        }
    }
}
