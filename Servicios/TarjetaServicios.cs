using AccesoDatos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class TarjetaServicios
    {
        #region variables globales
        private TarjetaDatos tarjetaDatos = new TarjetaDatos();
        #endregion

        #region metodos
        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 26/03/2020
        /// Efecto: Regresa la lista de tarjetas 
        /// Requiere: -
        /// Modifica: -
        /// Devuelve: Lista de tarjetas
        /// </summary>
        /// <returns></returns>
        public List<Tarjeta> ObtenerTarjetas()
        {
            return tarjetaDatos.ObtenerTarjetas();
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 26/03/2020
        /// Efecto : Elimina una tarjeta 
        /// Requiere : Tarjeta que se desea eliminar
        /// Modifica : Tarjetas 
        /// Devuelve : -
        /// </summary>
        /// <param name="tarjeta"></param>
        public void EliminarTarjeta(Tarjeta tarjeta)
        {
            tarjetaDatos.EliminarTarjeta(tarjeta);
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 26/03/2020
        /// Efecto : Modifica una tarjeta
        /// Requiere : Tarjeta con los datos actualizados
        /// Modifica : Tarjeta 
        /// Devuelve : -
        /// </summary>
        /// <param name="tarjeta"></param>
        public void ActualizarTarjeta(Tarjeta tarjeta)
        {
            tarjetaDatos.ActualizarTarjeta(tarjeta);
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 26/03/2020
        /// Efecto : Guarda una tarjeta 
        /// Requiere : Tarjeta que se desea guardar
        /// Modifica : Tarjetas 
        /// Devuelve : -
        /// </summary>
        /// <<param name="tarjeta"></param>
        public void InsertarTarjeta(Tarjeta tarjeta)
        {
            tarjetaDatos.InsertarTarjeta(tarjeta);
        }

        /// <summary>
        /// Mariela Calvo Mendez
        /// Mayo/2020
        /// Efecto: Regresa una tarjeta 
        /// Requiere: -
        /// Modifica: -
        /// Devuelve: Tarjeta de un asistente
        /// </summary>
        /// <returns></returns>
        public Tarjeta ObtenerTarjetaAsistente(int idAsistente)
        {
            return tarjetaDatos.ObtenerTarjetaAsistente(idAsistente);
        }
        #endregion
    }
}
