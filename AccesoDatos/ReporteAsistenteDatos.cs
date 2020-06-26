using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class ReporteAsistenteDatos
    {

        private ConexionDatos conexion = new ConexionDatos();

        /// <summary>
        /// Mariela Calvo
        /// Mayo/2020
        /// Efecto: Devuelve la consulta necesaria para ser mostrada en el reporte
        /// Requiere: - 
        /// Modifica: 
        /// Devuelve:  
        /// </summary>
        /// 
        public List<ReporteAsistente> ObtenerReporteAsistente(int idUnidad, int idPeriodo)
        {
            SqlConnection sqlConnection = conexion.ConexionControlAsistentes();
            List<ReporteAsistente> listaReporte = new List<ReporteAsistente>();

            String consulta = @"SELECT a.id_asistente, a.nombre_completo as asistente ,a.carnet,a.cantidad_periodos_nombrado, ea.id_encargado,e.nombre_completo as encargado, eu.id_unidad,u.nombre as unidad, p.id_periodo, p.semestre, p.ano_periodo, 
                              n.id_nombramiento, n.cantidad_horas,pr.id_proyecto,pr.nombre as proyecto,pr.descripcion, t.id_tarjeta,t.numeroTarjeta as tarjeta,us.id_usuario, us.nombre_completo as usuario
                              FROM Asistente a
                              JOIN Encargado_Asistente ea ON a.id_asistente=ea.id_asistente
                              JOIN Encargado_Unidad eu ON ea.id_encargado=eu.id_encargado
                              JOIN Encargado e ON ea.id_encargado=e.id_encargado
                              JOIN Unidad u ON eu.id_unidad=u.id_unidad
                              JOIN Nombramiento n ON a.id_asistente = n.id_asistente
                              JOIN Periodo p ON n.id_periodo=p.id_periodo 
                              FULL OUTER JOIN Asistente_Proyecto ap ON a.id_asistente=ap.id_asistente
                              FULL OUTER JOIN Proyecto pr ON ap.id_proyecto=pr.id_proyecto
                              FULL OUTER JOIN Tarjeta t ON a.id_asistente=t.id_asistente
                              FULL OUTER JOIN Usuario us ON a.id_asistente=us.id_asistente  ";


            if (idUnidad != 0 && idPeriodo != 0)
            {
                consulta += "WHERE n.solicitud=1 AND p.id_periodo=@idPeriodo AND eu.id_unidad=@idUnidad " +
                            "GROUP BY  p.id_periodo, a.id_asistente, a.nombre_completo,a.carnet,a.cantidad_periodos_nombrado, ea.id_encargado,e.nombre_completo," +
                            "eu.id_unidad,u.nombre, p.semestre, p.ano_periodo, n.id_nombramiento, n.cantidad_horas,pr.id_proyecto,pr.nombre,pr.descripcion,t.id_tarjeta," +
                            "t.numeroTarjeta,us.id_usuario, us.nombre_completo " +
                            "ORDER BY p.ano_periodo ASC";
            }
            else if (idUnidad != 0)
            {
                consulta += "WHERE n.solicitud=1 AND eu.id_unidad=@idUnidad " +
                            "GROUP BY  p.id_periodo, a.id_asistente, a.nombre_completo,a.carnet,a.cantidad_periodos_nombrado, ea.id_encargado,e.nombre_completo," +
                            "eu.id_unidad,u.nombre, p.semestre, p.ano_periodo, n.id_nombramiento, n.cantidad_horas,pr.id_proyecto,pr.nombre,pr.descripcion,t.id_tarjeta," +
                            "t.numeroTarjeta,us.id_usuario, us.nombre_completo " +
                            "ORDER BY p.ano_periodo ASC";
            }
            else if (idPeriodo != 0)
            {
                consulta += "WHERE n.solicitud=1 AND  p.id_periodo=@idPeriodo " +
                            "GROUP BY  p.id_periodo, a.id_asistente, a.nombre_completo,a.carnet,a.cantidad_periodos_nombrado, ea.id_encargado,e.nombre_completo," +
                            "eu.id_unidad,u.nombre, p.semestre, p.ano_periodo, n.id_nombramiento, n.cantidad_horas,pr.id_proyecto,pr.nombre,pr.descripcion,t.id_tarjeta," +
                            "t.numeroTarjeta,us.id_usuario, us.nombre_completo " +
                            "ORDER BY p.ano_periodo ASC";
            }
            else
            {
                consulta += "WHERE n.solicitud=1 " +
                            "GROUP BY  p.id_periodo, a.id_asistente, a.nombre_completo,a.carnet,a.cantidad_periodos_nombrado, ea.id_encargado,e.nombre_completo," +
                            "eu.id_unidad,u.nombre, p.semestre, p.ano_periodo, n.id_nombramiento, n.cantidad_horas,pr.id_proyecto,pr.nombre,pr.descripcion,t.id_tarjeta," +
                            "t.numeroTarjeta,us.id_usuario, us.nombre_completo " +
                            "ORDER BY p.ano_periodo ASC";

            }
            SqlCommand sqlCommand = new SqlCommand(consulta, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@idUnidad", idUnidad);
            sqlCommand.Parameters.AddWithValue("@idPeriodo", idPeriodo);


            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {

                ReporteAsistente reporteAsistente = new ReporteAsistente();
                // NOMBRAMIENTO
                reporteAsistente.idNombramiento = Convert.ToInt32(reader["id_nombramiento"].ToString());
                reporteAsistente.cantidadHoras = Convert.ToInt32(reader["cantidad_horas"].ToString());

                // ASISTENTE
                reporteAsistente.idAsistente = Convert.ToInt32(reader["id_asistente"].ToString());
                reporteAsistente.asistente = reader["asistente"].ToString();
                reporteAsistente.carnet = reader["carnet"].ToString();
                reporteAsistente.cantidadPeriodosNombrados = ObtenerCantidadAsistencias(reporteAsistente.idAsistente);

                // PERIODO
                reporteAsistente.idPeriodo = Convert.ToInt32(reader["id_periodo"].ToString());
                reporteAsistente.periodo = reader["semestre"].ToString() + " Semestre - " + Convert.ToInt32(reader["ano_periodo"].ToString()); ;

                // UNIDAD
                Unidad unidad = new Unidad();
                reporteAsistente.idUnidad = Convert.ToInt32(reader["id_unidad"].ToString());
                reporteAsistente.unidad = reader["unidad"].ToString();

                // ENCARGADO
                reporteAsistente.idUnidad = Convert.ToInt32(reader["id_encargado"].ToString());
                reporteAsistente.encargado = reader["encargado"].ToString();

                // PROYECTO
                if ((reader["id_proyecto"].ToString()) == "")
                {
                    reporteAsistente.idProyecto = 0;
                    reporteAsistente.proyecto = "Sin asignar";
                    reporteAsistente.descripcionProyecto = "Sin asignar";
                }
                else
                {

                    reporteAsistente.idProyecto = Convert.ToInt32(reader["id_proyecto"].ToString());
                    reporteAsistente.proyecto = reader["proyecto"].ToString();
                    reporteAsistente.descripcionProyecto = reader["descripcion"].ToString();
                }

                // TARJETA
                if ((reader["id_tarjeta"].ToString()) == "")
                {
                    reporteAsistente.idTarjeta = 0;
                    reporteAsistente.tarjeta = "Sin asignar";
                }
                else
                {
                    reporteAsistente.idTarjeta = Convert.ToInt32(reader["id_tarjeta"].ToString());
                    reporteAsistente.tarjeta = reader["tarjeta"].ToString();
                }

                // USUARIO
                if ((reader["id_usuario"].ToString())=="")
                {
                    reporteAsistente.idUsuario = 0;
                    reporteAsistente.usuario = "Sin asignar";
                }
                else
                {
                    reporteAsistente.idUsuario = Convert.ToInt32(reader["id_usuario"].ToString());
                    reporteAsistente.usuario = reader["usuario"].ToString();
                }

                

                listaReporte.Add(reporteAsistente);
            }

            sqlConnection.Close();

            return listaReporte;
        }

        // <summary>
        // Mariela Calvo
        // Mayo/2019
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
