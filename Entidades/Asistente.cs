using System;

namespace Entidades
{
    /// <summary>
    /// Mariela Calvo
    /// 20/noviembre/2019
    /// Clase para administrar la entidad Asistente
    /// </summary>
    public class Asistente
    {
        public int idAsistente { get; set; }
        public string nombreCompleto { get; set; }
        public string carnet { get; set; }

        public string telefono { get; set; }
        public int cantidadPeriodosNombrado { get; set; }

        public Unidad unidad { get; set; }




    }
}
