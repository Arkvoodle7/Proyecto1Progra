package com.pagosmoviles.services;

import com.pagosmoviles.entities.Usuario;
import com.pagosmoviles.repository.UsuarioRepository;
import com.pagosmoviles.dto.AuthResponse;
import org.springframework.stereotype.Service;

@Service
public class AuthService {

    private final UsuarioRepository usuarioRepository;

    public AuthService(UsuarioRepository usuarioRepository) {
        this.usuarioRepository = usuarioRepository;
    }

    public AuthResponse autenticarUsuario(String nombreUsuario, String contrasena) {
        Usuario usuario = usuarioRepository.obtenerUsuarioPorCredenciales(nombreUsuario, contrasena);

        if (usuario != null) {
            // Credenciales correctas
            return new AuthResponse(0, "");
        } else {
            // Credenciales incorrectas
            return new AuthResponse(-1, "Usuario y/o contraseña incorrectos");
        }
    }
}
