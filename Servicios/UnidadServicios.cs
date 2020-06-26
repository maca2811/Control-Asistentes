using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    /// <summary>
    /// Mariela Calvo
    /// 29/noviembre/2019
    /// Clase para administrar los servicios de Unidad
    /// </summary>
    public class UnidadServicios
    {
        UnidadDatos unidadDatos = new UnidadDatos();

        public List<Unidad> ObtenerUnidades()
        {
            return this.unidadDatos.ObtenerUnidades();
        }

        public List<Unidad> ObtenerUnidadesPorNombre(string nombre)
        {
            return this.unidadDatos.ObtenerUnidades();
        }

        public int insertarUnidad(Unidad unidad)
        {
            return unidadDatos.InsertarUnidad(unidad);
        }

        public Unidad ObtenerUnidadPorId(int idUnidad)
        {
            return unidadDatos.ObtenerUnidadPorId(idUnidad);
        }

        public Unidad ObtenerUnidadPorEncargado(string nombreEncargado)
        {
            return unidadDatos.ObtenerUnidadPorNombreEncargado(nombreEncargado);
        }

        public void eliminarUnidad(int idUnidad)
        {
            unidadDatos.EliminarUnidad(idUnidad);
        }

        public void editarUnidad(Unidad unidad)
        {
            unidadDatos.EditarUnidad(unidad);
        }
    }
}
