using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Datos;
using System.Data;
using static Datos.AD_UsuarioBD;
using Entidades;

namespace Negocio
{
    public class AD_Usuarios
    {
        // Clave fija de 16 bytes en hexadecimal
        private readonly byte[] key = Encoding.UTF8.GetBytes("1234567890abcdef");

        public void CrearUsuario(string identificacion, string nombreUsuario, string nombreCompleto, string contrasena, string telefono)
        {
            // Encripta la contraseña con la clave fija
            var (encryptedDataBase64, ivBase64, tagBase64) = Encriptacion_Usuario.Encrypt(contrasena, key);

            AD_UsuarioBD repo = new AD_UsuarioBD();
            repo.InsertUsuarios(identificacion, nombreUsuario, nombreCompleto, encryptedDataBase64, telefono);
        }

        public void ActualizarUsuario(string identificacion, string nombre_usuario, string nombre_completo, string contrasena, string telefono)
        {
            string encryptedPassword = null;

            if (!string.IsNullOrEmpty(contrasena))
            {
                var (encryptedData, _, _) = Encriptacion_Usuario.Encrypt(contrasena, key);
                encryptedPassword = encryptedData;
            }

            AD_UsuarioBD usuarioBD = new AD_UsuarioBD();
            usuarioBD.UpdateUsuarios(identificacion, nombre_usuario, nombre_completo, encryptedPassword, telefono);
        }

        public void EliminarUsuario(string identificacion)
        {
            AD_UsuarioBD usuarioDB = new AD_UsuarioBD();
            usuarioDB.DeleteUsuario(identificacion);
        }

        public List<Usuario> ListarUsuarios()
        {
            AD_UsuarioBD usuarioBD = new AD_UsuarioBD();

            return usuarioBD.MostrarUsuarios();
        }

        public Usuario ObtenerUsuario(string identificacion)
        {
            AD_UsuarioBD usuarioDB = new AD_UsuarioBD();
            return usuarioDB.MostrarSoloUsuario(identificacion);
        }

    }
}
