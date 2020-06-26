using Entidades;
using Microsoft.Reporting.WebForms;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistentes.Informes.ReporteAsistentesEncargado
{
    public partial class ReporteAsistentesEncargado : System.Web.UI.Page
    {
       
        private ReporteAsistenteServicios reporteAsistentesServicios = new ReporteAsistenteServicios();
        private UnidadServicios unidadServicios = new UnidadServicios();
        private PeriodoServicios periodoServicios = new PeriodoServicios();
        private static Unidad unidadEncargado = new Unidad();

        protected void Page_Load(object sender, EventArgs e)
        {
            int[] rolesPermitidos = { 1 };
            Utilidades.escogerMenu(Page, rolesPermitidos);
            Page.Master.FindControl("MenuControl").Visible = false;


            if (Session["nombreCompleto"] != null)
            {
                unidadEncargado= unidadServicios.ObtenerUnidadPorEncargado(Session["nombreCompleto"].ToString());
                tituloUn.Text = "" + unidadEncargado.nombre;
            }
            else
            {
                Session["nombreCompleto"] = "Wilson Arguello";
                unidadEncargado = unidadServicios.ObtenerUnidadPorEncargado(Session["nombreCompleto"].ToString());
                tituloUn.Text = "" + unidadEncargado.nombre;
            }
            if (!IsPostBack)
            {
                periodosDDL();
                obtenerReporte();

            }
        }

        #region eventos
        /// <summary>
        /// Mariela Calvo
        /// Mayo/2020
        /// Efecto: llean el DropDownList con los periodos que se encuentran en la base de datos
        /// Requiere: - 
        /// Modifica: DropDownList
        /// Devuelve: -
        /// </summary>
        protected void periodosDDL()
        {
            List<Periodo> periodos = new List<Periodo>();
            ddlPeriodo.Items.Clear();
            periodos = periodoServicios.ObtenerPeriodos();
            string valor = "";
            string actual = "";
            ddlPeriodo.Items.Add("Seleccione el período");
            foreach (Periodo periodo in periodos)
            {
                if (periodo.habilitado)
                {
                    valor = periodo.semestre + "     - Semestre " + periodo.anoPeriodo;

                }
                else
                {
                    valor = periodo.semestre + "     - Semestre " + periodo.anoPeriodo;
                }
                ListItem itemPeriodo = new ListItem(valor, periodo.idPeriodo + "");
                ddlPeriodo.Items.Add(itemPeriodo);


            }
        }

        
        #endregion

        protected void ddlUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idUnidad = unidadEncargado.idUnidad;
            int idPeriodo = 0;


            
            if (!ddlPeriodo.SelectedValue.Equals("Seleccione el periodo"))
            {
                idPeriodo = Convert.ToInt32(ddlPeriodo.SelectedValue);
            }
            
            if (ddlPeriodo.SelectedValue.Equals("Seleccione el periodo"))
            {
                idPeriodo = 0;
            }
            ReporteEncargado.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            ReporteEncargado.LocalReport.ReportPath = Server.MapPath("~/Informes/ReporteAsistentesEncargado/ReporteAsistentes.rdlc");
            ReportDataSource reporteAsistentes = new ReportDataSource("ReporteAsistentes", reporteAsistentesServicios.ObtenerReporteAsistente(idUnidad, idPeriodo));
            ReporteEncargado.LocalReport.DataSources.Clear();
            ReporteEncargado.LocalReport.DataSources.Add(reporteAsistentes);


        }

        protected void obtenerReporte()
        {
            int idUnidad = unidadEncargado.idUnidad;
            int idPeriodo = 0;

            ReporteEncargado.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            ReporteEncargado.LocalReport.ReportPath = Server.MapPath("~/Informes/ReporteAsistentesEncargado/ReporteAsistentes.rdlc");
            ReportDataSource reporteAsistentes = new ReportDataSource("ReporteAsistentes", reporteAsistentesServicios.ObtenerReporteAsistente(idUnidad, idPeriodo));
            ReporteEncargado.LocalReport.DataSources.Clear();
            ReporteEncargado.LocalReport.DataSources.Add(reporteAsistentes);
        }
    }
}
 