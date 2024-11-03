package com.pagosmoviles.repository;

import com.pagosmoviles.entities.Usuario;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Repository;

@Repository
public class BancoRepository {

    private final JdbcTemplate jdbcTemplate;

    public BancoRepository(@Qualifier("bancoJdbcTemplate") JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    public void registrarCliente(Usuario usuario) {
        String query = "INSERT INTO Usuarios (identificacion, nombre_usuario, nombre_completo, contrasena, telefono) VALUES (?, ?, ?, ?, ?)";
        jdbcTemplate.update(query, usuario.getIdentificacion(), usuario.getNombreUsuario(), usuario.getNombreCompleto(), usuario.getContrasena(), usuario.getTelefono());
    }

    public void actualizarCliente(Usuario usuario) {
        String query = "UPDATE Usuarios SET nombre_usuario = ?, nombre_completo = ?, contrasena = ?, telefono = ? WHERE identificacion = ?";
        jdbcTemplate.update(query, usuario.getNombreUsuario(), usuario.getNombreCompleto(), usuario.getContrasena(), usuario.getIdentificacion());
    }

    public boolean clienteExiste(String identificacion) {
        String query = "SELECT COUNT(*) FROM Usuarios WHERE identificacion = ?";
        Integer count = jdbcTemplate.queryForObject(query, Integer.class, identificacion);
        return count != null && count > 0;
    }
}
