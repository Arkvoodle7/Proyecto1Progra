package interfaces;

import dto.TransaccionDto;

public interface ITransaccion {
    String aplicarTransaccion(TransaccionDto transaccion);

    String consultarSaldo(String identificacion, String numeroCuenta);
}
