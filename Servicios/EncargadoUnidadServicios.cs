using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class EncargadoUnidadServicios
    {
        private EncargadoUnidadDatos unidadEncargadoDatos = new EncargadoUnidadDatos();

        public void insertarEncargadoUnidad(Unidad unidad, Encargado encargado)
        {
            unidadEncargadoDatos.insertarEncargadoDeUnidad(unidad,encargado);
        }

        public void eliminarEncargadoUnidad(int idUnidad, int idEncargado)
        {
            unidadEncargadoDatos.eliminarUnidadEncargado(idUnidad,idEncargado);
        }
    }
}
