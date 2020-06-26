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

namespace ControlAsistentes.CatalogoUTI.Tarjetas
{
    public partial class AdministrarTarjetas : System.Web.UI.Page
    {
        #region variables globales

        private TarjetaServicios tarjetaServicios = new TarjetaServicios();
        private AsistenteServicios asistenteServicios = new AsistenteServicios();
        private const string NUEVO = "NUEVO";
        private const string EDITAR = "EDITAR";
        private const string ELIMINAR = "ELIMINAR";

        //Paginacion
        readonly PagedDataSource pgsource = new PagedDataSource();
        private int elmentosMostrar = 10;

        #region variables globales paginacion tarjetas
        int primerIndex, ultimoIndex;
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

        #region variables globales paginacion asistentes
        int primerIndexAsistentes, ultimoIndexAsistentes;
        private int paginaActualAsistentes
        {
            get
            {
                if (ViewState["paginaActualAsistentes"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["paginaActualAsistentes"]);
            }
            set
            {
                ViewState["paginaActualAsistentes"] = value;
            }
        }
        #endregion
        #endregion

        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            int[] rolesPermitidos = { 1 };
            Utilidades.escogerMenu(Page, rolesPermitidos);
            Page.Master.FindControl("MenuControl").Visible = false;

            if (!IsPostBack)
            {
                List<Asistente> asistentes = asistenteServicios.ObtenerAsistentes();
                List<Tarjeta> tarjetas = tarjetaServicios.ObtenerTarjetas();
                Session["listaTarjetas"] = tarjetas;
                Session["listaTarjetasFiltrada"] = tarjetas;
                mostrarTarjetas();
                Session["listaAsistentes"] = asistentes;
                Session["listaAsistentesFiltrada"] = asistentes;
                mostrarAsistentes();
            }
        }
        #endregion

