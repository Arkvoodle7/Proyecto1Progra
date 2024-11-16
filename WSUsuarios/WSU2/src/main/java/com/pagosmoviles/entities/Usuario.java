package com.pagosmoviles.entities;

import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlRootElement;
import com.fasterxml.jackson.dataformat.xml.annotation.JacksonXmlProperty;

@JacksonXmlRootElement(localName = "usuario")
public class Usuario {

    @JacksonXmlProperty(localName = "identificacion")
    private String identificacion;

    @JacksonXmlProperty(localName = "nombreUsuario")
    private String nombreUsuario;

    @JacksonXmlProperty(localName = "nombreCompleto")
    private String nombreCompleto;

    @JacksonXmlProperty(localName = "contrasena")
    private String contrasena;

    @JacksonXmlProperty(localName = "telefono")
    private String telefono;

    public String getIdentificacion() {
        return identificacion;
    }

    public void setIdentificacion(String identificacion) {
        this.identificacion = identificacion;
    }

    public String getNombreUsuario() {
        return nombreUsuario;
    }

    public void setNombreUsuario(String nombreUsuario) {
        this.nombreUsuario = nombreUsuario;
    }

    public String getNombreCompleto() {
        return nombreCompleto;
    }

    public void setNombreCompleto(String nombreCompleto) {
        this.nombreCompleto = nombreCompleto;
    }

    public String getContrasena() {
        return contrasena;
    }

    public void setContrasena(String contrasena) {
        this.contrasena = contrasena;
    }

    public String getTelefono() {
        return telefono;
    }

    public void setTelefono(String telefono) {
        this.telefono = telefono;
    }
}