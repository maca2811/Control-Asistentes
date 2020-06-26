using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ArchivoServicios
    {
        ArchivoDatos archivoDatos = new ArchivoDatos();

        public int insertarArchivo(Archivo archivo)
        {
            return archivoDatos.insertarArchivo(archivo);
        }

        public int insertarArchivoNombramiento(int idArchivo, int idNombramiento)
        {
            return archivoDatos.insertarArchivoNombramiento(idArchivo, idNombramiento);
        }

        public List<Archivo> ObtenerArchivosAsistente(int idAsistente, int idPeriodo)
        {
            return archivoDatos.getArchivosAsistente(idAsistente,idPeriodo);
        }
        public Archivo ObtenerArchivoAsistente(int idAsistente,int idArchivo)
        {
            return archivoDatos.getArchivoAsistente(idAsistente,idArchivo);
        }
    }
}

