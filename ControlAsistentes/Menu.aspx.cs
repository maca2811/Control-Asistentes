using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistentes
{
    public partial class Menuaspx : System.Web.UI.Page
    {
        #region Variables Globales
        public static int rol = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            int[] rolesPermitidos = { 1,2,5 };
            Utilidades.escogerMenu(Page, rolesPermitidos);
            Page.Master.FindControl("menu").Visible = false;
        }

        /// <summary>
        /// Mariela Calvo 
        ///27/11/2019
        /// Efecto:Metodo que se activa cuando se le da click al enlace de Unidades del Menu Secretaria
        /// cambia las variables del sistema a laboratorio de ensayos
        /// Requiere: -
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        protected void Control_Asistentes_Secretaria_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Inicio.aspx");
            Response.Redirect(url);
        }
    }
}