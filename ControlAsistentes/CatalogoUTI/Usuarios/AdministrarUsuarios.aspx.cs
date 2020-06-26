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

namespace ControlAsistentes.CatalogoUTI.Usuarios
{
    public partial class AdministrarUsuarios : System.Web.UI.Page
    {

        #region variables globales

        private UsuariosServicios usuariosServicios = new UsuariosServicios();
        private AsistenteServicios asistenteSevicios = new AsistenteServicios();
        static Usuario usuarioSeleccionado = new Usuario();
        public static int accion = 0;
        #region variables globales paginacion tarjetas
        readonly PagedDataSource pgsource = new PagedDataSource();
        int primerIndex, ultimoIndex;
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

        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            int[] rolesPermitidos = { 1 };
            Utilidades.escogerMenu(Page, rolesPermitidos);
            Page.Master.FindControl("MenuControl").Visible = false;

            if (!IsPostBack)
            {
                List<Usuario> usuarios = usuariosServicios.ObtenerUsuarios();
                Session["listaUsuarios"] = usuarios;
                Session["listaUsuariosFiltrada"] = usuarios;
                mostrarUsuarios();
            }
        }

        #endregion

        private void mostrarUsuarios()
        {
            List<Usuario> listaUsuarios = (List<Usuario>)Session["listaUsuariosFiltrada"];
            String filtro = "";
            if (!String.IsNullOrEmpty(txtBuscarUsuario.Text))
            {
                filtro = txtBuscarUsuario.Text;
            }
            List<Usuario> listaUsuariosFiltrada = (List<Usuario>)listaUsuarios.Where(x => x.nombre.ToUpper().Contains(filtro.ToUpper())).ToList();
            Session["listaUsuariosFiltrada"] = listaUsuariosFiltrada;
            var dt = listaUsuariosFiltrada;
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
            rpUsuarios.DataSource = pgsource;
            rpUsuarios.DataBind();
            Paginacion();
        }



        #region paginacion
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
            mostrarUsuarios();
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
            mostrarUsuarios();
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
            mostrarUsuarios();
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
            mostrarUsuarios();
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
            mostrarUsuarios();
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

