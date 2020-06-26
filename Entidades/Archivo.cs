using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Entidades
{
    public class Archivo
    {
        public int idArchivo { get; set; }
        public DateTime fechaCreacion { get; set; }
        public String nombreArchivo { get; set; }
        public String rutaArchivo { get; set; }
        public int tipoArchivo { get; set; }

        public string creadoPor { get; set; }
        
    }
}
