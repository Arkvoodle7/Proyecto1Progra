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
        try {
            // Validar campos vacíos
            if (identificacion == null || identificacion.trim().isEmpty() ||
                    numeroCuenta == null || numeroCuenta.trim().isEmpty()) {
                return "ERROR|Todos los campos son obligatorios y deben ser válidos.";
            }

            // Buscar el cliente y la cuenta
            Cuenta cuenta = cuentaRepository.buscarCuenta(identificacion, numeroCuenta);
            if (cuenta == null) {
                // Verificar si el cliente existe
                boolean clienteExiste = cuentaRepository.clienteExiste(identificacion);
                if (!clienteExiste) {
                    return "ERROR|Datos inexistentes";
                } else {
                    // Si el cliente existe pero la cuenta no coincide
                    return "ERROR|Cuenta incorrecta";
                }
            }

            // Si todo es correcto, devolver el saldo
            return "OK|" + cuenta.getSaldo();

        } catch (Exception e) {
            e.printStackTrace();
            return "ERROR|Error al procesar la solicitud";
        }
    }
}
