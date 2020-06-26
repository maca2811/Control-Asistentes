using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class AsistenteServicios
    {
        AsistenteDatos asistenteDatos = new AsistenteDatos();
	
        /// <summary>
		/// Marilea
		/// 02/abr/2020
		/// Efecto: Obtiene los asistentes de la capa de datos 
		/// Requiere: - 
		/// Modifica: 
		/// Devuelve: Lista de asistentes 
		/// </summary>
        public List<Asistente> ObtenerAsistentes()
        {
            return asistenteDatos.ObtenerAsistentes();
        }

		/// <summary>
		/// Jesús Torres
		/// 02/abr/2020
		/// Efecto: Obtiene los asistentes de la capa de datos por unidad y periodo actual
		/// Requiere: - 
		/// Modifica: 
		/// Devuelve: Lista de asistentes 
		/// </summary>
		public List<Asistente> ObtenerAsistentesPorUnidad(int idUnidad)
        {
            return asistenteDatos.ObtenerAsistentesPorUnidad(idUnidad);
        }
		/// <summary>
		/// Jesús Torres
		/// 02/abr/2020
		/// Efecto: Obtiene los asistentes de la capa de datos por unidad y periodo actual
		/// Requiere: - 
		/// Modifica: 
		/// Devuelve: Lista de asistentes 
		/// </summary>
		public List<Asistente> ObtenerAsistentesPorUnidad1(int idUnidad)
		{
			return asistenteDatos.ObtenerAsistentesPorUnidad1(idUnidad);
		}

		public int insertarAsistente(Asistente asistente)
        {
            return asistenteDatos.insertarAsistente(asistente);
        }

		/// <summary>
		/// Jesús Torres
		/// 02/abr/2020
		/// Efecto: Obtiene los asistentes de la capa de datos 
		/// Requiere: - 
		/// Modifica: 
		/// Devuelve: Lista de asistentes 
		/// </summary>
		public List<Asistente> ObtenerAsistentesSinUsuarios()
		{
			return asistenteDatos.ObtenerAsistentesSinUsuarios();
		}

		/// <summary>
		/// Mariela Calvo
		/// Abr/2020
		/// Efecto: Obtiene los asistentes de la capa de datos de una unidad especifica
		/// Requiere: - 
		/// Modifica: 
		/// Devuelve: Lista de asistentes 
		/// </summary>
		public List<Asistente> ObtenerAsistentesXUnidad(int idUnidad)
        {
            return asistenteDatos.ObtenerAsistentesXUnidad(idUnidad);
        }

		/// <summary>
		/// Mariela Calvo
		/// Abr/2020
		/// Efecto: Obtiene los asistentes de la capa de datos de una unidad especifica
		/// Requiere: - 
		/// Modifica: 
		/// Devuelve: Lista de asistentes 
		/// </summary>
		public List<Nombramiento> ObtenerAsistentesXUnidadSinNombrar(int idUnidad)
		{
			return asistenteDatos.ObtenerAsistentesXUnidadSinNombrar(idUnidad);
		}

		/// <summary>
		/// Mariela Calvo
		/// Abr/2020
		/// Efecto: Elimina un asistente de la tabla asistentes por unidad
		/// Requiere: - 
		/// Modifica: Tabla asistentes encargado
		/// Devuelve: Lista de asistentes 
		/// </summary>

		public void eliminarAsistente(int idAsistente)
		{
			asistenteDatos.eliminarAsistente(idAsistente);
		}

		/// <summary>
		/// Mariela Calvo
		/// Abr/2020
		/// Efecto: Edita un asistente de la tabla asistentes por unidad
		/// Requiere: - 
		/// Modifica: Tabla asistentes encargado
		/// Devuelve: Lista de asistentes 
		/// </summary>
		public void editarAsistente(Asistente asistente)
		{
			asistenteDatos.EditarAsistente(asistente);
		}

		/// <summary>
		/// Jesús Torres
		/// 02/abr/2020
		/// Efecto: Obtiene los asistentes de la capa de datos 
		/// Requiere: - 
		/// Modifica: 
		/// Devuelve: Lista de asistentes 
		/// </summary>
		public List<Asistente> ObtenerAsistentesSinNombramiento(int idUnidad)
		{
			return asistenteDatos.ObtenerAsistentesSinNombramiento(idUnidad);
		}

		/// <summary>
		/// Jesús Torres
		/// Abr/2020
		/// Efecto: Obtiene los asistentes de la capa de datos 
		/// Requiere: - 
		/// Modifica: 
		/// Devuelve: Lista de asistentes 
		/// </summary>
		public List<Asistente> ObtenerNombramientosPorUnidad(int idUnidad)
		{
			return asistenteDatos.ObtenerAsistentesSinNombramiento(idUnidad);
		}
	}
}
