using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.Threading;

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

            //configurar timeout de 20 segundos (20000 milisegundos)
            client.SendTimeout = 20000;  //timeout para enviar datos
            client.ReceiveTimeout = 20000;  //timeout para recibir datos

            NetworkStream stream = client.GetStream();

            //enviar la trama al Socket Externo
            byte[] dataToSend = Encoding.ASCII.GetBytes(tramaJSON);
            stream.Write(dataToSend, 0, dataToSend.Length);
            Console.WriteLine($"Trama enviada al Socket Externo: {tramaJSON}");

            //recibir la respuesta del Socket Externo
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string respuesta = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Respuesta recibida del Socket Externo: {respuesta}");

            //cerrar la conexion
            client.Close();

            //registrar en la bitacora del Receptor Externo
            Thread bitacoraThread = new Thread(() => BitacoraHandler.RegistrarBitacora(tramaRecibida, respuesta));
            bitacoraThread.Start();

            return respuesta;
        }
        catch (SocketException ex)
        {
            return ex.Message;
        }
        catch (IOException ex)
        {
            return ex.Message;
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

            //mostrar la trama recibida desde el Orquestador
            Console.WriteLine($"Trama recibida de Orquestador: {tramaRecibida}");

            //generar la respuesta
            string respuesta = Enviar(tramaRecibida);

            //convertir la respuesta JSON a XML
            dynamic respuestaObj = JsonConvert.DeserializeObject(respuesta);
            string codigo = respuestaObj.codigo.ToString();
            string descripcionRespuesta = respuestaObj.descripcion.ToString();

            string respuestaXML = $"<respuesta><codigo>{codigo}</codigo><descripcion>{descripcionRespuesta}</descripcion></respuesta>";

            //enviar la respuesta al Orquestador
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(respuestaXML);
            stream.Write(respuestaBytes, 0, respuestaBytes.Length);

            //mostrar la respuesta enviada al Orquestador
            Console.WriteLine($"Respuesta enviada al Orquestador: {respuestaXML}");

            //cerrar la conexion
            orquestadorClient.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
