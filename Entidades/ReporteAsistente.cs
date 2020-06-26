using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ReporteAsistente
    {
        // ASISTENTE
        public int idAsistente { get; set; }
        public string asistente { get; set; }
        public string carnet { get; set; }
        public int cantidadPeriodosNombrados { get; set; }

        // ENCARGADO
        public int idEncargado { get; set; }
        public string encargado { get; set; }

        // UNIDAD
        public int idUnidad { get; set; }
        public string unidad { get; set; }

        // PERÍODO
        public int idPeriodo { get; set; }
        public string periodo { get; set; }
       
        // NOMBRAMIENTO
        public int idNombramiento { get; set; }
        public int cantidadHoras { get; set; }

        // PROYECTO
        public int idProyecto { get; set; }
        public string proyecto { get; set; }
        public string descripcionProyecto { get; set; }

        // TARJETA
        public int idTarjeta { get; set; }
        public string tarjeta { get; set; }

        // USUARIO
        public int idUsuario { get; set; }
        public string usuario { get; set; }

        
    }
}
