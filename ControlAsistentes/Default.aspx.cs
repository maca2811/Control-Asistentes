using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistentes
{
    public partial class Default : System.Web.UI.Page
    {
        #region Variables Globales
        #endregion


        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Master.FindControl("menu").Visible = true;
            Page.Master.FindControl("MenuControl").Visible = true;
            int[] rolesPermitidos = {1,2,5 };
            Utilidades.escogerMenu(Page, rolesPermitidos);
        }
        #endregion

        

    }
}