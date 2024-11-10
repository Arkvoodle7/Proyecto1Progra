package com.pagosmoviles.wsusuarios.util;

import org.w3c.dom.Document;
import org.w3c.dom.Element;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import java.io.ByteArrayInputStream;

public class ParserXML {

    public static Element parseXmlResponse(String xmlResponse) throws Exception {
        DocumentBuilder builder = DocumentBuilderFactory.newInstance().newDocumentBuilder();

        // parsear xml desde el string
        Document doc = builder.parse(new ByteArrayInputStream(xmlResponse.getBytes()));

        return doc.getDocumentElement();

    }

}
