<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarAsistentesEncargado.aspx.cs" Inherits="ControlAsistentes.CatalogoEncargado.AdministrarAsistentesEncargado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="MainScriptManager" runat="server" EnableCdn="true"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <center>
             <div class="col-md-12 col-xs-12 col-sm-12">
            <center>
                <asp:Label id="titulo" runat="server" Text="Administración de Asistentes" Font-Size="Large" ForeColor="Black"></asp:Label>
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <asp:Label id="tituloAS" runat="server" Text="" Font-Size="Large" ForeColor="Black"></asp:Label>
                <p class="mt-1">En esta sección podrá aprobar los nombramientos de los asistentes</p>
            </center>
        </div>
        </center>
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <hr />
                </div>
                <br />



                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>



                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <div class="col-md-12 col-xs-12 col-sm-12" style="">
                    <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10" style="text-align: right">
                        <asp:Button ID="Button1" runat="server" Text="Nuevo Asistente" CssClass="btn btn-primary boton-nuevo" OnClick="btnNuevoAsistente_Click" />
                    </div>
                </div>

                <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                    <table class="table table-bordered">
                        <thead style="text-align: center !important; align-content: center">
                            <tr style="text-align: center" class="btn-primary">
                                <th></th>
                                <th>Nombre</th>
                                <th>Carné</th>
                                <th>Unidad Asistencia</th>
                                <th>Telefono</th>



                            </tr>
                        </thead>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-primary glyphicon glyphicon-search" OnClick="filtrarAsistentes" aria-hidden="true"></asp:LinkButton></td>
                            <td>
                                <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control chat-input" placeholder="filtro nombre asistente" AutoPostBack="true" onkeypress="enter_click()"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txBool" runat="server" CssClass="form-control chat-input" placeholder="filtro descripción" AutoPostBack="true" Visible="false"></asp:TextBox></td>

                            <td></td>
                            <td></td>



                        </tr>
                        <asp:Repeater ID="rpAsistentes" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr style="text-align: center">
                                    <td>
                                        <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar" CommandArgument='<%# Eval("idAsistente") %>' OnClick="btnEditarAsistente" class="btn glyphicon glyphicon-pencil"></asp:LinkButton>
                                        <asp:LinkButton ID="btnEliminar" runat="server" ToolTip="Editar" CommandArgument='<%# Eval("idAsistente") %>' OnClick="btnEliminarAsistente" class="btn glyphicon glyphicon-trash"></asp:LinkButton>
                                    </td>
                                    <td><%# Eval("nombreCompleto") %></td>
                                    <td><%# Eval("carnet") %></td>
                                    <td><%# Eval("unidad.nombre") %></td>
                                    <td><%# Eval("telefono") %></td>


                                </tr>

                            </ItemTemplate>

                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
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
            </div>

            <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12 mt-2">
                <hr />
            </div>


            <!-- Modal nuevo asistente -->

            <contenttemplate>

                    <div id="modalNuevoAsistente" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Nuevo Asistente</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="label4" runat="server" Text="Nombre Completo <span style='color:red'></span>" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:TextBox class="form-control" ID="txtNombre" runat="server" Width="325 px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbCarnet" runat="server" Text="Carné <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                             <div class="col-md-6 col-xs-4 col-sm-4">
                                            <div class="input-group">
                                                <asp:TextBox class="form-control" ID="txtCarnet" runat="server"></asp:TextBox>
                                            </div>                                            
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbTelefono" runat="server" Text="Numéro Teléfono <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                            <div class="input-group">
                                                <asp:TextBox class="form-control" ID="txtTelefono" runat="server"></asp:TextBox>
                                            </div>    
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="lbUnidadNombramiento" runat="server" Text="Unidad Perteneciente <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" ></asp:Label>
                                            </div>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                            <div class="input-group">
                                                 <asp:TextBox class="form-control" ID="txtUnidadNA" runat="server" ReadOnly="true" Width="320px"></asp:TextBox>
                                            </div>
                                           
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                       
                                       
                                       <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-xs-3">
                                                <asp:Label ID="Label1" runat="server" Text="Encargado Asistente: <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" ></asp:Label>
                                            </div>

                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                            <div class="input-group">
                                                 <asp:TextBox class="form-control" ID="txtEncargado" runat="server" ReadOnly="true" Width="320px"></asp:TextBox>
                                            </div>
                                           
                                            </div>
                                        </div>
                                    
                                    </div>
                                </div>
                                <div class="modal-footer" >
                                    <asp:Button ID="btnNuevoAsistenteModal" runat="server" Text="Guardar" CssClass="btn btn-primary boton-nuevo" OnClick="guardarNuevoAsistente_Click" />
                                    <button type="button" class="btn btn-primary boton-otro" data-dismiss="modal">Cerrar</button>
                                </div>

                            </div>

                        </div>
                    </div>
                </contenttemplate>
            <triggers> 
                <asp:PostBackTrigger ControlID="btnNuevoAsistenteModal" />
          </triggers>
            <!-- Fin modal nuevo asistente -->

            <!-- Modal Eliminar -->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="modalEliminarAsistente" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Eliminar Asistente</h4>
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
                                                <asp:Label ID="lbNombreAsistenteE" runat="server" Text="Nombre Asistente: " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>

                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="lbNombreAsistente" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>


                                        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbCarne" runat="server" Text="Carné: " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>

                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txtCarneE" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>


                                        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbTelefonoE" runat="server" Text="Telefono: " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>

                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txtTelefonoE" ReadOnly="true" runat="server" Font-Bold="false"></asp:TextBox>
                                            </div>
                                        </div>

                                        <%-- fin campos a llenar --%>

                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                        <asp:Label ID="Label5" runat="server" Text="Unidad Perteneciente:<span style='color:red'></span>" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                    </div>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:TextBox class="form-control" ID="txtUnidadEl" runat="server" ReadOnly="true" Width="320px"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                            <br />
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                           <div class="col-md-3 col-xs-3 col-sm-3">
                                        <asp:Label ID="Label6" runat="server" Text="Encargado Asistente<span style='color:red'></span>" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                             </div>

                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:TextBox class="form-control" ID="txtEncargadoEl" runat="server" ReadOnly="true" Width="320px"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <%-- botones --%>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnEliminarAsistenteE" runat="server" Text="Eliminar" CssClass="btn btn-primary boton-eliminar" OnClick="btnConfirmarEliminarAsistente" />
                                        <button type="button" class="btn btn-primary boton-otro" data-dismiss="modal">Cerrar</button>
                                    </div>
                                    <%-- fin botones --%>
                                </div>


                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- FIN modal eliminar unidad -->

            <!-- Modal Confirmar Eliminar Asistente-->
            <asp:UpdatePanel ID="UPconfirmarEliminarAsistente" runat="server">
                <ContentTemplate>
                    <div id="modalConfirmarAsistente" class="modal" role="alertdialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Confirmar Eliminar Asistente</h4>
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
                                             <p>¿Está seguro que desea eliminar el Asistente?</p> 
                                              <asp:Label ID="lbConfAsistente" runat="server" Text="" Font-Size="Large" ForeColor="Black" CssClass="label"></asp:Label>             
                                            </center>
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" CssClass="btn btn-primary boton-eliminar" OnClick="eliminarAsistente" />
                                    <button type="button" class="btn btn-default boton-otro" data-dismiss="modal">Cancelar</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- FIN Modal Confirmar Eliminar Asistente -->
        </ContentTemplate>

    </asp:UpdatePanel>
    <!-- Modal editar unidad -->
    <asp:UpdatePanel ID="UpdatePanelEditarAsistente" runat="server">
        <ContentTemplate>
            <div id="modalEditarAsistente" class="modal fade" role="alertdialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Editar Asistente</h4>
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
                                        <asp:Label ID="lbAsistenteEd" runat="server" Text="Nombre Asistente<span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                    </div>

                                    <div class="col-md-4 col-xs-4 col-sm-4">
                                        <asp:TextBox class="form-control" ID="txtNombreAsistenteEditar" runat="server"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <br />
                                </div>

                                <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                    <div class="col-md-3 col-xs-3 col-sm-3">
                                        <asp:Label ID="lbCarnetEd" runat="server" Text="Carné<span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                    </div>

                                    <div class="col-md-4 col-xs-4 col-sm-4">
                                        <asp:TextBox class="form-control" ID="txtCarnetEd" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <br />
                                </div>

                                <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                    <div class="col-md-3 col-xs-3 col-sm-3">
                                        <asp:Label ID="lbTelefonoEd" runat="server" Text="Telefono<span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                    </div>

                                    <div class="col-md-4 col-xs-4 col-sm-4">
                                        <asp:TextBox class="form-control" ID="txtTelefonoEd" runat="server"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-md-3 col-xs-3 col-sm-3">
                                        <asp:Label ID="Label2" runat="server" Text="Unidad Perteneciente<span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                    </div>
                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                        <div class="input-group">
                                            <asp:TextBox class="form-control" ID="txtUnidadPE" runat="server" ReadOnly="true" Width="320px"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-md-3 col-xs-3 col-sm-3">
                                        <asp:Label ID="Label3" runat="server" Text="Encargado Asistente<span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                    </div>

                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                        <div class="input-group">
                                            <asp:TextBox class="form-control" ID="txtEncargadoEd" runat="server" ReadOnly="true" Width="320px"></asp:TextBox>
                                        </div>

                                    </div>
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
                            <asp:Button ID="btnEditarAsistente1" runat="server" Text="Actualizar" CssClass="btn btn-primary boton-editar" OnClick="editarAsistente" />
                            <button type="button" class="btn btn-primary boton-otro " data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- FIN modal editar unidad -->

       <!-- Modal Confirmar Eliminar Asistente-->
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div id="modalConfirmarEliminarNombramiento" class="modal" role="alertdialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Confirmar Eliminar Nombramiento</h4>
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
                                             <p>¿Está seguro que desea eliminar el Nombramiento?</p> 
                                              <asp:Label ID="Label7" runat="server" Text="" Font-Size="Large" ForeColor="Black" CssClass="label"></asp:Label>             
                                            </center>
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="Button2" runat="server" Text="Confirmar" CssClass="btn btn-primary boton-eliminar"  />
                                    <button type="button" class="btn btn-default boton-otro" data-dismiss="modal">Cancelar</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- FIN Modal Confirmar Eliminar Asistente -->
       


    <!-- Script inicio -->
    <script type="text/javascript">
        function activarModalNuevoAsistente() {
            $('#modalNuevoAsistente').modal('show');
        };
        function activarModalEliminarAsistente() {
            $('#modalEliminarAsistente').modal('show');
        };
        function activarModalEditarAsistente() {
            $('#modalEditarAsistente').modal('show');
        };
        function activarModalConfirmarAsistente() {
            $('#modalConfirmarAsistente').modal('show');
        };

        function enter_click() {
            if (window.event.keyCode == 13) {
                document.getElementById('<%=btnFiltrar.ClientID%>').focus();
                document.getElementById('<%=btnFiltrar.ClientID%>').click();
            }
        }

    </script>
    <!-- Script fin -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
