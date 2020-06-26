using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
namespace AccesoDatos
{
    /// <summary>
    /// Mariela Calvo
    /// 20/noviembre/2019
    /// Clase para administrar la entidad Archivo
    /// </summary>
    public class ConexionDatos
    {

        /// <summary>
        /// Mariela Calvo
        /// 20/noviembre/2019
        /// Metodo para establecer conexion con el servidor de Control de Asistentes
        /// </summary>
        public SqlConnection ConexionControlAsistentes()
        {
           
            return new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ControlAsistentesConnectionString"].ConnectionString);
        }


        /// <summary>
        /// Mariela Calvo
        /// 20/noviembre/2019
        /// Metodo para establecer conexion con el servidor de Login
        /// </summary>
        public SqlConnection ConexionLogin()
        {

            return new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["LoginConnectionString"].ConnectionString);
        }

        /// <summary>
        /// Mariela Calvo
        /// noviembre/2019
        /// Efecto: Comprueba que el usuario pueda loguearse
        /// Requiere: - 
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        public object[] loguearse(String usuario)
        {
            object[] rolNombreCompleto = new object[2];
            SqlConnection sqlConnection = ConexionLogin();
            SqlCommand sqlCommand = new SqlCommand("select R.id_rol, U.nombre_completo from Rol R, Usuario U, Usuario_Rol_Aplicacion URA where  U.usuario = @usuario and URA.id_usuario = u.id_usuario and R.id_rol = URA.id_rol ", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@usuario", usuario);
            
            SqlDataReader reader;
            sqlConnection.Open();
            reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {

                int rol = Int32.Parse(reader.GetValue(0).ToString());
                String nombreCompleto = reader.GetValue(1).ToString();

                rolNombreCompleto[0] = rol;
                rolNombreCompleto[1] = nombreCompleto;
            }

            sqlConnection.Close();

            return rolNombreCompleto;
        }

        
    }
}
