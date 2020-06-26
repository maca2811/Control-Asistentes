using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class EncargadoServicios
    {
        EncargadoDatos encargadoDatos = new EncargadoDatos();


        public List<Encargado> listaEncargados()
        {
            return encargadoDatos.listaEncargados();
        }

      

        public int insertarEncargado(Encargado encargado)
        {
            return 1;
        }
    }
}
