using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class NombramientoServicios {


        NombramientoAsistenteDatos nombramientoDatos = new NombramientoAsistenteDatos();


        public int insertarNombramiento(Nombramiento nombramiento)
        {
            return nombramientoDatos.insertarNombramientoAsistente(nombramiento);
        }
        public void ActualizarNombramientoAsistente(string numeroCarnet, int aprobado, string observaciones, int solicitud)
        {
            nombramientoDatos.actualizarAsistenteNombramiento(numeroCarnet, aprobado, observaciones,solicitud );
        }


        public List<Nombramiento> ObtenerNombramientosPorUnidad(int idUnidad)
        {
            return nombramientoDatos.ObtenerNombramientosPorUnidad(idUnidad);
        }


        public void EliminarNombramiento(int idNombramiento)
        {
            nombramientoDatos.eliminarNombramiento(idNombramiento);
        }

        public void EditarNombramiento(Nombramiento nombramiento)
        {
            nombramientoDatos.EditarNombramiento(nombramiento);
        }

        public Nombramiento ObtenerDetallesNombramiento(int idNombramiento)
        {
           return nombramientoDatos.ObtenerDetallesNombramiento(idNombramiento);
        }

        public List<Nombramiento> ObtenerNombramientos()
        {
            return nombramientoDatos.ObtenerNombramientos();
        }

        public List<Nombramiento> ObtenerNombramientosPorPeriodo(int idPeriodo, int idUnidad)
        {
            return nombramientoDatos.ObtenerNombramientosPorPeriodo(idPeriodo, idUnidad);
        }

        public List<Nombramiento> ObtenerNombramientosReporte(int idPeriodo, int idUnidad)
        {
            return nombramientoDatos.ObtenerNombramientosReporte(idPeriodo, idUnidad);
        }
    }
}