        #region logica

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 27/03/2020
        /// Efecto: Habilita y restablece los campos del formulario
        /// Requiere: -
        /// Modifica: Campos del formulario
        /// Devuelve: -
        /// </summary>
        private void HabilitarFormulario()
        {
            txtNumeroTarjeta.CssClass = "form-control chat-input";
            txtNumeroTarjeta.Enabled = true;
            txtNumeroTarjeta.Text = "";
            cbxDisponible.Enabled = true;
            cbxDisponible.Checked = false;
            cbxExtraviada.Enabled = true;
            cbxExtraviada.Checked = false;
            panelTarjetaPagada.Visible = false;
            txtAsistente.Text = "";
            spanAgregarAsistenes.Visible = true;
            btnEliminarAsistente.Visible = true;
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 27/03/2020
        /// Efecto: Deshabilita los campos del formulario
        /// Requiere: -
        /// Modifica: Campos del formulario
        /// Devuelve: -
        /// </summary>
        private void DeshabilitarFormulario()
        {
            txtNumeroTarjeta.Enabled = false;
            cbxDisponible.Enabled = false;
            cbxExtraviada.Enabled = false;
            spanAgregarAsistenes.Visible = false;
            btnEliminarAsistente.Visible = false;
            cbxPagada.Enabled = false;
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 27/03/2020
        /// Efecto: Valida que los campos del formulario tengan el formato requerido
        /// Requiere: -
        /// Modifica: -
        /// Devuelve : True si esta correcto, False si hay un campo que no cumple con los requisitos
        /// </summary>
        private bool ValidaFormulario()
        {
            string action = (string)Session["action"];
            List<Tarjeta> tarjetas = (List<Tarjeta>)Session["listaTarjetas"];
            bool result = true;
            if (String.IsNullOrEmpty(txtNumeroTarjeta.Text) || (tarjetas.Any(tarjeta => tarjeta.numeroTarjeta.Equals(txtNumeroTarjeta.Text)) && action!= EDITAR && action!= ELIMINAR))
            {
                result = false;
                txtNumeroTarjeta.CssClass = "form-control alert-danger";
            }
            return result;
        }
        #endregion

        #region events
        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 01/11/2019
        /// Efecto: carga los datos filtrados en la tabla y realiza la paginacion correspondiente
        /// Requiere: -
        /// Modifica: los datos mostrados en pantalla
        /// Devuelve: -
        /// </summary>
        protected void mostrarTarjetas()
        {
            List<Tarjeta> listaTarjetas = (List<Tarjeta>)Session["listaTarjetas"];
            String filtro = "";
            if (!String.IsNullOrEmpty(txtBuscarTarjeta.Text))
            {
                filtro = txtBuscarTarjeta.Text;
            }
            List<Tarjeta> listaTarjetasFiltrada = (List<Tarjeta>)listaTarjetas.Where(tarjeta => tarjeta.numeroTarjeta.ToUpper().Contains(filtro.ToUpper())).ToList();
            Session["listaTarjetasFiltrada"] = listaTarjetasFiltrada;
            var dt = listaTarjetasFiltrada;
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
            rpTarjetas.DataSource = pgsource;
            rpTarjetas.DataBind();
            Paginacion();
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 30/03/2020
        /// Efecto: carga los datos filtrados en la tabla y realiza la paginacion correspondiente
        /// Requiere: -
        /// Modifica: los datos mostrados en pantalla
        /// Devuelve: -
        /// </summary>
        public void mostrarAsistentes()
        {
            List<Asistente> listaAsistentes = (List<Asistente>)Session["listaAsistentes"];
            String filtro = "";

            if (!String.IsNullOrEmpty(txtBuscarAsistente.Text))
            {
                filtro = txtBuscarAsistente.Text;
            }
            List<Asistente> listaAsistenteFiltrada = (List<Asistente>)listaAsistentes.Where(asistente => asistente.nombreCompleto.ToUpper().Contains(filtro.ToUpper())).ToList();
            Session["listaAsistentesFiltrada"] = listaAsistenteFiltrada;
            var dt = listaAsistenteFiltrada;
            pgsource.DataSource = dt;
            pgsource.AllowPaging = true;
            //numero de items que se muestran en el Repeater
            pgsource.PageSize = elmentosMostrar;
            pgsource.CurrentPageIndex = paginaActualAsistentes;
            //mantiene el total de paginas en View State
            ViewState["TotalPaginasAsistentes"] = pgsource.PageCount;
            //Ejemplo: "Página 1 al 10"
            lblpaginaAsistentes.Text = "Página " + (paginaActualAsistentes + 1) + " de " + pgsource.PageCount + " (" + dt.Count + " - elementos)";
            //Habilitar los botones primero, último, anterior y siguiente
            lbAnteriorAsistentes.Enabled = !pgsource.IsFirstPage;
            lbSiguienteAsistentes.Enabled = !pgsource.IsLastPage;
            lbPrimeroAsistentes.Enabled = !pgsource.IsFirstPage;
            lbUltimoAsistentes.Enabled = !pgsource.IsLastPage;
            rpAsistentes.DataSource = pgsource;
            rpAsistentes.DataBind();
            PaginacionAsistentes();
        }

        /// <summary>
        /// Leonardo Carrion
        /// 12/jun/2019
        /// Efecto: filtra la tabla segun los datos ingresados en los filtros
        /// Requiere: dar clic en el boton de flitrar e ingresar datos en los filtros
        /// Modifica: datos de la tabla
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            paginaActual = 0;
            mostrarTarjetas();
        }

        /// <summary>
        /// Leonardo Carrion
        /// 12/jun/2019
        /// Efecto: filtra la tabla segun los datos ingresados en los filtros
        /// Requiere: dar clic en el boton de flitrar e ingresar datos en los filtros
        /// Modifica: datos de la tabla
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFiltrarAsistentes_Click(object sender, EventArgs e)
        {
            paginaActual = 0;
            mostrarAsistentes();
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 31/03/2020
        /// Efecto: Guarda un asistente para asignarle la tarjeta
        /// Requiere: Seleccionar un asistente en el modal de asistentes
        /// Modifica: txtAsistente
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeleccionarAsistente_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            Asistente asistente = ((List<Asistente>)Session["listaAsistentesFiltrada"]).FirstOrDefault(a => a.idAsistente == id);
            txtAsistente.Text = asistente.nombreCompleto;
            cbxDisponible.Checked = false;
            Session["idAsistenteSeleccionado"] = id;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "closeModalAsistentes();", true);
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 31/03/2020
        /// Efecto: Visualmente elimina un asistente de una tarjeta
        /// Requiere: Clickear el boton "eliminar" del modal de asistentes
        /// Modifica: - txtAsistetes, session de idAsistente
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminarAsistente_Click(object sender, EventArgs e)
        {
            txtAsistente.Text = "";
            Session["idAsistenteSeleccionado"] = 0;
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 27/03/2020
        /// Efecto: Muestra un modal para agregar una tarjeta nueva
        /// Requiere: Clickear el boton "Nueva Tarjeta" del formulario
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarFormulario();
            modalTitle.Text = "Nueva Tarjeta";
            btnConfirmar.Text = "Guardar";
            cbxDisponible.Checked = true;
            cbxExtraviada.Checked = false;
            btnConfirmar.CssClass = "btn btn-primary boton-nuevo";
            Session["idAsistenteSeleccionado"] = 0;
            Session["action"] = NUEVO;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "openModalTarjetas();", true);
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 27/03/2020
        /// Efecto: Muestra un modal para editar una tarjeta 
        /// Requiere: Clickear el boton "Editar" de la tabla de tarjetas
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            HabilitarFormulario();
            int id = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Tarjeta> tarjetas = (List<Tarjeta>)Session["listaTarjetasFiltrada"];
            Tarjeta tarjetaSeleccionada = tarjetas.FirstOrDefault(tarjeta => tarjeta.idTarjeta == id);
            modalTitle.Text = "Editar Tarjeta";
            btnConfirmar.Text = "Actualizar";
            txtNumeroTarjeta.Text = tarjetaSeleccionada.numeroTarjeta;
            cbxDisponible.Checked = tarjetaSeleccionada.disponible;
            cbxExtraviada.Checked = tarjetaSeleccionada.tarjetaExtraviada;
            panelTarjetaPagada.Visible = cbxExtraviada.Checked;
            cbxPagada.Checked = tarjetaSeleccionada.pagada;
            txtAsistente.Text = tarjetaSeleccionada.asistente.nombreCompleto;
            btnConfirmar.CssClass = "btn btn-primary boton-editar";
            Session["idAsistenteSeleccionado"] = tarjetaSeleccionada.asistente.idAsistente;
            Session["action"] = EDITAR;
            Session["idSeleccionado"] = id;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "openModalTarjetas();", true);
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 27/03/2020
        /// Efecto: Muestra un modal para eliminar una tarjeta
        /// Requiere: Clickear el boton "Eliminar" de la tabla de tarjetas
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            DeshabilitarFormulario();
            int id = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Tarjeta> tarjetas = (List<Tarjeta>)Session["listaTarjetasFiltrada"];
            Tarjeta tarjetaSeleccionada = tarjetas.FirstOrDefault(tarjeta => tarjeta.idTarjeta == id);
            modalTitle.Text = "Eliminar Tarjeta";
            btnConfirmar.Text = "Eliminar";
            txtNumeroTarjeta.Text = tarjetaSeleccionada.numeroTarjeta;
            cbxDisponible.Checked = tarjetaSeleccionada.disponible;
            cbxExtraviada.Checked = tarjetaSeleccionada.tarjetaExtraviada;
            panelTarjetaPagada.Visible = cbxExtraviada.Checked;
            cbxPagada.Checked = tarjetaSeleccionada.pagada;
            txtAsistente.Text = tarjetaSeleccionada.asistente.nombreCompleto;
            btnConfirmar.CssClass = "btn btn-primary boton-eliminar";
            Session["idAsistenteSeleccionado"] = tarjetaSeleccionada.asistente.idAsistente;
            Session["action"] = ELIMINAR;
            Session["idSeleccionado"] = id;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "openModalTarjetas();", true);
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 27/03/2020
        /// Efecto: Confirma la accion del usuario
        /// Requiere: Clickear el boton Guardar, Actualizar o Eliminar del modal
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (ValidaFormulario())
            {
                Tarjeta tarjeta = new Tarjeta();
                tarjeta.numeroTarjeta = txtNumeroTarjeta.Text;
                tarjeta.tarjetaExtraviada = cbxExtraviada.Checked;
                tarjeta.disponible = cbxDisponible.Checked;
                tarjeta.pagada = cbxPagada.Checked;
                tarjeta.asistente = asistenteServicios.ObtenerAsistentes().FirstOrDefault(a => a.idAsistente == Convert.ToInt32(Session["idAsistenteSeleccionado"]));
                switch ((Session["action"]))
                {
                    case NUEVO:
                        tarjetaServicios.InsertarTarjeta(tarjeta);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "Se agregó correctamente la tarjeta " + tarjeta.numeroTarjeta+ " exitosamente');", true);
                        
                        break;
                    case EDITAR:
                        tarjeta.idTarjeta =  Convert.ToInt32(Session["idSeleccionado"]);
                        tarjetaServicios.ActualizarTarjeta(tarjeta);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "Se actualizó correctamente la tarjeta " + tarjeta.numeroTarjeta + " exitosamente');", true);
                        break;
                    case ELIMINAR:
                        tarjeta.idTarjeta = Convert.ToInt32(Session["idSeleccionado"]);
                        tarjetaServicios.EliminarTarjeta(tarjeta);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "Se eliminó correctamente la tarjeta " + tarjeta.numeroTarjeta + " exitosamente');", true);
                        break;
                    default:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Algo salió mal, intentelo de nuevo');", true);
                        break;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "closeModalTarjetas();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Formulario Incompleto');", true);
            }
            List<Tarjeta> tarjetas = tarjetaServicios.ObtenerTarjetas();
            Session["listaTarjetas"] = tarjetas;
            mostrarTarjetas();
        }

        /// <summary>
        /// Jean Carlos Monge Mendez 
        /// 30/03/2020
        /// Efecto: Regresa al menu principal de la aplicacion
        /// Requiere: Clickear el boton "Atras" del formulario
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAtras_Click(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Default.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Jean Carlos Monge Mendez
        /// 02/04/2020
        /// Efecto: Oculta o muestra el panel de tarjeta pagada
        /// Requiere: Cambiar la selecccion del cbxExtraviada
        /// Modifica: Formulario
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxExtraviada_CheckedChanged(object sender, EventArgs e)
        {
                panelTarjetaPagada.Visible = cbxExtraviada.Checked;
        }
        #endregion

        #region paginacion

        #region tarjetas
            /// <summary>
            /// Leonardo Carrion
            /// 14/jun/2019
            /// Efecto: realiza la paginacion
            /// Requiere: -
            /// Modifica: paginacion mostrada en pantalla
            /// Devuelve: -
            /// </summary>
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
            mostrarTarjetas();
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
            mostrarTarjetas();
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
            mostrarTarjetas();
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
            mostrarTarjetas();
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
            mostrarTarjetas();
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

        #region asistentes
        /// <summary>
        /// Leonardo Carrion
        /// 14/jun/2019
        /// Efecto: realiza la paginacion
        /// Requiere: -
        /// Modifica: paginacion mostrada en pantalla
        /// Devuelve: -
        /// </summary>
        private void PaginacionAsistentes()
        {
            var dt = new DataTable();
            dt.Columns.Add("IndexPagina"); //Inicia en 0
            dt.Columns.Add("PaginaText"); //Inicia en 1

            primerIndexAsistentes = paginaActualAsistentes - 2;
            if (paginaActualAsistentes > 2)
                ultimoIndexAsistentes = paginaActualAsistentes + 2;
            else
                ultimoIndexAsistentes = 4;

            //se revisa que la ultima pagina sea menor que el total de paginas a mostrar, sino se resta para que muestre bien la paginacion
            if (ultimoIndexAsistentes > Convert.ToInt32(ViewState["TotalPaginasAsistentes"]))
            {
                ultimoIndexAsistentes = Convert.ToInt32(ViewState["TotalPaginasAsistentes"]);
                primerIndexAsistentes = ultimoIndexAsistentes - 4;
            }

            if (primerIndexAsistentes < 0)
                primerIndexAsistentes = 0;

            //se crea el numero de paginas basado en la primera y ultima pagina
            for (var i = primerIndexAsistentes; i < ultimoIndexAsistentes; i++)
            {
                var dr = dt.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                dt.Rows.Add(dr);
            }

            rptPaginacionAsistentes.DataSource = dt;
            rptPaginacionAsistentes.DataBind();
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
        protected void lbPrimeroAsistentes_Click(object sender, EventArgs e)
        {
            paginaActualAsistentes = 0;
            mostrarAsistentes();
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
        protected void lbUltimoAsistentes_Click(object sender, EventArgs e)
        {
            paginaActualAsistentes = (Convert.ToInt32(ViewState["TotalPaginasAsistentes"]) - 1);
            mostrarAsistentes();
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
        protected void lbAnteriorAsistentes_Click(object sender, EventArgs e)
        {
            paginaActualAsistentes -= 1;
            mostrarAsistentes();
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
        protected void lbSiguienteAsistentes_Click(object sender, EventArgs e)
        {
            paginaActualAsistentes += 1;
            mostrarAsistentes();
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
        protected void rptPaginacionAsistentes_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("nuevaPagina")) return;
            paginaActualAsistentes = Convert.ToInt32(e.CommandArgument.ToString());
            mostrarAsistentes();
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
        protected void rptPaginacionAsistentes_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPagina = (LinkButton)e.Item.FindControl("lbPaginacionAsistentes");
            if (lnkPagina.CommandArgument != paginaActualAsistentes.ToString()) return;
            lnkPagina.Enabled = false;
            lnkPagina.BackColor = Color.FromName("#005da4");
            lnkPagina.ForeColor = Color.FromName("#FFFFFF");
        }
        #endregion
        #endregion
    }
}