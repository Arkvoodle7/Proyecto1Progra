using Datos;
using System.Threading.Tasks;

namespace Negocios
{
    public class NegociosRegistro
    {
        private readonly DatosRegistro datosRegistro = new DatosRegistro();

        public async Task<bool> RegistrarUsuarioAsync(Usuario usuarioNegocios)
        {
            // Validaciones adicionales si es necesario
            if (string.IsNullOrEmpty(usuarioNegocios.Identificacion) ||
                string.IsNullOrEmpty(usuarioNegocios.NombreUsuario) ||
                string.IsNullOrEmpty(usuarioNegocios.NombreCompleto) ||
                string.IsNullOrEmpty(usuarioNegocios.Contrasena) ||
                string.IsNullOrEmpty(usuarioNegocios.Telefono))
            {
                // No se puede registrar el usuario debido a datos incompletos
                return false;
            }

            // Mapear Usuario de Negocios a Usuario de Datos
            var usuarioDatos = new Datos.Usuario
            {
                Identificacion = usuarioNegocios.Identificacion,
                NombreUsuario = usuarioNegocios.NombreUsuario,
                NombreCompleto = usuarioNegocios.NombreCompleto,
                Contrasena = usuarioNegocios.Contrasena,
                Telefono = usuarioNegocios.Telefono
            };

            // Llamar al método de Datos para registrar el usuario
            return await datosRegistro.RegistrarUsuarioAsync(usuarioDatos);
        }
    }

    // Definición de la clase Usuario dentro del mismo archivo
    public class Usuario
    {
        public string Identificacion { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Contrasena { get; set; }
        public string Telefono { get; set; }
    }
}
