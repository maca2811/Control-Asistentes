using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    /// <summary>
    /// Mariela Calvo
    /// 20/noviembre/2019
    /// Clase para administrar la entidad Usuario
    /// </summary>
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public bool disponible { get; set; }
        public string contraseña { get; set; }
        public Asistente asistente { get; set; }
    }
}
