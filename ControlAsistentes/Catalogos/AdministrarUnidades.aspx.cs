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
    public partial class AdministrarUnidades : System.Web.UI.Page
    {
        #region Variable globales
        UnidadServicios unidadServicios = new UnidadServicios();
        EncargadoUnidadServicios encargadoUnidadServicios = new EncargadoUnidadServicios();
        EncargadoServicios encargadoServicios = new EncargadoServicios();
        public static Unidad unidadSeleccionada = new Unidad();

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
            object[] rolesPermitidos = { 1, 2, 5 };
            Page.Master.FindControl("MenuControl").Visible = false;
            if (!IsPostBack)
            {
                Session["listaUnidades"] = null;
                Session["listaUnidadesFiltrada"] = null;
                Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
                List<Unidad> listaUnidades = unidadServicios.ObtenerUnidades();
                Session["listaUnidades"] = listaUnidades;
                Session["listaUnidadesFiltrada"] = listaUnidades;
                MostrarUnidades();
                llenarDdlEncargados();
            }

        }

        #region Eventos

        protected void MostrarUnidades()
        {
            List<Unidad> listaUnidades = (List<Unidad>)Session["listaUnidades"];
            String nombreUnidad = "";

            if (!String.IsNullOrEmpty(txtBuscarNombre.Text))
            {
                nombreUnidad = txtBuscarNombre.Text;
            }

            List<Unidad> listaUnidadesFiltrada = (List<Unidad>)listaUnidades.Where(unidad => unidad.nombre.ToUpper().Contains(nombreUnidad.ToUpper())).ToList();

            Session["listaUnidadesFiltrada"] = listaUnidadesFiltrada;

            var dt = listaUnidadesFiltrada;
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
            rpUnidades.DataSource = pgsource;
            rpUnidades.DataBind();
            Paginacion();
        }

        /// <summary>
        /// Mariela Calvo
        /// 29/nov/2019
        /// Efecto: llean el DropDownList con los encargados que se encuentran en la base de datos
        /// Requiere: - 
        /// Modifica: DropDownList
        /// Devuelve: -
        /// </summary>
        protected void llenarDdlEncargados()
        {
            List<Encargado> encargados = new List<Encargado>();
            ddlEncargadoNueva.Items.Clear();
            encargados = encargadoServicios.listaEncargados();
            foreach (Encargado encargado in encargados)
            {
                ListItem itemEncargado = new ListItem(encargado.nombreCompleto, encargado.idEncargado + "");
                ddlEncargadoNueva.Items.Add(itemEncargado);
            }
        }

        /// <summary>
        /// Mariela Calvo
        /// Septiembre/2019
        /// Efecto: Activar modal nuevo periodo
        /// Requiere: Presionar boton nuevo periodo
        /// Modifica: Tabla Periodos
        /// Devuelve: -
        /// </summary>
        protected void btnNuevaUnidad_Click(object sender, EventArgs e)
        {
            txtNuevaUnidad.CssClass = "form-control";
            txtNuevaUnidad.Text = "";
            txtDescNueva.CssClass = "form-control";
            txtDescNueva.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevaUnidad();", true);
        }

        protected void btnGuardarNuevaUnidad(object sender, EventArgs e)
        {
            int idEncargado = Convert.ToInt32(ddlEncargadoNueva.SelectedValue);
            int idUnidad = 0;

            if (validarUnidadNueva())
            {

                Unidad unidad = new Unidad();
                unidad.nombre = txtNuevaUnidad.Text;
                unidad.descripcion = txtDescNueva.Text;
                unidad.disponible = true;
                Encargado encargado = new Encargado();
                encargado.idEncargado = idEncargado;

                idUnidad = unidadServicios.insertarUnidad(unidad);


                if (idUnidad != 0)
                {
                    unidad.idUnidad = idUnidad;
                    encargadoUnidadServicios.insertarEncargadoUnidad(unidad, encargado);
                    List<Unidad> listaUnidades = unidadServicios.ObtenerUnidades();
                    Session["listaUnidades"] = listaUnidades;
                    Session["listaUnidadesFiltrada"] = listaUnidades;
                    MostrarUnidades();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "La unidad " + unidad.nombre + " fue registrada con éxito!');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevaUnidad", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevaUnidad').hide();", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "La unidad no fue registrada, intente de nuevo');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Formulario Incompleto');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevaUnidad", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevaUnidad').hide();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevaUnidad();", true);
            }
        }


        /// <summary>
        /// Mariela Calvo
        /// 29/nov/2019
        /// Efecto: filtra la tabla segun los datos ingresados en los filtros
        /// Requiere: dar clic en el boton de flitrar e ingresar datos en los filtros
        /// Modifica: datos de la tabla
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFiltrarUnidad_Click(object sender, EventArgs e)
        {
            paginaActual = 0;
            MostrarUnidades();
        }

        /// <summary>
        /// Mariela Calvo
        /// 29/nov/2019
        /// Efecto: guarda los datos de la nueva unidad
        /// Requiere: dar clic en el boton de flitrar e ingresar datos en los filtros
        /// Modifica: datos de la tabla
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public Boolean validarUnidadNueva()
        {
            Boolean valido = true;
            txtNuevaUnidad.CssClass = "form-control";
            txtDescNueva.CssClass = "form-control";

            if (String.IsNullOrEmpty(txtNuevaUnidad.Text) || txtNuevaUnidad.Text.Trim() == String.Empty || txtNuevaUnidad.Text.Length > 255)
            {
                txtNuevaUnidad.CssClass = "form-control alert-danger";
                valido = false;
            }
            if (String.IsNullOrEmpty(txtDescNueva.Text) || txtDescNueva.Text.Trim() == String.Empty || txtDescNueva.Text.Length > 255)
            {
                txtDescNueva.CssClass = "form-control alert-danger";
                valido = false;
            }
            return valido;
        }

        /// <summary>
        /// Mariela Calvo
        /// Diciembre/2019
        /// Efecto: Activar modal eliminar unidad para proceder a eliminar una unidad
        /// Requiere: Presionar boto nuevo periodo
        /// Modifica: Unidades
        /// Devuelve: -
        /// </summary>
        protected void btnEliminarUnidad(object sender, EventArgs e)
        {

            int idUnidad = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            Unidad unidad = unidadServicios.ObtenerUnidadPorId(idUnidad);
            unidadSeleccionada = unidad;
            lbUnidadE2.Text = unidad.nombre;
            txtEncargadoEliminar.Text = unidad.encargado.nombreCompleto;
            txtDescripcionEliminar.Text = unidad.descripcion;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalEliminarUnidad();", true);
        }

        public void btnConfirmarEliminarUnidad(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalConfirmarUnidad", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalConfirmarUnidad').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalConfirmarUnidad()", true);
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
        public void eliminarUnidad(object sender, EventArgs e)
        {
            int idUnidad = unidadSeleccionada.idUnidad;
            int idEncargado = unidadSeleccionada.encargado.idEncargado;

            encargadoUnidadServicios.eliminarEncargadoUnidad(idUnidad, idEncargado);
            unidadServicios.eliminarUnidad(idUnidad);

            List<Unidad> listaUnidades = unidadServicios.ObtenerUnidades();

            if (!listaUnidades.Contains(unidadSeleccionada))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "La unidad " + unidadSeleccionada.nombre + " fue eliminada con éxito!');", true);
                Session["listaUnidades"] = listaUnidades;
                Session["listaUnidadesFiltrada"] = listaUnidades;
                MostrarUnidades();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalConfirmarUnidad", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalConfirmarUnidad').hide();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "La unidad " + unidadSeleccionada.nombre + " no fue eliminada, intente de nuevo');", true);
            }
        }

        /// <summary>
        /// Mariela Calvo
        /// Diciembre/2019
        /// Efecto: Activar modal eliminar unidad para proceder a eliminar una unidad
        /// Requiere: Presionar boto nuevo periodo
        /// Modifica: Unidades
        /// Devuelve: -
        /// </summary>
        protected void btnEditarUnidad(object sender, EventArgs e)
        {

            int idUnidad = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            unidadSeleccionada = unidadServicios.ObtenerUnidadPorId(idUnidad);

            txtNombreUnidadEditar.CssClass = "form-control";
            txtDescEditar.CssClass = "form-control";

            txtNombreUnidadEditar.Text = unidadSeleccionada.nombre;
            txtDescEditar.Text = unidadSeleccionada.descripcion;
            lbEncargadoEditar2.Text = unidadSeleccionada.encargado.nombreCompleto;


            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalEditarUnidad();", true);


        }
        /// <summary>
        /// Mariela Calvo
        /// Diciembre/2019
        /// Efecto: Activar modal eliminar unidad para proceder a eliminar una unidad
        /// Requiere: Presionar boto nuevo periodo
        /// Modifica: Unidades
        /// Devuelve: -
        /// </summary>
        public void editarUnidad(object sender, EventArgs e)
        {
            if (validarUnidadAEditar())
            {
                Unidad unidadEditar = unidadServicios.ObtenerUnidadPorId(unidadSeleccionada.idUnidad);
                unidadEditar.nombre = txtNombreUnidadEditar.Text;
                unidadEditar.descripcion = txtDescEditar.Text;
                unidadServicios.editarUnidad(unidadEditar);
                txtNombreUnidadEditar.Text = "";
                txtDescEditar.Text = "";

                List<Unidad> listaUnidades = unidadServicios.ObtenerUnidades();

                Session["listaUnidades"] = listaUnidades;
                Session["listaUnidadesFiltradas"] = listaUnidades;

                MostrarUnidades();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "La unidad " + unidadEditar.nombre + " fue registrada con éxito!');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalEditarUnidad", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalEditarUnidad').hide();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "La unidad no fue registrada, intente de nuevo.');", true);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalEditarUnidad", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalEditarUnidad').hide();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalEditarUnidad();", true);
            }
        }


        /// <summary>
        /// Mariela Calvo
        /// 29/nov/2019
        /// Efecto: guarda los datos de la nueva unidad
        /// Requiere: dar clic en el boton de flitrar e ingresar datos en los filtros
        /// Modifica: datos de la tabla
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public Boolean validarUnidadAEditar()
        {
            Boolean valido = true;
            txtNombreUnidadEditar.CssClass = "form-control";
            txtDescEditar.CssClass = "form-control";

            if (String.IsNullOrEmpty(txtNombreUnidadEditar.Text) || txtNombreUnidadEditar.Text.Trim() == String.Empty || txtNombreUnidadEditar.Text.Length > 255)
            {
                txtNombreUnidadEditar.CssClass = "form-control alert-danger";
                valido = false;
            }
            if (String.IsNullOrEmpty(txtDescEditar.Text) || txtDescEditar.Text.Trim() == String.Empty || txtDescEditar.Text.Length > 255)
            {
                txtDescEditar.CssClass = "form-control alert-danger";
                valido = false;
            }
            return valido;
        }


        public void btnDevolverse(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Default.aspx");
            Response.Redirect(url);
        }
        #endregion

        #region Paginación

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
        /// 29/nov/2019
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
            MostrarUnidades();
        }

        /// <summary>
        /// Mariela Calvo
        /// 29/nov/2019
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
            MostrarUnidades();
        }

        /// <summary>
        /// Mariela Calvo
        /// 29/nov/2019
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
            MostrarUnidades();
        }

        /// <summary>
        /// Mariela Calvo
        /// 29/nov/2019
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
            MostrarUnidades();
        }

        /// <summary>
        /// Mariela Calvo 
        /// 29/nov/2019
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
            MostrarUnidades();
        }

        /// Mariela Calvo
        /// 29/nov/2019
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