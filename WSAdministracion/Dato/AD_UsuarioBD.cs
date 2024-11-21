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
        // conexion con bancario
        private string connectionStringBancario = ConfigurationManager.ConnectionStrings["PagosMovilesBancario"].ConnectionString;

        // conexion con usuario
        private string connectionStringUsuarios = ConfigurationManager.ConnectionStrings["PagosMovilesUsuarios"].ConnectionString;


        private SqlConnection conexionBancario;
        private SqlConnection conexionUsuarios;

        DataSet mi_set = new DataSet();

        public DataTable TablaClientes { get => mi_set.Tables[0]; }


        #region 'Metodos'

        //abrir y cerrar la conexión de Bancario
        private void AbrirConexionBancario()
        {
            conexionBancario = new SqlConnection(connectionStringBancario);
            conexionBancario.Open();
        }

        private void CerrarConexionBancario()
        {
            if (conexionBancario != null && conexionBancario.State == ConnectionState.Open)
                conexionBancario.Close();
        }

        //abrir y cerrar la conexión de Usuarios
        private void AbrirConexionUsuarios()
        {
            conexionUsuarios = new SqlConnection(connectionStringUsuarios);
            conexionUsuarios.Open();
        }

        private void CerrarConexionUsuarios()
        {
            if (conexionUsuarios != null && conexionUsuarios.State == ConnectionState.Open)
                conexionUsuarios.Close();
        }

        //agregar contrasena
        public void InsertUsuarios(string identificacion, string nombre_usuario, string nombre_completo, string contrasenaEncriptadaBase64, string telefono)
        {
            SqlCommand instruccionSQL;

            AbrirConexionBancario();

            instruccionSQL = new SqlCommand("insert into Usuarios (identificacion, nombre_usuario, nombre_completo, contrasena, telefono) " +
                                            "values (@identificacion, @nombre_usuario, @nombre_completo, @contrasenaEncriptadaBase64, @telefono)", conexionBancario);

            instruccionSQL.Parameters.AddWithValue("@identificacion", identificacion);
            instruccionSQL.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);
            instruccionSQL.Parameters.AddWithValue("@nombre_completo", nombre_completo);
            instruccionSQL.Parameters.AddWithValue("@contrasenaEncriptadaBase64", contrasenaEncriptadaBase64);
            instruccionSQL.Parameters.AddWithValue("@telefono", telefono);

            instruccionSQL.ExecuteNonQuery();

            //verifica que el usuario exista en clientes
            instruccionSQL = new SqlCommand("select count(*) from Clientes where identificacion = @identificacion", conexionBancario);
            instruccionSQL.Parameters.AddWithValue("@identificacion", identificacion);
            int count = (int)instruccionSQL.ExecuteScalar();

            //inserta en clientes
            if (count == 0)
            {
                instruccionSQL = new SqlCommand("insert into Clientes (identificacion, nombre_completo, telefono) " +
                                                "values (@identificacion, @nombre_completo, @telefono)", conexionBancario);

                instruccionSQL.Parameters.AddWithValue("@identificacion", identificacion);
                instruccionSQL.Parameters.AddWithValue("@nombre_completo", nombre_completo);
                instruccionSQL.Parameters.AddWithValue("@telefono", telefono);

                instruccionSQL.ExecuteNonQuery();
            }

            CerrarConexionBancario();


            // insert en PagosMovilesUsuarios
            AbrirConexionUsuarios();
            SqlCommand instruccionSQLUsuarios = new SqlCommand("insert into Usuarios (identificacion, nombre_usuario, nombre_completo, contrasena, telefono) " +
                                                               "values (@identificacion, @nombre_usuario, @nombre_completo, @contrasenaEncriptadaBase64, @telefono)", conexionUsuarios);

            instruccionSQLUsuarios.Parameters.AddWithValue("@identificacion", identificacion);
            instruccionSQLUsuarios.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);
            instruccionSQLUsuarios.Parameters.AddWithValue("@nombre_completo", nombre_completo);
            instruccionSQLUsuarios.Parameters.AddWithValue("@contrasenaEncriptadaBase64", contrasenaEncriptadaBase64);
            instruccionSQLUsuarios.Parameters.AddWithValue("@telefono", telefono);
            instruccionSQLUsuarios.ExecuteNonQuery();

            CerrarConexionUsuarios();
        }

        //modificar bd
        public void UpdateUsuarios(string identificacion, string nombre_usuario, string nombre_completo, string contrasenaEncriptada, string telefono)
        {
            SqlCommand instruccionSQL;
            AbrirConexionBancario();

            string query = "update Usuarios set nombre_usuario = @nombre_usuario, nombre_completo = @nombre_completo, telefono = @telefono";

            if (contrasenaEncriptada != null)
            {
                query += ", contrasena = @contrasenaEncriptada";
            }

            query += " where identificacion = @identificacion";

            instruccionSQL = new SqlCommand(query, conexionBancario);

            instruccionSQL.Parameters.AddWithValue("@identificacion", identificacion);
            instruccionSQL.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);
            instruccionSQL.Parameters.AddWithValue("@nombre_completo", nombre_completo);
            if (contrasenaEncriptada != null)
            {
                instruccionSQL.Parameters.AddWithValue("@contrasenaEncriptada", contrasenaEncriptada);
            }
            instruccionSQL.Parameters.AddWithValue("@telefono", telefono);

            instruccionSQL.ExecuteNonQuery();

            //actualiza clientes
            instruccionSQL = new SqlCommand("update clientes set nombre_completo = @nombre_completo, telefono = @telefono " +
                                             "where identificacion = @identificacion", conexionBancario);

            instruccionSQL.Parameters.AddWithValue("@identificacion", identificacion);
            instruccionSQL.Parameters.AddWithValue("@nombre_completo", nombre_completo);
            instruccionSQL.Parameters.AddWithValue("@telefono", telefono);

            instruccionSQL.ExecuteNonQuery();

            CerrarConexionBancario();

            // update en pagosmovilesusuarios
            AbrirConexionUsuarios();

            instruccionSQL = new SqlCommand(query, conexionUsuarios);
            instruccionSQL.Parameters.AddWithValue("@identificacion", identificacion);
            instruccionSQL.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);
            instruccionSQL.Parameters.AddWithValue("@nombre_completo", nombre_completo);

            if (contrasenaEncriptada != null)
            {
                instruccionSQL.Parameters.AddWithValue("@contrasenaencriptada", contrasenaEncriptada);
            }

            instruccionSQL.Parameters.AddWithValue("@telefono", telefono);

            instruccionSQL.ExecuteNonQuery();

            CerrarConexionUsuarios();

        }

        public void DeleteUsuario(string identificacion)
        {
            SqlCommand instruccionSQL;

            AbrirConexionUsuarios();


            instruccionSQL = new SqlCommand("delete from Usuarios where identificacion = @identificacion", conexionUsuarios);

            instruccionSQL.Parameters.AddWithValue("@identificacion", identificacion);

            instruccionSQL.ExecuteNonQuery();

            CerrarConexionUsuarios();
        }

        //muestra todos los usuarios
        public List<Usuario> MostrarUsuarios()
        {
            SqlCommand instruccionSQL;
            List<Usuario> listaUsuarios = new List<Usuario>();

            AbrirConexionBancario();

            instruccionSQL = new SqlCommand("select * from Usuarios", conexionBancario);

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
                CerrarConexionBancario();
            }

            return listaUsuarios;
        }

        //muestra solo un usuario
        public Usuario MostrarSoloUsuario(string identificacion)
        {
            SqlCommand instruccionSQL;
            Usuario usuario = null;

            AbrirConexionBancario();

            instruccionSQL = new SqlCommand("select * from Usuarios where identificacion = @identificacion", conexionBancario);
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
                CerrarConexionBancario();
            }

            return usuario;
        }

        #endregion
    }
}