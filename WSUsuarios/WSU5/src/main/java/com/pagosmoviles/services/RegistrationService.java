package com.pagosmoviles.services;

import com.pagosmoviles.dto.RegistrationRequestDto;
import com.pagosmoviles.dto.RegistrationResponseDto;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import java.io.*;
import java.net.InetSocketAddress;
import java.net.Socket;

@Service
public class RegistrationService {

    @Value("${socket.host:localhost}")
    private String socketHost;

    @Value("${socket.port:8080}")
    private int socketPort;


    public RegistrationResponseDto registerUser(RegistrationRequestDto request) {
        String xmlMessage = buildXmlMessage(request);
        System.out.println("Mensaje XML a enviar: " + xmlMessage);

        try {
            String socketResponse = sendMessageOverSocket(xmlMessage);
            System.out.println("Respuesta recibida del socket: " + socketResponse);
            return parseSocketResponse(socketResponse);
        } catch (IOException e) {
            System.err.println("Error de comunicación con el socket: " + e.getMessage());
            return new RegistrationResponseDto(-1, "Error de comunicación");
        }
    }


    private String buildXmlMessage(RegistrationRequestDto request) {
        return "<inscripcion>" +
                "<cuenta>" + request.getCuenta() + "</cuenta>" +
                "<identificacion>" + request.getIdentificacion() + "</identificacion>" +
                "<telefono>" + request.getTelefono() + "</telefono>" +
                "</inscripcion>";
    }

    private String sendMessageOverSocket(String message) throws IOException {
        try (Socket socket = new Socket()) {
            // Configura el timeout para la conexión del socket a 5 segundos
            socket.connect(new InetSocketAddress(socketHost, socketPort), 5000);

            PrintWriter out = new PrintWriter(socket.getOutputStream(), true);
            BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()));

            // Envía el mensaje
            out.println(message);

            // Lee la respuesta
            StringBuilder response = new StringBuilder();
            String line;
            while ((line = in.readLine()) != null) {
                response.append(line);
            }

            return response.toString();
        }
    }

    private RegistrationResponseDto parseSocketResponse(String response) {
        if (response.contains("<codigo>0</codigo>")) {
            String mensaje = extractTagValue(response, "descripcion");
            return new RegistrationResponseDto(0, mensaje);
        } else {
            String mensaje = extractTagValue(response, "descripcion");
            return new RegistrationResponseDto(-1, mensaje);
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