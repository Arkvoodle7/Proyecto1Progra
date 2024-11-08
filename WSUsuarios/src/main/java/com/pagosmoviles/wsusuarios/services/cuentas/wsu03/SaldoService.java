package com.pagosmoviles.wsusuarios.services.cuentas.wsu03;

import com.pagosmoviles.wsusuarios.dto.cuentas.wsu03.SaldoRequest;
import com.pagosmoviles.wsusuarios.dto.cuentas.wsu03.SaldoResponse;
import com.pagosmoviles.wsusuarios.util.SocketClient;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.ws.server.endpoint.annotation.Endpoint;
import org.springframework.ws.server.endpoint.annotation.PayloadRoot;
import org.springframework.ws.server.endpoint.annotation.RequestPayload;
import org.springframework.ws.server.endpoint.annotation.ResponsePayload;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import java.io.ByteArrayInputStream;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@Endpoint
public class SaldoService {
    private static final String NAMESPACE_URI = "http://pagosmoviles.com/wsusuarios";
    private static final Logger logger = LoggerFactory.getLogger(SaldoService.class);

    private final SocketClient socketClient;

    @Autowired
    public SaldoService(SocketClient socketClient) {
        this.socketClient = socketClient;
    }

    @PayloadRoot(namespace = NAMESPACE_URI, localPart = "saldoRequest")
    @ResponsePayload
    public SaldoResponse consultarSaldo(@RequestPayload SaldoRequest request) {
        // Crear la trama XML para enviar al socket
        String message = "<saldo><telefono>" + request.getTelefono() + "</telefono></saldo>";
        logger.info("Enviando mensaje al socket: {}", message);

        // Enviar la solicitud al socket y recibir la respuesta
        String responseXml = socketClient.sendMessage(message);
        logger.info("Respuesta recibida del socket: {}", responseXml);

        // Crear la respuesta SOAP
        SaldoResponse response = new SaldoResponse();

        try {
            // Parsear la respuesta XML
            DocumentBuilder builder = DocumentBuilderFactory.newInstance().newDocumentBuilder();
            Document doc = builder.parse(new ByteArrayInputStream(responseXml.getBytes()));
            Element root = doc.getDocumentElement();

            // Obtener y establecer el código y la descripción
            int codigo = Integer.parseInt(root.getElementsByTagName("codigo").item(0).getTextContent());
            response.setCodigo(codigo);

            // Si el código es 0, se obtiene el saldo
            if (codigo == 0) {
                double saldo = Double.parseDouble(root.getElementsByTagName("saldo").item(0).getTextContent());
                response.setSaldo(saldo);
                response.setDescripcion("Operación exitosa");
            } else {
                String descripcion = root.getElementsByTagName("descripcion").item(0).getTextContent();
                response.setDescripcion(descripcion);
            }

        } catch (Exception e) {
            // Manejo de errores en el parsing de la respuesta
            logger.error("Error procesando la respuesta del socket", e);
            response.setCodigo(-1);
            response.setDescripcion("Error de comunicación o formato incorrecto en la respuesta");
        }

        return response;
    }
}
