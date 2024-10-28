package com.progra.wsusuarios;

public class HelloWorld {
    public String sayHello() {
        return "Hello, World!";
    }

    public static void main(String[] args) {
        HelloWorld service = new HelloWorld();
        System.out.println(service.sayHello());
    }
}
