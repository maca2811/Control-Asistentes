using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistentes
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int[] rolesPeromitidos = { 1,2,5 };
            Utilidades.escogerMenu(Page, rolesPeromitidos);
        }
    }
}