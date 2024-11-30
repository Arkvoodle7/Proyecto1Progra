using Datos;
using System.Threading.Tasks;

namespace Negocios
{
    public class NegociosRegistro
    {
        private readonly DatosRegistro datosRegistro = new DatosRegistro();

        public async Task<bool> RegistrarUsuarioAsync(Usuario usuarioNegocios)
        {
            if (string.IsNullOrEmpty(usuarioNegocios.Identificacion) ||
                string.IsNullOrEmpty(usuarioNegocios.NombreUsuario) ||
                string.IsNullOrEmpty(usuarioNegocios.NombreCompleto) ||
                string.IsNullOrEmpty(usuarioNegocios.Contrasena) ||
                string.IsNullOrEmpty(usuarioNegocios.Telefono))
            {
                //no se puede registrar el usuario si hay datos incompletos
                return false;
            }

            //mapear Usuario de Negocios a Usuario de Datos
            var usuarioDatos = new Datos.Usuario
            {
                Identificacion = usuarioNegocios.Identificacion,
                NombreUsuario = usuarioNegocios.NombreUsuario,
                NombreCompleto = usuarioNegocios.NombreCompleto,
                Contrasena = usuarioNegocios.Contrasena,
                Telefono = usuarioNegocios.Telefono
            };

            //llamar al metodo de Datos para registrar el usuario
            return await datosRegistro.RegistrarUsuarioAsync(usuarioDatos);
        }
    }

    //definicion de la clase Usuario dentro del mismo archivo
    public class Usuario
    {
        public string Identificacion { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Contrasena { get; set; }
        public string Telefono { get; set; }
    }
}
