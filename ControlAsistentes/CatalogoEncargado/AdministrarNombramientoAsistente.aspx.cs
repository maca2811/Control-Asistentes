using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlAsistentes.CatalogoEncargado
{
    public partial class AdministrarNombramientoAsistente : System.Web.UI.Page
    {
        #region variables globales
        AsistenteServicios asistenteServicios = new AsistenteServicios();
        UnidadServicios unidadServicios = new UnidadServicios();
        PeriodoServicios periodoServicios = new PeriodoServicios();
        NombramientoServicios nombramientoServicios = new NombramientoServicios();
        public static Nombramiento nombramientoSeleccionado = new Nombramiento();
        ArchivoServicios archivoServicios = new ArchivoServicios();
        public static Unidad unidadEncargado = new Unidad();
        public static Asistente asistenteSelecionado = new Asistente();
        readonly PagedDataSource pgsource = new PagedDataSource();
        int primerIndex, ultimoIndex, primerIndex2, ultimoIndex2;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            object[] rolesPermitidos = { 1, 2, 5 };
            Page.Master.FindControl("MenuControl").Visible = false;


            if (Session["nombreCompleto"] != null)
            {
                unidadEncargado = unidadServicios.ObtenerUnidadPorEncargado(Session["nombreCompleto"].ToString());
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
                Session["listaAsistentes"] = null;
                Session["listaAsistentesFiltrada"] = null;
                Session["archivos"] = null;

                List<Nombramiento> listaAsistentes = asistenteServicios.ObtenerAsistentesXUnidadSinNombrar(unidadEncargado.idUnidad);
                Session["listaAsistentes"] = listaAsistentes;
                Session["listaAsistentesFiltrada"] = listaAsistentes;

                List<Asistente> listaAsistentesAnombrar = asistenteServicios.ObtenerAsistentesSinNombramiento(unidadEncargado.idUnidad);
                Session["listaAsistentesAnombrar"] = listaAsistentesAnombrar;
                Session["listaAsistentesAnombrarFiltrada"] = listaAsistentesAnombrar;

                ddlPeriodos();
                MostrarAsistentes();
                mostrarAsistentesAnombrar();
            }
            else
            {
                if (fileExpediente.HasFiles)
                {
                    Session["archivos"] = fileExpediente;
                }
                if (Session["archivos"] != null)
                {
                    fileExpediente = (FileUpload)Session["archivos"];
                }
            }
        }


        //#region eventos
        protected void MostrarAsistentes()
        {
            List<Nombramiento> listaAsistentes = (List<Nombramiento>)Session["listaAsistentes"];
            String nombreasistente = "";

            if (!String.IsNullOrEmpty(txtBuscarNombre.Text))
            {
                nombreasistente = txtBuscarNombre.Text;
            }

            List<Nombramiento> listaAsistentesFiltrada = (List<Nombramiento>)listaAsistentes.Where(nombramiento => nombramiento.asistente.nombreCompleto.ToUpper().Contains(nombreasistente.ToUpper())).ToList();

            Session["listaAsistentesFiltrada"] = listaAsistentesFiltrada;

            var dt = listaAsistentesFiltrada;
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
            rpAsistentes.DataSource = pgsource;

            rpAsistentes.DataBind();
            Paginacion();
        }

        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
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
        /// Marzo20
        /// Efecto: llean el DropDownList con el periodo actual para el nombramiento
        /// Requiere: - 
        /// Modifica: DropDownList
        /// Devuelve: -
        /// </summary>
        protected void ddlPeriodos()
        {
            List<Periodo> periodos = new List<Periodo>();
            periodosDDL.Items.Clear();
            periodos = periodoServicios.ObtenerPeriodos();

            foreach (Periodo periodo in periodos)
            {
                if (periodo.habilitado)
                {
                    ListItem itemPeriodos = new ListItem(periodo.semestre + " Semestre -" + periodo.anoPeriodo, periodo.idPeriodo + "");
                    periodosDDL.Items.Add(itemPeriodos);
                }
            }

        }
        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Devolverse al menu principal
        /// Requiere: -
        /// Modifica: 
        /// Devuelve: -
        /// </summary>
        public void btnDevolverse(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Default.aspx");
            Response.Redirect(url);
        }
        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Mostrar modal nuevo nombramiento
        /// Requiere: -
        /// Modifica:
        /// Devuelve: -
        /// </summary>
        protected void btnNombramiento_Click(object sender, EventArgs e)
        {
            txtHorasN.CssClass = "form-control";
            txtHorasN.Text = "";

            txtU.CssClass = "form-control";
            txtU.Text = unidadEncargado.nombre;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoNombramiento();", true);
        }
        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Guardar un nuevo nombramiento para una unidad en especifico junto con sus archivos
        /// Requiere: -
        /// Modifica:
        /// Devuelve: -
        /// </summary>
        protected void guardarNombramiento_Click(object sender, EventArgs e)
        {
            if (validarNombramiento())
            {
                string periodoSemestre = periodosDDL.SelectedValue.ToString();
                int idAsistente = asistenteSelecionado.idAsistente;
                int idPeriodo = Convert.ToInt32(periodosDDL.SelectedValue);
                Periodo periodo = periodoServicios.ObtenerPeriodoPorId(idPeriodo);
                Asistente asistente = (asistenteServicios.ObtenerAsistentesXUnidad(unidadEncargado.idUnidad)).FirstOrDefault(a => a.idAsistente == idAsistente);


                /* INSERCIÓN NOMBRAMIENTO ASISTENTE */
                Nombramiento nombramiento = new Nombramiento();
                nombramiento.asistente = asistente;
                nombramiento.periodo = periodo;
                Unidad unidad = new Unidad();
                unidad.idUnidad = unidadEncargado.idUnidad;
                nombramiento.unidad = unidad;
                nombramiento.aprobado = false;
                nombramiento.recibeInduccion = Convert.ToBoolean(ChckBxInduccion.Checked);
                nombramiento.cantidadHorasNombrado = Convert.ToInt32(txtHorasN.Text);
                int idNombramiento = nombramientoServicios.insertarNombramiento(nombramiento);
                nombramiento.idNombramiento = idNombramiento;

                /* INSERCIÓN ARCHIVOS DEL ASISTENTE */
                int tipo = 1;
                List<FileUpload> listaArchivosInsertar = new List<FileUpload>();
                listaArchivosInsertar.Add(fileExpediente);
                listaArchivosInsertar.Add(fileInforme);
                listaArchivosInsertar.Add(fileCV);
                listaArchivosInsertar.Add(fileCuenta);

                List<Archivo> listaArchivos = guardarArchivos(nombramiento, listaArchivosInsertar);

                foreach (Archivo archivo in listaArchivos)
                {
                    archivo.tipoArchivo = tipo;
                    int idArchivo = archivoServicios.insertarArchivo(archivo);
                    archivoServicios.insertarArchivoNombramiento(idArchivo, idNombramiento);
                    tipo++;
                }
                List<Nombramiento> listaAsistentes = asistenteServicios.ObtenerAsistentesXUnidadSinNombrar(unidadEncargado.idUnidad);
                Session["listaAsistentes"] = listaAsistentes;
                Session["listaAsistentesFiltrada"] = listaAsistentes;
                MostrarAsistentes();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "El asistente " + nombramiento.asistente.nombreCompleto + " fue registrado exitosamente!');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoNombramiento", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoNombramientohide();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Formulario Incompleto');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoNombramiento();", true);
            }

        }


        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Guardar los archivos del nombramiento del asistente
        /// Requiere: -
        /// Modifica:
        /// Devuelve: -
        /// </summary>
        public List<Archivo> guardarArchivos(Nombramiento nombramiento, List<FileUpload> files)
        {
            List<Archivo> listaArchivos = new List<Archivo>();

            String archivosRepetidos = "";
            foreach (FileUpload archivo in files)
            {
                foreach (HttpPostedFile file in archivo.PostedFiles)
                {
                    String nombreArchivo = Path.GetFileName(file.FileName);
                    nombreArchivo = nombreArchivo.Replace(' ', '_');
                    DateTime fechaHoy = new DateTime();
                    fechaHoy = DateTime.Now;
                    String carpeta = nombramiento.asistente.nombreCompleto + "-" + nombramiento.periodo.semestre + "_" + nombramiento.periodo.anoPeriodo;

                    int guardado = Utilidades.SaveFile(file, fechaHoy.Year, nombreArchivo, carpeta);

                    if (guardado == 0)
                    {
                        Archivo archivoNuevo = new Archivo();
                        archivoNuevo.nombreArchivo = nombreArchivo;
                        archivoNuevo.rutaArchivo = Utilidades.path + fechaHoy.Year + "\\" + carpeta + "\\" + nombreArchivo;
                        archivoNuevo.fechaCreacion = fechaHoy;
                        archivoNuevo.creadoPor = "Mariela Calvo";//Session["nombreCompleto"].ToString();
                        listaArchivos.Add(archivoNuevo);
                    }
                    else
                    {
                        archivosRepetidos += "* " + nombreArchivo + ", \n";
                    }
                }
            }

            if (archivosRepetidos.Trim() != "")
            {
                archivosRepetidos = archivosRepetidos.Remove(archivosRepetidos.Length - 3);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Los archivos " + archivosRepetidos + " no se pudieron guardar porque ya había archivos con ese nombre');", true);
            }

            return listaArchivos;
        }

        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Validar que los datos del nuevo nombramiento sean ingresados
        /// Requiere: -
        /// Modifica:
        /// Devuelve: -
        /// </summary>
        public Boolean validarNombramiento()
        {
            Boolean valido = true;

            txtHorasN.CssClass = "form-control";
            fileExpediente.CssClass = "form-control";
            fileInforme.CssClass = "form-control";
            fileCV.CssClass = "form-control";
            fileCuenta.CssClass = "form-control";



            #region nombre

            if (String.IsNullOrEmpty(txtHorasN.Text) || txtHorasN.Text.Trim() == String.Empty || txtHorasN.Text.Length > 255)
            {
                txtHorasN.CssClass = "form-control alert-danger";
                valido = false;
            }
            if (!fileExpediente.HasFile)
            {
                valido = false;
                fileExpediente.CssClass = "form-control alert-danger";
            }
            if (!fileInforme.HasFile)
            {
                valido = false;
                fileInforme.CssClass = "form-control alert-danger";
            }
            if (!fileCV.HasFile)
            {
                valido = false;
                fileCV.CssClass = "form-control alert-danger";
            }
            if (!fileCuenta.HasFile)
            {
                valido = false;
                fileCuenta.CssClass = "form-control alert-danger";
            }
            #endregion

            return valido;
        }


        #region asistentes nombramiento
        /// <summary>
        /// Jean Carlos Monge Mendez
        /// Abril/2020
        /// Efecto: Guarda un asistente para poder enviar a aprobacion su nombramiento
        /// Requiere: Seleccionar un asistente en el modal de asistentes
        /// Modifica: txtAsistente
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSeleccionarAsistente_Click(object sender, EventArgs e)
        {
            txtAsistente.CssClass = "form-control";
            int id = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            asistenteSelecionado = (asistenteServicios.ObtenerAsistentesSinNombramiento(unidadEncargado.idUnidad)).FirstOrDefault(a => a.idAsistente == id);
            txtAsistente.Text = asistenteSelecionado.nombreCompleto;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "cerrarModalAsistenteNombramiento();", true);
        }


        protected void btnFiltrarAsistentes_Click(object sender, EventArgs e)
        {
            paginaActual = 0;
            mostrarAsistentesAnombrar();
        }

        /// <summary>
        /// Mariela Calvo   
        /// Abril/2020
        /// Efecto: Visualmente elimina un asistente de una tarjeta
        /// Requiere: Clickear el boton "eliminar" del modal de asistentes
        /// Modifica: - txtAsistetes, session de idAsistente
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminarAsistenteNombramiento_Click(object sender, EventArgs e)
        {
            txtAsistente.Text = "";
            asistenteSelecionado = null;
        }
        /// <summary>
        /// Mariela Calvo   
        /// Abril/2020
        /// Efecto: Visualmente elimina un asistente de una tarjeta
        /// Requiere: Clickear el boton "eliminar"  de la tabla de nombramientos
        /// Modifica: - 
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEliminarNombramiento(object sender, EventArgs e)
        {
            int idNombramiento = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Nombramiento> nombramientos = nombramientoServicios.ObtenerNombramientosPorUnidad(unidadEncargado.idUnidad);
            nombramientoSeleccionado = nombramientos.FirstOrDefault(a => a.idNombramiento == idNombramiento);

            txtCarneE.CssClass = "form-control";
            txtHorasNE.CssClass = "form-control";
            txtPeriodosNE.CssClass = "form-control";
            txtPeriodoNE.CssClass = "form-control";

            txtCarneE.Text = nombramientoSeleccionado.asistente.carnet;
            txtHorasNE.Text = nombramientoSeleccionado.cantidadHorasNombrado + "";
            txtPeriodosNE.Text = nombramientoSeleccionado.asistente.cantidadPeriodosNombrado + "";
            txtPeriodoNE.Text = nombramientoSeleccionado.periodo.semestre + " Semestre - " + nombramientoSeleccionado.periodo.anoPeriodo;
            lbNombreAsistente.Text = nombramientoSeleccionado.asistente.nombreCompleto;


            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalEliminarNombramiento();", true);
        }

        /// <summary>
        /// Mariela Calvo   
        /// Abril/2020
        /// Efecto: Visualmente elimina un asistente de una tarjeta
        /// Requiere: Clickear el boton "eliminar" del modal de eliminar asistentes
        /// Modifica: - 
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnConfirmarEliminarNombramiento(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalConfirmarEliminarNombramiento", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalConfirmarEliminarNombramiento').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalConfirmarEliminarNombramiento()", true);
        }


        /// <summary>
        /// Mariela Calvo
        /// Noviembre/2019
        /// Efecto: Se encarga eliminar una unidad seleccionada
        /// Requiere:Seleccionar eliminar del modal confirmar
        /// Modifica: Tabla Unidades
        /// Devuelve: -
        /// </summary>
        ///
        public void eliminarNombramiento(object sender, EventArgs e)
        {
            nombramientoServicios.EliminarNombramiento(nombramientoSeleccionado.idNombramiento);
            List<Nombramiento> listaNombramientos = nombramientoServicios.ObtenerNombramientosPorUnidad(unidadEncargado.idUnidad);

            if (!listaNombramientos.Contains(nombramientoSeleccionado))
            {
                listaNombramientos = nombramientoServicios.ObtenerNombramientosPorUnidad(unidadEncargado.idUnidad);
                Session["listaAsistentes"] = listaNombramientos;
                Session["listaAsistentesFiltrada"] = listaNombramientos;
                MostrarAsistentes();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "El nombramiento del asistente fue eliminado exitosamente!);", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalConfirmarEliminarNombramiento", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalConfirmarNombramiento').hide();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "El nombramiento del asistente no pudo ser eliminado, intente nuevamente);", true);
            }
        }
        /// <summary>
        /// Mariela Calvo   
        /// Abril/2020
        /// Efecto: Visualmente elimina un asistente de una tarjeta
        /// Requiere: Clickear el boton "eliminar"  de la tabla de nombramientos
        /// Modifica: - 
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEditarNombramiento(object sender, EventArgs e)
        {
            int idNombramiento = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Nombramiento> nombramientos = nombramientoServicios.ObtenerNombramientosPorUnidad(unidadEncargado.idUnidad);
            nombramientoSeleccionado = nombramientos.FirstOrDefault(a => a.idNombramiento == idNombramiento);
            List<Archivo> archivos = nombramientoSeleccionado.listaArchivos;
            asistenteSelecionado = nombramientoSeleccionado.asistente;
            int AS = nombramientoSeleccionado.asistente.idAsistente;
           
            txtHorasM.CssClass = "form-control";
            txtAsistenteM.CssClass = "form-control";
            txtPeriodoM.CssClass = "form-control";
            txtUnidadM.CssClass = "form-control";

            fileExpedienteM.CssClass = "form-control";
            fileInformeM.CssClass = "form-control";
            fileCVM.CssClass = "form-control";
            fileCuenta.CssClass = "form-control";


            foreach (Archivo archivo in archivos)
            {
                if (archivo.tipoArchivo == 2)
                {
                    btnInforme.CommandArgument = archivo.idArchivo + "";
                }
                if (archivo.tipoArchivo == 1)
                {
                    btnExpediente.CommandArgument = archivo.idArchivo + "";
                }
                if (archivo.tipoArchivo == 3)
                {
                    btnCV.CommandArgument = archivo.idArchivo + "";
                }
                if (archivo.tipoArchivo == 4)
                {
                    btnCuenta.CommandArgument = archivo.idArchivo + "";
                }
            }

            txtUnidadM.Text = unidadEncargado.nombre;
            checkBM.Checked = nombramientoSeleccionado.recibeInduccion;
            txtHorasM.Text = nombramientoSeleccionado.cantidadHorasNombrado + "";
            txtPeriodoM.Text = nombramientoSeleccionado.periodo.semestre + " Semestre - " + nombramientoSeleccionado.periodo.anoPeriodo;
            txtAsistenteM.Text = nombramientoSeleccionado.asistente.nombreCompleto;


            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalEditarNombramiento();", true);
        }

        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Guardar un nuevo nombramiento para una unidad en especifico junto con sus archivos
        /// Requiere: -
        /// Modifica:
        /// Devuelve: -
        /// </summary>
        protected void editarNombramiento_Click(object sender, EventArgs e)
        {

            nombramientoSeleccionado.listaArchivos = archivoServicios.ObtenerArchivosAsistente(nombramientoSeleccionado.idNombramiento, nombramientoSeleccionado.periodo.idPeriodo);
            int editado = 0;

            if (validarEditarNombramiento())
            {
                /* INSERCIÓN NOMBRAMIENTO ASISTENTE */
                Nombramiento nombramiento = nombramientoSeleccionado;
                nombramiento.recibeInduccion = checkBM.Checked;
                nombramiento.cantidadHorasNombrado = Convert.ToInt32(txtHorasM.Text);
                nombramientoServicios.EditarNombramiento(nombramiento);

                List<FileUpload> listaArchivosEditar = new List<FileUpload>();
                List<int> tipoArchivos = new List<int>();

                if (fileExpedienteM.HasFiles)
                {
                    listaArchivosEditar.Add(fileExpedienteM);
                    tipoArchivos.Add(1);
                }
                if (fileInformeM.HasFiles)
                {
                    listaArchivosEditar.Add(fileInformeM);
                    tipoArchivos.Add(2);
                }
                if (fileCVM.HasFiles)
                {
                    listaArchivosEditar.Add(fileCVM);
                    tipoArchivos.Add(3);
                }
                if (fileCuentaM.HasFiles)
                {
                    listaArchivosEditar.Add(fileCVM);
                    tipoArchivos.Add(4);
                }



                if (listaArchivosEditar.Count > 0)
                {
                    foreach (FileUpload fileUp in listaArchivosEditar)
                    {
                        foreach (int tipo in tipoArchivos)
                        {
                            editado = editarArchivos(nombramientoSeleccionado, fileUp, tipo);
                        }
                    }
                }

                List<Nombramiento> listaAsistentes = asistenteServicios.ObtenerAsistentesXUnidadSinNombrar(unidadEncargado.idUnidad);
                Session["listaAsistentes"] = listaAsistentes;
                Session["listaAsistentesFiltrada"] = listaAsistentes;
                MostrarAsistentes();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "Se editó el nombramiento del asistente " + nombramiento.asistente.nombreCompleto + " exitosamente!);", true);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoNombramiento", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoNombramientohide();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Formulario Incompleto! );", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoNombramiento", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoNombramiento').hide();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoNombramiento();", true);
            }

        }

        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Validar que los datos del nuevo nombramiento sean ingresados
        /// Requiere: -
        /// Modifica:
        /// Devuelve: -
        /// </summary>
        public Boolean validarEditarNombramiento()
        {
            Boolean valido = true;

            txtHorasM.CssClass = "form-control";

            #region nombre

            if (String.IsNullOrEmpty(txtHorasM.Text) || txtHorasM.Text.Trim() == String.Empty || txtHorasM.Text.Length > 255)
            {
                txtHorasN.CssClass = "form-control alert-danger";
                valido = false;
            }

            #endregion

            return valido;
        }

        /// <summary>
        /// Mariela Calvo
        /// Mayo/2020
        /// Efecto: Guardar los archivos del nombramiento del asistente
        /// Requiere: -
        /// Modifica:
        /// Devuelve: -
        /// </summary>
        public int editarArchivos(Nombramiento nombramiento, FileUpload fileUp, int tipo)
        {
            int editado = 0;


            foreach (HttpPostedFile file in fileUp.PostedFiles)
            {

                String nombreArchivo = Path.GetFileName(file.FileName);
                nombreArchivo = nombreArchivo.Replace(' ', '_');
                DateTime fechaHoy = new DateTime();
                fechaHoy = DateTime.Now;
                String carpeta = nombramiento.asistente.nombreCompleto + "-" + nombramiento.periodo.semestre + "_" + nombramiento.periodo.anoPeriodo; ;
                String nuevoTextoConsecutivo = file.FileName;
                editado = Utilidades.OverWriteFile(fileUp, fechaHoy.Year, nombreArchivo, nuevoTextoConsecutivo, carpeta);

                if (editado == 0)
                {
                    Archivo archivoNuevo = new Archivo();
                    archivoNuevo.nombreArchivo = nombreArchivo;
                    archivoNuevo.rutaArchivo = Utilidades.path + fechaHoy.Year + "\\" + carpeta + "\\" + nombreArchivo;
                    archivoNuevo.fechaCreacion = fechaHoy;
                    archivoNuevo.creadoPor = "Mariela Calvo";//Session["nombreCompleto"].ToString();
                    archivoNuevo.tipoArchivo = tipo;
                    int idA = archivoServicios.insertarArchivo(archivoNuevo);
                    archivoServicios.insertarArchivoNombramiento(idA, nombramiento.idNombramiento);
                }
            }

            return editado;
        }

        /// <summary>
        /// Mariela Calvo   
        /// Abril/2020
        /// Efecto: Visualmente elimina un asistente de una tarjeta
        /// Requiere: Clickear el boton "eliminar"  de la tabla de nombramientos
        /// Modifica: - 
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDetallesNombramiento(object sender, EventArgs e)
        {
            int idNombramiento = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            Nombramiento nombramiento = nombramientoServicios.ObtenerDetallesNombramiento(idNombramiento);


            txtAsistenteD.CssClass = "form-control";
            txtDetalles.CssClass = "form-control";
            txtSolicitudD.Visible = false;
            Label18.Visible = false;
            if (nombramiento.aprobado && nombramiento.solicitud == 1)
            {
                txtSolicitudD.Style.Add("background-color", "#0BA55E");
                txtSolicitudD.Text = "APROBADO";
            }
            else if (!nombramiento.aprobado && nombramiento.solicitud == 2)
            {
                txtSolicitudD.Style.Add("background-color", "#D96F6F");
                txtSolicitudD.Text = "RECHAZADO";
            }
            else if (!nombramiento.aprobado && nombramiento.solicitud == 0)
            {
                txtSolicitudD.Style.Add("background-color", "#E88C01");
                txtSolicitudD.Text = "PENDIENTE";
            }

            txtAsistenteD.Text = nombramiento.asistente.nombreCompleto;
            txtDetalles.Text = nombramiento.observaciones;


            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalDetallesNombramiento();", true);
        }
        /// <summary>
        /// Mariela Calvo   
        /// Abril/2020
        /// Efecto: Visualmente elimina un asistente de una tarjeta
        /// Requiere: Clickear el boton "eliminar"  de la tabla de nombramientos
        /// Modifica: - 
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVerArchivos_Click(object sender, EventArgs e)
        {
            String idAsistente = (((LinkButton)(sender)).CommandArgument).ToString();
            List<Asistente> listAsistente = new List<Asistente>();
            listAsistente = asistenteServicios.ObtenerAsistentes();
            List<Asistente> tempAsistente = new List<Asistente>();
            tempAsistente = listAsistente.Where(item => item.idAsistente == Convert.ToInt32(idAsistente)).ToList();
            List<Archivo> listArchivosAsistente = archivoServicios.ObtenerArchivosAsistente(Convert.ToInt32(idAsistente),1);
            foreach (Archivo archivo in listArchivosAsistente)
            {
                try
                {
                    FileStream fileStream = new FileStream(archivo.rutaArchivo, FileMode.Open, FileAccess.Read);
                    BinaryReader binaryReader = new BinaryReader(fileStream);
                    Byte[] blobValue = binaryReader.ReadBytes(Convert.ToInt32(fileStream.Length));

                    fileStream.Close();
                    binaryReader.Close();

                    descargar(archivo.rutaArchivo);
                }
                catch (DirectoryNotFoundException)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Error al cargar los archvios!);", true);
                    (this.Master as SiteMaster).Toastr("error", "Error al cargar los archivos");

                }
            }
            if (listArchivosAsistente.Count() == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "No contiene archvios asociados!);", true);
            }

        }

        /// <summary>
        /// Mariela Calvo   
        /// Abril/2020
        /// Efecto: Visualmente elimina un asistente de una tarjeta
        /// Requiere: Clickear el boton "eliminar"  de la tabla de nombramientos
        /// Modifica: - 
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnVerArchivo_Click(object sender, EventArgs e)
        {
            String idAsistente = asistenteSelecionado.idAsistente + "";
            int idArchivo = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            List<Asistente> listAsistente = new List<Asistente>();
            listAsistente = asistenteServicios.ObtenerAsistentes();
            List<Asistente> tempAsistente = new List<Asistente>();
            tempAsistente = listAsistente.Where(item => item.idAsistente == Convert.ToInt32(idAsistente)).ToList();
            Archivo archivosAsistente = archivoServicios.ObtenerArchivoAsistente(Convert.ToInt32(idAsistente), idArchivo);
            try
            {
                FileStream fileStream = new FileStream(archivosAsistente.rutaArchivo, FileMode.Open, FileAccess.Read);
                BinaryReader binaryReader = new BinaryReader(fileStream);
                Byte[] blobValue = binaryReader.ReadBytes(Convert.ToInt32(fileStream.Length));

                fileStream.Close();
                binaryReader.Close();

                descargar(archivosAsistente.rutaArchivo);
            }
            catch (DirectoryNotFoundException)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Error al cargar los archvios!);", true);
                (this.Master as SiteMaster).Toastr("error", "Error al cargar los archivos");

            }

            if (archivosAsistente.idArchivo == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "No contiene archvios asociados!);", true);
            }

        }
        /// <summary>
        /// Mariela Calvo   e
        /// Abril/2020
        /// Efecto: Permite descargar los documentos para que puedan verlos
        /// Requiere: Clickear el boton "Ver documentos" de la tabla nombramiento
        /// Modifica: - 
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void descargar(string ruta)
        {

            Process proceso = new Process();
            proceso.StartInfo.FileName = ruta;
            proceso.Start();


        }
        /// <summary>
        /// Mariela Calvo   
        /// Abril/2020
        /// Efecto: Visualmente elimina un asistente de una tarjeta
        /// Requiere: Clickear el boton "eliminar" del modal de eliminar asistentes
        /// Modifica: - 
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mostrarAsistentesAnombrar()
        {
            List<Asistente> listaAsistentes = (List<Asistente>)Session["listaAsistentesAnombrar"];
            String filtro = "";

            if (!String.IsNullOrEmpty(txtBuscarAsistente.Text))
            {
                filtro = txtBuscarAsistente.Text;
            }
            List<Asistente> listaAsistenteFiltrada = (List<Asistente>)listaAsistentes.Where(asistente => asistente.nombreCompleto.ToUpper().Contains(filtro.ToUpper())).ToList();
            Session["listaAsistentesAnombrarFiltrada"] = listaAsistenteFiltrada;
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
            rpAsistentesNombramiento.DataSource = pgsource;
            rpAsistentesNombramiento.DataBind();
            PaginacionAsistentes();
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
            MostrarAsistentes();
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
            MostrarAsistentes();
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
            MostrarAsistentes();
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
            MostrarAsistentes();
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

        #region paginacion asistentes 
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
            mostrarAsistentesAnombrar();
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
            mostrarAsistentesAnombrar();
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
            mostrarAsistentesAnombrar();
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
            mostrarAsistentesAnombrar();
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
            mostrarAsistentesAnombrar();
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
        #endregion



    }
}