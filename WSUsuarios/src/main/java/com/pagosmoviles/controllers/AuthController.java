package com.pagosmoviles.controllers;

import com.pagosmoviles.services.AuthService;
import com.pagosmoviles.services.AuthService.AuthResponse;
import com.pagosmoviles.dto.AuthRequestDto;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/auth")
public class AuthController {

    private final AuthService authService;

    @Autowired
    public AuthController(AuthService authService) {
        this.authService = authService;
    }

    @PostMapping(value = "/login", consumes = MediaType.APPLICATION_XML_VALUE, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<AuthResponse> autenticarUsuario(@RequestBody AuthRequestDto authRequest) {
        String nombreUsuario = authRequest.getNombreUsuario();
        String contrasena = authRequest.getContrasena();

        AuthResponse authResponse = authService.autenticarUsuario(nombreUsuario, contrasena);

        if (authResponse.getResultado() == 0) {
            // Respuesta de éxito
            return ResponseEntity.ok(authResponse);
        } else {
            // Respuesta de error
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(authResponse);
        }
    }
}
