package com.pagosmoviles.dto;

import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlRootElement;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;

@JacksonXmlRootElement(localName = "authResponse")
public class AuthResponse {

    @JacksonXmlProperty(localName = "resultado")
    private int resultado;

    @JacksonXmlProperty(localName = "mensaje")
    private String mensaje;

    public AuthResponse() {
    }

    public AuthResponse(int resultado, String mensaje) {
        this.resultado = resultado;
        this.mensaje = mensaje;
    }

    public int getResultado() {
        return resultado;
    }

    public void setResultado(int resultado) {
        this.resultado = resultado;
    }

    public String getMensaje() {
        return mensaje;
    }

    public void setMensaje(String mensaje) {
        this.mensaje = mensaje;
    }
}
