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
            //leer la IP y el puerto del Socket Externo desde el archivo de configuracion
            var config = ConfigManager.LeerConfiguracion("Config.ini");
            string ipSocketExterno = config["SocketExterno.IP"];
            int portSocketExterno = int.Parse(config["SocketExterno.Port"]);

            //validar la trama recibida del Orquestador
            dynamic transaccion = Newtonsoft.Json.JsonConvert.DeserializeObject(tramaRecibida);

            //validar el telefono
            string telefono = transaccion.telefono;
            if (telefono.Length != 8 || !telefono.All(char.IsDigit))
            {
                return "{\"codigo\":-1, \"descripcion\":\"Debe enviar los datos completos y válidos\"}";
            }

            //validar el monto
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

            //validar la descripcion
            string descripcion = transaccion.descripcion;
            if (descripcion.Length > 25)
            {
                return "{\"codigo\":-1, \"descripcion\":\"La descripción no puede superar 25 caracteres\"}";
            }

            //generar la trama JSON para el Socket Externo
            string tramaJSON = $"{{\"telefono\":\"{telefono}\",\"monto\":{monto},\"descripcion\":\"{descripcion}\"}}";

            //conectar al Socket Externo
            TcpClient client = new TcpClient(ipSocketExterno, portSocketExterno);
            NetworkStream stream = client.GetStream();

            //enviar la trama al Socket Externo
            byte[] dataToSend = Encoding.ASCII.GetBytes(tramaJSON);
            stream.Write(dataToSend, 0, dataToSend.Length);

            //recibir la respuesta del Socket Externo
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string respuesta = Encoding.ASCII.GetString(buffer, 0, bytesRead);

            //cerrar la conexion
            client.Close();

            //registrar en la bitáaora del Receptor Externo
            BitacoraHandler.RegistrarBitacora(tramaRecibida, respuesta);

            return respuesta;  //retornar la respuesta al Orquestador
        }
        catch (Exception ex)
        {
            return "{\"codigo\": -1, \"descripcion\": \"Error al conectar con el Socket Externo: " + ex.Message + "\"}";
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
