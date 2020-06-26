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
    /// marzo/2020
    /// Clase para administrar el CRUD para las Nombramientos
    /// </summary>
    public class NombramientoAsistenteDatos
    {
        private ConexionDatos conexion = new ConexionDatos();
        ArchivoDatos archivoDatos = new ArchivoDatos();



        public int insertarNombramientoAsistente(Nombramiento nombramiento)
        {
            SqlConnection connection = conexion.ConexionControlAsistentes();

            String consulta
                = @"INSERT Nombramiento (id_asistente,id_periodo,id_unidad, aprobado, cantidad_horas,induccion) 
                    VALUES (@asistente,@periodo,@unidad,@aprobado,@horas,@induccion);
                    SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(consulta, connection);
            command.Parameters.AddWithValue("@asistente", nombramiento.asistente.idAsistente);
            command.Parameters.AddWithValue("@periodo", nombramiento.periodo.idPeriodo);
            command.Parameters.AddWithValue("@unidad", nombramiento.unidad.idUnidad);
            command.Parameters.AddWithValue("@aprobado", nombramiento.aprobado);
            command.Parameters.AddWithValue("@horas", nombramiento.cantidadHorasNombrado);
            command.Parameters.AddWithValue("@induccion", nombramiento.recibeInduccion);

            connection.Open();
            int idNombramiento = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return idNombramiento;
        }

        public void actualizarAsistenteNombramiento(string numeroCarnet, int aprobado, string observaciones,int solicitud)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();

            String consulta = @"update Nombramiento set aprobado=@aprobado,observaciones=@observaciones,solicitud=@solicitud
                                 from Nombramiento N,Asistente A
                                 where N.id_asistente=A.id_asistente and A.carnet=@numeroCarnet";


            SqlCommand command = new SqlCommand(consulta, sqlConnection);
            command.Parameters.AddWithValue("@aprobado", aprobado);
            command.Parameters.AddWithValue("@numeroCarnet", numeroCarnet);
            command.Parameters.AddWithValue("@observaciones", observaciones);
            command.Parameters.AddWithValue("@solicitud", solicitud);




            sqlConnection.Open();
            command.ExecuteReader();
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
        public List<Nombramiento> ObtenerNombramientosPorUnidad(int idUnidad)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Nombramiento> nombramientos = new List<Nombramiento>();

            String consulta = @"SELECT p.id_periodo, n.solicitud,n.induccion, n.id_nombramiento, a.id_asistente, a.nombre_completo,a.carnet,a.telefono, n.aprobado, p.semestre, p.ano_periodo,n.cantidad_horas, a.cantidad_periodos_nombrado, u.nombre as unidad,  e.nombre_completo as nombre_encargado " +
                              " FROM Asistente a JOIN Nombramiento n ON a.id_asistente=n.id_asistente JOIN Encargado_Asistente ea ON a.id_asistente=ea.id_asistente " +
                              " JOIN Encargado e ON ea.id_encargado=e.id_encargado JOIN Encargado_Unidad eu ON ea.id_encargado=eu.id_encargado JOIN Unidad u ON eu.id_unidad=u.id_unidad " +
                               " JOIN Periodo p ON n.id_periodo=p.id_periodo WHERE p.habilitado=1 AND n.id_unidad=@idUnidad  AND a.disponible=1 AND n.disponible=1 ";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idUnidad", idUnidad);
     

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {

                Nombramiento nombramiento = new Nombramiento();
                nombramiento.idNombramiento = Convert.ToInt32(reader["id_nombramiento"].ToString());
                nombramiento.recibeInduccion = Convert.ToBoolean(reader["induccion"].ToString());
                nombramiento.aprobado = Convert.ToBoolean(reader["aprobado"].ToString());
                nombramiento.cantidadHorasNombrado = Convert.ToInt32(reader["cantidad_horas"].ToString());
                nombramiento.solicitud = Convert.ToInt32(reader["solicitud"].ToString());


                Asistente asistente = new Asistente();
                asistente.idAsistente = Convert.ToInt32(reader["id_asistente"].ToString());
                asistente.nombreCompleto = reader["nombre_completo"].ToString();
                asistente.carnet = reader["carnet"].ToString();
                asistente.cantidadPeriodosNombrado = ObtenerCantidadAsistencias(asistente.idAsistente);

                nombramiento.asistente = asistente;


                Periodo periodo = new Periodo();
                periodo.idPeriodo= periodo.anoPeriodo = Convert.ToInt32(reader["id_periodo"].ToString());
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

                List<Archivo> archivos = new List<Archivo>();
                archivos = archivoDatos.getArchivosAsistente(asistente.idAsistente, periodo.idPeriodo);

                nombramiento.listaArchivos = archivos;
                nombramientos.Add(nombramiento);
            }

            sqlConnection.Close();

            return nombramientos;
        }

        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Obtiene los nombramientos de los asistentes 
        /// Requiere: - 
        /// Modifica: 
        /// Devuelve: Lista de nombramiento
        /// </summary>
        public List<Nombramiento> ObtenerNombramientosPorPeriodo(int idPeriodo, int idUnidad)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Nombramiento> nombramientos = new List<Nombramiento>();

            String consulta = @"SELECT n.solicitud,u.id_unidad, n.induccion, n.id_nombramiento, a.id_asistente, a.nombre_completo,a.carnet,a.telefono, n.aprobado, p.semestre, p.ano_periodo,n.cantidad_horas, a.cantidad_periodos_nombrado, u.nombre as unidad,  e.nombre_completo as nombre_encargado " +
                              " FROM Asistente a JOIN Nombramiento n ON a.id_asistente=n.id_asistente JOIN Encargado_Asistente ea ON a.id_asistente=ea.id_asistente " +
                              " JOIN Encargado e ON ea.id_encargado=e.id_encargado JOIN Encargado_Unidad eu ON ea.id_encargado=eu.id_encargado JOIN Unidad u ON eu.id_unidad=u.id_unidad " +
                               " JOIN Periodo p ON n.id_periodo=p.id_periodo WHERE n.disponible=1 AND n.id_periodo=@idPeriodo ";

            if (idUnidad!=0)
            {
                consulta += " AND n.id_unidad = @idUnidad";
            }
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idPeriodo", idPeriodo);
            sqlCommand.Parameters.AddWithValue("@idUnidad", idUnidad);

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {

                Nombramiento nombramiento = new Nombramiento();
                nombramiento.idNombramiento = Convert.ToInt32(reader["id_nombramiento"].ToString());
                nombramiento.recibeInduccion = Convert.ToBoolean(reader["induccion"].ToString());
                nombramiento.aprobado = Convert.ToBoolean(reader["aprobado"].ToString());
                nombramiento.cantidadHorasNombrado = Convert.ToInt32(reader["cantidad_horas"].ToString());
                nombramiento.solicitud = Convert.ToInt32(reader["solicitud"].ToString());

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
                unidad.idUnidad = Convert.ToInt32(reader["id_unidad"].ToString()); ;

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
        /// Efecto: Obtiene los nombramientos de los asistentes 
        /// Requiere: - 
        /// Modifica: 
        /// Devuelve: Lista de nombramiento
        /// </summary>
        public List<Nombramiento> ObtenerNombramientos()
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Nombramiento> nombramientos = new List<Nombramiento>();

            String consulta = @"SELECT n.solicitud,u.id_unidad, n.induccion, n.id_nombramiento, a.id_asistente, a.nombre_completo,a.carnet,a.telefono, n.aprobado, p.semestre, p.ano_periodo,n.cantidad_horas, a.cantidad_periodos_nombrado, u.nombre as unidad,  e.nombre_completo as nombre_encargado " +
                              " FROM Asistente a JOIN Nombramiento n ON a.id_asistente=n.id_asistente JOIN Encargado_Asistente ea ON a.id_asistente=ea.id_asistente " +
                              " JOIN Encargado e ON ea.id_encargado=e.id_encargado JOIN Encargado_Unidad eu ON ea.id_encargado=eu.id_encargado JOIN Unidad u ON eu.id_unidad=u.id_unidad " +
                               " JOIN Periodo p ON n.id_periodo=p.id_periodo WHERE p.habilitado=1 AND n.disponible=1 ";
           
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {

                Nombramiento nombramiento = new Nombramiento();
                nombramiento.idNombramiento = Convert.ToInt32(reader["id_nombramiento"].ToString());
                nombramiento.recibeInduccion = Convert.ToBoolean(reader["induccion"].ToString());
                nombramiento.aprobado = Convert.ToBoolean(reader["aprobado"].ToString());
                nombramiento.cantidadHorasNombrado = Convert.ToInt32(reader["cantidad_horas"].ToString());
                nombramiento.solicitud= Convert.ToInt32(reader["solicitud"].ToString());

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
                unidad.idUnidad = Convert.ToInt32(reader["id_unidad"].ToString()); ;

                Encargado encargado = new Encargado();
                encargado.nombreCompleto = reader["nombre_encargado"].ToString();
                unidad.encargado = encargado;

                nombramiento.unidad = unidad;
                nombramientos.Add(nombramiento);
            }

            sqlConnection.Close();

            return nombramientos;
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
        public void eliminarNombramiento(int idNombramiento)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();

            SqlCommand sqlCommand = new SqlCommand("UPDATE Nombramiento SET disponible=0 where id_nombramiento=@id_nombramiento_;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_nombramiento_", idNombramiento);

            sqlConnection.Open();

            sqlCommand.ExecuteScalar();

            sqlConnection.Close();
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
        public void EditarNombramiento(Nombramiento nombramiento)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();

            SqlCommand sqlCommand = new SqlCommand("update Nombramiento set cantidad_horas=@horas_, induccion=@induccion_ where id_nombramiento=@id_nombramiento_;", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id_nombramiento_", nombramiento.idNombramiento);
            sqlCommand.Parameters.AddWithValue("@induccion_", nombramiento.recibeInduccion);
            sqlCommand.Parameters.AddWithValue("@horas_", nombramiento.cantidadHorasNombrado);

            sqlConnection.Open();

            sqlCommand.ExecuteScalar();

            sqlConnection.Close();
        }


        public Nombramiento ObtenerDetallesNombramiento(int idNombramiento)
        {
            Nombramiento nombramiento = new Nombramiento();
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();


            String consulta = @"SELECT n.solicitud,n.observaciones, a.id_asistente, a.nombre_completo, n.aprobado FROM Nombramiento n
                                JOIN Asistente a ON n.id_asistente=a.id_asistente WHERE n.disponible=1 AND a.disponible=1 AND n.id_nombramiento=@idNombramiento";

            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idNombramiento", idNombramiento);

            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                nombramiento.idNombramiento = idNombramiento;
                nombramiento.observaciones = reader["observaciones"].ToString();
                nombramiento.aprobado = Convert.ToBoolean(reader["aprobado"].ToString());
                nombramiento.solicitud = Convert.ToInt32(reader["solicitud"].ToString());
                Asistente asistente = new Asistente();
                asistente.idAsistente = Convert.ToInt32(reader["id_asistente"].ToString());
                asistente.nombreCompleto = reader["nombre_completo"].ToString();
                asistente.cantidadPeriodosNombrado = ObtenerCantidadAsistencias(asistente.idAsistente);
                nombramiento.asistente = asistente;
                nombramiento.solicitud = Convert.ToInt32(reader["solicitud"].ToString());

            }

            sqlConnection.Close();

            return nombramiento;
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

        /// <summary>
        /// Mariela Calvo
        /// Mayo/2020
        /// Efecto: Obtiene los asistentes de acuerdo a su Unidad
        /// Requiere: - 
        /// Modifica: 
        /// Devuelve: Lista de asistentes 
        /// </summary>
        public List<Nombramiento> ObtenerNombramientosReporte(int idUnidad,int idPeriodo)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<Nombramiento> nombramientos = new List<Nombramiento>();

            String consulta = @"SELECT a.id_asistente, a.nombre_completo,a.carnet,a.cantidad_periodos_nombrado, ea.id_encargado,e.nombre_completo as encargado, eu.id_unidad,u.nombre,
                                p.id_periodo, p.semestre, p.ano_periodo, n.id_nombramiento, n.cantidad_horas FROM Asistente a
                                JOIN Encargado_Asistente ea ON a.id_asistente=ea.id_asistente
                                JOIN Encargado_Unidad eu ON ea.id_encargado=eu.id_encargado
                                JOIN Encargado e ON ea.id_encargado=e.id_encargado
                                JOIN Unidad u ON eu.id_unidad=u.id_unidad
                                JOIN Nombramiento n ON a.id_asistente = n.id_asistente
                                JOIN Periodo p ON n.id_periodo=p.id_periodo  ";
            if (idUnidad!=0 && idPeriodo!=0)
            {
                consulta += "WHERE n.solicitud=1 AND p.id_periodo=@idPeriodo AND eu.id_unidad=@idUnidad " +
                            "GROUP BY  p.id_periodo, a.id_asistente, a.nombre_completo,a.carnet,a.cantidad_periodos_nombrado, ea.id_encargado,e.nombre_completo, " +
                            "eu.id_unidad,u.nombre, p.semestre, p.ano_periodo, n.id_nombramiento, n.cantidad_horas";
            }
            else if (idUnidad!=0)
            {
                consulta += "WHERE n.solicitud=1 AND eu.id_unidad=@idUnidad " +
                           "GROUP BY  p.id_periodo, a.id_asistente, a.nombre_completo,a.carnet,a.cantidad_periodos_nombrado, ea.id_encargado,e.nombre_completo, " +
                           "eu.id_unidad,u.nombre, p.semestre, p.ano_periodo, n.id_nombramiento, n.cantidad_horas";
            }
            else if (idPeriodo!=0)
            {
                consulta+= "WHERE n.solicitud=1 AND  p.id_periodo=@idPeriodo " +
                          "GROUP BY  p.id_periodo, a.id_asistente, a.nombre_completo,a.carnet,a.cantidad_periodos_nombrado, ea.id_encargado,e.nombre_completo, " +
                          "eu.id_unidad,u.nombre, p.semestre, p.ano_periodo, n.id_nombramiento, n.cantidad_horas";
            }
            else
            {
                consulta+="WHERE n.solicitud=1" +
                          "GROUP BY  p.id_periodo, a.id_asistente, a.nombre_completo,a.carnet,a.cantidad_periodos_nombrado, ea.id_encargado,e.nombre_completo, " +
                          "eu.id_unidad,u.nombre, p.semestre, p.ano_periodo, n.id_nombramiento, n.cantidad_horas";

            }
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idUnidad", idUnidad);
            sqlCommand.Parameters.AddWithValue("@idPeriodo", idPeriodo);


            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {

                Nombramiento nombramiento = new Nombramiento();
                nombramiento.idNombramiento = Convert.ToInt32(reader["id_nombramiento"].ToString());
                nombramiento.cantidadHorasNombrado = Convert.ToInt32(reader["cantidad_horas"].ToString());


                Asistente asistente = new Asistente();
                asistente.idAsistente = Convert.ToInt32(reader["id_asistente"].ToString());
                asistente.nombreCompleto = reader["nombre_completo"].ToString();
                asistente.carnet = reader["carnet"].ToString();
                asistente.cantidadPeriodosNombrado = ObtenerCantidadAsistencias(asistente.idAsistente);

                nombramiento.asistente = asistente;


                Periodo periodo = new Periodo();
                periodo.idPeriodo = periodo.anoPeriodo = Convert.ToInt32(reader["id_periodo"].ToString());
                periodo.semestre = reader["semestre"].ToString();
                periodo.anoPeriodo = Convert.ToInt32(reader["ano_periodo"].ToString());
                nombramiento.periodo = periodo;


                Unidad unidad = new Unidad();
                unidad.nombre = reader["nombre"].ToString();
                unidad.idUnidad = idUnidad;

                Encargado encargado = new Encargado();
                encargado.nombreCompleto = reader["encargado"].ToString();
                unidad.encargado = encargado;

                nombramiento.unidad = unidad;

                List<Archivo> archivos = new List<Archivo>();
                archivos = archivoDatos.getArchivosAsistente(asistente.idAsistente, periodo.idPeriodo);

                nombramiento.listaArchivos = archivos;
                nombramientos.Add(nombramiento);
            }

            sqlConnection.Close();

            return nombramientos;
        }


    }

}

