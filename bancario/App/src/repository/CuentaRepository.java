package repository;

import config.ConfigProps;
import entities.Cuenta;
import interfaces.ICuenta;

import java.sql.*;

public class CuentaRepository implements ICuenta {

    // Método para obtener la conexión a la base de datos
    private Connection getConnection() throws SQLException {

        String url = ConfigProps.getProp("db_url");
        return DriverManager.getConnection(url);
    }

    @Override
    public Cuenta buscarCuenta(String identificacion, String numeroCuenta) {
        Cuenta cuenta = null;
        String query = "SELECT * FROM Cuentas WHERE identificacion = ? AND numero_cuenta = ?";

        try (Connection connection = getConnection();
             PreparedStatement statement = connection.prepareStatement(query)) {

            statement.setString(1, identificacion);
            statement.setString(2, numeroCuenta);
            ResultSet rs = statement.executeQuery();

            if (rs.next()) {
                cuenta = new Cuenta(
                        rs.getInt("id_cuenta"),
                        rs.getString("identificacion"),
                        rs.getString("numero_cuenta"),
                        rs.getDouble("saldo")
                );
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }

        return cuenta;
    }


    public void actualizarCuenta(Cuenta cuenta) {
        String query = "UPDATE Cuentas SET saldo = ? WHERE id_cuenta = ?";

        try (Connection connection = getConnection();
             PreparedStatement statement = connection.prepareStatement(query)) {

            statement.setDouble(1, cuenta.getSaldo());
            statement.setInt(2, cuenta.getIdCuenta());
            statement.executeUpdate();

        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
