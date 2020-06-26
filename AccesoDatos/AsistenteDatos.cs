using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{  /// <summary>
   /// Mariela Calvo    
   /// marzo/2020
   /// Clase para administrar el CRUD para las Asistentes
   /// </summary>
    public class AsistenteDatos
    {
        private ConexionDatos conexion = new ConexionDatos();




        /// <summary>
        /// Obtiene las Asistentes de la base de datos segun el periodo
        /// </summary>
        /// <returns>Retorna una lista <code>LinkedList<Asistente></code> que contiene las Asistentes</returns>
        public List<Asistente> ObtenerAsistentes()
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Asistente> asistentes = new List<Asistente>();

            String consulta = @"SELECT a.id_asistente, a.nombre_completo,a.carnet,a.telefono,n.aprobado, p.semestre,n.id_periodo, p.ano_periodo,n.cantidad_horas, a.cantidad_periodos_nombrado, u.nombre as unidad,  e.nombre_completo as nombre_encargado ,n.solicitud ,n.observaciones" +
            " FROM Asistente a JOIN Nombramiento n ON a.id_asistente=n.id_asistente JOIN Periodo p ON n.id_periodo=p.id_periodo JOIN Unidad u ON n.id_unidad=u.id_unidad JOIN Encargado_Unidad eu ON u.id_unidad=eu.id_unidad JOIN Encargado e ON e.id_encargado = eu.id_encargado where p.habilitado=1  AND a.disponible=1; ";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);


            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Asistente asistente = new Asistente();
                asistente.idAsistente = Convert.ToInt32(reader["id_Asistente"].ToString());
                asistente.nombreCompleto = reader["nombre_completo"].ToString();
                asistente.carnet = reader["carnet"].ToString();
                Periodo periodo = new Periodo();
                periodo.semestre = reader["semestre"].ToString();
                periodo.anoPeriodo = Convert.ToInt32(reader["ano_periodo"].ToString());
                periodo.idPeriodo = Convert.ToInt32(reader["id_periodo"].ToString());
                asistente.cantidadPeriodosNombrado = ObtenerCantidadAsistencias(asistente.idAsistente);
                Unidad unidad = new Unidad();
                unidad.nombre = reader["unidad"].ToString();
                Encargado encargado = new Encargado();
                encargado.nombreCompleto = reader["nombre_encargado"].ToString();
                unidad.encargado = encargado;
                asistentes.Add(asistente);
            }

            sqlConnection.Close();

            return asistentes;
        }

        public List<Asistente> ObtenerAsistentesPorUnidad(int idUnidad)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Asistente> asistentes = new List<Asistente>();

            String consulta = @"SELECT distinct a.id_asistente, a.nombre_completo,a.carnet,a.telefono,n.aprobado, p.semestre, p.ano_periodo,n.cantidad_horas,n.solicitud,n.observaciones, a.cantidad_periodos_nombrado, u.nombre as unidadA,  e.nombre_completo as nombre_encargado" +
            " FROM Asistente a JOIN Nombramiento n ON a.id_asistente=n.id_asistente JOIN Periodo p ON n.id_periodo=p.id_periodo JOIN Unidad u ON n.id_unidad=u.id_unidad JOIN Encargado_Unidad eu ON u.id_unidad=eu.id_unidad JOIN Encargado e ON e.id_encargado = eu.id_encargado" +
            " WHERE n.id_unidad=@id_unidad and p.habilitado=1  AND a.disponible=1; ";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_unidad", idUnidad);

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Asistente asistente = new Asistente();
                asistente.idAsistente = Convert.ToInt32(reader["id_asistente"].ToString());
                asistente.nombreCompleto = reader["nombre_completo"].ToString();
                asistente.carnet = reader["carnet"].ToString();
                Periodo periodo = new Periodo();
                periodo.semestre = reader["semestre"].ToString();
                periodo.anoPeriodo = Convert.ToInt32(reader["ano_periodo"].ToString());
                asistente.cantidadPeriodosNombrado = ObtenerCantidadAsistencias(asistente.idAsistente);
                Unidad unidad = new Unidad();
                unidad.nombre = reader["unidadA"].ToString();
                unidad.idUnidad = idUnidad;
                Encargado encargado = new Encargado();
                encargado.nombreCompleto = reader["nombre_encargado"].ToString();
                unidad.encargado = encargado;
                asistentes.Add(asistente);
            }

            sqlConnection.Close();

            return asistentes;
        }
        public List<Asistente> ObtenerAsistentesPorUnidad1(int idUnidad)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Asistente> asistentes = new List<Asistente>();

            String consulta = @"SELECT distinct a.id_asistente, a.nombre_completo,a.carnet,a.telefono, e.nombre_completo as nombre_encargado, u.nombre as unidadA,u.id_unidad 
                                FROM Asistente a JOIN Encargado_Asistente ea ON a.id_asistente=ea.id_asistente JOIN Encargado e ON e.id_encargado = ea.id_encargado JOIN Encargado_Unidad eu ON e.id_encargado=eu.id_encargado 
                                JOIN Unidad u ON eu.id_unidad=u.id_unidad WHERE eu.id_unidad=@id_unidad AND a.disponible=1; ";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_unidad", idUnidad);

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Asistente asistente = new Asistente();
                asistente.idAsistente = Convert.ToInt32(reader["id_asistente"].ToString());
                asistente.nombreCompleto = reader["nombre_completo"].ToString();
                asistente.carnet = reader["carnet"].ToString();
                
                
               
                
                Unidad unidad = new Unidad();
                unidad.nombre = reader["unidadA"].ToString();
                unidad.idUnidad = idUnidad;
                Encargado encargado = new Encargado();
                encargado.nombreCompleto = reader["nombre_encargado"].ToString();
                unidad.encargado = encargado;
                asistentes.Add(asistente);
            }

            sqlConnection.Close();

            return asistentes;
        }

        public Asistente ObtenerAsistentesPorID(int idAsistente)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Asistente> asistentes = new List<Asistente>();
            Asistente asistente = new Asistente();
            String consulta = @"SELECT distinct a.id_asistente, a.nombre_completo,a.carnet,a.telefono,n.aprobado, p.semestre, p.ano_periodo,n.cantidad_horas,n.solicitud,n.observaciones, a.cantidad_periodos_nombrado, u.nombre as unidadA,  e.nombre_completo as nombre_encargado" +
            " FROM Asistente a JOIN Nombramiento n ON a.id_asistente=n.id_asistente JOIN Periodo p ON n.id_periodo=p.id_periodo JOIN Unidad u ON n.id_unidad=u.id_unidad JOIN Encargado_Unidad eu ON u.id_unidad=eu.id_unidad JOIN Encargado e ON e.id_encargado = eu.id_encargado" +
            " WHERE n.id_unidad=@id_unidad and p.habilitado=1  AND a.disponible=1; ";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_asistente", idAsistente);

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                
                asistente.idAsistente = Convert.ToInt32(reader["id_asistente"].ToString());
                asistente.nombreCompleto = reader["nombre_completo"].ToString();
                asistente.carnet = reader["carnet"].ToString();
                Periodo periodo = new Periodo();
                periodo.semestre = reader["semestre"].ToString();
                periodo.anoPeriodo = Convert.ToInt32(reader["ano_periodo"].ToString());
                asistente.cantidadPeriodosNombrado = ObtenerCantidadAsistencias(asistente.idAsistente);
                Unidad unidad = new Unidad();
                unidad.nombre = reader["unidadA"].ToString();
                unidad.idUnidad =idAsistente;
                Encargado encargado = new Encargado();
                encargado.nombreCompleto = reader["nombre_encargado"].ToString();
                unidad.encargado = encargado;
                asistentes.Add(asistente);
            }

            sqlConnection.Close();

            return asistente;
        }
        public int insertarAsistente(Asistente asistente)
        {
            SqlConnection connection = conexion.ConexionControlAsistentes();

            String consulta
                = @"INSERT Asistente (nombre_completo,carnet,telefono,cantidad_periodos_nombrado) 
                    VALUES (@nombre,@carne,@telefono,@cantidadP);
                    SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(consulta, connection);
            command.Parameters.AddWithValue("@nombre", asistente.nombreCompleto);
            command.Parameters.AddWithValue("@carne", asistente.carnet);
            command.Parameters.AddWithValue("@cantidadP", asistente.cantidadPeriodosNombrado);

            connection.Open();
            int idAsistente = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return idAsistente;
        }

		/// <summary>
		/// Jesús Torres
		/// 02/abr/2020
		/// Efecto: Obtiene los asistentes que no tienen usuarios asociados 
		/// Requiere: - 
		/// Modifica: 
		/// Devuelve: Lista de asistentes 
		/// </summary>
		public List<Asistente> ObtenerAsistentesSinUsuarios()
		{
			SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
			List<Asistente> asistentes = new List<Asistente>();

			String consulta = @"SELECT id_Asistente,nombre_completo,carnet FROM Asistente WHERE id_asistente NOT IN (SELECT a.id_asistente FROM Asistente a JOIN Usuario u ON a.id_asistente = u.id_asistente)";

			SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);


			SqlDataReader reader;
			sqlConnection.Open();
			reader = sqlCommand.ExecuteReader();

			while (reader.Read())
			{
				Asistente asistente = new Asistente();
				asistente.idAsistente = Convert.ToInt32(reader["id_Asistente"].ToString());
				asistente.nombreCompleto = reader["nombre_completo"].ToString();
				asistente.carnet = reader["carnet"].ToString();
				asistentes.Add(asistente);
			}

			sqlConnection.Close();

			return asistentes;
		}


        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Obtiene los asistentes de acuerdo a su Unidad
        /// Requiere: - 
        /// Modifica: 
        /// Devuelve: Lista de asistentes 
        /// </summary>
        public List<Asistente> ObtenerAsistentesXUnidad(int idUnidad)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Asistente> asistentes = new List<Asistente>();

            String consulta = @"SELECT a.id_asistente, a.nombre_completo as nombreA, a.carnet, a.telefono, eu.id_unidad,u.nombre FROM Asistente a JOIN Encargado_Asistente ea ON a.id_asistente=ea.id_asistente "
                               + "JOIN Encargado e ON ea.id_encargado=e.id_encargado JOIN Encargado_Unidad eu ON e.id_encargado=eu.id_encargado JOIN Unidad u ON eu.id_unidad=u.id_unidad WHERE eu.id_unidad=@idUnidad AND a.disponible=1";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idUnidad", idUnidad);

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Asistente asistente = new Asistente();
                asistente.idAsistente = Convert.ToInt32(reader["id_Asistente"].ToString());
                asistente.nombreCompleto = reader["nombreA"].ToString();
                asistente.carnet = reader["carnet"].ToString();
                asistente.cantidadPeriodosNombrado = ObtenerCantidadAsistencias(asistente.idAsistente);
                asistente.telefono= reader["telefono"].ToString();
                Unidad unidad = new Unidad();
                unidad.idUnidad= Convert.ToInt32(reader["id_unidad"].ToString());
                unidad.nombre= reader["nombre"].ToString();
                asistente.unidad = unidad;
                asistentes.Add(asistente);
            }

            sqlConnection.Close();

            return asistentes;
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
        public void eliminarAsistente(int idAsistente)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            SqlCommand sqlCommand = new SqlCommand("UPDATE Asistente SET disponible=0 where id_asistente=@id_asistente_;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_asistente_", idAsistente);
            sqlConnection.Open();
            sqlCommand.ExecuteScalar();
            sqlConnection.Close();
        }
        // <summary>
        // Mariela Calvo
        // Abril/2019
        // Efecto: Actualiza un asistente de la base de datos
        // Requiere: Asistente
        // Modifica: -
        // Devuelve: -
        // </summary>
        // <param name="Unidad"></param>
        public void EditarAsistente(Asistente Asistente)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            SqlCommand sqlCommand = new SqlCommand("update Asistente set nombre_completo=@nombre_completo_, telefono=@telefono_,carnet=@carnet_ where id_asistente=@id_asistente_;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_asistente_", Asistente.idAsistente);
            sqlCommand.Parameters.AddWithValue("@nombre_completo_", Asistente.nombreCompleto);
            sqlCommand.Parameters.AddWithValue("@carnet_", Asistente.carnet);
            sqlConnection.Open();
            sqlCommand.ExecuteScalar();
            sqlConnection.Close();
        }
        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Obtiene los asistentes de acuerdo a su Unidad
        /// Requiere: - 
        /// Modifica: 
        /// Devuelve: Lista de asistentes 
        /// </summary>
        public List<Nombramiento> ObtenerAsistentesXUnidadSinNombrar(int idUnidad)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Nombramiento> nombramientos = new List<Nombramiento>();

           
            String consulta = @"SELECT n.id_nombramiento, a.id_asistente, a.nombre_completo,a.carnet,a.telefono,n.aprobado, p.semestre, p.ano_periodo,n.cantidad_horas, a.cantidad_periodos_nombrado, u.nombre as unidad,  e.nombre_completo as nombre_encargado " +
                              " FROM Asistente a JOIN Nombramiento n ON a.id_asistente=n.id_asistente JOIN Encargado_Asistente ea ON a.id_asistente=ea.id_asistente " +
                              " JOIN Encargado e ON ea.id_encargado=e.id_encargado JOIN Encargado_Unidad eu ON ea.id_encargado=eu.id_encargado JOIN Unidad u ON eu.id_unidad=u.id_unidad " +
                              " JOIN Periodo p ON n.id_periodo=p.id_periodo WHERE p.habilitado=1 AND n.solicitud=1 OR n.solicitud=0 AND n.id_unidad=@idUnidad AND a.disponible=1 AND n.disponible=1";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idUnidad", idUnidad);
            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {

                Nombramiento nombramiento = new Nombramiento();
                nombramiento.idNombramiento = Convert.ToInt32(reader["id_nombramiento"].ToString());
                nombramiento.aprobado = Convert.ToBoolean(reader["aprobado"].ToString());
                nombramiento.cantidadHorasNombrado = Convert.ToInt32(reader["cantidad_horas"].ToString());

                Asistente asistente = new Asistente();
                asistente.idAsistente = Convert.ToInt32(reader["id_asistente"].ToString());
                asistente.nombreCompleto = reader["nombre_completo"].ToString();
                asistente.carnet = reader["carnet"].ToString();
                asistente.cantidadPeriodosNombrado = ObtenerCantidadAsistencias(asistente.idAsistente);

                nombramiento.asistente = asistente;
                Periodo periodo = new Periodo();
                periodo.semestre = reader["semestre"].ToString();
                periodo.anoPeriodo = Convert.ToInt32(reader["ano_periodo"].ToString());
                nombramiento.periodo = periodo;

                Unidad unidad = new Unidad();
                unidad.nombre = reader["unidad"].ToString();
                unidad.idUnidad = idUnidad;
                Encargado encargado = new Encargado();
                encargado.nombreCompleto = reader["nombre_encargado"].ToString();
                unidad.encargado = encargado;

                nombramiento.unidad = unidad;
                nombramientos.Add(nombramiento);
            }
            sqlConnection.Close();
            return nombramientos;
        }
        /// <summary>
        /// Mariela Calvo   
        /// Abril/2020
        /// Efecto: Obtiene los asistentes que no tienen nombramientos asociados 
        /// Requiere: - 
        /// Modifica: 
        /// Devuelve: Lista de asistentes 
        /// </summary>
        public List<Asistente> ObtenerAsistentesSinNombramiento(int idUnidad)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Asistente> asistentes = new List<Asistente>();

            String consulta = @"SELECT a.id_asistente,a.nombre_completo,a.carnet FROM Asistente a JOIN Encargado_Asistente ea On a.id_asistente=ea.id_asistente "
                                + "JOIN Encargado_Unidad eu ON ea.id_encargado=eu.id_encargado WHERE eu.id_unidad=@idUnidad AND a.id_asistente NOT IN " +
                     "(SELECT a.id_asistente FROM Asistente a JOIN Nombramiento n ON a.id_asistente = n.id_asistente) AND a.disponible=1 ";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idUnidad", idUnidad);
            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Asistente asistente = new Asistente();
                asistente.idAsistente = Convert.ToInt32(reader["id_Asistente"].ToString());
                asistente.nombreCompleto = reader["nombre_completo"].ToString();
                asistente.carnet = reader["carnet"].ToString();
                asistentes.Add(asistente);
            }
            sqlConnection.Close();
            return asistentes;
        }

        // <summary>
        // Mariela Calvo
        // Abril/2019
        // Efecto: Actualiza un Nombramiento de la base de datos
        // Requiere: Nombramiento
        // Modifica: -
        // Devuelve: -
        // </summary>
        // <param name="Unidad"></param>
        private int ObtenerCantidadAsistencias(int idAsistente)
        {
            int asistencias = 0;
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Asistente> asistentes = new List<Asistente>();

            String consulta = @"SELECT count(id_asistente) as asistencias FROM Nombramiento WHERE id_asistente=@idAsistente";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idAsistente", idAsistente);

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                asistencias = Convert.ToInt32(reader["asistencias"].ToString());
            }

            sqlConnection.Close();

            return asistencias;
        }




    }

}
