using Entidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class AD_CuentasBD
    {
        // conexion
        private string connectionString = ConfigurationManager.ConnectionStrings["PagosMovilesBancario"].ConnectionString;

        private SqlConnection conexion;



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

        public void InsertCuentas(string numero_cuenta, string nombre_usuario, string tipo_cuenta, decimal saldo)
        {
            SqlCommand instruccionSQL;

            AbrirConexion();

            instruccionSQL = new SqlCommand("insert into Cuentas (numero_cuenta, nombre_usuario, tipo_cuenta, saldo) " +
                                            "values (@numero_cuenta, @nombre_usuario, @tipo_cuenta, @saldo)", conexion);

            instruccionSQL.Parameters.AddWithValue("@numero_cuenta", numero_cuenta);
            instruccionSQL.Parameters.AddWithValue("@nombre_usuario", nombre_usuario); 
            instruccionSQL.Parameters.AddWithValue("@tipo_cuenta", tipo_cuenta);
            instruccionSQL.Parameters.AddWithValue("@saldo", saldo);

            instruccionSQL.ExecuteNonQuery();

            CerrarConexion();
        }

        public List<Cuentas> MostrarCuentas(string nombre_usuario)
        {
            SqlCommand instruccionSQL;
            List<Cuentas> listaCuentas = new List<Cuentas>();

            try
            {
                Console.WriteLine("Abriendo conexión con la base de datos...");
                AbrirConexion();

                instruccionSQL = new SqlCommand("SELECT * FROM Cuentas WHERE nombre_usuario = @nombre_usuario", conexion);
                instruccionSQL.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);

                Console.WriteLine("Ejecutando consulta SQL...");
                SqlDataReader reader = instruccionSQL.ExecuteReader();

                while (reader.Read())
                {
                    listaCuentas.Add(new Cuentas
                    {
                        NumeroCuenta = reader["numero_cuenta"].ToString(),
                        NombreUsuario = reader["nombre_usuario"].ToString(),
                        TipoCuenta = reader["tipo_cuenta"].ToString(),
                        Saldo = Convert.ToDecimal(reader["saldo"])
                    });
                }

                reader.Close();
                Console.WriteLine($"Consulta SQL completada. Cuentas encontradas: {listaCuentas.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar las cuentas: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                throw new Exception("Error al consultar las cuentas: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
                Console.WriteLine("Conexión cerrada.");
            }

            return listaCuentas;
        }
        // Nuevo método para obtener el teléfono del usuario
        public string ObtenerTelefonoPorUsuario(string nombre_usuario)
        {
            string telefono = string.Empty;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand(
                    "SELECT Telefono FROM Usuarios WHERE nombre_usuario = @nombre_usuario", conexion);
                comando.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);

                object resultado = comando.ExecuteScalar();
                if (resultado != null)
                {
                    telefono = resultado.ToString();
                }
            }

            return telefono;
        }

        #endregion
    }
}
