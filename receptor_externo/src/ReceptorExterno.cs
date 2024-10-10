using System;
using System.Net;
using System.Net.Sockets;

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
            Thread clientThread = new Thread(() => ManejarCliente(client));
            clientThread.Start();
        }
    }

    static void ManejarCliente(TcpClient client)
    {
        TransaccionHandler handler = new TransaccionHandler();
        handler.ManejarCliente(client);
    }
}
