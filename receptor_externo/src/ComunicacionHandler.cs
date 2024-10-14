using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

class ComunicacionHandler
{
    public static string Enviar(string tramaRecibida)
    {
        try
        {
            // Leer la IP y el puerto del Socket Externo desde el archivo de configuración
            var config = ConfigManager.LeerConfiguracion("Config.ini");
            string ipSocketExterno = config["SocketExterno.IP"];
            int portSocketExterno = int.Parse(config["SocketExterno.Port"]);

            // Validar la trama recibida del Orquestador
            dynamic transaccion = Newtonsoft.Json.JsonConvert.DeserializeObject(tramaRecibida);

            // Validar el teléfono
            string telefono = transaccion.telefono;
            if (telefono.Length != 8 || !telefono.All(char.IsDigit))
            {
                return "{\"codigo\":-1, \"descripcion\":\"Debe enviar los datos completos y válidos\"}";
            }

            // Validar el monto
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

            // Validar la descripción
            string descripcion = transaccion.descripcion;
            if (descripcion.Length > 25)
            {
                return "{\"codigo\":-1, \"descripcion\":\"La descripción no puede superar 25 caracteres\"}";
            }

            // Generar la trama JSON para el Socket Externo
            string tramaJSON = $"{{\"telefono\":\"{telefono}\",\"monto\":{monto},\"descripcion\":\"{descripcion}\"}}";

            // Conectar al Socket Externo
            TcpClient client = new TcpClient(ipSocketExterno, portSocketExterno);

            // Configurar timeout de 20 segundos (20000 milisegundos)
            client.SendTimeout = 20000;  // Timeout para enviar datos
            client.ReceiveTimeout = 20000;  // Timeout para recibir datos

            NetworkStream stream = client.GetStream();

            // Enviar la trama al Socket Externo
            byte[] dataToSend = Encoding.ASCII.GetBytes(tramaJSON);
            stream.Write(dataToSend, 0, dataToSend.Length);

            // Recibir la respuesta del Socket Externo
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string respuesta = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            // Cerrar la conexión
            client.Close();

            // Registrar en la bitácora del Receptor Externo
            BitacoraHandler.RegistrarBitacora(tramaRecibida, respuesta);

            return respuesta;  // Retornar la respuesta al Orquestador
        }
        catch (SocketException ex)
        {
            return "{\"codigo\": -1, \"descripcion\": \"Error en el Socket Externo: " + ex.Message + "\"}";
        }
        catch (IOException ex)
        {
            return "{\"codigo\": -1, \"descripcion\": \"Error de comunicación: " + ex.Message + "\"}";
        }
    }


    public void RecibirTramaDesdeOrquestador(TcpClient orquestadorClient)
    {
        try
        {
            //obtener el stream de la conexion con el Orquestador
            NetworkStream stream = orquestadorClient.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string tramaRecibida = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            Console.WriteLine($"Trama recibida del Orquestador: {tramaRecibida}");

            //validar la trama recibida
            string respuesta = Enviar(tramaRecibida);  //llamar al método 'Enviar' que procesa y envia la trama al Socket Externo

            //enviar la respuesta de vuelta al Orquestador
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuesta);
            stream.Write(respuestaBytes, 0, respuestaBytes.Length);

            //cerrar la conexión
            orquestadorClient.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al procesar la trama desde el Orquestador: {ex.Message}");
        }
    }
}
