package com.pagosmoviles.services;

import com.pagosmoviles.dto.DesinscripcionRequestDto;
import com.pagosmoviles.dto.DesinscripcionResponseDto;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import java.io.*;
import java.net.InetSocketAddress;
import java.net.Socket;

@Service
public class DesinscripcionService {

    @Value("${socket.host:localhost}")
    private String socketHost;

    @Value("${socket.port:8080}")
    private int socketPort;

    public DesinscripcionResponseDto desinscribirUsuario(DesinscripcionRequestDto request) {
        String xmlMessage = buildXmlMessage(request);
        System.out.println("Mensaje XML a enviar: " + xmlMessage);

        try {
            String socketResponse = sendMessageOverSocket(xmlMessage);
            System.out.println("Respuesta recibida del socket: " + socketResponse);
            return parseSocketResponse(socketResponse);
        } catch (IOException e) {
            System.err.println("Error de comunicación con el socket: " + e.getMessage());
            return new DesinscripcionResponseDto(-1, "Error de comunicación");
        }
    }

    private String buildXmlMessage(DesinscripcionRequestDto request) {
        return "<desinscripcion>" +
                "<cuenta>" + request.getCuenta() + "</cuenta>" +
                "<identificacion>" + request.getIdentificacion() + "</identificacion>" +
                "<telefono>" + request.getTelefono() + "</telefono>" +
                "</desinscripcion>";
    }

    private String sendMessageOverSocket(String message) throws IOException {
        try (Socket socket = new Socket()) {
            socket.connect(new InetSocketAddress(socketHost, socketPort), 5000);

            PrintWriter out = new PrintWriter(socket.getOutputStream(), true);
            BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()));

            out.println(message);

            StringBuilder response = new StringBuilder();
            String line;
            while ((line = in.readLine()) != null) {
                response.append(line);
            }

            return response.toString();
        }
    }

    private DesinscripcionResponseDto parseSocketResponse(String response) {
        if (response.contains("<codigo>0</codigo>")) {
            String mensaje = extractTagValue(response, "descripcion");
            return new DesinscripcionResponseDto(0, mensaje);
        } else {
            String mensaje = extractTagValue(response, "descripcion");
            return new DesinscripcionResponseDto(-1, mensaje);
        }
    }

    private String extractTagValue(String xml, String tagName) {
        String startTag = "<" + tagName + ">";
        String endTag = "</" + tagName + ">";
        int start = xml.indexOf(startTag);
        int end = xml.indexOf(endTag);
        if (start != -1 && end != -1) {
            return xml.substring(start + startTag.length(), end);
        }
        return "";
    }
}
