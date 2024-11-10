package com.pagosmoviles.wsusuarios.util;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.SocketTimeoutException;

@Component
public class SocketClient {

    private static final int TIMEOUT = 10000; // 10 segundos de tiempo de espera
    @Value("${socket.host}")
    private String host;
    @Value("${socket.port}")
    private int port;

    public String sendMessage(String message) {
        try (Socket socket = new Socket(host, port)) {
            //  tiempo de espera para evitar que se bloquee indefinidamente
            socket.setSoTimeout(TIMEOUT);

            // Enviar el mensaje al socket
            try (PrintWriter out = new PrintWriter(socket.getOutputStream(), true);
                 BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()))) {

                out.println(message);


                StringBuilder responseBuilder = new StringBuilder();
                char[] buffer = new char[1024];
                int charsRead;

                // Lee los datos disponibles en el buffer
                while ((charsRead = in.read(buffer)) != -1) {
                    responseBuilder.append(buffer, 0, charsRead);
                    if (in.ready()) { // Verifica si hay más datos disponibles
                        continue;
                    } else {
                        break;
                    }
                }

                return responseBuilder.toString();
            }

        } catch (SocketTimeoutException e) {
            // Manejar el caso en que el tiempo de espera del socket se agote
            return "<respuesta><codigo>-1</codigo><descripcion>Tiempo de espera agotado en el socket</descripcion></respuesta>";
        } catch (IOException e) {

            return "<respuesta><codigo>-1</codigo><descripcion>Error de comunicación</descripcion></respuesta>";
        }
    }
}


