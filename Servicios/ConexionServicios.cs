using AccesoDatos;
using System;
using System.Collections.Generic;
using System.Text;


namespace Servicios
{
    /// <summary>
    /// Mariela Calvo
    /// 20/noviembre/2019
    /// Clase para la conexion a BD
    /// </summary>
    public class ConexionServicios
    {
        #region Variables Globales
        ConexionDatos conexionDatos = new ConexionDatos();
        #endregion


        public object[] loguearse(String usuario)
        {
            return conexionDatos.loguearse(usuario);
            
        }
    }
}
