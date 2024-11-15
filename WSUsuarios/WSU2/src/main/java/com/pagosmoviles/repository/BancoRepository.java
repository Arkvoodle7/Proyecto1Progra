package com.pagosmoviles.repository;

import com.pagosmoviles.entities.Usuario;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

@Repository
public class BancoRepository {

    private final JdbcTemplate jdbcTemplate;

    public BancoRepository(@Qualifier("bancoJdbcTemplate") JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    public void registrarUsuarioEnUsuariosBancario(Usuario usuario) {
        String query = "INSERT INTO Usuarios (identificacion, nombre_usuario, nombre_completo, contrasena, telefono) VALUES (?, ?, ?, ?, ?)";
        jdbcTemplate.update(query, usuario.getIdentificacion(), usuario.getNombreUsuario(), usuario.getNombreCompleto(), usuario.getContrasena(), usuario.getTelefono());
    }

    public void registrarClienteEnClientes(Usuario usuario) {
        String query = "INSERT INTO Clientes (identificacion, nombre_completo, telefono) VALUES (?, ?, ?)";
        jdbcTemplate.update(query, usuario.getIdentificacion(), usuario.getNombreCompleto(), usuario.getTelefono());
    }

    public void actualizarClienteEnClientes(Usuario usuario) {
        String query = "UPDATE Clientes SET nombre_completo = ?, telefono = ? WHERE identificacion = ?";
        jdbcTemplate.update(query, usuario.getNombreCompleto(), usuario.getTelefono(), usuario.getIdentificacion());
    }

    public void actualizarUsuarioEnUsuariosBancario(Usuario usuario) {
        String query = "UPDATE Usuarios SET nombre_usuario = ?, nombre_completo = ?, contrasena = ?, telefono = ? WHERE identificacion = ?";
        jdbcTemplate.update(query, usuario.getNombreUsuario(), usuario.getNombreCompleto(), usuario.getContrasena(), usuario.getTelefono(), usuario.getIdentificacion());
    }

    public boolean clienteExisteEnClientes(String identificacion) {
        String query = "SELECT COUNT(*) FROM Clientes WHERE identificacion = ?";
        Integer count = jdbcTemplate.queryForObject(query, Integer.class, identificacion);
        return count != null && count > 0;
    }
}
