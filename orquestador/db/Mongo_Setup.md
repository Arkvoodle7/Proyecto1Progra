//crear la base de datos PagosMovilesOrquestador
use PagosMovilesOrquestador

//crear la colección TelefonosXCuentas
db.createCollection("TelefonosXCuentas")

//crear un índice para garantizar que "telefono" sea único
db.TelefonosXCuentas.createIndex({ "telefono": 1 }, { unique: true })

//insertar múltiples documentos de tu archivo JSON
db.TelefonosXCuentas.insertMany([
    {
        "identificacion": "101110111",
        "numero_cuenta": "1234567890",
        "telefono": "88888888"
    },
    {
        "identificacion": "202220222",
        "numero_cuenta": "9876543210",
        "telefono": "77777777"
    },
    {
        "identificacion": "303330333",
        "numero_cuenta": "1122334455",
        "telefono": "66666666"
    },
    {
        "identificacion": "404440444",
        "numero_cuenta": "6677889900",
        "telefono": "55555555"
    },
    {
        "identificacion": "505550555",
        "numero_cuenta": "9988776655",
        "telefono": "44444444"
    },
    {
        "identificacion": "606660666",
        "numero_cuenta": "5544332211",
        "telefono": "33333333"
    },
    {
        "identificacion": "707770777",
        "numero_cuenta": "1234987654",
        "telefono": "22222222"
    },
    {
        "identificacion": "808880888",
        "numero_cuenta": "8765432109",
        "telefono": "11111111"
    },
    {
        "identificacion": "909990999",
        "numero_cuenta": "1029384756",
        "telefono": "99999999"
    },
    {
        "identificacion": "111110000",
        "numero_cuenta": "1928374650",
        "telefono": "00000000"
    }
])
