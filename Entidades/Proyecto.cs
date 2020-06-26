using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    /// <summary>
    /// Mariela Calvo
    /// 20/noviembre/2019
    /// Clase para administrar la entidad de Proyecto
    /// </summary>
    public class Proyecto
    {
        public int idProyecto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinalizacion { get; set; }
        public bool disponible { get; set; }
        public bool finalizado { get; set; }

        public Asistente asistente { get; set; }
    }
}
