package com.pagosmoviles.repository;

import com.pagosmoviles.entities.Usuario;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.dao.EmptyResultDataAccessException;
import org.springframework.stereotype.Repository;

import java.sql.ResultSet;

@Repository
public class UsuarioRepository {

    private final JdbcTemplate jdbcTemplate;

    public UsuarioRepository(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    public Usuario obtenerUsuarioPorNombreUsuario(String nombreUsuario) {
        String query = "SELECT * FROM Usuarios WHERE nombre_usuario = ?";

        try {
            return jdbcTemplate.queryForObject(query, new Object[]{nombreUsuario}, (ResultSet rs, int rowNum) -> {
                Usuario usuario = new Usuario();
                usuario.setIdentificacion(rs.getString("identificacion"));
                usuario.setNombreUsuario(rs.getString("nombre_usuario"));
                usuario.setNombreCompleto(rs.getString("nombre_completo"));
                usuario.setContrasena(rs.getString("contrasena"));
                usuario.setTelefono(rs.getString("telefono"));
                return usuario;
            });
        } catch (EmptyResultDataAccessException e) {
            return null;
        }
    }
}
