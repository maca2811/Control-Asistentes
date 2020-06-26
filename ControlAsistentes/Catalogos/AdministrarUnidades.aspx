<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarUnidades.aspx.cs" Inherits="ControlAsistentes.Catalogos.AdministrarUnidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="MainScriptManager" runat="server" EnableCdn="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="PanelUnidades" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <br />
            <div class="row">
                <%-- titulo pantalla --%>
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <center>
                <asp:Label runat="server" Text="Administrar Unidades" Font-Size="Large" ForeColor="Black"></asp:Label>
                <p class="mt-1">Puede ingresar una nueva unidad, modificarla o eliminarla</p>
            </center>
                </div>
                <%-- fin titulo pantalla --%>
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <hr />
                </div>
                <br />



                <div class="col-md-12 col-xs-12 col-sm-12" style="">
                    <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10" style="text-align: right">
                        <asp:Button ID="btnNuevaUnidad" runat="server" Text="Nueva Unidad" CssClass="btn btn-primary boton-nuevo" OnClick="btnNuevaUnidad_Click" />
                    </div>
                </div>

                <%-- tabla--%>
                <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                    <table class="table table-bordered">
                        <thead style="text-align: center !important; align-content: center">
                            <tr style="text-align: center" class="btn-primary">
                                <th></th>
                                <th>Unidad</th>
                                <th>Descripción</th>
                                <th>Encargado</th>
                            </tr>
                        </thead>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-primary glyphicon glyphicon-search" OnClick="btnFiltrarUnidad_Click" aria-hidden="true"></asp:LinkButton></td>
                            <td>
                                <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control chat-input" placeholder="filtro nombre unidad" onkeypress="enter_click()"></asp:TextBox>
                            </td>
                            <td></td>
                            <td></td>


                        </tr>
                        <asp:Repeater ID="rpUnidades" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="text-align: center">
                                    <td>
                                        <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar" CommandArgument='<%# Eval("idUnidad") %>' OnClick="btnEditarUnidad" class="btn glyphicon glyphicon-pencil"></asp:LinkButton>
                                        <asp:LinkButton ID="btnEliminar" runat="server" ToolTip="Editar" CommandArgument='<%# Eval("idUnidad") %>' OnClick="btnEliminarUnidad" class="btn glyphicon glyphicon-trash"></asp:LinkButton>
                                    </td>
                                    <td><%# Eval("nombre") %></td>
                                    <td><%# Eval("descripcion") %></td>
                                    <td><%# Eval("encargado.nombreCompleto") %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </div>
            <div style="text-align: right">
                <asp:Button ID="btnAtras" runat="server" Text="Atrás" CssClass="btn btn-primary boton-otro" OnClick="btnDevolverse" />
            </div>
            <%--paginación--%>
            <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                <center>
                     <table class="table" style="max-width:664px;">
                         <tr style="padding:1px !important">
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbPrimero" runat="server" CssClass="btn btn-primary" OnClick="lbPrimero_Click"><span class="glyphicon glyphicon-fast-backward"></span></asp:LinkButton>
                             </td>
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbAnterior" runat="server" CssClass="btn btn-default" OnClick="lbAnterior_Click"><span class="glyphicon glyphicon-backward"></asp:LinkButton>
                             </td>
                              <td style="padding:1px !important">
                                  <asp:DataList ID="rptPaginacion" runat="server"
                                    OnItemCommand="rptPaginacion_ItemCommand"
                                    OnItemDataBound="rptPaginacion_ItemDataBound" RepeatDirection="Horizontal">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="lbPaginacion" runat="server" CssClass="btn btn-default"
                                            CommandArgument='<%# Eval("IndexPagina") %>' CommandName="nuevaPagina"
                                            Text='<%# Eval("PaginaText") %>' ></asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:DataList>
                              </td>
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbSiguiente" CssClass="btn btn-default" runat="server" OnClick="lbSiguiente_Click"><span class="glyphicon glyphicon-forward"></asp:LinkButton>
                             </td>
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbUltimo" CssClass="btn btn-primary" runat="server" OnClick="lbUltimo_Click"><span class="glyphicon glyphicon-fast-forward"></asp:LinkButton>
                             </td>
                             <td style="padding:1px !important">
                                 <asp:Label ID="lblpagina" runat="server" Text=""></asp:Label>
                             </td>
                         </tr>
                     </table>
                 </center>
            </div>
            <%--fn paginación--%>
            <br />
            <br />
            <br />
            <br />




            <!-- Modal Nueva Unidad-->
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <div id="modalNuevaUnidad" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Nueva Unidad</h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                        <%-- fin titulo accion --%>

                                        <%-- campos a llenar --%>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lblNuevaUnidad" runat="server" Text="Nombre unidad <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-8 col-xs-8 col-sm-8">
                                                <asp:TextBox class="form-control" ID="txtNuevaUnidad" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lblDescNueva" runat="server" Text="Descripción Unidad <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-8 col-xs-8 col-sm-8">
                                                <asp:TextBox class="form-control" ID="txtDescNueva" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lblEncagadoNueva" runat="server" Text="Encargado unidad <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-8 col-xs-8 col-sm-8">
                                                <asp:DropDownList ID="ddlEncargadoNueva" class="btn btn-default dropdown-toggle" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <%-- botones --%>
                                        <div class="col-md-3 col-xs-3 col-sm-3 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary boton-nuevo" OnClick="btnGuardarNuevaUnidad" />
                                            <button type="button" class="btn btn-primary boton-otro" data-dismiss="modal">Cerrar</button>
                                        </div>
                                        <%-- fin botones --%>
                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

            <!-- Modal Eliminar -->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="modalEliminarUnidad" class="modal" role="alertdialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Eliminar Unidad</h4>
                                </div>
                                <br />
                                <div class="modal-body">
                                    <div class="row">

                                        <%-- campos a llenar --%>

                                        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbUnidadE1" runat="server" Text="Unidad: " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>

                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="lbUnidadE2" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>


                                        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbDescripcionUnidadEliminar" runat="server" Text="Descripción Unidad: " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>

                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txtDescripcionEliminar" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>


                                        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbEncragado" runat="server" Text="Encargado: " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>

                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txtEncargadoEliminar" ReadOnly="true" runat="server" Font-Bold="false"></asp:TextBox>
                                            </div>
                                        </div>

                                        <%-- fin campos a llenar --%>

                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <hr />
                                        </div>

                                        <%-- botones --%>
                                        <div class="modal-footer">
                                            <asp:Button ID="btnEliminarUnidadP" runat="server" Text="Eliminar" CssClass="btn btn-primary boton-eliminar" OnClick="btnConfirmarEliminarUnidad" />
                                            <button type="button" class="btn btn-primary boton-otro" data-dismiss="modal">Cerrar</button>
                                        </div>
                                        <%-- fin botones --%>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- FIN modal eliminar unidad -->

            <!-- Modal Confirmar Eliminar Unidad-->
            <asp:UpdatePanel ID="UPconfirmarEliminarUnidad" runat="server">
                <ContentTemplate>
                    <div id="modalConfirmarUnidad" class="modal" role="alertdialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Confirmar Eliminar Unidad</h4>
                                </div>
                                <div class="modal-body">
                                    <%-- campos a llenar --%>
                                    <div class="row">

                                        <%-- fin campos a llenar --%>

                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <center>
                                             <asp:Label runat="server" Text="" Font-Size="Large" ForeColor="Black"></asp:Label>
                                             <p>¿Está seguro que desea eliminar la Unidad?</p> 
                                              <asp:Label ID="lbConfUnidad" runat="server" Text="" Font-Size="Large" ForeColor="Black" CssClass="label"></asp:Label>             
                                            </center>
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="btn btn-primary boton-eliminar" OnClick="eliminarUnidad" />
                                    <button type="button" class="btn btn-default boton-otro" data-dismiss="modal">Cancelar</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- FIN Modal Confirmar Eliminar PECTO -->

            <!-- Modal editar unidad -->
            <asp:UpdatePanel ID="UpdatePanelEditarUnidad" runat="server">
                <ContentTemplate>
                    <div id="modalEditarUnidad" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Editar Unidad</h4>
                                </div>
                                <div class="modal-body">
                                    <%-- campos a llenar --%>
                                    <div class="row">

                                        <%-- fin campos a llenar --%>

                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbUnidadEdi" runat="server" Text="Nombre Unidad<span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>

                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txtNombreUnidadEditar" runat="server"></asp:TextBox>
                                            </div>
                                        </div>


                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbDescEditar" runat="server" Text="Descripción Unidad<span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>

                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txtDescEditar" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbEncargadoEditar1" runat="server" Text="Encargado" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>

                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbEncargadoEditar2" runat="server" Text="Encargado" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>

                                        <div class="col-xs-12">
                                            <br />
                                            <div class="col-xs-12">
                                                <h6 style="text-align: left">Los campos marcados con <span style='color: red'>*</span> son requeridos.</h6>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnEditarUnidad1" runat="server" Text="Actualizar" CssClass="btn btn-primary boton-editar" OnClick="editarUnidad" />
                                    <button type="button" class="btn btn-primary boton-otro " data-dismiss="modal">Cerrar</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- FIN modal editar unidad -->

        </ContentTemplate>

    </asp:UpdatePanel>



    <script type="text/javascript">
        function activarModalNuevaUnidad() {
            $('#modalNuevaUnidad').modal('show');
        };
        function activarModalEliminarUnidad() {
            $('#modalEliminarUnidad').modal('show');
        };
        function activarModalConfirmarUnidad() {
            $('#modalConfirmarUnidad').modal('show');
        };
        function activarModalEditarUnidad() {
            $('#modalEditarUnidad').modal('show');
        };
        function enter_click() {
            if (window.event.keyCode == 13) {
                document.getElementById('<%=btnFiltrar.ClientID%>').focus();
                document.getElementById('<%=btnFiltrar.ClientID%>').click();
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

