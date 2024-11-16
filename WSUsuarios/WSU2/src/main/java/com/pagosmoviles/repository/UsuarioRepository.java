package com.pagosmoviles.repository;

import com.pagosmoviles.entities.Usuario;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.dao.EmptyResultDataAccessException;
import org.springframework.stereotype.Repository;

import java.sql.ResultSet;

@Repository
public class UsuarioRepository {

    private final JdbcTemplate jdbcTemplate;

    public UsuarioRepository(@Qualifier("usuariosJdbcTemplate") JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    public Usuario obtenerUsuarioPorCredenciales(String nombreUsuario, String contrasena) {
        String query = "SELECT * FROM Usuarios WHERE nombre_usuario = ? AND contrasena = ?";

        try {
            return jdbcTemplate.queryForObject(query, new Object[]{nombreUsuario, contrasena}, (ResultSet rs, int rowNum) -> {
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

    public void crearUsuario(Usuario usuario) {
        String query = "INSERT INTO Usuarios (identificacion, nombre_usuario, nombre_completo, contrasena, telefono) VALUES (?, ?, ?, ?, ?)";
        jdbcTemplate.update(query, usuario.getIdentificacion(), usuario.getNombreUsuario(), usuario.getNombreCompleto(), usuario.getContrasena(), usuario.getTelefono());
    }

    public void editarUsuario(Usuario usuario) {
        String query = "UPDATE Usuarios SET nombre_usuario = ?, nombre_completo = ?, contrasena = ?, telefono = ? WHERE identificacion = ?";
        jdbcTemplate.update(query, usuario.getNombreUsuario(), usuario.getNombreCompleto(), usuario.getContrasena(), usuario.getTelefono(), usuario.getIdentificacion());
    }

    public Usuario obtenerUsuarioPorIdentificacion(String identificacion) {
        String query = "SELECT * FROM Usuarios WHERE identificacion = ?";

        try {
            return jdbcTemplate.queryForObject(query, new Object[]{identificacion}, (ResultSet rs, int rowNum) -> {
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