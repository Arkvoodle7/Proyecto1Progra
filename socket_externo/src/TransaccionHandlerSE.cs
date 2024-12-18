﻿using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using System.Threading;

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

            //respuesta en formato JSON
            string tramaRespuesta = "{\"codigo\":0,\"descripcion\":\"Transacción procesada\"}";

            //enviar la respuesta al Receptor Externo
            byte[] respuestaBytes = Encoding.ASCII.GetBytes(tramaRespuesta);
            stream.Write(respuestaBytes, 0, respuestaBytes.Length);
            Console.WriteLine($"Respuesta enviada al Receptor Externo: {tramaRespuesta}");

            //registrar en la bitacora
            Thread bitacoraThread = new Thread(() => BitacoraHandlerSE.RegistrarBitacora(tramaRecibida, tramaRespuesta));
            bitacoraThread.Start();

            //cerrar la conexion
            client.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
