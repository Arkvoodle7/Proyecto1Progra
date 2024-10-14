using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class ReceptorExterno
{
    static void Main(string[] args)
    {
        var config = ConfigManager.LeerConfiguracion("Config.ini");
        string ipReceptorExterno = config["ReceptorExterno.IP"];
        int portOrquestador = int.Parse(config["ReceptorExterno.PortOrquestador"]);
        int portSimulador = int.Parse(config["ReceptorExterno.PortSimulador"]);

        IPAddress ip = IPAddress.Parse(ipReceptorExterno);

        // Crear TcpListeners para cada puerto
        TcpListener listenerOrquestador = new TcpListener(ip, portOrquestador);
        TcpListener listenerSimulador = new TcpListener(ip, portSimulador);

        listenerOrquestador.Start();
        listenerSimulador.Start();

        Console.WriteLine($"Receptor externo corriendo en {ipReceptorExterno}:{portOrquestador} y {ipReceptorExterno}:{portSimulador}");

        // Iniciar tareas para aceptar conexiones en ambos puertos
        Task.Run(() => AceptarConexiones(listenerOrquestador, ManejarClienteDesdeOrquestador));
        Task.Run(() => AceptarConexiones(listenerSimulador, ManejarClienteDesdeSimuladorOtroBanco));

        // Prevenir que el programa termine
        Console.ReadLine();
    }

    static void AceptarConexiones(TcpListener listener, Action<TcpClient> manejador)
    {
        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Thread clientThread = new Thread(() => manejador(client));
            clientThread.Start();
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
}
