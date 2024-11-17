package com.pagosmoviles.dto;

import jakarta.xml.bind.annotation.XmlRootElement;

@XmlRootElement
public class RegistrationResponseDto {
    private int codigo;
    private String mensaje;

    public RegistrationResponseDto() {}

    public RegistrationResponseDto(int codigo, String mensaje) {
        this.codigo = codigo;
        this.mensaje = mensaje;
    }

    // Getters and setters
    public int getCodigo() {
        return codigo;
    }

    public void setCodigo(int codigo) {
        this.codigo = codigo;
    }

    public String getMensaje() {
        return mensaje;
    }

    public void setMensaje(String mensaje) {
        this.mensaje = mensaje;
    }
}
