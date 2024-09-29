//crear la base de datos PagosMovilesOrquestador
use PagosMovilesOrquestador

//crear la colección TelefonosXCuentas
db.createCollection("TelefonosXCuentas")

//crear un índice para garantizar que "telefono" sea único
db.TelefonosXCuentas.createIndex({ "telefono": 1 }, { unique: true })

//insertar un documento sin especificar el _id (MongoDB generará el ObjectId automáticamente)
db.TelefonosXCuentas.insertOne({
    "identificacion": "101110111",
    "numero_cuenta": "1234567890",
    "telefono": "88888888"
})

//consultar todos los documentos de la colección
db.TelefonosXCuentas.find()
