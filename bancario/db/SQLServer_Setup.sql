CREATE DATABASE PagosMovilesBancario;
USE PagosMovilesBancario;

CREATE TABLE Cuentas (
    id_cuenta INT PRIMARY KEY IDENTITY(1,1),
    identificacion VARCHAR(20) NOT NULL,
    numero_cuenta VARCHAR(20) UNIQUE NOT NULL,
    saldo DECIMAL(15, 2) NOT NULL
);

INSERT INTO Cuentas (identificacion, numero_cuenta, saldo)
VALUES ('101110111', '1234567890', 1000.00);

SELECT * FROM Cuentas;
