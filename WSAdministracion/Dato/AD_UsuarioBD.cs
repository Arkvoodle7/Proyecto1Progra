using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Entidades;

namespace Datos
{
    public class AD_UsuarioBD
    {
        // conexion
        private string connectionString = ConfigurationManager.ConnectionStrings["PagosMovilesBancario"].ConnectionString;

        private SqlConnection conexion;

        DataSet mi_set = new DataSet();

        public DataTable TablaClientes { get => mi_set.Tables[0]; }


        #region 'Metodos'
        //Abre la bd
        private void AbrirConexion()
        {
            try
            {
                conexion = new SqlConnection(connectionString);
                conexion.Open();
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error al abrir conexion:" + ex.Message);
            }
        }
        //Cierra la bd
        private void CerrarConexion()
        {
            conexion.Close();
        }

        //agregar contrasena
        public void InsertUsuarios(string identificacion, string nombre_usuario, string nombre_completo, string contrasenaEncriptadaBase64, string telefono)
        {
            SqlCommand instruccionSQL;

            AbrirConexion();

            instruccionSQL = new SqlCommand("INSERT INTO Usuarios (identificacion, nombre_usuario, nombre_completo, contrasena, telefono) " +
                                            "VALUES (@identificacion, @nombre_usuario, @nombre_completo, @contrasenaEncriptadaBase64, @telefono)", conexion);

            instruccionSQL.Parameters.AddWithValue("@identificacion", identificacion);
            instruccionSQL.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);
            instruccionSQL.Parameters.AddWithValue("@nombre_completo", nombre_completo);
            instruccionSQL.Parameters.AddWithValue("@contrasenaEncriptadaBase64", contrasenaEncriptadaBase64);
            instruccionSQL.Parameters.AddWithValue("@telefono", telefono);

            instruccionSQL.ExecuteNonQuery();

            CerrarConexion();
        }

        //modificar bd
        public void UpdateUsuarios(string identificacion, string nombre_usuario, string nombre_completo, string contrasenaEncriptada, string telefono)
        {
            SqlCommand instruccionSQL;
            AbrirConexion();

            string query = "UPDATE Usuarios SET nombre_usuario = @nombre_usuario, nombre_completo = @nombre_completo, telefono = @telefono";

            if (contrasenaEncriptada != null)
            {
                query += ", contrasena = @contrasenaEncriptada";
            }

            query += " WHERE identificacion = @identificacion";

            instruccionSQL = new SqlCommand(query, conexion);

            instruccionSQL.Parameters.AddWithValue("@identificacion", identificacion);
            instruccionSQL.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);
            instruccionSQL.Parameters.AddWithValue("@nombre_completo", nombre_completo);
            if (contrasenaEncriptada != null)
            {
                instruccionSQL.Parameters.AddWithValue("@contrasenaEncriptada", contrasenaEncriptada);
            }
            instruccionSQL.Parameters.AddWithValue("@telefono", telefono);

            instruccionSQL.ExecuteNonQuery();
            CerrarConexion();
        }

        public void DeleteUsuario(string identificacion)
        {
            SqlCommand instruccionSQL;

            AbrirConexion();


            instruccionSQL = new SqlCommand("delete from Usuarios where identificacion = @identificacion", conexion);

            instruccionSQL.Parameters.AddWithValue("@identificacion", identificacion);

            instruccionSQL.ExecuteNonQuery();

            CerrarConexion();
        }

        //muestra todos los usuarios
        public List<Usuario> MostrarUsuarios()
        {
            SqlCommand instruccionSQL;
            List<Usuario> listaUsuarios = new List<Usuario>();

            AbrirConexion();

            instruccionSQL = new SqlCommand("select * from Usuarios", conexion);

            try
            {
                SqlDataReader reader = instruccionSQL.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        Identificacion = reader["identificacion"].ToString(),
                        NombreUsuario = reader["nombre_usuario"].ToString(),
                        NombreCompleto = reader["nombre_completo"].ToString(),
                        Contrasena = reader["contrasena"].ToString(),
                        Telefono = reader["telefono"].ToString()
                    };
                    listaUsuarios.Add(usuario);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error al leer los datos: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }

            return listaUsuarios;
        }

        //muestra solo un usuario
        public Usuario MostrarSoloUsuario(string identificacion)
        {
            SqlCommand instruccionSQL;
            Usuario usuario = null;

            AbrirConexion();

            instruccionSQL = new SqlCommand("select * from Usuarios where identificacion = @identificacion", conexion);
            instruccionSQL.Parameters.AddWithValue("@identificacion", identificacion);

            try
            {
                SqlDataReader reader = instruccionSQL.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        Identificacion = reader["identificacion"].ToString(),
                        NombreUsuario = reader["nombre_usuario"].ToString(),
                        NombreCompleto = reader["nombre_completo"].ToString(),
                        Contrasena = reader["contrasena"].ToString(),
                        Telefono = reader["telefono"].ToString()
                    };
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new System.Exception("Error al leer los datos: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }

            return usuario;
        }

        #endregion
    }
}
