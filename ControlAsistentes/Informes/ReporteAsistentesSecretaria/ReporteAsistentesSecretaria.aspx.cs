using Entidades;
using Microsoft.Reporting.WebForms;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistentes.Informes.ReporteAsistentesSecretaria
{
    public partial class ReporteAsistentesSecretaria : System.Web.UI.Page
    {
        private ReporteAsistenteServicios reporteAsistentesServicios = new ReporteAsistenteServicios();
        private UnidadServicios unidadServicios = new UnidadServicios();
        private PeriodoServicios periodoServicios = new PeriodoServicios();


        protected void Page_Load(object sender, EventArgs e)
        {
            int[] rolesPermitidos = { 1 };
            Utilidades.escogerMenu(Page, rolesPermitidos);
            Page.Master.FindControl("MenuControl").Visible = false;

            if (!IsPostBack)
            {
                periodosDDL();
                unidadesDDL();
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

        /// <summary>
        /// Mariela Calvo
        /// Mayo/2020
        /// Efecto: llean el DropDownList con los encargados que se encuentran en la base de datos
        /// Requiere: - 
        /// Modifica: DropDownList
        /// Devuelve: -
        /// </summary>
        protected void unidadesDDL()
        {
            List<Unidad> unidades = new List<Unidad>();
            ddlUnidad.Items.Clear();
            unidades = unidadServicios.ObtenerUnidades();
            ddlUnidad.Items.Add("Seleccione la unidad");
            foreach (Unidad unidad in unidades)
            {
                ListItem itemEncargado = new ListItem(unidad.nombre, unidad.idUnidad + "");
                ddlUnidad.Items.Add(itemEncargado);
            }
        }
        #endregion

        protected void ddlUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idUnidad = 0;
            int idPeriodo = 0;
            if (!ddlUnidad.SelectedValue.Equals("Seleccione la unidad"))
            {
               idUnidad = Convert.ToInt32(ddlUnidad.SelectedValue);
            }
            if (!ddlPeriodo.SelectedValue.Equals("Seleccione el periodo"))
            {
                idPeriodo = Convert.ToInt32(ddlPeriodo.SelectedValue);
            }
            if (ddlUnidad.SelectedValue.Equals("Seleccione la unidad"))
            {
                idUnidad = 0;
            }
            if (ddlPeriodo.SelectedValue.Equals("Seleccione el periodo"))
            {
                idPeriodo = 0;
            }

            ReporteSecretaria.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            ReporteSecretaria.LocalReport.ReportPath = Server.MapPath("~/Informes/ReporteAsistentesSecretaria/ReporteAsistentes.rdlc");
            ReportDataSource reporteAsistentes = new ReportDataSource("ReporteAsistentes", reporteAsistentesServicios.ObtenerReporteAsistente(idUnidad, idPeriodo));
            ReporteSecretaria.LocalReport.DataSources.Clear();
            ReporteSecretaria.LocalReport.DataSources.Add(reporteAsistentes);


        }

        protected void obtenerReporte()
        {
            int idUnidad = 0;
            int idPeriodo = 0;

            ReporteSecretaria.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            ReporteSecretaria.LocalReport.ReportPath = Server.MapPath("~/Informes/ReporteAsistentesSecretaria/ReporteAsistentes.rdlc");
            ReportDataSource reporteAsistentes = new ReportDataSource("ReporteAsistentes", reporteAsistentesServicios.ObtenerReporteAsistente(idUnidad,idPeriodo));
            ReporteSecretaria.LocalReport.DataSources.Clear();
            ReporteSecretaria.LocalReport.DataSources.Add(reporteAsistentes);
        }
    }
}