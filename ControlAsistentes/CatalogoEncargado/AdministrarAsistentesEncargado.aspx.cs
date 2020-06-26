using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistentes.CatalogoEncargado
{
    public partial class AdministrarAsistentesEncargado : System.Web.UI.Page
    {
        #region variables globales
        AsistenteServicios asistenteServicios = new AsistenteServicios();
        PeriodoServicios periodoServicios = new PeriodoServicios();
        NombramientoServicios nombramientoServicios = new NombramientoServicios();
        ArchivoServicios archivoServicios = new ArchivoServicios();
        UnidadServicios unidadServicios = new UnidadServicios();
        EncargadoAsistenteServicios encargadoAsistenteServicios = new EncargadoAsistenteServicios();
        Unidad unidadEncargado = new Unidad();
        public static Asistente asistenteSeleccionado = new Asistente();

        readonly PagedDataSource pgsource = new PagedDataSource();
        int primerIndex, ultimoIndex, primerIndex2, ultimoIndex2;
        private int elmentosMostrar = 10;
        #endregion

        #region paginacion
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
        protected void Page_Load(object sender, EventArgs e)
        {
            object[] rolesPermitidos = { 1, 2, 5 };
            Page.Master.FindControl("MenuControl").Visible = false;

            if (Session["nombreCompleto"] != null)
            {
                unidadEncargado = unidadServicios.ObtenerUnidadPorEncargado(Session["nombreCompleto"].ToString());
                tituloAS.Text = "" + unidadEncargado.nombre;
            }
            else
            {
                Session["nombreCompleto"] = "Wilson Arguello";
                unidadEncargado = unidadServicios.ObtenerUnidadPorEncargado(Session["nombreCompleto"].ToString());
                tituloAS.Text = "" + unidadEncargado.nombre;
            }

            if (!IsPostBack)
            {
                Session["listaAsistentes"] = null;
                Session["listaAsistentesFiltrada"] = null;

                List<Asistente> listaAsistentes = asistenteServicios.ObtenerAsistentesXUnidad(unidadEncargado.idUnidad);
                Session["listaAsistentes"] = listaAsistentes;
                Session["listaAsistentesFiltrada"] = listaAsistentes;

                MostrarAsistentes();
                Page.Form.Attributes.Add("enctype", "multipart/form-data");

            }
            
        }


        #region eventos
        protected void MostrarAsistentes()
        {
            List<Asistente> listaAsistentes = (List<Asistente>)Session["listaAsistentes"];
            String nombreasistente = "";

            if (!String.IsNullOrEmpty(txtBuscarNombre.Text))
            {
                nombreasistente = txtBuscarNombre.ToString();
            }

            List<Asistente> listaAsistentesFiltrada = (List<Asistente>)listaAsistentes.Where(asistente => asistente.nombreCompleto.ToUpper().Contains(nombreasistente.ToUpper())).ToList();
            Session["listaAsistentesFiltrada"] = listaAsistentesFiltrada;

            var dt = listaAsistentesFiltrada;
            pgsource.DataSource = dt;
            pgsource.AllowPaging = true;
            //numero de items que se Asistenten en el Repeater
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
            rpAsistentes.DataSource = pgsource;
            rpAsistentes.DataBind();
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
        /// Efecto: Activar modal nuevo asistente
        /// Requiere: Presionar boton nuevo asistente
        /// Modifica: Tabla Asistentes
        /// Devuelve: -
        /// </summary>
        protected void btnNuevoAsistente_Click(object sender, EventArgs e)
        {
            txtNombre.CssClass = "form-control";
            txtNombre.Text = "";
            txtTelefono.CssClass = "form-control";
            txtTelefono.Text = "";
            txtCarnet.CssClass = "form-control";
            txtCarnet.Text = "";

            txtUnidadNA.Text="  "+unidadEncargado.nombre;
            txtEncargado.Text = " " + unidadEncargado.encargado.nombreCompleto;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoAsistente();", true);
        }

        protected void guardarNuevoAsistente_Click(object sender, EventArgs e)
        {
            if (validarAsistenteNuevo())
            {
                int idAsistente = 0;
                /* INSERCIÓN ASISTENTE */
                Asistente asistente = new Asistente();
                asistente.nombreCompleto = txtNombre.Text;
                asistente.carnet = txtCarnet.Text;
                asistente.telefono = txtTelefono.Text;
                asistente.cantidadPeriodosNombrado = 0;
                idAsistente = asistenteServicios.insertarAsistente(asistente);
                asistente.idAsistente = idAsistente;
                encargadoAsistenteServicios.insertarEncargadoAsistente(unidadEncargado.encargado.idEncargado,idAsistente);
                
                List<Asistente> listaAsistentes = asistenteServicios.ObtenerAsistentesXUnidad(unidadEncargado.idUnidad);
                Session["listaAsistentes"] = listaAsistentes;
                Session["listaAsistentesFiltrada"] = listaAsistentes;
                MostrarAsistentes();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "Se registró el asistente " + asistente.nombreCompleto + " exitosamente!" + "');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoAsistente", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoAsistente').hide();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Formulario Incompleto" + "');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevaUnidad", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoAsistente').hide();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoAsistente();", true);
            }

        }




        /// <summary>
        ///Mariela Calvo
        /// marzo/2020
        /// Efecto: filtra la tabla segun los datos ingresados en los filtros
        /// Requiere: dar clic en el boton de flitrar e ingresar datos en los filtros
        /// Modifica: datos de la tabla
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void filtrarAsistentes(object sender, EventArgs e)
        {
            paginaActual = 0;
            MostrarAsistentes();

        }
        /// <summary>
        /// Mariela Calvo
        /// Marzo/2020
        /// Efecto: Validar que los datos del nuevo asistente sean ingresados
        /// Requiere: -
        /// Modifica: Tabla Asistentes
        /// Devuelve: -
        /// </summary>
        public Boolean validarAsistenteNuevo()
        {
            Boolean valido = true;

            txtNombre.CssClass = "form-control";
            txtCarnet.CssClass = "form-control";
            txtTelefono.CssClass = "form-control";

            #region nombre
            if (String.IsNullOrEmpty(txtNombre.Text) || txtNombre.Text.Trim() == String.Empty || txtNombre.Text.Length > 255)
            {
                txtNombre.CssClass = "form-control alert-danger";
                valido = false;
            }
            if (String.IsNullOrEmpty(txtCarnet.Text) || txtCarnet.Text.Trim() == String.Empty || txtCarnet.Text.Length > 255)
            {
                txtCarnet.CssClass = "form-control alert-danger";
                valido = false;
            }
            if (String.IsNullOrEmpty(txtTelefono.Text) || txtTelefono.Text.Trim() == String.Empty || txtTelefono.Text.Length > 255)
            {
                txtTelefono.CssClass = "form-control alert-danger";
                valido = false;
            }
            #endregion

            return valido;
        }

        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Validar que los datos del nuevo asistente sean ingresados
        /// Requiere: -
        /// Modifica: Tabla Asistentes
        /// Devuelve: -
        /// </summary>
        public Boolean validarAsistenteNuevoEditar()
        {
            Boolean valido = true;

            txtNombreAsistenteEditar.CssClass = "form-control";
            txtCarnetEd.CssClass = "form-control";
            txtTelefonoEd.CssClass = "form-control";

            #region nombre
            if (String.IsNullOrEmpty(txtNombreAsistenteEditar.Text) || txtNombreAsistenteEditar.Text.Trim() == String.Empty || txtNombreAsistenteEditar.Text.Length > 255)
            {
                txtNombreAsistenteEditar.CssClass = "form-control alert-danger";
                valido = false;
            }
            if (String.IsNullOrEmpty(txtCarnetEd.Text) || txtCarnetEd.Text.Trim() == String.Empty || txtCarnetEd.Text.Length > 255)
            {
                txtCarnetEd.CssClass = "form-control alert-danger";
                valido = false;
            }
            if (String.IsNullOrEmpty(txtTelefonoEd.Text) || txtTelefonoEd.Text.Trim() == String.Empty || txtTelefonoEd.Text.Length > 255)
            {
                txtTelefonoEd.CssClass = "form-control alert-danger";
                valido = false;
            }

            return valido;
        }
        protected void btnEditarAsistente(object sender, EventArgs e)
        {
            int idAsistente = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Asistente> asistentes = asistenteServicios.ObtenerAsistentesXUnidad(unidadEncargado.idUnidad);
            asistenteSeleccionado = asistentes.FirstOrDefault(a => a.idAsistente == idAsistente);

            txtCarnetEd.CssClass = "form-control";
            txtTelefonoEd.CssClass = "form-control";
            txtNombreAsistenteEditar.CssClass = "form-control";
            txtUnidadPE.CssClass = "form-control";
            txtEncargadoEd.CssClass = "form-control";

            txtCarnetEd.Text = asistenteSeleccionado.carnet;
            txtTelefonoEd.Text = asistenteSeleccionado.telefono;
            txtNombreAsistenteEditar.Text = asistenteSeleccionado.nombreCompleto;
            txtUnidadPE.Text = unidadEncargado.nombre;
            txtEncargadoEd.Text = unidadEncargado.encargado.nombreCompleto;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalEditarAsistente();", true);
        }
        public void editarAsistente(object sender, EventArgs e)
        {
            if (validarAsistenteNuevoEditar())
            {
                int idAsistente = asistenteSeleccionado.idAsistente;

                Asistente asistenteEditar = new Asistente();
                asistenteEditar.idAsistente = idAsistente;
                asistenteEditar.nombreCompleto = txtNombreAsistenteEditar.Text;
                asistenteEditar.carnet = txtCarnetEd.Text;
                asistenteEditar.telefono = txtTelefonoEd.Text;
                asistenteServicios.editarAsistente(asistenteEditar);
                txtNombreAsistenteEditar.Text = "";
                txtCarnetEd.Text = "";
                txtTelefono.Text = "";

                List<Asistente> listaAsistentees = asistenteServicios.ObtenerAsistentesXUnidad(unidadEncargado.idUnidad);

                Session["listaAsistentes"] = listaAsistentees;
                Session["listaAsistentesFiltrada"] = listaAsistentees;

                MostrarAsistentes();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "El asistente " + asistenteEditar.nombreCompleto + "  fue actualizado exitosamente!');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalEditarAsistente", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalEditarAsistente').hide();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalEditarAsistente", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalEditarAsistente').hide();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalEditarAsistente();", true);
            }
        }

        protected void btnEliminarAsistente(object sender, EventArgs e)
        {
            int idAsistente = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Asistente> asistentes = asistenteServicios.ObtenerAsistentesXUnidad(unidadEncargado.idUnidad);
            asistenteSeleccionado = asistentes.FirstOrDefault(a => a.idAsistente == idAsistente);

            txtCarneE.CssClass = "form-control";
            txtTelefonoE.CssClass = "form-control";
            txtUnidadEl.CssClass = "form-control";
            txtEncargadoEl.CssClass = "form-control";

            txtCarneE.Text = asistenteSeleccionado.carnet;
            txtTelefonoE.Text = asistenteSeleccionado.telefono;
            lbNombreAsistente.Text = asistenteSeleccionado.nombreCompleto;
            txtUnidadEl.Text = unidadEncargado.nombre;
            txtEncargadoEl.Text = unidadEncargado.encargado.nombreCompleto;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalEliminarAsistente();", true);
        }

        public void btnConfirmarEliminarAsistente(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalConfirmarAsistente", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalConfirmarAsistente').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalConfirmarAsistente()", true);
        }

        /// <summary>
        /// Mariela Calvo
        /// Noviembre/2019
        /// Efecto: Se encarga eliminar una unidad seleccionada
        /// Requiere:Seleccionar icono de basurero en alguna de las unidades
        /// Modifica: Tabla Unidades
        /// Devuelve: -
        /// </summary>
        ///
        public void eliminarAsistente(object sender, EventArgs e)
        {
            int idAsistente = asistenteSeleccionado.idAsistente;
            asistenteServicios.eliminarAsistente(idAsistente);
            List<Asistente> listaAsistentes = asistenteServicios.ObtenerAsistentesXUnidad(unidadEncargado.idUnidad);

            if (!listaAsistentes.Contains(asistenteSeleccionado))
            {
                listaAsistentes = asistenteServicios.ObtenerAsistentesXUnidad(unidadEncargado.idUnidad);
                Session["listaAsistentes"] = listaAsistentes;
                Session["listaAsistentesFiltrada"] = listaAsistentes;
                MostrarAsistentes();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "El asistente " + asistenteSeleccionado.nombreCompleto + " fue eliminado exitosamente!');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalConfirmarAsistente", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalConfirmarAsistente').hide();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "El asistente no pudo ser eliminado, intente de nuevo');", true);
            }
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
        /// Efecto: se devuelve a la primera pagina y Asistente los datos de la misma
        /// Requiere: dar clic al boton de "Primer pagina"
        /// Modifica: elementos mostrados en la tabla de contactos
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbPrimero_Click(object sender, EventArgs e)
        {
            paginaActual = 0;
            MostrarAsistentes();
        }

        /// <summary>
        /// Mariela Calvo
        /// marzo/2020
        /// Efecto: se devuelve a la pagina anterior y Asistente los datos de la misma
        /// Requiere: dar clic al boton de "Anterior pagina"
        /// Modifica: elementos mostrados en la tabla de contactos
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbAnterior_Click(object sender, EventArgs e)
        {
            paginaActual -= 1;
            MostrarAsistentes();
        }

        /// <summary>
        /// Mariela Calvo
        /// marzo/2020
        /// Efecto: se devuelve a la pagina siguiente y Asistente los datos de la misma
        /// Requiere: dar clic al boton de "Siguiente pagina"
        /// Modifica: elementos mostrados en la tabla de contactos
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbSiguiente_Click(object sender, EventArgs e)
        {
            paginaActual += 1;
            MostrarAsistentes();
        }

        /// <summary>
        /// Mariela Calvo
        /// marzo/2020
        /// Efecto: se devuelve a la ultima pagina y Asistente los datos de la misma
        /// Requiere: dar clic al boton de "Ultima pagina"
        /// Modifica: elementos mostrados en la tabla de contactos
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbUltimo_Click(object sender, EventArgs e)
        {
            paginaActual = (Convert.ToInt32(ViewState["TotalPaginas"]) - 1);
            MostrarAsistentes();
        }

        /// <summary>
        /// Mariela Calvo 
        /// marzo/2020
        /// Efecto: actualiza la la pagina actual y Asistente los datos de la misma
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
            MostrarAsistentes();
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
#endregion