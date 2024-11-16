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
        private readonly byte[] key = new byte[32]; // Genera o carga una clave segura

        public AD_Usuarios()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }
        }

        public void CrearUsuario(string identificacion, string nombreUsuario, string nombreCompleto, string contrasena, string telefono)
        {
            // contrasena encriptada con base64
            var (encryptedDataBase64, ivBase64, tagBase64) = Encriptacion_Usuario.Encrypt(contrasena, key);

            
            AD_UsuarioBD repo = new AD_UsuarioBD();
            repo.InsertUsuarios(identificacion, nombreUsuario, nombreCompleto, encryptedDataBase64, telefono);
        }

        public string ActualizarUsuario(string identificacion, string nombre_usuario, string nombre_completo, string contrasena, string telefono)
        {
            try
            {
                string encryptedPassword = null;

                //encripta la nueva contrasena
                if (!string.IsNullOrEmpty(contrasena))
                {
                    var (encryptedData, _, _) = Encriptacion_Usuario.Encrypt(contrasena, key);
                    encryptedPassword = encryptedData;
                }

                AD_UsuarioBD usuarioBD = new AD_UsuarioBD();
                usuarioBD.UpdateUsuarios(identificacion, nombre_usuario, nombre_completo, encryptedPassword, telefono);

                return "Usuario actualizado exitosamente.";
            }
            catch (Exception ex)
            {
                return $"Error al actualizar el usuario: {ex.Message}";
            }
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
