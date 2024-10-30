package services;

import dto.TransaccionDto;
import entities.Cuenta;
import interfaces.ICuenta;
import interfaces.ITransaccion;

public class TransaccionService implements ITransaccion {
    private final ICuenta cuentaRepository;

    public TransaccionService(ICuenta cuentaRepository) {
        this.cuentaRepository = cuentaRepository;
    }

    //metodo para que se aplique la Transaccion en la cuenta del usuario
    @Override
    public String aplicarTransaccion(TransaccionDto transaccion) {
        // Obtener el nombre de usuario a partir de la identificación
        String nombreUsuario = cuentaRepository.obtenerNombreUsuario(transaccion.getIdentificacion());
        if (nombreUsuario == null) {
            return "ERROR|Usuario no encontrado";
        }

        // Buscar la cuenta usando el nombre de usuario y el número de cuenta
        Cuenta cuenta = cuentaRepository.buscarCuenta(nombreUsuario, transaccion.getNumeroCuenta());
        if (cuenta == null) {
            return "ERROR|Cuenta inexistente";
        }

        // Aplicar la transacción
        if ("DEB".equals(transaccion.getTipo())) {
            cuenta.setSaldo(cuenta.getSaldo() - transaccion.getMonto());
        } else if ("CRE".equals(transaccion.getTipo())) {
            cuenta.setSaldo(cuenta.getSaldo() + transaccion.getMonto());
        }

        // Actualizar la cuenta en la base de datos
        cuentaRepository.actualizarCuenta(cuenta);
        return "OK|Transacción aplicada";
    }

    //metodo para consultar el saldo de una cuenta.
    public String consultarSaldo(String identificacion, String numeroCuenta) {
        try {
            // Obtener el nombre de usuario a partir de la identificación
            String nombreUsuario = cuentaRepository.obtenerNombreUsuario(identificacion);
            if (nombreUsuario == null) {
                return "ERROR|Usuario no encontrado";
            }

            // Buscar la cuenta usando el nombre de usuario y el número de cuenta
            Cuenta cuenta = cuentaRepository.buscarCuenta(nombreUsuario, numeroCuenta);
            if (cuenta == null) {
                return "ERROR|Cuenta incorrecta";
            }

            return "OK|" + cuenta.getSaldo();

        } catch (Exception e) {
            e.printStackTrace();
            return "ERROR|Error al procesar la solicitud";
        }

    }
}
