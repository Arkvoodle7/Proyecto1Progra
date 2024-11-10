package com.pagosmoviles.wsusuarios.dto.cuentas.wsu04;


import jakarta.xml.bind.annotation.XmlRootElement;
import jakarta.xml.bind.annotation.XmlElement;

@XmlRootElement(name = "transaccionRequest", namespace = "http://pagosmoviles.com/wsusuarios")
public class TransaccionRequest {

    private String Telefono;
    private double Monto;
    private String Descripcion;

    @XmlElement(name = "telefono", namespace = "http://pagosmoviles.com/wsusuarios")
    public String getTelefono() {
        return Telefono;
    }

    public void setTelefono(String telefono) {
        Telefono = telefono;
    }

    @XmlElement(name = "monto", namespace = "http://pagosmoviles.com/wsusuarios")
    public double getMonto() {
        return Monto;
    }

    public void setMonto(double monto) {
        Monto = monto;
    }

    @XmlElement(name = "descripcion", namespace = "http://pagosmoviles.com/wsusuarios")
    public String getDescripcion() {
        return Descripcion;
    }

    public void setDescripcion(String descripcion) {
        Descripcion = descripcion;
    }
}
