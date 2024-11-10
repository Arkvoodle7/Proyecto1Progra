using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Services;

namespace WSAdministracion
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WSA2 : WebService
    {
        private readonly IMongoCollection<BsonDocument> _adminColeccion;
        private readonly Encriptar _encriptador;

        //Constructor
        public WSA2()
        {
            var conn = new MongoClient("mongodb://localhost:27017");
            var PagosMovAdmin = conn.GetDatabase("PagosMovilesAdministracion");
            _adminColeccion = PagosMovAdmin.GetCollection<BsonDocument>("Administradores");

            // Leer la clave secreta desde web.config
            string base64Key = ConfigurationManager.AppSettings["ClaveSecreta"];
            _encriptador = new Encriptar(base64Key);
        }

        [WebMethod]
        public string crear_usuario(string nombre_usuario, string nombre_completo, string contra)
        {
            var contraEncriptada = _encriptador.Encrypt(contra);

            var usuario = new BsonDocument
            {
                { "nombre_usuario", nombre_usuario },
                { "nombre_completo", nombre_completo },
                { "contra", contraEncriptada }
            };

            _adminColeccion.InsertOne(usuario);
            return "Usuario creado con éxito";
        }

        [WebMethod]
        public string editar_usuario(string nombre_usuario, string nombre_completo, string contra)
        {
            var contraEncriptada = _encriptador.Encrypt(contra);

            var seleccion = Builders<BsonDocument>.Filter.Eq("nombre_usuario", nombre_usuario);
            var actualizar = Builders<BsonDocument>.Update
                .Set("nombre_completo", nombre_completo)
                .Set("contra", contraEncriptada);

            var result = _adminColeccion.UpdateOne(seleccion, actualizar);
            return result.ModifiedCount > 0 ? "Usuario editado con éxito" : "Usuario no encontrado";
        }
        [WebMethod]
        public string listar_usuarios()
        {
            // Obtenemos todos los usuarios sin los campos _id y contra
            var projection = Builders<BsonDocument>.Projection
                .Exclude("_id")  // Excluimos el campo _id
                .Exclude("contra"); // Excluimos el campo contra (contraseña)

            // Recuperamos todos los usuarios con la proyección aplicada
            var usuarios = _adminColeccion.Find(new BsonDocument()).Project(projection).ToList();

            // Creamos una lista de strings con la información de los usuarios
            var resultado = new List<string>();

            foreach (var usuario in usuarios)
            {
                resultado.Add(usuario.ToString());
            }

            // Unimos todos los resultados en una sola cadena separada por saltos de línea
            return string.Join("\n", resultado);
        }
        [WebMethod]
        public string listar_usuario(string nombre_usuario)
        {
            // Creamos un filtro para buscar el usuario por nombre_usuario
            var filter = Builders<BsonDocument>.Filter.Eq("nombre_usuario", nombre_usuario);

            // Usamos un Proyección para excluir los campos _id y contra (contraseña)
            var projection = Builders<BsonDocument>.Projection
                .Exclude("_id")  // Excluimos el campo _id
                .Exclude("contra"); // Excluimos el campo contra (contraseña)

            // Buscamos el usuario con la proyección aplicada
            var usuario = _adminColeccion.Find(filter).Project(projection).FirstOrDefault();

            if (usuario != null)
            {
                // Convertimos el documento filtrado a una cadena para mostrarlo
                return usuario.ToString();
            }
            else
            {
                return "Usuario no encontrado";
            }
        }
        [WebMethod]
        public string borrar_usuario(string nombre_usuario)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("nombre_usuario", nombre_usuario);
            var result = _adminColeccion.DeleteOne(filter);

            return result.DeletedCount > 0 ? "Usuario eliminado con éxito" : "Usuario no encontrado";
        }
    }
}