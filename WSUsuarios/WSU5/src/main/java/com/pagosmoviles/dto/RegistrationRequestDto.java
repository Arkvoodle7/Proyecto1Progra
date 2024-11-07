package com.pagosmoviles.dto;

import jakarta.xml.bind.annotation.XmlRootElement;

@XmlRootElement
public class RegistrationRequestDto {
    private String cuenta;
    private String identificacion;
    private String telefono;

    // Getters and setters
    public String getCuenta() {
        return cuenta;
    }

    public void setCuenta(String cuenta) {
        this.cuenta = cuenta;
    }

    public String getIdentificacion() {
        return identificacion;
    }

    public void setIdentificacion(String identificacion) {
        this.identificacion = identificacion;
    }

    public String getTelefono() {
        return telefono;
    }

    public void setTelefono(String telefono) {
        this.telefono = telefono;
    }
}
