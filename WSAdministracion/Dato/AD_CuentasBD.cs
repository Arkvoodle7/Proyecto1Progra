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

        //muestra todas las cuentas de un usuario
        public List<Cuentas> MostrarCuentas(string nombre_usuario)
        {
            SqlCommand instruccionSQL;
            List<Cuentas> listaCuentas = new List<Cuentas>();

            AbrirConexion();

            instruccionSQL = new SqlCommand("select * from Cuentas where nombre_usuario = @nombre_usuario", conexion);
            instruccionSQL.Parameters.AddWithValue("@nombre_usuario", nombre_usuario);

            try
            {
                SqlDataReader reader = instruccionSQL.ExecuteReader();

                while (reader.Read())
                {
                    Cuentas cuenta = new Cuentas
                    {
                        NumeroCuenta = reader["numero_cuenta"].ToString(),
                        NombreUsuario = reader["nombre_usuario"].ToString(),
                        TipoCuenta = reader["tipo_cuenta"].ToString(),
                        Saldo = Convert.ToDecimal(reader["saldo"])
                    };
                    listaCuentas.Add(cuenta);
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

            return listaCuentas;
        }

        public List<Cuentas> MostrarTodasCuentas()
        {
            SqlCommand instruccionSQL;
            List<Cuentas> listaCuentas = new List<Cuentas>();

            AbrirConexion();

            instruccionSQL = new SqlCommand("select * from Cuentas", conexion);

            try
            {
                SqlDataReader reader = instruccionSQL.ExecuteReader();

                while (reader.Read())
                {
                    Cuentas cuenta = new Cuentas
                    {
                        NumeroCuenta = reader["numero_cuenta"].ToString(),
                        NombreUsuario = reader["nombre_usuario"].ToString(),
                        TipoCuenta = reader["tipo_cuenta"].ToString(),
                        Saldo = Convert.ToDecimal(reader["saldo"])
                    };
                    listaCuentas.Add(cuenta);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer los datos: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }

            return listaCuentas;
        }


        public string ObtenerNombreUsuarioPorCedula(string cedula)
        {
            string nombreUsuario = string.Empty;

            try
            {
                AbrirConexion();

                var comando = new SqlCommand("select nombre_usuario from Usuarios where identificacion = @cedula", conexion);
                comando.Parameters.AddWithValue("@cedula", cedula);

                var resultado = comando.ExecuteScalar();
                nombreUsuario = resultado?.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar el usuario por cedula: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }

            return nombreUsuario;
        }

        #endregion
    }
}
