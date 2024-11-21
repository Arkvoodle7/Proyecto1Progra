using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;
using System.Web.Services;
using System.Xml.Serialization;

namespace WSAdministracion
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WSA1 : WebService
    {
        private readonly IMongoCollection<BsonDocument> _adminColeccion;
        private readonly Encriptar _encriptador;

        public WSA1()
        {
            var conn = new MongoClient("mongodb://localhost:27017");
            var PagosMovAdmin = conn.GetDatabase("PagosMovilesAdministracion");
            _adminColeccion = PagosMovAdmin.GetCollection<BsonDocument>("Administradores");

            // Leer la clave secreta desde web.config
            string base64Key = ConfigurationManager.AppSettings["ClaveSecreta"];
            _encriptador = new Encriptar(base64Key);
        }

        [WebMethod]
        public LoginAdmin Login_Administradores(string usuario, string contra)
        {
            bool existe = ValidarAdmin(usuario, contra);

            LoginAdmin respuesta = new LoginAdmin
            {
                resultado = existe ? 0 : -1,
                mensaje = existe ? string.Empty : "Usuario y/o contraseña incorrectos"
            };
            return respuesta;
        }

        private bool ValidarAdmin(string _admin, string _contra)
        {
            var consulta = Builders<BsonDocument>.Filter.Eq("nombre_usuario", _admin);
            var admin = _adminColeccion.Find(consulta).FirstOrDefault();

            if (admin != null)
            {
                string contraEncriptada = admin["contra"].AsString;
                string contraDesencriptada = _encriptador.Decrypt(contraEncriptada);
                return contraDesencriptada == _contra;
            }
            return false;
        }
    }

    [XmlRoot("respuesta")]
    public class LoginAdmin
    {
        public int resultado { get; set; }
        public string mensaje { get; set; }
    }
}
