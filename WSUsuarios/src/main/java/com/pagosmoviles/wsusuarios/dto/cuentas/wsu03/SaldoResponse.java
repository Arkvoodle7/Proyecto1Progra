package com.pagosmoviles.wsusuarios.dto.cuentas.wsu03;

import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "saldoResponse", namespace = "http://pagosmoviles.com/wsusuarios")
public class SaldoResponse {
    private int codigo;
    private String descripcion;
    private Double saldo;

    @XmlElement(name = "codigo")
    public int getCodigo() {
        return codigo;
    }

    public void setCodigo(int codigo) {
        this.codigo = codigo;
    }

    @XmlElement(name = "descripcion")
    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    @XmlElement(name = "saldo")
    public Double getSaldo() {
        return saldo;
    }

    public void setSaldo(Double saldo) {
        this.saldo = saldo;
    }
}