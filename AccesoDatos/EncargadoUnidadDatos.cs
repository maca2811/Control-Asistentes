using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class EncargadoUnidadDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Mariela Calvo
        /// 04/nov/2019
        /// Efecto: inserta el encargado con su respectiva unidad
        /// Requiere: 
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        /// <param name="insertar"></param>
        /// <returns></returns>
        public void insertarEncargadoDeUnidad(Unidad unidad, Encargado encargado)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            SqlCommand sqlCommand = new SqlCommand("insert into Encargado_Unidad(id_unidad, id_encargado)  " +
                "values(@idUnidad_, @idEncargado_);SELECT SCOPE_IDENTITY();", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idUnidad_", unidad.idUnidad);
            sqlCommand.Parameters.AddWithValue("@idEncargado_",encargado.idEncargado);

            sqlConnection.Open();
            sqlCommand.ExecuteReader();

            sqlConnection.Close();

          
        }

        /// <summary>
        /// Mariela Calvo
        /// Noviembre/2019
        /// Efecto: método que elimina de la base de datos una asociación entre Registro y procedimiento
        /// Requiere: Procedimiento,  Registro
        /// Modifica: elimina  en la base de datos un registro de Encargado_Unidad
        /// Devuelve: -
        /// <param name="unidad"></param>
        /// <param name="encargado"></param>
        /// </summary>
        /// <returns></returns>
        public void eliminarUnidadEncargado(int idUnidad,int idEncargado)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            SqlCommand sqlCommand = new SqlCommand("DELETE FROM Encargado_Unidad WHERE id_unidad = @id_unidad and id_encargado = @id_encargado", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_unidad", idUnidad);
            sqlCommand.Parameters.AddWithValue("@id_encargado",idEncargado);
            sqlConnection.Open();
            sqlCommand.ExecuteReader();
            sqlConnection.Close();
        }



        public List<Encargado> listaEncargados()
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Encargado> encargados = new List<Encargado>();
            SqlCommand sqlCommand = new SqlCommand("SELECT id_encargado, nombre_completo from Encargado", sqlConnection);

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Encargado encargado = new Encargado();
                encargado.idEncargado = Convert.ToInt32(reader["id_encargado"].ToString());
                encargado.nombreCompleto = reader["nombre_completo"].ToString();
                encargados.Add(encargado);

            }
            sqlConnection.Close();

            return encargados;
        }
    }
}
