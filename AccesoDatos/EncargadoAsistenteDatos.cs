using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class EncargadoAsistenteDatos
    {
        private ConexionDatos conexion = new ConexionDatos();


        public int insertarEncargadoAsistente(int idEncargado, int idAsistente)
        {
            SqlConnection connection = conexion.ConexionControlAsistentes();

            String consulta
                = @"INSERT Encargado_Asistente(id_Encargado,id_asistente) 
                    VALUES (@idEncargado,@idAsistente);";

            SqlCommand command = new SqlCommand(consulta, connection);
            command.Parameters.AddWithValue("@idEncargado", idEncargado);
            command.Parameters.AddWithValue("@idAsistente", idAsistente);
            connection.Open();
            command.ExecuteScalar();

            connection.Close();

            return idEncargado;
        }


    }
}
