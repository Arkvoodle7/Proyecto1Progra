package com.pagosmoviles.dto;

import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlRootElement;

@JacksonXmlRootElement(localName = "authRequestDto")
public class AuthRequestDto {

    @JacksonXmlProperty(localName = "nombreUsuario")
    private String nombreUsuario;

    @JacksonXmlProperty(localName = "contrasena")
    private String contrasena;

    // Constructor por defecto necesario para la deserialización
    public AuthRequestDto() {
    }

    public String getNombreUsuario() {
        return nombreUsuario;
    }

    public void setNombreUsuario(String nombreUsuario) {
        this.nombreUsuario = nombreUsuario;
    }

    public String getContrasena() {
        return contrasena;
    }

    public void setContrasena(String contrasena) {
        this.contrasena = contrasena;
    }
}
