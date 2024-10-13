using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class ReceptorExterno
{
    static void Main(string[] args)
    {
        var config = ConfigManager.LeerConfiguracion("Config.ini");
        string ipReceptorExterno = config["ReceptorExterno.IP"];
        int portReceptorExterno = int.Parse(config["ReceptorExterno.Port"]);

        IPAddress ip = IPAddress.Parse(ipReceptorExterno);
        TcpListener server = new TcpListener(ip, portReceptorExterno);
        server.Start();
        Console.WriteLine($"Receptor externo corriendo en {ipReceptorExterno}:{portReceptorExterno}");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();

            // Determinar si la conexión viene desde el Orquestador o desde el SimuladorOtroBanco basado en el puerto
            if (EsConexionDesdeOrquestador(client))
            {
                Thread clientThread = new Thread(() => ManejarClienteDesdeOrquestador(client));
                clientThread.Start();
            }
            else
            {
                Thread clientThread = new Thread(() => ManejarClienteDesdeSimuladorOtroBanco(client));
                clientThread.Start();
            }
        }
    }

    // Método para manejar tramas desde el Orquestador
    static void ManejarClienteDesdeOrquestador(TcpClient client)
    {
        try
        {
            Console.WriteLine("Procesando transacción desde Orquestador...");
            ComunicacionHandler handler = new ComunicacionHandler();
            handler.RecibirTramaDesdeOrquestador(client);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al manejar la conexión del Orquestador: {ex.Message}");
        }
        finally
        {
            client.Close();
        }
    }

    // Método para manejar tramas desde el SimuladorOtroBanco
    static void ManejarClienteDesdeSimuladorOtroBanco(TcpClient client)
    {
        try
        {
            Console.WriteLine("Procesando transacción desde SimuladorOtroBanco...");
            TransaccionHandler handler = new TransaccionHandler();
            handler.ManejarCliente(client);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al manejar la conexión del SimuladorOtroBanco: {ex.Message}");
        }
        finally
        {
            client.Close();
        }
    }

    // Método para identificar si la conexión viene desde el Orquestador basado en el puerto
    static bool EsConexionDesdeOrquestador(TcpClient client)
    {
        try
        {
            // Identificar si la conexión es desde el puerto del Orquestador (8080)
            return ((IPEndPoint)client.Client.RemoteEndPoint).Port == 8080;
        }
        catch
        {
            return false;
        }
    }
}
