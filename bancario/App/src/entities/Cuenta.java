package entities;

public class Cuenta {
    private int idCuenta;
    private String identificacion;  // Identificación del propietario de la cuenta
    private String numeroCuenta;    // Número de la cuenta bancaria
    private double saldo;           // Saldo de la cuenta


    public Cuenta(int idCuenta, String identificacion, String numeroCuenta, double saldo) {
        this.idCuenta = idCuenta;
        this.identificacion = identificacion;
        this.numeroCuenta = numeroCuenta;
        this.saldo = saldo;
    }

    // Getters y Setters
    public int getIdCuenta() {
        return idCuenta;
    }

    public void setIdCuenta(int idCuenta) {
        this.idCuenta = idCuenta;
    }

    public String getIdentificacion() {
        return identificacion;
    }

    public void setIdentificacion(String identificacion) {
        this.identificacion = identificacion;
    }

    public String getNumeroCuenta() {
        return numeroCuenta;
    }

    public void setNumeroCuenta(String numeroCuenta) {
        this.numeroCuenta = numeroCuenta;
    }

    public double getSaldo() {
        return saldo;
    }

    public void setSaldo(double saldo) {
        this.saldo = saldo;
    }
}
