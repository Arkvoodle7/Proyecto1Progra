package services;

import entities.Cuenta;
import interfaces.ICuenta;
import interfaces.ITransaccion;
import dto.TransaccionDto;

public class TransaccionService implements ITransaccion {
    private final ICuenta cuentaRepository;

    public TransaccionService(ICuenta cuentaRepository) {
        this.cuentaRepository = cuentaRepository;
    }

    // Método para que se aplique la Transacción en la Cuenta del Usuario
    @Override
    public String aplicarTransaccion(TransaccionDto transaccion) {
        // Busca la Cuenta por medio de la identificación
        Cuenta cuenta = cuentaRepository.buscarCuenta(transaccion.getIdentificacion(), transaccion.getNumeroCuenta());
        if (cuenta == null) {
            return "ERROR|Cuenta inexistente";
        }

        if ("DEB".equals(transaccion.getTipo())) {
            cuenta.setSaldo(cuenta.getSaldo() - transaccion.getMonto());
        } else if ("CRE".equals(transaccion.getTipo())) {
            cuenta.setSaldo(cuenta.getSaldo() + transaccion.getMonto());
        }

        cuentaRepository.actualizarCuenta(cuenta);
        return "OK|Transacción aplicada";
    }
    // Método para Consultar el Saldo de una Cuenta.
    @Override
    public String consultarSaldo(String identificacion, String numeroCuenta) {
        Cuenta cuenta = cuentaRepository.buscarCuenta(identificacion, numeroCuenta);
        if (cuenta == null) {
            return "ERROR|Cuenta inexistente";
        }
        return "OK|" + cuenta.getSaldo();
    }

}
