package com.pagosmoviles.wsusuarios.util;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import java.io.*;
import java.net.Socket;
import java.net.SocketTimeoutException;

@Component
public class SocketClient {

    @Value("${socket.host}")
    private String host;

    @Value("${socket.port}")
    private int port;

    private static final int TIMEOUT = 10000; // 10 segundos de tiempo de espera

    public String sendMessage(String message) {
        try (Socket socket = new Socket(host, port)) {
            // Configurar el tiempo de espera para evitar que se bloquee indefinidamente
            socket.setSoTimeout(TIMEOUT);

            // Enviar el mensaje al socket
            try (PrintWriter out = new PrintWriter(socket.getOutputStream(), true);
                 BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()))) {

                out.println(message);

                // Leer la respuesta desde el socket
                StringBuilder responseBuilder = new StringBuilder();
                String line;
                while ((line = in.readLine()) != null) {
                    responseBuilder.append(line);
                }

                return responseBuilder.toString();
            }

        } catch (SocketTimeoutException e) {
            // Manejar el caso en que el tiempo de espera del socket se agote
            return "<respuesta><codigo>-1</codigo><descripcion>Tiempo de espera agotado en el socket</descripcion></respuesta>";
        } catch (IOException e) {
            // Manejar otros errores de comunicación
            return "<respuesta><codigo>-1</codigo><descripcion>Error de comunicación</descripcion></respuesta>";
        }
    }
}
