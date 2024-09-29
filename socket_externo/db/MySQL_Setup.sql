CREATE DATABASE PagosMovilesSocketExterno;
USE PagosMovilesSocketExterno;

-- Tabla Bitácora
CREATE TABLE Bitacora (
    id_bitacora INT PRIMARY KEY AUTO_INCREMENT,
    fecha_hora DATETIME DEFAULT CURRENT_TIMESTAMP,
    trama_recibida TEXT,
    trama_respuesta TEXT
);

SELECT * FROM Bitacora;