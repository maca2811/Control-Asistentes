using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ControlAsistentes.Catalogos
{
    public partial class AdministrarPeriodos : System.Web.UI.Page
    {
        #region variable globales
        PeriodoServicios periodoServicios = new PeriodoServicios();
        public static Periodo periodoSelccionado = new Periodo();
        Periodo periodoActual = new Periodo();


        #region Paginación
        readonly PagedDataSource pgsource = new PagedDataSource();
        int primerIndex, ultimoIndex, primerIndex2, ultimoIndex2, primerIndex3, ultimoIndex3, primerIndex4, ultimoIndex4, primerIndex5, ultimoIndex5;
        private int elmentosMostrar = 10;
        private int paginaActual
        {
            get
            {
                if (ViewState["paginaActual"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["paginaActual"]);
            }
            set
            {
                ViewState["paginaActual"] = value;
            }
        }
        #endregion
        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            object[] rolesPermitidos = {1,2,5 };
            Page.Master.FindControl("MenuControl").Visible = false;

            if (!IsPostBack)
            {
                Session["listaPeriodos"] = null;
                
                List<Periodo> listaPeriodos = periodoServicios.ObtenerPeriodos();
                Session["listaPeriodos"] = listaPeriodos;
                MostrarPeriodos();
            }
        }

        #region Eventos
        protected void MostrarPeriodos()
        {
            List < Periodo> listaPeriodos = (List<Periodo>)Session["listaPeriodos"];
          
            

            int anoHabilitado = 0;


            if (listaPeriodos.Count > 0)
            {
                foreach (Periodo periodo in listaPeriodos)
                {
                    string nombre;

                    if (periodo.habilitado)
                    {
                        nombre = periodo.anoPeriodo.ToString() + " (Actual)";
                        anoHabilitado = periodo.anoPeriodo;
                    }
                    else
                    {
                        nombre = periodo.anoPeriodo.ToString();
                    }
                    ListItem itemPeriodo = new ListItem(nombre, periodo.anoPeriodo.ToString());
                }
            }
            var dt = listaPeriodos;


            pgsource.DataSource = dt;
            pgsource.AllowPaging = true;
            //numero de items que se muestran en el Repeater
            pgsource.PageSize = elmentosMostrar;
            pgsource.CurrentPageIndex = paginaActual;
            //mantiene el total de paginas en View State
            ViewState["TotalPaginas"] = pgsource.PageCount;
            //Ejemplo: "Página 1 al 10"
            lblpagina.Text = "Página " + (paginaActual + 1) + " de " + pgsource.PageCount + " (" + dt.Count + " - elementos)";
            //Habilitar los botones primero, último, anterior y siguiente
            lbAnterior.Enabled = !pgsource.IsFirstPage;
            lbSiguiente.Enabled = !pgsource.IsLastPage;
            lbPrimero.Enabled = !pgsource.IsFirstPage;
            lbUltimo.Enabled = !pgsource.IsLastPage;

            rpPeriodos.DataSource = pgsource;
            rpPeriodos.DataBind();

            //metodo que realiza la paginacion
            Paginacion();
        }

        public void btnDevolverse(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Default.aspx");
            Response.Redirect(url);
        }
        /// <summary>
        /// Mariela Calvo
        /// Marzo/2020
        /// Efecto: Establecer un periodo como actual
        /// Requiere: Presionar boton con icono de manita arriba en tabla periodo de algun periodo
        /// Modifica: Tabla Periodos
        /// Devuelve: -
        /// </summary>
        protected void EstablecerPeriodoActual_Click(object sender, EventArgs e)
        {
            int idPeriodo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            periodoSelccionado = periodoServicios.ObtenerPeriodoPorId(idPeriodo);
            int anoPeriodo = periodoSelccionado.anoPeriodo;

            if (anoPeriodo != 0)
            {
               List<Periodo> listaPeriodos = (List<Periodo>)Session["listaPeriodos"];
               periodoActual = new Periodo();

                foreach (Periodo periodo in listaPeriodos)
                {
                    if (periodo.anoPeriodo == anoPeriodo)
                    {
                        periodoActual = periodo;
                    }
                }
                bool respuesta = this.periodoServicios.HabilitarPeriodo(periodoSelccionado);

                if (respuesta)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "Período " + anoPeriodo + " habilitado con éxito');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Error al habilitar el período " + anoPeriodo + " como actual, intentelo de nuevo');", true);
                }
                
                Session["listaPeriodos"] = periodoServicios.ObtenerPeriodos();
                MostrarPeriodos();
            }
        }
        /// <summary>
        /// Mariela Calvo
        /// Marzo/2020
        /// Efecto: Activar modal nuevo periodo
        /// Requiere: Presionar boton nuevo periodo
        /// Modifica: Tabla Periodos
        /// Devuelve: -
        /// </summary>
        protected void btnNuevoPeriodo_Click(object sender, EventArgs e)
        {
            txtNuevoP.CssClass = "form-control";
            txtNuevoP.Text = "";
            

            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoPeriodo();", true);
        }
        /// <summary>
        /// Mariela CalvobtnNuevoPeriodoModal_Click
        /// Septiembre/2019
        /// Efecto: Guardar un nuevo periodo
        /// Requiere: Introducir datos del nuevo periodo, presionar boton guardar del modal nuevo periodo
        /// Modifica: Tabla Periodos
        /// Devuelve: -
        /// </summary>
        protected void btnNuevoPeriodoModal_Click(object sender, EventArgs e)
        {
            int respuesta = 0;
            if (validarPeriodoNuevo())
            {
                Periodo periodo = new Periodo();
                periodo.anoPeriodo = Convert.ToInt32(txtNuevoP.Text);
                periodo.semestre = ddlSemestre.SelectedValue.ToString();
                periodo.habilitado = false;

                respuesta = periodoServicios.InsertarPeriodo(periodo);
                txtNuevoP.Text = "";
                if (respuesta == periodo.anoPeriodo)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "Período " + periodo.anoPeriodo + " registrado con éxito');", true);
                    List<Periodo> listaPeriodos = periodoServicios.ObtenerPeriodos();
                    Session["listaPeriodos"] = listaPeriodos;
                    MostrarPeriodos();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoPeriodo", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoPeriodo').hide();", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoPeriodo", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoPeriodo').hide();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoPeriodo();", true);
            }
        }

        /// <summary>
        /// Mariela Calvo
        /// Marzo/2020
        /// Efecto: Validar que los datos del nuevo periodo sean ingresados
        /// Requiere: -
        /// Modifica: Tabla Periodos
        /// Devuelve: -
        /// </summary>
        public Boolean validarPeriodoNuevo()
        {
            Boolean valido = true;
            txtNuevoP.CssClass = "form-control";

            #region nombre
            if (String.IsNullOrEmpty(txtNuevoP.Text) || txtNuevoP.Text.Trim() == String.Empty || txtNuevoP.Text.Length > 255)
            {
                txtNuevoP.CssClass = "form-control alert-danger";
                valido = false;
            }
            #endregion

            return valido;
        }

        /// <summary>
        /// Mariela Calvo
        /// Septiembre/2019
        /// Efecto: Activar modal eliminar periodo para proceder a eliminar un periodo
        /// Requiere: Presionar boto nuevo periodo
        /// Modifica: Tabla Periodos
        /// Devuelve: -
        /// </summary>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            txtPeriodoEliminarModal.CssClass = "form-control";
            txtSemestreEliminarModal.CssClass = "form-control";

            int anoPeriodo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Periodo> listaPeriodos = (List<Periodo>)Session["listaPeriodos"];

            foreach (Periodo periodo in listaPeriodos)
            {
                if (periodo.anoPeriodo == anoPeriodo)
                {
                    periodoSelccionado = periodo;
                    txtPeriodoEliminarModal.Text = periodo.anoPeriodo.ToString();
                    txtSemestreEliminarModal.Text = periodo.semestre.ToString();
                    break;
                }
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalEliminarPeriodo", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalEliminarPeriodo').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalEliminarPeriodo();", true);
        }
        /// <summary>
        /// Mariela Calvo
        /// Marzo/2020
        /// Efecto: Mensaje de confirmacion para la eliminacion de un periodo
        /// Requiere: Presionar boto eliminar del modal eliminar periodo
        /// Modifica: Tabla Periodos
        /// Devuelve: -
        /// </summary>
        public void btnConfirmarEliminarPeriodo_Click(Object sender, EventArgs e)
        {
            //lbConfPer.Text = periodoSelccionado.anoPeriodo.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalConfirmarPeriodo", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalConfirmarPeriodo').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalConfirmarPeriodo() ", true);
        }

        /// <summary>
        /// Mariela Calvo
        /// Marzo/2020
        /// Efecto: Encargado de eliminar el periodo luego de la confirmacion
        /// Requiere: Presionar boton confirmar del modal confirmar eliminar periodo
        /// Modifica: Tabla Periodos
        /// Devuelve: -
        /// </summary>
        /// 
        protected void btnEliminarModal_Click(object sender, EventArgs e)
        {

            Periodo periodo = periodoSelccionado;
            periodoServicios.EliminarPeriodo(periodo.anoPeriodo);

            List<Periodo> listaPeriodos = periodoServicios.ObtenerPeriodos();

            if (listaPeriodos.Contains(periodo))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Error al eliminar el período " + periodo.anoPeriodo + ", intentelo de nuevo');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "Se eliminó el período  " + periodo.anoPeriodo + " exitosamente');", true);
            }
            Session["listaPeriodos"] = listaPeriodos;

            MostrarPeriodos();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalConfirmarPeriodo", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalConfirmarPeriodo').hide();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalEliminarPeriodo", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalEliminarPeriodo').hide();", true);
        }

        #endregion

        #region metodos paginacion
        public void Paginacion()
        {
            var dt = new DataTable();
            dt.Columns.Add("IndexPagina"); //Inicia en 0
            dt.Columns.Add("PaginaText"); //Inicia en 1

            primerIndex = paginaActual - 2;
            if (paginaActual > 2)
                ultimoIndex = paginaActual + 2;
            else
                ultimoIndex = 4;

            //se revisa que la ultima pagina sea menor que el total de paginas a mostrar, sino se resta para que muestre bien la paginacion
            if (ultimoIndex > Convert.ToInt32(ViewState["TotalPaginas"]))
            {
                ultimoIndex = Convert.ToInt32(ViewState["TotalPaginas"]);
                primerIndex = ultimoIndex - 4;
            }

            if (primerIndex < 0)
                primerIndex = 0;

            //se crea el numero de paginas basado en la primera y ultima pagina
            for (var i = primerIndex; i < ultimoIndex; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptPaginacion.DataSource = dt;
            rptPaginacion.DataBind();
        }

        /// <summary>
        /// Mariela Calvo
        /// marzo/2020
        /// Efecto: se devuelve a la primera pagina y muestra los datos de la misma
        /// Requiere: dar clic al boton de "Primer pagina"
        /// Modifica: elementos mostrados en la tabla de contactos
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbPrimero_Click(object sender, EventArgs e)
        {
            paginaActual = 0;
            MostrarPeriodos();
        }

        /// <summary>
        /// Mariela Calvo
        /// marzo/2020
        /// Efecto: se devuelve a la pagina anterior y muestra los datos de la misma
        /// Requiere: dar clic al boton de "Anterior pagina"
        /// Modifica: elementos mostrados en la tabla de contactos
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbAnterior_Click(object sender, EventArgs e)
        {
            paginaActual -= 1;
            MostrarPeriodos();
        }

        /// <summary>
        /// Mariela Calvo
        /// marzo/2020
        /// Efecto: se devuelve a la pagina siguiente y muestra los datos de la misma
        /// Requiere: dar clic al boton de "Siguiente pagina"
        /// Modifica: elementos mostrados en la tabla de contactos
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbSiguiente_Click(object sender, EventArgs e)
        {
            paginaActual += 1;
            MostrarPeriodos();
        }

        /// <summary>
        /// Mariela Calvo
        /// marzo/2020
        /// Efecto: se devuelve a la ultima pagina y muestra los datos de la misma
        /// Requiere: dar clic al boton de "Ultima pagina"
        /// Modifica: elementos mostrados en la tabla de contactos
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbUltimo_Click(object sender, EventArgs e)
        {
            paginaActual = (Convert.ToInt32(ViewState["TotalPaginas"]) - 1);
            MostrarPeriodos();
        }

        /// <summary>
        /// Mariela Calvo 
        /// marzo/2020
        /// Efecto: actualiza la la pagina actual y muestra los datos de la misma
        /// Requiere: -
        /// Modifica: elementos de la tabla
        /// Devuelve: -
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptPaginacion_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("nuevaPagina")) return;
            paginaActual = Convert.ToInt32(e.CommandArgument.ToString());
            MostrarPeriodos();
        }

        /// Mariela Calvo
        /// marzo/2020
        /// Efecto: marca el boton de la pagina seleccionada
        /// Requiere: dar clic al boton de paginacion
        /// Modifica: color del boton seleccionado
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptPaginacion_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPagina = (LinkButton)e.Item.FindControl("lbPaginacion");
            if (lnkPagina.CommandArgument != paginaActual.ToString()) return;
            lnkPagina.Enabled = false;
            lnkPagina.BackColor = Color.FromName("#005da4");
            lnkPagina.ForeColor = Color.FromName("#FFFFFF");
        }
        #endregion

    }
}