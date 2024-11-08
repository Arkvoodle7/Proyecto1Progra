package com.pagosmoviles.wsusuarios.services.auth;

import com.pagosmoviles.wsusuarios.dto.auth.LoginRequest;
import com.pagosmoviles.wsusuarios.dto.auth.LoginResponse;
import org.springframework.ws.server.endpoint.annotation.Endpoint;
import org.springframework.ws.server.endpoint.annotation.PayloadRoot;
import org.springframework.ws.server.endpoint.annotation.RequestPayload;
import org.springframework.ws.server.endpoint.annotation.ResponsePayload;

@Endpoint
public class AuthService {

    private static final String NAMESPACE_URI = "http://pagosmoviles.com/wsusuarios";

    @PayloadRoot(namespace = NAMESPACE_URI, localPart = "loginRequest")
    @ResponsePayload
    public LoginResponse login(@RequestPayload LoginRequest request) {
        LoginResponse response = new LoginResponse();
        response.setMessage("WSU-01: WebService Login");
        return response;

    }

}
