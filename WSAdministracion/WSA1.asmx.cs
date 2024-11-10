using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Web.Services;
using System.Xml.Serialization;

namespace WSAdministracion
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WSA1 : System.Web.Services.WebService
    {
        //variable para la colección de mongo, se asigna una vez por clase
        private readonly IMongoCollection<BsonDocument> _adminColeccion;

        //Constructor
        public WSA1()
        {
            //Base de datos
            var conn = new MongoClient("mongodb://localhost:27017");
            var PagosMovAdmin = conn.GetDatabase("PagosMovilesAdministracion");
            _adminColeccion = PagosMovAdmin.GetCollection<BsonDocument>("Administradores");
        }

        [WebMethod] //Inicio de sesión
        public LoginAdmin Login_Administradores(string usuario, string contra)
        {
            bool existe = ValidarAdmin(usuario, contra);

            LoginAdmin respuesta = new LoginAdmin();

            if (existe)
            {
                respuesta.resultado = 0;
                respuesta.mensaje = string.Empty;
            }
            else
            {
                respuesta.resultado = -1;
                respuesta.mensaje = "Usuario y/o contraseña incorrectos";
            }
            return respuesta;
        }

        //Validar admin
        private bool ValidarAdmin(string _admin, string _contra)
        {
            var consulta = Builders<BsonDocument>.Filter.Eq("nombre_usuario", _admin); //hacer filtro 
            var admin = _adminColeccion.Find(consulta).FirstOrDefault(); //obtener el administrador

            // Validación de usuario y contraseña
            if (admin != null)
            {
                string contrasena = admin["contra"].AsString; //extraer contraseña

                //Lógica de descifrado debajo del comentario
                return contrasena == Cifrar(_contra);
            }
            return false;
        }

        private string Cifrar(string password)
        {
            //Lógica de cifrado debajo del comentario
            return password;
        }
    }

    //Convertir el objeto a XML a
    [XmlRoot("respuesta")]
    public class LoginAdmin
    {
        public int resultado { get; set; }
        public string mensaje { get; set; }
    }
}