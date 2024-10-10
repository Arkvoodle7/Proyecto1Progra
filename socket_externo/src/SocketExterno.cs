using System;
using System.Net;
using System.Net.Sockets;

class SocketExterno
{
    static void Main(string[] args)
    {
        //cargar la configuracion desde el archivo .ini
        var config = LeerConfiguracion("Config.ini");
        string ipSocketExterno = config["SocketExterno.IP"];
        int portSocketExterno = int.Parse(config["SocketExterno.Port"]);

        //iniciar el socket
        IPAddress ip = IPAddress.Parse(ipSocketExterno);
        TcpListener server = new TcpListener(ip, portSocketExterno);
        server.Start();
        Console.WriteLine($"Socket externo corriendo en {ipSocketExterno}:{portSocketExterno}");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Thread clientThread = new Thread(() => ManejarCliente(client));
            clientThread.Start();
        }
    }

    static void ManejarCliente(TcpClient client)
    {
        TransaccionHandlerSE handler = new TransaccionHandlerSE();
        handler.ManejarCliente(client);
    }
}
