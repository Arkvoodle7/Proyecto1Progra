using Datos;
using System.Threading.Tasks;

namespace Negocios
{
    public class NegociosLogin
    {
        private readonly DatosLogin datosLogin = new DatosLogin();

        public async Task<LoginResult> ValidarCredencialesAsync(string nombreUsuario, string contrasena)
        {
            var authResponse = await datosLogin.ValidarCredencialesAsync(nombreUsuario, contrasena);

            //mapear AuthResponse a LoginResult
            return new LoginResult
            {
                Resultado = authResponse.Resultado,
                Mensaje = authResponse.Mensaje
            };
        }
    }

    //clase generica para enviar datos a la GUI
    public class LoginResult
    {
        public int Resultado { get; set; }
        public string Mensaje { get; set; }
    }
}
