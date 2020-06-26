using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    /// <summary>
    /// Mariela Calvo
    /// 20/noviembre/2019
    /// Clase para administrar la entidad Unidad
    /// </summary>
    public class Unidad
    {
        public int idUnidad { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public Boolean disponible { get; set; }
        public Encargado encargado { get; set; }

    }
}
