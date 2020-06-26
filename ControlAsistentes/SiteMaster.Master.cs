using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistentes
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["nombreCompleto"] != null)
            {
                username.InnerText = "Bienvenid@ " + Session["nombreCompleto"].ToString();
            }
        }

        #region 
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
            String url = Page.ResolveUrl("~/Login.aspx");
            Response.Redirect(url);
        }
        #endregion

        #region mensaje toast
        public void Toastr(string tipo, string mensajes)
        {
            mensaje.Text = mensajes;
            alert.Text = tipo;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr." + tipo + "('" + mensaje + "');", true);
        }


        public void Mensaje(string mensajes , string tipo)
        {
            mensaje.Text = mensajes;
            alert.Text = tipo;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr." + tipo + "('" + mensaje + "');", true);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["CheckRefresh"] = Session["CheckRefresh"];
        }



        #endregion
    }
}