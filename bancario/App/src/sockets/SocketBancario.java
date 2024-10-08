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
            System.out.println("Socket Bancario escuchando en el puerto " + socketPort);

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

    private void manejarConexion(Socket socket) {
        try (
                BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()));
                BufferedWriter out = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()))
        ) {
            // Leer la trama recibida desde el cliente
            String tramaRecibida = in.readLine();
            System.out.println("Trama recibida: " + tramaRecibida);

            // Parsear la trama (por simplicidad, se espera en el formato: "Identificacion|Cuenta|Monto|Tipo")
            String[] partes = tramaRecibida.split("\\|");
            if (partes.length == 4) {
                TransaccionDto transaccionDTO = new TransaccionDto();
                transaccionDTO.setIdentificacion(partes[0]);
                transaccionDTO.setNumeroCuenta(partes[1]);
                transaccionDTO.setMonto(Double.parseDouble(partes[2]));
                transaccionDTO.setTipo(partes[3]);

                // Procesar la transacción
                String respuesta;
                if (transaccionDTO.getTipo().equals("DEB") || transaccionDTO.getTipo().equals("CRE")) {
                    respuesta = transaccionService.aplicarTransaccion(transaccionDTO);
                } else {
                    respuesta = "ERROR|Tipo de transacción inválido";
                }

                // Enviar la respuesta al cliente
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
