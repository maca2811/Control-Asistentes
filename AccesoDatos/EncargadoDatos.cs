using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class EncargadoDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Mariela Calvo
        /// 04/nov/2019
        /// Efecto: devuelve los encargados de las unidades
        /// Requiere: 
        /// Modifica: -
        /// Devuelve: encargados
        /// </summary>
        /// <param name="listaEncargados"></param>
        /// <returns></returns>
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
                encargado.idEncargado= Convert.ToInt32(reader["id_encargado"].ToString());
                encargado.nombreCompleto = reader["nombre_completo"].ToString();
                encargados.Add(encargado);

            }
            sqlConnection.Close();

            return encargados;
        }

       

       
    }
}
