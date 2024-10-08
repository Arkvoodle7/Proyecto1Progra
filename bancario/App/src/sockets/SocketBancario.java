package sockets;

import config.ConfigProps;
import dto.TransaccionDto;
import interfaces.ITransaccion;

import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;

public class SocketBancario {
    private final ITransaccion transaccionService;
    private final int socketPort; //  puerto en el que escuchará el socket

    public SocketBancario(ITransaccion transaccionService) {
        this.transaccionService = transaccionService;
        this.socketPort = Integer.parseInt(ConfigProps.getProp("socket_port"));
    }

    public void start() {
        try (ServerSocket serverSocket = new ServerSocket(socketPort)) {
            System.out.println("Socket Bancario escuchando en el puerto: " + socketPort);

            // Mantiene el servidor escuchando indefinidamente
            while (true) {
                Socket socket = serverSocket.accept();
                System.out.println("Cliente conectado");

                // Manejar la conexión en un hilo independiente
                new Thread(() -> manejarConexion(socket)).start();
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    //M
    private void manejarConexion(Socket socket) {
        try (
                BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
                BufferedWriter out = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()))
        ) {
            // Lee la trama recibida desde el cliente
            String tramaRecibida = in.readLine();
            System.out.println("Trama recibida: " + tramaRecibida);

            // Parsear la trama (se espera en el formato: "Identificacion|Cuenta|Monto|Tipo")
            String[] partes = tramaRecibida.split("\\|");
            if (partes.length == 4) {
                TransaccionDto transaccion = new TransaccionDto();
                transaccion.setIdentificacion(partes[0]);
                transaccion.setNumeroCuenta(partes[1]);
                transaccion.setMonto(Double.parseDouble(partes[2]));
                transaccion.setTipo(partes[3]);

                // Procesar la transacción
                String respuesta;
                if (transaccion.getTipo().equals("DEB") || transaccion.getTipo().equals("CRE")) {
                    respuesta = transaccionService.aplicarTransaccion(transaccion);
                } else {
                    respuesta = "ERROR|Tipo de transacción inválido";
                }

                // Enviar respuesta al cliente
                out.write(respuesta);
                out.newLine();
                out.flush();
                System.out.println("Respuesta enviada: " + respuesta);
            } else {
                out.write("ERROR|Trama inválida");
                out.newLine();
                out.flush();
                System.out.println("Trama inválida recibida");
            }

        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            try {
                socket.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}
