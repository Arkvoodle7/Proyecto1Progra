package interfaces;

import entities.Cuenta;

public interface ICuenta {
    Cuenta buscarCuenta(String identificacion, String numeroCuenta);
    void actualizarCuenta(Cuenta cuenta);
    boolean clienteExiste(String identificacion);  // Agregar esta línea
}
