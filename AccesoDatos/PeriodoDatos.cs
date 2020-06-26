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
    /// Clase para administrar el CRUD para las Periodoes
    /// </summary>
    public class PeriodoDatos
    {
        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Obtiene todos los periodos de la base de datos
        /// </summary>
        /// <returns>Retorna una lista <code>LinkedList<Periodo></code> que contiene todos los periodos</returns>
        public List<Periodo> ObtenerPeriodos()
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Periodo> periodos = new List<Periodo>();
            SqlCommand sqlCommand = new SqlCommand("SELECT id_periodo,ano_periodo, habilitado,semestre FROM Periodo WHERE disponible=1; ", sqlConnection);

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Periodo periodo = new Periodo();
                periodo.idPeriodo = Convert.ToInt32(reader["id_periodo"].ToString());
                periodo.anoPeriodo = Convert.ToInt32(reader["ano_periodo"].ToString());
                periodo.habilitado = Convert.ToBoolean(reader["habilitado"].ToString());
                periodo.semestre = reader["semestre"].ToString();
                periodos.Add(periodo);
            }
            sqlConnection.Close();

            return periodos;
        }

        public Periodo ObtenerPeriodoPorId(int idPeriodo)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            Periodo periodo = new Periodo();
            SqlCommand sqlCommand = new SqlCommand("SELECT id_periodo,ano_periodo, habilitado,semestre FROM Periodo WHERE disponible=1 AND id_periodo=@id_periodo_; ", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_periodo_",idPeriodo);
            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                periodo.idPeriodo = Convert.ToInt32(reader["id_periodo"].ToString());
                periodo.anoPeriodo = Convert.ToInt32(reader["ano_periodo"].ToString());
                periodo.habilitado = Convert.ToBoolean(reader["habilitado"].ToString());
                periodo.semestre = reader["semestre"].ToString();
            }
            sqlConnection.Close();

            return periodo;
        }

        public Periodo ObtenerPeriodoActual()
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            SqlCommand sqlCommand = new SqlCommand("SELECT distinct id_periodo,ano_periodo, habilitado, semestre FROM Periodo WHERE habilitado=1; ", sqlConnection);
            Periodo periodo = new Periodo();
            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                periodo.anoPeriodo = Convert.ToInt32(reader["ano_periodo"].ToString());
                periodo.habilitado = Convert.ToBoolean(reader["habilitado"].ToString());
                periodo.semestre = reader["semestre"].ToString();
            }
            sqlConnection.Close();
            return periodo;
        }

        /// <summary>
        /// Primer deshabilita todos los periodos existentes, y habilita el periodo indicado según los criterios del usuario
        /// </summary>
        /// <param name="anoPeriodo">Valor de tipo <code>int</code> que representa el año del periodo que se desea habilitar</param>
        /// <returns>Retorna si el periodo fue habilitado</returns>
        public bool HabilitarPeriodo(Periodo periodo)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            bool habilitado = false;
            SqlCommand sqlCommandDeshabilitarTodos = new SqlCommand("update Periodo set habilitado=@habilitado_;", sqlConnection);
            sqlCommandDeshabilitarTodos.Parameters.AddWithValue("@habilitado_", 0);

            SqlCommand sqlCommandHabilitarPeriodo = new SqlCommand("update Periodo set habilitado=@habilitado_ output INSERTED.ano_periodo where ano_periodo=@ano_periodo_ and semestre=@semestre_;", sqlConnection);
            sqlCommandHabilitarPeriodo.Parameters.AddWithValue("@ano_periodo_", periodo.anoPeriodo);
            sqlCommandHabilitarPeriodo.Parameters.AddWithValue("@semestre_", periodo.semestre);
            sqlCommandHabilitarPeriodo.Parameters.AddWithValue("@habilitado_", 1);

            sqlConnection.Open();
            sqlCommandDeshabilitarTodos.ExecuteScalar();
            int idPeriodo = (int)sqlCommandHabilitarPeriodo.ExecuteScalar();

            if (idPeriodo > 0)
            {
                habilitado = true;
            }

            sqlConnection.Close();

            return habilitado;
        }

        /// <summary>
        /// Inserta la entidad Periodo en la base de datos
        /// </summary>
        /// <param name="periodo">Elemento de tipo <code>Periodo</code> que va a ser insertado</param>
        /// <returns>Retorna un valor <code>int</code> con el identificador del periodo insertado</returns>
        public int InsertarPeriodo(Periodo periodo)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            SqlCommand sqlCommand = new SqlCommand("insert into Periodo(ano_periodo, habilitado, semestre, disponible) output INSERTED.ano_periodo values(@ano_periodo_, @habilitado_, @semestre_,@disponible_);", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ano_periodo_", periodo.anoPeriodo);
            sqlCommand.Parameters.AddWithValue("@habilitado_", periodo.habilitado);
            sqlCommand.Parameters.AddWithValue("@semestre_", periodo.semestre);
            sqlCommand.Parameters.AddWithValue("@disponible_", 1);

            sqlConnection.Open();
            int anoPeriodo = 0;
            anoPeriodo = (int)sqlCommand.ExecuteScalar();
            sqlConnection.Close();

            return anoPeriodo;
        }

        // <summary>
        // Mariela Calvo
        // Marzo/2020
        // Efecto: Elimina un periodo de forma logica de la base de datos
        // Requiere: Periodo
        // Modifica: -
        // Devuelve: -
        // </summary>
        // <param name="idPeriodo"></param>
        public void EliminarPeriodo(int anoPeriodo)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();

            SqlCommand sqlCommand = new SqlCommand("update Periodo set disponible=0 where ano_periodo=@ano_periodo_;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ano_periodo_", anoPeriodo);

            sqlConnection.Open();

            sqlCommand.ExecuteScalar();

            sqlConnection.Close();
        }

    }
}
