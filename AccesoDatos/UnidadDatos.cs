using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    /// <summary>
    /// Mariela Calvo
    /// 27/noviembre/2019
    /// Clase para administrar el CRUD para las unidades
    /// </summary>
    public class UnidadDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        public List<Unidad> ObtenerUnidades()
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Unidad> unidades = new List<Unidad>();
            SqlCommand sqlCommand = new SqlCommand("SELECT U.id_unidad, U.nombre, U.descripcion,E.nombre_completo FROM Unidad U JOIN Encargado_Unidad EU ON U.id_unidad=EU.id_unidad"
                + " JOIN Encargado E ON EU.id_encargado = E.id_encargado WHERE disponible=1; ", sqlConnection);

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Unidad unidad = new Unidad();
                unidad.idUnidad = Convert.ToInt32(reader["id_unidad"].ToString());
                unidad.nombre = reader["nombre"].ToString();
                unidad.descripcion = reader["descripcion"].ToString();
                Encargado encargado = new Encargado();
                encargado.nombreCompleto = reader["nombre_completo"].ToString();
                unidad.encargado = encargado;
                unidades.Add(unidad);
            }
            sqlConnection.Close();

            return unidades;
        }


        /// <summary>
        /// Mariela Calvo
        /// 27/noviembre/2019
        /// Metodo para insertar nuevas unidades
        /// </summary>
        public int InsertarUnidad(Unidad unidad)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            SqlCommand sqlCommand = new SqlCommand("insert into Unidad(nombre, descripcion) output INSERTED.id_unidad " +
                "values(@nombre_, @descripcion_);", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@nombre_", unidad.nombre);
            sqlCommand.Parameters.AddWithValue("@descripcion_", unidad.descripcion);
          
            sqlConnection.Open();
            int idUnidad = (int)sqlCommand.ExecuteScalar();

            sqlConnection.Close();

            return idUnidad;
        }
        // <summary>
        // Mariela Calvo
        // Diciembre/2019
        // Efecto: Retorna la información de la unidad de acuerdo a su id
        // Requiere: Unidad
        // Modifica: -
        // Devuelve: -
        // </summary>
        // <param name="idUnidad"></param>
        public Unidad ObtenerUnidadPorId(int idUnidad)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            Unidad unidad = new Unidad();
            SqlCommand sqlCommand = new SqlCommand("SELECT U.id_unidad, U.nombre, U.descripcion,E.id_encargado,E.nombre_completo FROM Unidad U JOIN Encargado_Unidad EU ON U.id_unidad=EU.id_unidad"
                + " JOIN Encargado E ON EU.id_encargado = E.id_encargado WHERE U.id_unidad=@idUnidad_; ", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idUnidad_",idUnidad);
            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                unidad.idUnidad = Convert.ToInt32(reader["id_unidad"].ToString());
                unidad.nombre = reader["nombre"].ToString();
                unidad.descripcion = reader["descripcion"].ToString();
                Encargado encargado = new Encargado();
                encargado.idEncargado= Convert.ToInt32(reader["id_encargado"].ToString());
                encargado.nombreCompleto = reader["nombre_completo"].ToString();
                unidad.encargado = encargado;
              
            }
            sqlConnection.Close();

            return unidad;
        }

        // <summary>
        // Mariela Calvo
        // Noviembre/2019
        // Efecto: Elimina una unidad de la base de datos
        // Requiere: Unidad
        // Modifica: -
        // Devuelve: -
        // </summary>
        // <param name="idUnidad"></param>
        public void EliminarUnidad(int idUnidad)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();

            SqlCommand sqlCommand = new SqlCommand("UPDATE Unidad SET disponible=0 where id_unidad=@id_unidad_;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_unidad_", idUnidad);

            sqlConnection.Open();

            sqlCommand.ExecuteScalar();

            sqlConnection.Close();
        }

        // <summary>
        // Mariela Calvo
        // Noviembre/2019
        // Efecto: Actualiza una unidad de la base de datos
        // Requiere: Unidad
        // Modifica: -
        // Devuelve: -
        // </summary>
        // <param name="Unidad"></param>
        public void EditarUnidad(Unidad unidad)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();

            SqlCommand sqlCommand = new SqlCommand("update Unidad set nombre=@nombre_unidad_, descripcion=@descripcion_ where id_unidad=@id_unidad_;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_unidad_", unidad.idUnidad);
            sqlCommand.Parameters.AddWithValue("@nombre_unidad_", unidad.nombre);
            sqlCommand.Parameters.AddWithValue("@descripcion_", unidad.descripcion);

            sqlConnection.Open();

            sqlCommand.ExecuteScalar();

            sqlConnection.Close();
        }

        // <summary>
        // Mariela Calvo
        // Diciembre/2019
        // Efecto: Retorna la información de la unidad de acuerdo a su id
        // Requiere: Unidad
        // Modifica: -
        // Devuelve: -
        // </summary>
        // <param name="idUnidad"></param>
        public Unidad ObtenerUnidadPorNombreEncargado(String nombreEncargado)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            Unidad unidad = new Unidad();
            SqlCommand sqlCommand = new SqlCommand("SELECT U.id_unidad, U.nombre, U.descripcion,E.id_encargado,E.nombre_completo FROM Unidad U JOIN Encargado_Unidad EU ON U.id_unidad=EU.id_unidad"
                + " JOIN Encargado E ON EU.id_encargado = E.id_encargado WHERE E.nombre_completo=@nombreEncargado_; ", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@nombreEncargado_",nombreEncargado);
            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                unidad.idUnidad = Convert.ToInt32(reader["id_unidad"].ToString());
                unidad.nombre = reader["nombre"].ToString();
                unidad.descripcion = reader["descripcion"].ToString();
                Encargado encargado = new Encargado();
                encargado.idEncargado = Convert.ToInt32(reader["id_encargado"].ToString());
                encargado.nombreCompleto = reader["nombre_completo"].ToString();
                unidad.encargado = encargado;

            }
            sqlConnection.Close();

            return unidad;
        }
    }

}
