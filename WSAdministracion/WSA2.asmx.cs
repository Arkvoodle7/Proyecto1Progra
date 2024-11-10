using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Web.Services;

namespace WSAdministracion
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WSA2 : WebService
    {
        private readonly IMongoCollection<BsonDocument> _adminColeccion;

        public WSA2()
        {
            var conn = new MongoClient("mongodb://localhost:27017");
            var PagosMovAdmin = conn.GetDatabase("PagosMovilesAdministracion");
            _adminColeccion = PagosMovAdmin.GetCollection<BsonDocument>("Administradores");
        }

        [WebMethod]
        public string crear_usuario(string nombre_usuario, string nombre_completo, string contra)
        {
            var usuario = new BsonDocument
            {
                { "nombre_usuario", nombre_usuario },
                { "nombre_completo", nombre_completo },
                { "contra", contra }
            };

            _adminColeccion.InsertOne(usuario);

            return "Usuario creado con éxito";
        }

        [WebMethod]
        public string editar_usuario(string nombre_usuario, string nombre_completo, string contra)
        {
            var seleccion = Builders<BsonDocument>.Filter.Eq("nombre_usuario", nombre_usuario);
            var actualizar = Builders<BsonDocument>.Update
                .Set("nombre_completo", nombre_completo)
                .Set("contra", contra);

            var result = _adminColeccion.UpdateOne(seleccion, actualizar);

            return result.ModifiedCount > 0 ? "Usuario editado con éxito" : "Usuario no encontrado";
        }

        [WebMethod]
        public string borrar_usuario(string nombre_usuario)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("nombre_usuario", nombre_usuario);
            var result = _adminColeccion.DeleteOne(filter);

            return result.DeletedCount > 0 ? "Usuario eliminado con éxito" : "Usuario no encontrado";
        }
        [WebMethod]
        public string listar_usuarios()
        {
            //Obtener usuarios 
            var _usuarios = Builders<BsonDocument>.Projection
                .Exclude("_id")  
                .Exclude("contra"); 

            //Recuperar usuarios 
            var usuarios = _adminColeccion.Find(new BsonDocument()).Project(_usuarios).ToList();

            //Lista con los usuarios
            var lista_usuarios = new List<string>();

            foreach (var usuario in usuarios)
            {
                lista_usuarios.Add(usuario.ToString());
            }

            //Usuarios con saltos de línea
            return string.Join("\n", lista_usuarios);
        }

        [WebMethod]
        public string listar_usuario(string nombre_usuario)
        {
            var consulta = Builders<BsonDocument>.Filter.Eq("nombre_usuario", nombre_usuario);

            var _usuario = Builders<BsonDocument>.Projection
                .Exclude("_id")  
                .Exclude("contra");

            //Recuperar usuario
            var usuario = _adminColeccion.Find(consulta).Project(_usuario).FirstOrDefault();

            if (usuario != null)
            {
                return usuario.ToString();
            }
            else
            {
                return "Usuario no encontrado";
            }
        }
    }
}