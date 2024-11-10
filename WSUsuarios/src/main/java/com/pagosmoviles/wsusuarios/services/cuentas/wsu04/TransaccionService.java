package com.pagosmoviles.wsusuarios.services.cuentas.wsu04;

import com.pagosmoviles.wsusuarios.dto.cuentas.wsu04.TransaccionRequest;
import com.pagosmoviles.wsusuarios.dto.cuentas.wsu04.TransaccionResponse;
import com.pagosmoviles.wsusuarios.util.ParserXML;
import com.pagosmoviles.wsusuarios.util.SocketClient;
import com.pagosmoviles.wsusuarios.util.ParserXML;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.ws.server.endpoint.annotation.Endpoint;
import org.springframework.ws.server.endpoint.annotation.PayloadRoot;
import org.springframework.ws.server.endpoint.annotation.RequestPayload;
import org.springframework.ws.server.endpoint.annotation.ResponsePayload;
import org.w3c.dom.Element;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

@Endpoint
public class TransaccionService {
    private static final String NAMESPACE_URI = "http://pagosmoviles.com/wsusuarios";
    private static final Logger logger = LoggerFactory.getLogger(TransaccionService.class);

    private final SocketClient socketClient;

    @Autowired
    public TransaccionService(SocketClient socketClient) {
        this.socketClient = socketClient;
    }

    @PayloadRoot(namespace = NAMESPACE_URI, localPart = "transaccionRequest")
    @ResponsePayload
    public TransaccionResponse aplicarTransaccion(@RequestPayload TransaccionRequest request) {
        String transaccionMessage = "<transaccion><telefono>" + request.getTelefono() + "</telefono>"
                + "<monto>" + request.getMonto() + "</monto>"
                + "<descripcion>" + request.getDescripcion() + "</descripcion></transaccion>";
        logger.info("Trama Enviada al Entorno Orquestador {}", transaccionMessage);

        String transaccionResponseXml = socketClient.sendMessage(transaccionMessage);
        logger.info("Trama Recibida del Entorno Orquestador: {}", transaccionResponseXml);

        TransaccionResponse transaccionResponse = new TransaccionResponse();

        try {
            Element root = ParserXML.parseXmlResponse(transaccionResponseXml);

            int codigo = Integer.parseInt(root.getElementsByTagName("codigo").item(0).getTextContent());
            transaccionResponse.setCodigo(codigo);

            if (codigo == 0) {
                transaccionResponse.setDescripcion("Transaccion Aplicada");
            } else {
                String descripcion = root.getElementsByTagName("descripcion").item(0).getTextContent();
                transaccionResponse.setDescripcion(descripcion);
            }

        } catch (Exception e) {
            logger.error("Error procesando la respuesta del socket", e);
            transaccionResponse.setCodigo(-1);
            transaccionResponse.setDescripcion("Error de comunicación o formato incorrecto en la respuesta");
        }

        return transaccionResponse;
    }
}
