package com.pagosmoviles.services;

import com.pagosmoviles.entities.Usuario;
import com.pagosmoviles.repository.BancoRepository;
import com.pagosmoviles.repository.UsuarioRepository;
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
        if (usuario.getIdentificacion() == null || usuario.getIdentificacion().isEmpty() ||
                usuario.getNombreUsuario() == null || usuario.getNombreUsuario().isEmpty() ||
                usuario.getNombreCompleto() == null || usuario.getNombreCompleto().isEmpty() ||
                usuario.getContrasena() == null || usuario.getContrasena().isEmpty() ||
                usuario.getTelefono() == null || usuario.getTelefono().isEmpty()) {
            throw new IllegalArgumentException("Datos incompletos");
        }

        if (usuarioRepository.obtenerUsuarioPorIdentificacion(usuario.getIdentificacion()) != null) {
            throw new IllegalArgumentException("El usuario ya existe");
        }

        String encryptedPassword = encrypter.encrypt(usuario.getContrasena());
        usuario.setContrasena(encryptedPassword);

        usuarioRepository.crearUsuario(usuario);

        if (!bancoRepository.clienteExisteEnClientes(usuario.getIdentificacion())) {
            bancoRepository.registrarClienteEnClientes(usuario);
        }

        bancoRepository.registrarUsuarioEnUsuariosBancario(usuario);
    }

    public void editarUsuario(Usuario usuario) throws Exception {
        Usuario existingUser = usuarioRepository.obtenerUsuarioPorIdentificacion(usuario.getIdentificacion());
        if (existingUser == null) {
            throw new IllegalArgumentException("El usuario no existe");
        }

        if (usuario.getContrasena() != null && !usuario.getContrasena().isEmpty()) {
            String encryptedPassword = encrypter.encrypt(usuario.getContrasena());
            usuario.setContrasena(encryptedPassword);
        } else {
            usuario.setContrasena(existingUser.getContrasena());
        }

        usuarioRepository.editarUsuario(usuario);

        boolean updateCliente = false;
        if (usuario.getNombreCompleto() != null && !usuario.getNombreCompleto().equals(existingUser.getNombreCompleto())) {
            updateCliente = true;
        }
        if (usuario.getTelefono() != null && !usuario.getTelefono().equals(existingUser.getTelefono())) {
            updateCliente = true;
        }

        if (updateCliente) {
            if (bancoRepository.clienteExisteEnClientes(usuario.getIdentificacion())) {
                bancoRepository.actualizarClienteEnClientes(usuario);
            }
        }

        bancoRepository.actualizarUsuarioEnUsuariosBancario(usuario);
    }

    public Usuario obtenerUsuarioPorIdentificacion(String identificacion) {
        return usuarioRepository.obtenerUsuarioPorIdentificacion(identificacion);
    }
}
