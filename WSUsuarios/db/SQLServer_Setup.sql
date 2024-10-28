CREATE DATABASE PagosMovilesUsuarios

CREATE TABLE Usuarios (
    identificacion CHAR(9) PRIMARY KEY,
    nombre_usuario VARCHAR(50) NOT NULL,
    nombre_completo VARCHAR(100) NOT NULL,
    contrasena NVARCHAR(255) NOT NULL,
    telefono CHAR(8)
);

INSERT INTO Usuarios (identificacion, nombre_usuario, nombre_completo, contrasena, telefono) VALUES 
('101110111', 'user_101110111', 'User Full Name 101110111', 'EncryptedPassword1', '88888888'),
('202220222', 'user_202220222', 'User Full Name 202220222', 'EncryptedPassword2', '77777777'),
('303330333', 'user_303330333', 'User Full Name 303330333', 'EncryptedPassword3', '66666666'),
('404440444', 'user_404440444', 'User Full Name 404440444', 'EncryptedPassword4', '55555555'),
('505550555', 'user_505550555', 'User Full Name 505550555', 'EncryptedPassword5', '44444444'),
('606660666', 'user_606660666', 'User Full Name 606660666', 'EncryptedPassword6', '33333333'),
('707770777', 'user_707770777', 'User Full Name 707770777', 'EncryptedPassword7', '22222222'),
('808880888', 'user_808880888', 'User Full Name 808880888', 'EncryptedPassword8', '11111111'),
('909990999', 'user_909990999', 'User Full Name 909990999', 'EncryptedPassword9', '99999999'),
('111110000', 'user_111110000', 'User Full Name 111110000', 'EncryptedPassword10', '00000000');
