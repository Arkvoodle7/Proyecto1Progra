package com.pagosmoviles.wsusuarios.dto.auth;

import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "loginResponse", namespace = "http://pagosmoviles.com/wsusuarios")
public class LoginResponse {

    private String message;
    @XmlElement
    public String getMessage(){
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}

