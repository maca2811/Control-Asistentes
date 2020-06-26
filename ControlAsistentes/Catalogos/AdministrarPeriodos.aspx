<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarPeriodos.aspx.cs" Inherits="ControlAsistentes.Catalogos.AdministrarPeriodos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="MainScriptManager" runat="server" EnableCdn="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <br />
            <div class="row">
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <div class="col-md-12 col-xs-12 col-sm-12">
                    <center>
                <asp:Label runat="server" Text="Apertura de Periodo" Font-Size="Large" ForeColor="Black"></asp:Label>
                <p class="mt-1">Seleccione un periodo, o ingrese uno nuevo</p>
            </center>
                </div>
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <hr />
                </div>
                <br />
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>
                <div class="col-md-12 col-xs-12 col-sm-12" style="">
                    <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10" style="text-align: right">
                        <asp:Button ID="btnNuevoPeriodo" runat="server" Text="Nuevo periodo" CssClass="btn btn-primary boton-nuevo" OnClick="btnNuevoPeriodo_Click" />
                    </div>
                </div>
                <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                    <table class="table table-bordered">
                        <thead style="text-align: center !important; align-content: center">
                            <tr style="text-align: center" class="btn-primary">
                                <th>Habilitar</th>
                                <th>Año</th>
                                <th>Semestre</th>
                                <th></th>
                            </tr>
                        </thead>

                        <asp:Repeater ID="rpPeriodos" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr style="text-align: center">
                                    <td>
                                        <div class="btn-group">
                                            <asp:HiddenField runat="server" ID="HFIdProyecto" Value='<%# Eval("anoPeriodo") %>' />
                                            <%--<asp:CheckBox ID="cbProyecto" runat="server" Text="" />--%>
                                            <asp:LinkButton ID="btnSelccionar" runat="server" ToolTip="Seleccionar" CommandArgument='<%# Eval("idPeriodo") %>' OnClick="EstablecerPeriodoActual_Click" class="glyphicon glyphicon-ok"></asp:LinkButton>
                                        </div>
                                    </td>
                                    <td><%# Eval("anoPeriodo") %> <%# (Eval("habilitado").ToString() == "True")? "(Actual)" : "" %></td>
                                    <td><%# Eval("semestre") %></td>
                                    <td>
                                        <asp:LinkButton ID="btnEliminar" runat="server" ToolTip="Eliminar" CommandArgument='<%# Eval("anoPeriodo") %>' OnClick="btnEliminar_Click" class="btn glyphicon glyphicon-trash"></asp:LinkButton>
                                    </td>
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
            </div>
            <!-- Modal nuevo periodo -->
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>

                    <div id="modalNuevoPeriodo" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Nuevo Período</h4>
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
                                                <asp:Label ID="label4" runat="server" Text="Período <span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:TextBox class="form-control" ID="txtNuevoP" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <br />
                                        <br />

                                        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="label1" runat="server" Text="Semestre<span style='color:red'>*</span>" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>

                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlSemestre" class="btn btn-default dropdown-toggle" runat="server">
                                                        <asp:ListItem Value="I">I Semestre</asp:ListItem>
                                                        <asp:ListItem Value="II">II Semestre</asp:ListItem>
                                                        <asp:ListItem Value="III">III Semestre</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>



                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>

                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnNuevoPeriodoModal" runat="server" Text="Guardar" CssClass="btn btn-primary boton-nuevo" OnClick="btnNuevoPeriodoModal_Click" />
                                    <button type="button" class="btn btn-primary boton-otro" data-dismiss="modal">Cerrar</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- Fin modal nuevo periodo -->

            <!-- Modal eliminar periodo -->
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div id="modalEliminarPeriodo" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Eliminar Periodo</h4>
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
                                                <asp:Label ID="lblProyecto" runat="server" Text="Perído" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="txtPeriodoEliminarModal" runat="server" Text="Período" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                        </div>

                                        <br />
                                        <br />
                                        <br />

                                        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="Label2" runat="server" Text="Semestre" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="txtSemestreEliminarModal" runat="server" Text="Semestre" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                        </div>


                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>


                                    </div>
                                    <div class="modal-footer" style="text-align: center">
                                        <asp:Button ID="btnEliminarModal" runat="server" Text="Eliminar" CssClass="btn btn-primary boton-eliminar" OnClick="btnConfirmarEliminarPeriodo_Click" />
                                        <button type="button" class="btn btn-primary boton-otro" data-dismiss="modal">Cerrar</button>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <!-- Fin modal eliminar periodo -->
                </ContentTemplate>
            </asp:UpdatePanel>

            <!-- Modal Confirmar Eliminar Periodo-->
            <asp:UpdatePanel ID="UPconfirmarPeriodo" runat="server">
                <ContentTemplate>
                    <div id="modalConfirmarPeriodo" class="modal" role="alertdialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Confirmar elimimar Periodo</h4>
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
                                             <p>¿Está seguro que desea eliminar el Período?</p> 
                                              <asp:Label ID="lbConfPer" runat="server" Text="" Font-Size="Large" ForeColor="Black" CssClass="label"></asp:Label>             
                                            </center>
                                        </div>

                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer" style="text-align: center">
                                    <asp:Button ID="btnConfPeriodo" runat="server" Text="Confirmar" CssClass="btn btn-primary boton-eliminar" OnClick="btnEliminarModal_Click" />
                                    <button type="button" class="btn btn-default boton-otro" data-dismiss="modal">Cancelar</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <!-- FIN Modal Confirmar Eliminar periodo -->
        </ContentTemplate>
    </asp:UpdatePanel>


    <!-- Script inicio -->
    <script type="text/javascript">
        function activarModalNuevoPeriodo() {
            $('#modalNuevoPeriodo').modal('show');
        };
        function activarModalEliminarPeriodo() {
            $('#modalEliminarPeriodo').modal('show');
        };
        function activarModalConfirmarPeriodo() {
            $('#modalConfirmarPeriodo').modal('show');
        }

    </script>
    <!-- Script fin -->






</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
