package com.pagosmoviles.dto;


import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "saldoRequest", namespace = "http://pagosmoviles.com/wsusuarios")
public class SaldoRequest {
    private String telefono;

    @XmlElement(name = "telefono", namespace = "http://pagosmoviles.com/wsusuarios")
    public String getTelefono() {
        return telefono;
    }

    public void setTelefono(String telefono) {
        this.telefono = telefono;

    }
}