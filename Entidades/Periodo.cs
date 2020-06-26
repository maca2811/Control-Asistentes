using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    /// <summary>
    /// Mariela Calvo
    /// 20/noviembre/2019
    /// Clase para administrar la entidad de Periodo
    /// </summary>
    public class Periodo
    {
        public int idPeriodo { get; set; }
        public int anoPeriodo { get; set; }
        public bool habilitado { get; set; }
        public string semestre { get; set; }
    }
}
