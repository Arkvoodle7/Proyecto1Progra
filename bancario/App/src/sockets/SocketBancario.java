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
            // Verificar si la conexión ha sido aceptada
            System.out.println("Conexión aceptada desde el Orquestador");

            // Leer la trama recibida desde el Orquestador
            String tramaRecibida = in.readLine();
            System.out.println("Trama recibida del Orquestador: " + tramaRecibida);
            if (tramaRecibida == null) {
                System.out.println("No se recibió ninguna trama. Cerrando conexión.");
                return;
            }

            // Parsear y validar trama
            String[] partes = tramaRecibida.split("\\|");

            String respuesta;

            if (partes.length == 2) {
                // Solicitud de saldo
                String identificacion = partes[0];
                String numeroCuenta = partes[1];

                System.out.println("Procesando consulta de saldo para identificación: " + identificacion + ", cuenta: " + numeroCuenta);

                respuesta = transaccionService.consultarSaldo(identificacion, numeroCuenta);

                System.out.println("Respuesta generada: " + respuesta);

            } else if (partes.length == 4) {
                // Procesar transacción
                TransaccionDto transaccionDTO = new TransaccionDto();
                transaccionDTO.setIdentificacion(partes[0]);
                transaccionDTO.setNumeroCuenta(partes[1]);
                transaccionDTO.setMonto(Double.parseDouble(partes[2]));
                transaccionDTO.setTipo(partes[3]);

                if (transaccionDTO.getTipo().equals("CRE") || transaccionDTO.getTipo().equals("DEB")) {
                    System.out.println("Procesando transacción de tipo: " + transaccionDTO.getTipo());
                    respuesta = transaccionService.aplicarTransaccion(transaccionDTO);
                } else {
                    respuesta = "ERROR|Tipo de transacción inválido";
                }

                System.out.println("Respuesta generada: " + respuesta);
            } else {
                respuesta = "ERROR|Trama inválida";
                System.out.println("Trama inválida recibida.");
            }

            // Enviar la respuesta de nuevo al Orquestador
            System.out.println("Enviando respuesta al Orquestador: " + respuesta);
            out.write(respuesta);
            out.newLine();
            out.flush();
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            try {
                socket.close();
                System.out.println("Conexión cerrada con el Orquestador");
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
}
