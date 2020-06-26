<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarTarjetas.aspx.cs" Inherits="ControlAsistentes.CatalogoUTI.Tarjetas.AdministrarTarjetas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="MainScriptManager" runat="server" EnableCdn="true"></asp:ScriptManager>
    <asp:UpdatePanel ID="PanelTarjetas" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <%--page title--%>
                <div class="col-md-12 col-xs-12 col-sm-12 center">
                    <asp:Label runat="server" Font-Size="Large" ForeColor="Black">Administración de tarjetas para asistentes</asp:Label>
                    <p class="mt-1">En esta sección podrá administrar las tarjetas de los asistentes</p>
                </div>

                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <hr />
                </div>
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <%--boton nuevo--%>

                <div class="col-md-12 col-xs-12 col-sm-12" style="">
                    <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10" style="text-align: right">
                        <asp:Button runat="server" Text="Nueva Tarjeta" CssClass="btn btn-primary boton-nuevo" OnClick="btnNuevo_Click" />
                    </div>
                </div>
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <%--tabla --%>
                <div class="table-responsive col-md-12 col col-xs-12 col-sm-12 center">
                    <table class="table table-bordered">
                        <thead>
                            <tr class="btn-primary">
                                <th>Acciones</th>
                                <th>Número de tarjeta</th>
                                <th>Disponibilidad</th>
                                <th>Tarjeta extraviada</th>
                                <th>Asistente</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td></td>
                                <td>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-search"></i></span>
                                        <asp:TextBox ID="txtBuscarTarjeta" runat="server" CssClass="form-control chat-input" placeholder="filtro número de tarjeta" AutoPostBack="true" OnTextChanged="btnFiltrar_Click"></asp:TextBox>
                                    </div>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <asp:Repeater ID="rpTarjetas" runat="server">
                                <HeaderTemplate></HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar" OnClick="btnEditar_Click" CommandArgument='<%# Eval("idTarjeta") %>' CssClass="glyphicon glyphicon-pencil btn"></asp:LinkButton>
                                            <asp:LinkButton ID="btnEliminar" runat="server" ToolTip="Eliminar" OnClick="btnEliminar_Click" CommandArgument='<%# Eval("idTarjeta") %>' CssClass="glyphicon glyphicon-trash btn"></asp:LinkButton>
                                        </td>
                                        <td><%# Eval("numeroTarjeta") %></td>
                                        <td style="color: #337ab7;">
                                            <div class='<%# Eval("disponible") %>'></div>
                                        </td>
                                        <td>
                                            <div style="cursor: pointer; color: #337ab7;" data-toggle="tooltip" title='<%# ((bool)Eval("pagada")) ? "Pagada" : "Sin pagar" %>'>
                                                <div class='<%# Eval("tarjetaExtraviada") %>'></div>
                                            </div>
                                        </td>
                                        <td><%# Eval("asistente.nombreCompleto") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></FooterTemplate>

                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>

                <%--boton atras--%>
                <div class="col-md-2 col-xs-2 col-md-offset-10">
                    <asp:Button ID="btnAtras" runat="server" Text="Atrás" CssClass="btn btn-primary boton-otro" OnClick="btnAtras_Click" />
                </div>

                <%--paginacion--%>
                <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                    <center>
                    <table class="table" style="max-width: 664px;">
                        <tr style="padding: 1px !important">
                            <td style="padding: 1px !important">
                                <asp:LinkButton ID="lbPrimero" runat="server" CssClass="btn btn-primary" OnClick="lbPrimero_Click"><span class="glyphicon glyphicon-fast-backward"></span></asp:LinkButton>
                            </td>
                            <td style="padding: 1px !important">
                                <asp:LinkButton ID="lbAnterior" runat="server" CssClass="btn btn-default" OnClick="lbAnterior_Click"><span class="glyphicon glyphicon-backward"></span></asp:LinkButton>
                            </td>
                            <td style="padding: 1px !important">
                                <asp:DataList ID="rptPaginacion" runat="server"
                                    OnItemCommand="rptPaginacion_ItemCommand"
                                    OnItemDataBound="rptPaginacion_ItemDataBound" RepeatDirection="Horizontal">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbPaginacion" runat="server" CssClass="btn btn-default"
                                            CommandArgument='<%# Eval("IndexPagina") %>' CommandName="nuevaPagina"
                                            Text='<%# Eval("PaginaText") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                            <td style="padding: 1px !important">
                                <asp:LinkButton ID="lbSiguiente" CssClass="btn btn-default" runat="server" OnClick="lbSiguiente_Click"><span class="glyphicon glyphicon-forward"></span></asp:LinkButton>
                            </td>
                            <td style="padding: 1px !important">
                                <asp:LinkButton ID="lbUltimo" CssClass="btn btn-primary" runat="server" OnClick="lbUltimo_Click"><span class="glyphicon glyphicon-fast-forward"></span></asp:LinkButton>
                            </td>
                            <td style="padding: 1px !important">
                                <asp:Label ID="lblpagina" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                                                </center>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--modal nuevo--%>
    <div id="modalNuevo" class="modal fade" role="alertdialog" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label ID="modalTitle" runat="server" ForeColor="Black" Font-Size="Medium"></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <%--campos a llenar--%>
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-md-3 col-xs-3 col-sm-3">
                                        <asp:Label runat="server" Text="Número de Tarjeta <span style='color:red'>*</span> " Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </div>
                                    <div class="col-md-6 col-xs-6 col-sm-4">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtNumeroTarjeta" runat="server" CssClass="form-control chat-input" placeholder="Número de tarjeta" Width="260"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-xs-12">
                                    <div class="col-xs-3">
                                        <asp:Label runat="server" Text="Disponible" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </div>
                                    <div class="col-xs-9">
                                        <asp:CheckBox ID="cbxDisponible" runat="server" />
                                    </div>
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-xs-12">
                                    <div class="col-xs-3">
                                        <asp:Label runat="server" Text="Tarjeta extraviada" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                    </div>
                                    <div class="col-xs-3">
                                        <asp:CheckBox ID="cbxExtraviada" runat="server" OnCheckedChanged="cbxExtraviada_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                </div>
                                <div class="col-xs-12">
                                    <asp:Panel ID="panelTarjetaPagada" runat="server" Visible="false">
                                        <div class="col-xs-3">
                                            <asp:Label runat="server" Text="Pagada?" Font-Size="Medium" ForeColor="Black"></asp:Label>
                                        </div>
                                        <div class="col-xs-3">
                                            <asp:CheckBox ID="cbxPagada" runat="server" />
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-xs-12 form-group">
                                    <div class="col-xs-3">
                                        <asp:Label runat="server" Text="Asistente" Font-Size="Medium" ForeColor="Black" Width="260"></asp:Label>
                                    </div>
                                    <div class="col-md-4 col-xs-4 col-sm-4">
                                        <div class="input-group">
                                            <asp:LinkButton ID="btnEliminarAsistente" runat="server" CssClass="input-group-addon boton-eliminar" OnClick="btnEliminarAsistente_Click">
                                            <i class="glyphicon glyphicon-trash"></i>
                                            </asp:LinkButton>

                                            <asp:TextBox ID="txtAsistente" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                            <span id="spanAgregarAsistenes" runat="server" style="cursor: pointer" data-toggle="modal" data-target="#modalAsistentes" class="input-group-addon boton-nuevo"><i class="glyphicon glyphicon-plus-sign"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-xs-3 col-xs-offset-9">
                                <asp:Button ID="btnConfirmar" runat="server" CssClass="btn btn-primary boton-nuevo" OnClick="btnConfirmar_Click" />
                                <button type="button" class="btn btn-primary boton-otro" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%--modal asistentes--%>
    <div id="modalAsistentes" class="modal fade" role="alertdialog" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="PanelAsistentes" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <asp:Label runat="server" ForeColor="Black" Font-Size="Medium">Seleccione un asistente</asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <%-- Tabla--%>
                                        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                                            <table id="tbAsistentes" class="table table-bordered">
                                                <thead>
                                                    <tr style="text-align: center" class="btn-primary">
                                                        <th></th>
                                                        <th>Carnét</th>
                                                        <th>Nombre Asistente</th>
                                                    </tr>
                                                </thead>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td>
                                                        <asp:TextBox ID="txtBuscarAsistente" runat="server" CssClass="form-control chat-input" placeholder="Buscar Asistente" AutoPostBack="true" OnTextChanged="btnFiltrarAsistentes_Click"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <asp:Repeater ID="rpAsistentes" runat="server">
                                                    <HeaderTemplate></HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton runat="server" ToolTip="Seleccionar" OnClick="btnSeleccionarAsistente_Click" CommandArgument='<%# Eval("idAsistente") %>' CssClass="btn glyphicon glyphicon-ok"></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                                <%# Eval("carnet") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("nombreCompleto") %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate></FooterTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                        <%--paginacion--%>
                                        <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                                            <center>
                    <table class="table" style="max-width:664px;">
                        <tr style="padding:1px !important">
                            <td style="padding:1px !important">
                                <asp:LinkButton ID="lbPrimeroAsistentes" runat="server" CssClass="btn btn-primary" OnClick="lbPrimeroAsistentes_Click"><span class="glyphicon glyphicon-fast-backward"></span></asp:LinkButton>
                                </td>
                            <td style="padding:1px !important">
                                <asp:LinkButton ID="lbAnteriorAsistentes" runat="server" CssClass="btn btn-default" OnClick="lbAnteriorAsistentes_Click"><span class="glyphicon glyphicon-backward"></asp:LinkButton>
                            </td>
                            <td style="padding:1px !important">
                                <asp:DataList ID="rptPaginacionAsistentes" runat="server" OnAsistenteCommand="rptPaginacionAsistentes_AsistenteCommand"
                                    OnAsistenteDataBound="rptPaginacionAsistentes_AsistenteDataBound"  RepeatDirection="Horizontal">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbPaginacionAsistentes" runat="server" CssClass="btn btn-default"
                                            CommandArgument='<%# Eval("IndexPagina") %>' CommandName="nuevaPagina"
                                            Text='<%# Eval("PaginaText") %>' ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>
                            </td>
                            <td style="padding:1px !important">
                                <asp:LinkButton ID="lbSiguienteAsistentes" CssClass="btn btn-default" runat="server" OnClick="lbSiguienteAsistentes_Click"><span class="glyphicon glyphicon-forward"></asp:LinkButton>
                                </td>
                            <td style="padding:1px !important">
                                <asp:LinkButton ID="lbUltimoAsistentes" CssClass="btn btn-primary" runat="server" OnClick="lbUltimoAsistentes_Click"><span class="glyphicon glyphicon-fast-forward"></asp:LinkButton>
                                </td>
                            <td style="padding:1px !important">
                                <asp:Label ID="lblpaginaAsistentes" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                        </center>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-xs-3 col-xs-offset-9">
                                <button type="button" class="btn btn-primary boton-otro" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function openModalTarjetas() {
            $('#modalNuevo').modal('show');
        }
        function closeModalTarjetas() {
            $('#modalNuevo').modal('hide');
        }
        function closeModalAsistentes() {
            $('#modalAsistentes').modal('hide');
        }
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
</asp:Content>
