using System;
using System.Net;
using System.Net.Sockets;

class SocketExterno
{
    static void Main(string[] args)
    {
        var config = ConfigManagerSE.LeerConfiguracion("Config.ini");
        string ipSocketExterno = config["SocketExterno.IP"];
        int portSocketExterno = int.Parse(config["SocketExterno.Port"]);

        IPAddress ip = IPAddress.Parse(ipSocketExterno);
        TcpListener server = new TcpListener(ip, portSocketExterno);
        server.Start();
        Console.WriteLine($"Socket Externo escuchando en {portSocketExterno}");

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
