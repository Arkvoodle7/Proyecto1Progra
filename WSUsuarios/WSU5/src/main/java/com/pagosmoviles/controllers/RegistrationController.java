package com.pagosmoviles.controllers;


import com.pagosmoviles.dto.RegistrationRequestDto;
import com.pagosmoviles.dto.RegistrationResponseDto;
import com.pagosmoviles.services.RegistrationService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api")
public class RegistrationController {

    private final RegistrationService registrationService;

    @Autowired
    public RegistrationController(RegistrationService registrationService) {
        this.registrationService = registrationService;
    }

    @PostMapping(value = "/register", consumes = "application/xml", produces = "application/xml")
    public RegistrationResponseDto registerUser(@RequestBody RegistrationRequestDto request) {
        return registrationService.registerUser(request);
    }
}
