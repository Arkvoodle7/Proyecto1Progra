using MySql.Data.MySqlClient;

class BitacoraHandler
{
    public static void RegistrarBitacora(string tramaRecibida, string tramaRespuesta)
    {
        string connectionString = "server=localhost;user=root;password=root;database=PagosMovilesReceptorExterno";
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO Bitacora (fecha_hora, trama_recibida, trama_respuesta) VALUES (@fecha_hora, @trama_recibida, @trama_respuesta)";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@fecha_hora", DateTime.Now);
                cmd.Parameters.AddWithValue("@trama_recibida", tramaRecibida);
                cmd.Parameters.AddWithValue("@trama_respuesta", tramaRespuesta);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
