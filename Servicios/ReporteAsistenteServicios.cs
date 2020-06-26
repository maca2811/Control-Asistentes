using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ReporteAsistenteServicios
    {
        ReporteAsistenteDatos reporteAsistenteDatos = new ReporteAsistenteDatos();

        /// <summary>
        /// Mariela Calvo Mendez
        /// Mayo/2020
        /// Efecto: Regresa la lista de los asistentes y su informacion sobre los nombramientos
        /// Requiere: -
        /// Modifica: -
        /// Devuelve: Reporte de Asistente
        /// </summary>
        /// <returns></returns>
        public List<ReporteAsistente> ObtenerReporteAsistente(int idUnidad, int idPeriodo)
        {
            return reporteAsistenteDatos.ObtenerReporteAsistente(idUnidad, idPeriodo);
        }
    }
}
