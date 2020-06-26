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

namespace ControlAsistentes.CatalogoUTI.Asistentes
{
    public partial class AdministrarUTIAsistentes : System.Web.UI.Page
    {

        #region variables globales

        AsistenteServicios asistenteServicios = new AsistenteServicios();
        UnidadServicios UnidadServicios = new UnidadServicios();
        TarjetaServicios TarjetaServicios = new TarjetaServicios();
        UsuariosServicios UsuariosServicios = new UsuariosServicios();
        NombramientoServicios nombramientoServicios = new NombramientoServicios();
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
        #endregion



        protected void Page_Load(object sender, EventArgs e)
        {
            object[] rolesPermitidos = { 1, 2, 5 };
            Page.Master.FindControl("MenuControl").Visible = false;

            if (!IsPostBack)
            {
                Session["listaAsistentes"] = null;
                Session["listaAsistentesFiltrada"] = null;

                List<Asistente> listaAsistentes = asistenteServicios.ObtenerAsistentes();
                Session["listaAsistentes"] = listaAsistentes;
                Session["listaAsistentesFiltrada"] = listaAsistentes;

                unidadesDDL();
                MostrarAsistentes();

            }
        }

        protected void MostrarAsistentes()
        {
            List<Asistente> listaAsistentes = (List<Asistente>)Session["listaAsistentes"];
            String filtro = "";


            if (!String.IsNullOrEmpty(txtBuscarNombre.Text))
            {
                filtro = txtBuscarNombre.Text;
            }

            List<Asistente> listaAsistentesFiltrada = (List<Asistente>)listaAsistentes.Where(asistente => asistente.nombreCompleto.ToUpper().Contains(filtro.ToUpper())).ToList();
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

        protected void unidadesDDL()
        {
            List<Unidad> unidades = new List<Unidad>();
            ddlUnidad.Items.Clear();
            unidades = UnidadServicios.ObtenerUnidades();
            ddlUnidad.Items.Add("Seleccione la Unidad");
            foreach (Unidad unidad in unidades)
            {
                ListItem itemEncargado = new ListItem(unidad.nombre, unidad.idUnidad + "");
                ddlUnidad.Items.Add(itemEncargado);
            }
        }


        /// <summary>
        /// Karen Guillén
        /// 15/04/20
        /// Efecto: Se cargan los datos del asisente y su respectiva tarjeta, y habilita o deshabilita la posibilidad de modificar. 
        /// Requiere: - 
        /// Modifica: #modalTarjetaAsistente
        /// Devuelve: -
        /// </summary>
        protected void CargaAsistenteTarjeta(String carnet)
        {
            //HabilitarFormulario();

            List<Asistente> asistentes = (List<Asistente>)Session["listaAsistentesFiltrada"];
            Asistente asistenteSeleccionado = asistentes.FirstOrDefault(asistente => asistente.carnet == carnet);

            txtAsistente.Text = asistenteSeleccionado.nombreCompleto;
            List<Tarjeta> listaTarjetas = TarjetaServicios.ObtenerTarjetas();
            DeshabilitarFormularioAsistente();
            btnAsignar.Enabled = true;

            foreach (Tarjeta tarjeta in listaTarjetas)
            {
                txtNumeroTarjeta.Text = "";
                rdExtraviada.Checked = false;

                if (tarjeta.asistente.carnet == asistenteSeleccionado.carnet)
                {
                    txtNumeroTarjeta.Text = tarjeta.numeroTarjeta;
                    rdExtraviada.Checked = tarjeta.tarjetaExtraviada;
                    if (tarjeta.numeroTarjeta != "")
                    {
                        btnAsignar.Enabled = false;
                    }
                }
            }

        }

        /// <summary>
        /// Karen Guillén A
        /// 30/04/2020
        /// Efecto: Deshabilita los campos del formulario
        /// Requiere: -
        /// Modifica: Campos del formulario
        /// Devuelve: -
        /// </summary>
        private void DeshabilitarFormularioAsistente()
        {
            rdExtraviada.Checked = false;
            txtNumeroTarjeta.CssClass = "form-control chat-input";
            txtNumeroTarjeta.Enabled = false;
            txtAsistente.CssClass = "form-control chat-input";
            txtAsistente.Enabled = false;

            rdExtraviada.Enabled = false;

        }

        /// <summary>
        /// Karen Guillén
        /// 17/04/20
        /// Efecto: Se cargan los datos del asistente y su respectivo usuario, en el modal AsistenteUsuario
        /// Requiere: - 
        /// Modifica: #modalAsistenteUsuario
        /// Devuelve: -
        /// </summary>
        protected void CargaUsuario(String carnet)
        {
            // HabilitarFormulario();

            List<Asistente> nombramientos = (List<Asistente>)Session["listaAsistentesFiltrada"];
            Asistente asistenteSeleccionado = nombramientos.FirstOrDefault(asistente =>asistente.carnet == carnet);


            txtAsistenteU.Text = asistenteSeleccionado.nombreCompleto;
            List<Usuario> listaUsuarios = UsuariosServicios.ObtenerUsuarios();

            DeshabilitarFormularioUsuario();
            btnAsignarUsuario.Enabled = true;
            foreach (Usuario usuario in listaUsuarios)
            {
                //txtNombre.Text = "";
                txtContrasenia.Text = "";
                rbDisponible.Checked = false;
                if (usuario.asistente != null)
                {
                    if (usuario.asistente.carnet == asistenteSeleccionado.carnet)
                    {

                        txtNombre.Text = usuario.nombre;
                        txtContrasenia.Attributes.Add("value", usuario.contraseña);

                        rbDisponible.Checked = usuario.disponible;
                        if (usuario.nombre != "")
                        {
                            btnAsignarUsuario.Enabled = false;
                        }

                    }
                }
                
            }
        }

        /// <summary>
        /// Karen Guillén
        /// 17/04/2020
        /// Efecto: Deshabilita los campos del modal AsistenteUsuario
        /// Requiere: -
        /// Modifica: #ModalAsistenteUsuario
        /// Devuelve: -
        /// </summary>
        private void DeshabilitarFormularioUsuario()
        {
            rbDisponible.Enabled = false;
            txtNombre.CssClass = "form-control chat-input";
            txtNombre.Enabled = false;
            txtContrasenia.CssClass = "form-control chat-input";
            txtContrasenia.Enabled = false;
            txtAsistenteU.CssClass = "form-control chat-input";
            txtAsistenteU.Enabled = false;

        }


        #region eventos

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

        public void btnDevolverse(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Default.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Karen Guillén
        /// 15/04/20
        /// Efecto: Abre el modal para ver y asignar tarjeta
        /// Requiere: Click en el icono de la tabla, para el asistente requerido
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnVerTarjetaAsistente(object sender, EventArgs e)
        {
            String carnet = (((LinkButton)(sender)).CommandArgument).ToString();
            CargaAsistenteTarjeta(carnet);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalTarjetaAsistente();", true);
        }



        /// <summary>
        /// Karen Guillén
        /// 17/04/20
        /// Efecto: Abre el modal para ver y asignar tarjeta
        /// Requiere: Click en el icono de la tabla, para el asistente requerido
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnVeUsuarioAsistente(object sender, EventArgs e)
        {
            String carnet = (((LinkButton)(sender)).CommandArgument).ToString();
            CargaUsuario(carnet);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalUsuarioAsistente();", true);
        }





        /// <summary>
        /// Karen Guillén
        /// 15/04/2020
        /// Efecto: Redireciona a Administrar Tarjeta
        /// Requiere: Click en el botón "Asignar" del modal Tarjeta Usuario
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnAsignar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CatalogoUTI/Tarjetas/AdministrarTarjetas.aspx");
        }

        /// <summary>
        /// Karen Guillén
        /// 17/04/2020
        /// Efecto: Redirecciona a Administrar Usuarios
        /// Requiere: Click en el botón "Asignar" del modal Usuario Asistente
        /// Modifica: -
        /// Devuelve: -
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnAsignarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CatalogoUTI/Usuarios/AdministrarUsuarios.aspx");
        }

        protected void ddlUnidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idUnidad = 0;
            if (!ddlUnidad.SelectedValue.Equals("Seleccione la Unidad"))
            {
                idUnidad = Convert.ToInt32(ddlUnidad.SelectedValue);
            }
           
            List<Asistente> listaAsistentes =asistenteServicios.ObtenerAsistentesPorUnidad1(idUnidad);

   

            Session["listaAsistentes"] = listaAsistentes;
            Session["listaAsistentesFiltrada"] = listaAsistentes;

            MostrarAsistentes();
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
        #endregion

    }
}