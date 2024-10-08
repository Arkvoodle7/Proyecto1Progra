package interfaces;

import entities.Cuenta;

public interface ICuenta {

    Cuenta buscarCuenta(String identificacion, String numeroCuenta);

    void actualizarCuenta(Cuenta cuenta);
}
