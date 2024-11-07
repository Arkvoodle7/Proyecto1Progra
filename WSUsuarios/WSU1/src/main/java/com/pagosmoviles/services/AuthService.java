package com.pagosmoviles.services;

import com.pagosmoviles.entities.Usuario;
import com.pagosmoviles.repository.UsuarioRepository;
import com.pagosmoviles.dto.AuthResponse;
import com.pagosmoviles.utils.Encrypter;
import org.springframework.stereotype.Service;

@Service
public class AuthService {

    private final UsuarioRepository usuarioRepository;
    private final Encrypter encrypter;

    public AuthService(UsuarioRepository usuarioRepository, Encrypter encrypter) {
        this.usuarioRepository = usuarioRepository;
        this.encrypter = encrypter;
    }

    public AuthResponse autenticarUsuario(String nombreUsuario, String contrasena) {
        Usuario usuario = usuarioRepository.obtenerUsuarioPorNombreUsuario(nombreUsuario);

        if (usuario != null) {
            try {
                String decryptedPassword = encrypter.decrypt(usuario.getContrasena());
                if (decryptedPassword.equals(contrasena)) {
                    return new AuthResponse(0, "");
                } else {
                    return new AuthResponse(-1, "Usuario y/o contraseña incorrectos");
                }
            } catch (Exception e) {
                e.printStackTrace();
                return new AuthResponse(-1, "Usuario y/o contraseña incorrectos");
            }
        } else {
            return new AuthResponse(-1, "Usuario y/o contraseña incorrectos");
        }
    }
}