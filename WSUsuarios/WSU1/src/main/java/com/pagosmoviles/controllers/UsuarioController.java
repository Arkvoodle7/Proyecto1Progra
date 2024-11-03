package com.pagosmoviles.controllers;

import com.pagosmoviles.entities.Usuario;
import com.pagosmoviles.dto.UsuarioResponseDto;
import com.pagosmoviles.services.UsuarioService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/usuario")
public class UsuarioController {

    private final UsuarioService usuarioService;

    @Autowired
    public UsuarioController(UsuarioService usuarioService) {
        this.usuarioService = usuarioService;
    }

    @PostMapping(
            value = "/crear",
            consumes = MediaType.APPLICATION_XML_VALUE,
            produces = MediaType.APPLICATION_XML_VALUE
    )
    public ResponseEntity<String> crearUsuario(@RequestBody Usuario usuario) {
        try {
            usuarioService.crearUsuario(usuario);
            return ResponseEntity.ok("<resultado>0</resultado><mensaje>Usuario creado exitosamente</mensaje>");
        } catch (Exception e) {
            e.printStackTrace();
            return ResponseEntity.badRequest().body("<resultado>-1</resultado><mensaje>Error al crear el usuario</mensaje>");
        }
    }

    @PutMapping(
            value = "/editar",
            consumes = MediaType.APPLICATION_XML_VALUE,
            produces = MediaType.APPLICATION_XML_VALUE
    )
    public ResponseEntity<String> editarUsuario(@RequestBody Usuario usuario) {
        try {
            usuarioService.editarUsuario(usuario);
            return ResponseEntity.ok("<resultado>0</resultado><mensaje>Usuario editado exitosamente</mensaje>");
        } catch (Exception e) {
            e.printStackTrace();
            return ResponseEntity.badRequest().body("<resultado>-1</resultado><mensaje>Error al editar el usuario</mensaje>");
        }
    }

    @GetMapping(
            value = "/{identificacion}",
            produces = MediaType.APPLICATION_XML_VALUE
    )
    public ResponseEntity<UsuarioResponseDto> obtenerUsuario(@PathVariable String identificacion) {
        Usuario usuario = usuarioService.obtenerUsuarioPorIdentificacion(identificacion);
        if (usuario != null) {
            UsuarioResponseDto usuarioDto = new UsuarioResponseDto(
                    usuario.getIdentificacion(),
                    usuario.getNombreUsuario(),
                    usuario.getNombreCompleto(),
                    usuario.getTelefono()
            );
            return ResponseEntity.ok(usuarioDto);
        } else {
            return ResponseEntity.notFound().build();
        }
    }
}
