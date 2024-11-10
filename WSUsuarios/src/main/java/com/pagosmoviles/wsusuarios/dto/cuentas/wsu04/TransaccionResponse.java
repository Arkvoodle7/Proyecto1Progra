package com.pagosmoviles.wsusuarios.dto.cuentas.wsu04;

import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "transaccionResponse", namespace = "http://pagosmoviles.com/wsusuarios")
public class TransaccionResponse {


    private int codigo;

    private String descripcion;

    @XmlElement(name = "codigo", namespace = "http://pagosmoviles.com/wsusuarios")
    public int getCodigo() {
        return codigo;
    }

    public void setCodigo(int codigo) {
        this.codigo = codigo;
    }

    @XmlElement(name = "descripcion", namespace = "http://pagosmoviles.com/wsusuarios")
    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }
}
