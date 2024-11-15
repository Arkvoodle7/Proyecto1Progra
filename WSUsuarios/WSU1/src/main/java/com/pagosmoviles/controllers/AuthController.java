package com.pagosmoviles.controllers;

import com.pagosmoviles.dto.AuthRequestDto;
import com.pagosmoviles.dto.AuthResponse;
import com.pagosmoviles.services.AuthService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/auth")
public class AuthController {

    private final AuthService authService;

    @Autowired
    public AuthController(AuthService authService) {
        this.authService = authService;
    }

    @PostMapping(
            value = "/login",
            consumes = MediaType.APPLICATION_XML_VALUE + ";charset=UTF-8",
            produces = MediaType.APPLICATION_XML_VALUE + ";charset=UTF-8"
    )
    public ResponseEntity<AuthResponse> autenticarUsuario(@RequestBody AuthRequestDto authRequest) {
        AuthResponse authResponse = authService.autenticarUsuario(
                authRequest.getNombreUsuario(),
                authRequest.getContrasena()
        );

        return ResponseEntity.ok(authResponse);
    }

}
