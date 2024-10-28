use Admin; // Selecciona o crea la base de datos

db.createCollection("Administradores"); // Crea la colección

// Inserta un documento de ejemplo
db.Administradores.insertOne({
    "nombre_usuario": "admin01",
    "nombre_completo": "Administrador Ejemplo",
    "contrasena": "contraseña_cifrada"
});
