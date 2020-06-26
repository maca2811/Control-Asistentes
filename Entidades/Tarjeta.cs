using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    /// <summary>
    /// Mariela Calvo
    /// 20/noviembre/2019
    /// Clase para administrar la entidad Tarjeta
    /// </summary>
    public class Tarjeta
    {
        public int idTarjeta{ get; set; }
        public string numeroTarjeta { get; set; }
        public bool disponible { get; set; }
        public bool tarjetaExtraviada { get; set; }
        public Asistente asistente { get; set; }
        public bool pagada { get; set; }
    }
}
