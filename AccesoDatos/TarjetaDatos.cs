using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class TarjetaDatos
    {

        #region variables globales
        private ConexionDatos conexion = new ConexionDatos();
        #endregion

        #region metodos
        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 26/03/2020
        /// Efecto: Regresa la lista de tarjetas de la base de datos
        /// Requiere: -
        /// Modifica: -
        /// Devuelve: Lista de tarjetas
        /// </summary>
        /// <returns></returns>
        public List<Tarjeta> ObtenerTarjetas()
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Tarjeta> tarjetas = new List<Tarjeta>();
            String consulta = @"SELECT t.id_tarjeta, t.numeroTarjeta, t.disponible, t.tarjeta_extraviada, 
a.id_asistente, a.nombre_completo, a.carnet, a.telefono, a.cantidad_periodos_nombrado, t.pagada
FROM Tarjeta t LEFT JOIN Asistente a ON t.id_asistente = a.id_asistente;";
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Tarjeta tarjeta = new Tarjeta();
                Asistente asistente = new Asistente();
                if (!(reader["id_Asistente"] == System.DBNull.Value))
                {
                    asistente.idAsistente = Convert.ToInt32(reader["id_Asistente"]);
                    asistente.nombreCompleto = reader["nombre_completo"].ToString();
                    asistente.carnet = reader["carnet"].ToString();
                    asistente.cantidadPeriodosNombrado = Convert.ToInt32(reader["cantidad_periodos_nombrado"].ToString());
                }
                tarjeta.idTarjeta = Convert.ToInt32(reader["id_tarjeta"].ToString());
                tarjeta.numeroTarjeta = reader["numeroTarjeta"].ToString();
                tarjeta.disponible = Convert.ToBoolean(reader["disponible"]);
                tarjeta.tarjetaExtraviada = Convert.ToBoolean(reader["tarjeta_extraviada"]);
                if (tarjeta.tarjetaExtraviada)
                {
                    sqlCommand.Parameters.AddWithValue("@pagada", tarjeta.pagada);
                }

                tarjeta.asistente = asistente;
                tarjetas.Add(tarjeta);
            }
            sqlConnection.Close();
            return tarjetas;
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 26/03/2020
        /// Efecto : Elimina una tarjeta de la base de datos
        /// Requiere : Tarjeta que se desea eliminar
        /// Modifica : Tarjetas en la base de datos
        /// Devuelve : -
        /// </summary>
        /// <param name="tarjeta"></param>
        public void EliminarTarjeta(Tarjeta tarjeta)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            String consulta = "DELETE FROM Tarjeta WHERE id_tarjeta = @id;";
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", tarjeta.idTarjeta);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 26/03/2020
        /// Efecto : Modifica una tarjeta en la base de datos
        /// Requiere : Tarjeta con los datos actualizados
        /// Modifica : Tarjeta en la base de datos 
        /// Devuelve : -
        /// </summary>
        /// <param name="tarjeta"></param>
        public void ActualizarTarjeta(Tarjeta tarjeta)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            String consulta = "UPDATE Tarjeta " +
                "SET numeroTarjeta = @numeroTarjeta, " +
                "disponible = @disponible, " +
                "tarjeta_extraviada = @extraviada," +
                "id_asistente = @idAsistente, " +
                "pagada = @pagada " +
                "WHERE id_tarjeta = @id";
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", tarjeta.idTarjeta);
            sqlCommand.Parameters.AddWithValue("@numeroTarjeta", tarjeta.numeroTarjeta);
            sqlCommand.Parameters.AddWithValue("@disponible", tarjeta.disponible);
            sqlCommand.Parameters.AddWithValue("@extraviada", tarjeta.tarjetaExtraviada);
            if (tarjeta.tarjetaExtraviada)
            {
                sqlCommand.Parameters.AddWithValue("@pagada", tarjeta.pagada);
            }
            
            if (tarjeta.asistente != null)
                sqlCommand.Parameters.AddWithValue("@idAsistente", tarjeta.asistente.idAsistente);
            else
                sqlCommand.Parameters.AddWithValue("@idAsistente", DBNull.Value);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 26/03/2020
        /// Efecto : Guarda una tarjeta en la base de datos
        /// Requiere : Tarjeta que se desea guardar
        /// Modifica : Tarjetas en la base de datos
        /// Devuelve : -
        /// </summary>
        /// <<param name="tarjeta"></param>
        public void InsertarTarjeta(Tarjeta tarjeta)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            String consulta = "INSERT INTO Tarjeta (numeroTarjeta, disponible, tarjeta_extraviada, id_asistente, pagada) "
                + "VALUES (@numeroTarjeta, @disponible, @tarjetaExtraviada, @idAsistente, @pagada) ;";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@numeroTarjeta", tarjeta.numeroTarjeta);
            sqlCommand.Parameters.AddWithValue("@disponible", tarjeta.disponible);
            sqlCommand.Parameters.AddWithValue("@tarjetaExtraviada", tarjeta.tarjetaExtraviada);
            if (tarjeta.tarjetaExtraviada)
            {
                sqlCommand.Parameters.AddWithValue("@pagada", tarjeta.pagada);
            }
            else
            {
                sqlCommand.Parameters.AddWithValue("@pagada", DBNull.Value);
            }
            if(tarjeta.asistente != null)
                sqlCommand.Parameters.AddWithValue("@idAsistente", tarjeta.asistente.idAsistente);
            else
                sqlCommand.Parameters.AddWithValue("@idAsistente", DBNull.Value);
            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlConnection.Open();
            sqlCommand.ExecuteReader();
            sqlConnection.Close();
        }

        /// <summary>
        /// Mariela Calvo Mendez
        /// Mayo/2020
        /// Efecto: Regresa la lista de tarjetas de la base de datos
        /// Requiere: -
        /// Modifica: -
        /// Devuelve: Tarjeta de un asistente
        /// </summary>
        /// <returns></returns>
        public Tarjeta ObtenerTarjetaAsistente(int idAsistente)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            Tarjeta tarjeta = new Tarjeta();
            String consulta = @"SELECT t.id_tarjeta, t.numeroTarjeta, t.disponible, t.tarjeta_extraviada, 
                              a.id_asistente, a.nombre_completo, a.carnet, a.telefono, a.cantidad_periodos_nombrado, t.pagada
                              FROM Tarjeta t LEFT JOIN Asistente a ON t.id_asistente =@idAsistente;";
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idAsistente", idAsistente);
            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Asistente asistente = new Asistente();
                asistente.idAsistente = Convert.ToInt32(reader["id_Asistente"]);
                tarjeta.idTarjeta = Convert.ToInt32(reader["id_tarjeta"].ToString());
                tarjeta.numeroTarjeta = reader["numeroTarjeta"].ToString();
                tarjeta.asistente = asistente;
            }
            sqlConnection.Close();
            return tarjeta;
        }
        #endregion
    }

}
