package com.pagosmoviles.controllers;

import com.pagosmoviles.dto.DesinscripcionRequestDto;
import com.pagosmoviles.dto.DesinscripcionResponseDto;
import com.pagosmoviles.services.DesinscripcionService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import org.springframework.http.MediaType;

@RestController
@RequestMapping("/api")
public class DesinscripcionController {

    private final DesinscripcionService desinscripcionService;

    @Autowired
    public DesinscripcionController(DesinscripcionService desinscripcionService) {
        this.desinscripcionService = desinscripcionService;
    }

    @PostMapping(value = "/unregister", consumes = "application/xml", produces = "application/xml")
    public DesinscripcionResponseDto desinscribirUsuario(@RequestBody DesinscripcionRequestDto request) {
        return desinscripcionService.desinscribirUsuario(request);
    }
}
