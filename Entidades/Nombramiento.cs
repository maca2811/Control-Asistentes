using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Nombramiento
    {
        /// <summary>
        /// Mariela Calvo
        /// 20/noviembre/2019
        /// Clase para administrar la entidad Asistente
        /// </summary>
        
            public int idNombramiento { get; set; }

            public Asistente asistente{get; set;}
            public Unidad unidad { get; set; }
            public Periodo periodo { get; set; }

            public bool aprobado { get; set; }
            public bool recibeInduccion { get; set; }
            public int cantidadHorasNombrado { get; set; }

            public DateTime fechaAprobacion { get; set; }

            public string observaciones { get; set; }
           
            public bool disponible { get; set; }

            public int solicitud { get; set; }
            public List<Archivo> listaArchivos { get; set; }


    }
}
