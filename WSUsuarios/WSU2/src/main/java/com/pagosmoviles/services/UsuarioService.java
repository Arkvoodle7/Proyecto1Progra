package com.pagosmoviles.services;

import com.pagosmoviles.entities.Usuario;
import com.pagosmoviles.repository.UsuarioRepository;
import com.pagosmoviles.repository.BancoRepository;
import com.pagosmoviles.utils.Encrypter;
import org.springframework.stereotype.Service;

@Service
public class UsuarioService {

    private final UsuarioRepository usuarioRepository;
    private final BancoRepository bancoRepository;
    private final Encrypter encrypter;

    public UsuarioService(UsuarioRepository usuarioRepository, BancoRepository bancoRepository, Encrypter encrypter) {
        this.usuarioRepository = usuarioRepository;
        this.bancoRepository = bancoRepository;
        this.encrypter = encrypter;
    }

    public void crearUsuario(Usuario usuario) throws Exception {
        String encryptedPassword = encrypter.encrypt(usuario.getContrasena());
        usuario.setContrasena(encryptedPassword);

        usuarioRepository.crearUsuario(usuario);

        if (!bancoRepository.clienteExiste(usuario.getIdentificacion())) {
            bancoRepository.registrarCliente(usuario);
        }
    }

    public void editarUsuario(Usuario usuario) throws Exception {
        String encryptedPassword = encrypter.encrypt(usuario.getContrasena());
        usuario.setContrasena(encryptedPassword);

        usuarioRepository.editarUsuario(usuario);

        if (bancoRepository.clienteExiste(usuario.getIdentificacion())) {
            bancoRepository.actualizarCliente(usuario);
        } else {
            bancoRepository.registrarCliente(usuario);
        }
    }

    public Usuario obtenerUsuarioPorIdentificacion(String identificacion) {
        return usuarioRepository.obtenerUsuarioPorIdentificacion(identificacion);
    }
}
