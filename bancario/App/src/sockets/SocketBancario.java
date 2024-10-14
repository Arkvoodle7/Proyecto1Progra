package sockets;

import config.ConfigProps;
import dto.TransaccionDto;
import interfaces.ITransaccion;

import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;

public class SocketBancario {
    private final ITransaccion transaccionService;
    private final int socketPort; //puerto en el que escuchara el socket

    public SocketBancario(ITransaccion transaccionService) {
        this.transaccionService = transaccionService;
        this.socketPort = Integer.parseInt(ConfigProps.getProp("socket_port"));
    }

    public void start() {
        try (ServerSocket serverSocket = new ServerSocket(socketPort)) {
            System.out.println("Bancario escuchando en " + socketPort);

            //mantiene el servidor escuchando indefinidamente
            while (true) {
                Socket socket = serverSocket.accept();

                //manejar la conexion en un hilo independiente
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
            //leer la trama recibida desde el Orquestador
            String tramaRecibida = in.readLine();
            System.out.println("Trama recibida de Orquestador: " + tramaRecibida);
            if (tramaRecibida == null) {
                return;
            }

            //parsear y validar trama
            String[] partes = tramaRecibida.split("\\|");

            String respuesta;

            if (partes.length == 2) {
                //solicitud de saldo
                String identificacion = partes[0];
                String numeroCuenta = partes[1];

                respuesta = transaccionService.consultarSaldo(identificacion, numeroCuenta);

                System.out.println("Respuesta enviada a Orquestador: " + respuesta);

            } else if (partes.length == 4) {
                //procesar transaccion
                TransaccionDto transaccionDTO = new TransaccionDto();
                transaccionDTO.setIdentificacion(partes[0]);
                transaccionDTO.setNumeroCuenta(partes[1]);
                transaccionDTO.setMonto(Double.parseDouble(partes[2]));
                transaccionDTO.setTipo(partes[3]);

                if (transaccionDTO.getTipo().equals("CRE") || transaccionDTO.getTipo().equals("DEB")) {
                    respuesta = transaccionService.aplicarTransaccion(transaccionDTO);
                } else {
                    respuesta = "ERROR|Tipo de transacción inválido";
                }

                System.out.println("Respuesta enviada a Orquestador: " + respuesta);
            } else {
                respuesta = "ERROR|Trama inválida";
                System.out.println(respuesta);
            }

            //enviar la respuesta de nuevo al Orquestador
            out.write(respuesta);
            out.newLine();
            out.flush();
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
