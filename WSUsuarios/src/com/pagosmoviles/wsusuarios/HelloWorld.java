package com.pagosmoviles.wsusuarios;

public class HelloWorld {
    public String sayHello() {
        return "Hello, World!";
    }

    public static void main(String[] args) {
        com.pagosmoviles.wsusuarios.HelloWorld service = new com.pagosmoviles.wsusuarios.HelloWorld();
        System.out.println(service.sayHello());
    }
}
