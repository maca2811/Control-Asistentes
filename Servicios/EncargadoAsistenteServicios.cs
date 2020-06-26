using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class EncargadoAsistenteServicios
    {
        EncargadoAsistenteDatos encargadoAsistenteDatos = new EncargadoAsistenteDatos();
        public int insertarEncargadoAsistente(int idEncargado, int idAsistente)
        {
            return encargadoAsistenteDatos.insertarEncargadoAsistente(idEncargado,idAsistente);
        }
    }
}
