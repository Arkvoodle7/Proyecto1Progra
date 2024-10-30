package repository;

import config.ConfigProps;
import entities.Cuenta;
import interfaces.ICuenta;

import java.sql.*;

public class CuentaRepository implements ICuenta {

    //metodo para obtener la conexion a la base de datos
    private Connection getConnection() throws SQLException {
        String url = ConfigProps.getProp("db_url");
        return DriverManager.getConnection(url);
    }

    @Override
    public Cuenta buscarCuenta(String nombreUsuario, String numeroCuenta) {
        Cuenta cuenta = null;
        String query = "SELECT * FROM Cuentas WHERE nombre_usuario = ? AND numero_cuenta = ?";

        try (Connection connection = getConnection();
             PreparedStatement statement = connection.prepareStatement(query)) {

            statement.setString(1, nombreUsuario);
            statement.setString(2, numeroCuenta);
            ResultSet rs = statement.executeQuery();

            if (rs.next()) {
                cuenta = new Cuenta(
                        rs.getString("numero_cuenta"),
                        rs.getString("nombre_usuario"),
                        rs.getString("tipo_cuenta"),
                        rs.getDouble("saldo")
                );
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return cuenta;
    }
    // TODO: Desacoplar de CuentaRepository , Hacer adaptación para el servicio, entidad, etc.
    public String obtenerNombreUsuario(String identificacion) {
        String query = "SELECT nombre_usuario FROM Usuarios WHERE identificacion = ?";
        try (Connection connection = getConnection();
             PreparedStatement statement = connection.prepareStatement(query)) {

            statement.setString(1, identificacion);
            ResultSet rs = statement.executeQuery();
            if (rs.next()) {
                return rs.getString("nombre_usuario");
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null; // Devuelve null si no se encuentra el usuario
    }


    @Override
    public void actualizarCuenta(Cuenta cuenta) {
        String query = "UPDATE Cuentas SET saldo = ? WHERE numero_cuenta = ?";

        try (Connection connection = getConnection();
             PreparedStatement statement = connection.prepareStatement(query)) {

            statement.setDouble(1, cuenta.getSaldo());
            statement.setString(2, cuenta.getNumeroCuenta());
            statement.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    @Override
    public boolean clienteExiste(String identificacion) {
        String query = "SELECT 1 FROM Clientes WHERE identificacion = ?";

        try (Connection connection = getConnection();
             PreparedStatement statement = connection.prepareStatement(query)) {

            statement.setString(1, identificacion);
            ResultSet rs = statement.executeQuery();

            return rs.next();
        } catch (SQLException e) {
            e.printStackTrace();
            return false;
        }
    }

}