        #region eventos
        public void btnDevolverse(object sender, EventArgs e)
        {
            String url = Page.ResolveUrl("~/Default.aspx");
            Response.Redirect(url);
        }

        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Elige donde va el usuario
        /// Requiere: Presionar boton guardar usuario
        /// Modifica: Tabla Usuarios
        /// Devuelve: -
        /// </summary>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            if (accion == 1)
            {
                btnGuardarNuevoUsuario();
            }
            else if (accion == 2)
            {
                btnEditarUsuario();
            }
            else
            {
                btnEliminarUsuario();
            }
        }

        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Guarda un nuevo usuario ingresado
        /// Requiere: Presionar boton guardar usuario
        /// Modifica: Tabla Usuarios
        /// Devuelve: -
        /// </summary>
        protected void btnEliminarUsuario()
        {
            lblModal.Text = "Eliminar Usuario";
            usuariosServicios.eliminarUsuarios(usuarioSeleccionado);

            List<Usuario> listaUsuarios = usuariosServicios.ObtenerUsuarios();
            Session["listaUsuarios"] = listaUsuarios;
            Session["listaUsuariosFiltrada"] = listaUsuarios;
            mostrarUsuarios();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "El usuario " + usuarioSeleccionado.nombre + " fue eliminado exitosamente!');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoUsuario", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoUsuario').hide();", true);

           


        }
        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Activar modal editar usuario
        /// Requiere: Presionar boton editar usuario
        /// Modifica: Tabla Usuarios
        /// Devuelve: -
        /// </summary>
        protected void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            accion = 2;
            int idUsuario = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            usuarioSeleccionado = usuariosServicios.ObtenerUsuarioPorId(idUsuario);
            usuarioSeleccionado.idUsuario = idUsuario;
            txtNuevoUsuario.CssClass = "form-control";
            txtNuevoUsuario.ReadOnly = false;
            txtNuevoUsuario.Text = usuarioSeleccionado.nombre;
            txtContrasena.CssClass = "form-control";
            txtContrasena.Text = usuarioSeleccionado.contraseña;
            txtContrasena.Attributes["Type"] = "text";
            txtContrasena.ReadOnly = false;
            llenarDdlAsistentes2();
            ddlSeleccionAsistenteNuevo.Enabled = true;
            lblModal.Text = "Editar Usuario";
            
            if (usuarioSeleccionado.asistente != null)
            {
                ddlSeleccionAsistenteNuevo.SelectedValue = usuarioSeleccionado.asistente.idAsistente + "";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoUsuario();", true);
        }
        
            /// <summary>
            /// Mariela Calvo
            /// Abril/2020
            /// Efecto: Activar modal editar usuario
            /// Requiere: Presionar boton editar usuario
            /// Modifica: Tabla Usuarios
            /// Devuelve: -
            /// </summary>
            protected void btnEliminarUsuario_Click(object sender, EventArgs e)
            {
            accion = 3;
            int idUsuario = Convert.ToInt32((((LinkButton)(sender)).CommandArgument).ToString());
            usuarioSeleccionado = usuariosServicios.ObtenerUsuarioPorId(idUsuario);
            txtNuevoUsuario.CssClass = "form-control";
            txtNuevoUsuario.Text = usuarioSeleccionado.nombre;
            txtNuevoUsuario.ReadOnly = true;
            txtContrasena.CssClass = "form-control";
            txtContrasena.ReadOnly = true;
            txtContrasena.Text = usuarioSeleccionado.contraseña;
            txtContrasena.Attributes["Type"] = "text";
            llenarDdlAsistentes2();
            ddlSeleccionAsistenteNuevo.Enabled = false;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoUsuario();", true);
        }

        /// <summary>
        /// Mariela Calvo
        /// Abril/2020
        /// Efecto: Guarda un nuevo usuario ingresado
        /// Requiere: Presionar boton guardar usuario
        /// Modifica: Tabla Usuarios
        /// Devuelve: -
        /// </summary>
        protected void btnEditarUsuario()
        {
            
            if (validaCamposUsuarios())//si campos estan llenos es igual a true
            {
                Usuario usuario = new Usuario();
                usuario.nombre = txtNuevoUsuario.Text;
                usuario.contraseña = txtContrasena.Text;
                usuario.idUsuario = usuarioSeleccionado.idUsuario;

                if (ddlSeleccionAsistenteNuevo.Text != "Sin asistente asignado")
                {
                    Asistente asistente = new Asistente();
                    asistente.idAsistente = Convert.ToInt32(ddlSeleccionAsistenteNuevo.SelectedValue);
                    usuario.asistente = asistente;
                    usuario.disponible = false;
                }
                else if (ddlSeleccionAsistenteNuevo.Text.Equals("Sin asistente asignado") && usuarioSeleccionado.asistente != null)
                {
                    usuario.asistente = usuarioSeleccionado.asistente;
                    usuario.disponible = usuarioSeleccionado.disponible;
                }
                else
                {
                    usuario.asistente = null;
                    usuario.disponible = true;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "El usuario " + usuario.nombre + " fue actualizado exitosamente!');", true);
                usuariosServicios.editarUsuarios(usuario);
                List<Usuario> listaUsuarios = usuariosServicios.ObtenerUsuarios();
                Session["listaUsuarios"] = listaUsuarios;
                Session["listaUsuariosFiltrada"] = listaUsuarios;
                mostrarUsuarios();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoUsuario", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoUsuario').hide();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Formulario Incompleto');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoUsuario", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoUsuario').hide();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoUsuario();", true);
            }
        }

        /// <summary>
        /// Jesús Torres
        /// 30/03/2020
        /// Efecto: Activar modal nuevo usuario
        /// Requiere: Presionar boton nuevo usuario
        /// Modifica: Tabla Usuarios
        /// Devuelve: -
        /// </summary>
        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            lblModal.Text = "Nuevo Usuario";
            accion = 1;
            txtNuevoUsuario.CssClass = "form-control";
            txtNuevoUsuario.Text = "";
            txtContrasena.CssClass = "form-control";
            txtContrasena.Text = "";
            txtNuevoUsuario.ReadOnly = false;
            txtContrasena.ReadOnly = false;
            llenarDdlAsistentes();
            ddlSeleccionAsistenteNuevo.Enabled = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoUsuario();", true);
        }
        /// <summary>
        /// Jesús Torres
        /// 30/03/2020
        /// Efecto: Guarda un nuevo usuario ingresado
        /// Requiere: Presionar boton guardar usuario
        /// Modifica: Tabla Usuarios
        /// Devuelve: -
        /// </summary>
        protected void btnGuardarNuevoUsuario()
        {
            if (validaCamposUsuarios())//si campos estan llenos es igual a true
            {
                Usuario usuario = new Usuario();
                usuario.nombre = txtNuevoUsuario.Text;
                usuario.contraseña = txtContrasena.Text;
                if (ddlSeleccionAsistenteNuevo.Text != "Sin asistente asignado")
                {
                    Asistente asistente = new Asistente();
                    asistente.idAsistente = Convert.ToInt32(ddlSeleccionAsistenteNuevo.SelectedValue);
                    usuario.asistente = asistente;
                    usuario.disponible = false;
                }
                else
                {
                    usuario.asistente = null;
                    usuario.disponible = true;
                }
                try
                {
                    usuariosServicios.insertarUsuarios(usuario);
                    List<Usuario> listaUsuarios = usuariosServicios.ObtenerUsuarios();
                    Session["listaUsuarios"] = listaUsuarios;
                    Session["listaUsuariosFiltrada"] = listaUsuarios;
                    mostrarUsuarios();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.success('" + "El usuario " + usuario.nombre + " fue registrado exitosamente!');", true);

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoUsuario", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoUsuario').hide();", true);

                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Error al registrar el usuario, intente nuevamente');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoUsuario", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoUsuario').hide();", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoUsuario();", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "toastr.error('" + "Formulario Incompleto');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoUsuario", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoUsuario').hide();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoUsuario();", true);
            }
        }
        /// <summary>
        /// Jesús Torres
        /// 30/03/2020
        /// Efecto: Permite al usuario ver o ocultar la contraseña a ingresar
        /// Requiere: Presionar para ver o ocultar contraseña
        /// Modifica: 
        /// Devuelve: -
        /// </summary>
        protected void verContraseña(object sender, EventArgs e)
        {
            if (txtContrasena.Attributes["Type"] == "password")
            {
                txtContrasena.Attributes["Type"] = "text";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modifica", "$('.icon').removeClass('glyphicon glyphicon-eye-open').addClass('glyphicon glyphicon-eye-close');", true);
            }
            else
            {
                txtContrasena.Attributes["Type"] = "password";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "modifica", "$('.icon').removeClass('glyphicon glyphicon-eye-close').addClass('glyphicon glyphicon-eye-open');", true);
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#modalNuevoUsuario", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#modalNuevoUsuario').hide();", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "activar", "activarModalNuevoUsuario();", true);

        }

        #endregion

        #region metodos
        /// <summary>
        /// Jesús Torres
        /// 02/abr/2020
        /// Efecto: llean el DropDownList con los Asistentes disponibles
        /// Requiere: - 
        /// Modifica: DropDownList
        /// Devuelve: -
        /// </summary>
        protected void llenarDdlAsistentes()
        {
            List<Asistente> asistentes = new List<Asistente>();
            ddlSeleccionAsistenteNuevo.Items.Clear();
            asistentes = asistenteSevicios.ObtenerAsistentesSinUsuarios();
            ddlSeleccionAsistenteNuevo.Items.Add("Sin asistente asignado");
            foreach (Asistente asistente in asistentes)
            {
                
                ListItem itemAsistente = new ListItem(asistente.nombreCompleto + "  " + asistente.carnet, asistente.idAsistente + "");
                String a = itemAsistente.Value;
                ddlSeleccionAsistenteNuevo.Items.Add(itemAsistente);
            }
        }

        protected void llenarDdlAsistentes2()
        {
            List<Asistente> asistentes = new List<Asistente>();
            ddlSeleccionAsistenteNuevo.Items.Clear();
            asistentes = asistenteSevicios.ObtenerAsistentesSinUsuarios();
            ddlSeleccionAsistenteNuevo.Items.Add("Sin asistente asignado");

            if (usuarioSeleccionado!=null && usuarioSeleccionado.asistente!=null)
            {
                ListItem itemAsistente = new ListItem(usuarioSeleccionado.asistente.nombreCompleto + "  " + usuarioSeleccionado.asistente.carnet, usuarioSeleccionado.asistente.idAsistente + "");
                ddlSeleccionAsistenteNuevo.Items.Add(itemAsistente);
            }
            foreach (Asistente asistente in asistentes)
            {

                ListItem itemAsistente = new ListItem(asistente.nombreCompleto + "  " + asistente.carnet, asistente.idAsistente + "");
                String a = itemAsistente.Value;
                ddlSeleccionAsistenteNuevo.Items.Add(itemAsistente);
            }
        }

        /// <summary>
        /// Jesús Torres
        /// 02/abr/2020
        /// Efecto: valida los campos para ingresar nuevo usuario
        /// Requiere: - 
        /// Modifica: 
        /// Devuelve: -
        /// </summary>
        private bool validaCamposUsuarios()
        {
            bool condicion = true;
            txtNuevoUsuario.CssClass = "form-control";
            txtContrasena.CssClass = "form-control";
            txtContrasena.Attributes["Type"] = "password";//se pasa a type password por recomendacion de seguridad
            if (txtNuevoUsuario.Text.Trim().Equals(""))
            {
                txtNuevoUsuario.CssClass = "form-control alert-danger";
                condicion = false;
            }
            if (txtContrasena.Text.Trim().Equals(""))
            {
                txtContrasena.CssClass = "form-control alert-danger";
                condicion = false;
            }
            return condicion;
        }
        #endregion

    }
}