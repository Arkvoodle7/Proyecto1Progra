package entities;

public class Cuenta {
    private String numeroCuenta;    // Número de la cuenta bancaria (PRIMARY KEY)
    private String nombreUsuario;   // Nombre del usuario propietario
    private String tipoCuenta;      // Tipo de cuenta (ahorros o empresarial)
    private double saldo;           // Saldo de la cuenta

    public Cuenta(String numeroCuenta, String nombreUsuario, String tipoCuenta, double saldo) {
        this.numeroCuenta = numeroCuenta;
        this.nombreUsuario = nombreUsuario;
        this.tipoCuenta = tipoCuenta;
        this.saldo = saldo;
    }

    // Getters y Setters
    public String getNumeroCuenta() {
        return numeroCuenta;
    }

    public void setNumeroCuenta(String numeroCuenta) {
        this.numeroCuenta = numeroCuenta;
    }

    public String getNombreUsuario() {
        return nombreUsuario;
    }

    public void setNombreUsuario(String nombreUsuario) {
        this.nombreUsuario = nombreUsuario;
    }

    public String getTipoCuenta() {
        return tipoCuenta;
    }

    public void setTipoCuenta(String tipoCuenta) {
        this.tipoCuenta = tipoCuenta;
    }

    public double getSaldo() {
        return saldo;
    }

    public void setSaldo(double saldo) {
        this.saldo = saldo;
    }
}
