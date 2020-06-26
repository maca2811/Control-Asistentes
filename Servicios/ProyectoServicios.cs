using AccesoDatos;
using Entidades;
using System.Collections.Generic;

namespace Servicios
{
    public class ProyectoServicios
    {
        ProyectoAsistenteDatos proyectoDatos = new ProyectoAsistenteDatos();

        public int insertar(Proyecto proyecto)
        {
            return proyectoDatos.Insertar(proyecto);
        }

        public int insertarAsistenteProyecto(int idAsistente, int idProyecto)
        {
            return proyectoDatos.InsertarAsistenteProyecto(idAsistente, idProyecto);
        }

        public List<Proyecto> ObtenerProyectos()
        {
            return proyectoDatos.ObtenerProyectos();
        }

        public List<Proyecto> ObtenerProyectoPorId(int idProyecto)
        {
            return proyectoDatos.ObtenerProyectoPorId(idProyecto);
        }

        public void Eliminar(int idProyecto)
        {
            proyectoDatos.EliminarProyecto(idProyecto);
        }

    }
}
