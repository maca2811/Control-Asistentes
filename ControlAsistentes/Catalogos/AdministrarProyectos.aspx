<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarProyectos.aspx.cs" Inherits="ControlAsistentes.Catalogos.AdministrarProyectos" %>
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
                <asp:Label id="tituloUn" runat="server" Text="Administración de Proyectos" Font-Size="Large" ForeColor="Black"></asp:Label>
                <p class="mt-1">En esta sección podrá asignar asistentes a los proyectos</p>
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

                <div class="col-md-6 col-xs-6 col-sm-6">
                    <h4>Proyecto</h4>
                    <%--<asp:DropDownList ID="ddlUnidad" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true" ></asp:DropDownList>    ddlUnidad_SelectedIndexChanged--%>
                <asp:DropDownList ID="ddlProyecto" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProyecto_SelectedIndexChanged"></asp:DropDownList>
               
                    
                </div>
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <div class="col-md-12 col-xs-6 col-sm-6">

                    <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10" style="text-align: right">
                        <%--<asp:Button ID="btnPendientes" runat="server" Text="Aprobaciones Pendientes" CssClass="btn btn-primary boton-nuevo" OnClick="btnPendientes_Click" />--%>
                    </div>
                </div>


                <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                    <table class="table table-bordered">
                        <thead style="text-align: center !important; align-content: center">
                            <tr style="text-align: center" class="btn-primary">
                                <th></th>
                                <th>Acciónes</th>
                                <th>Proyecto</th>
                                <th>Descripción</th>
                                <th>Unidad Asistencia</th>
                                <th>Fecha Inicio</th>
                                <th>Fecha Finalización</th>
                                <th>Finalizado</th>
                            </tr>
                        </thead>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-primary" OnClick="filtrarProyectos"><span aria-hidden="true" class="glyphicon glyphicon-search"></span> </asp:LinkButton>

                            </td>
                            <td>

                                <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control chat-input" placeholder="filtro descripción" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txBool" runat="server" CssClass="form-control chat-input" placeholder="filtro por nombre" AutoPostBack="true" Visible="false"></asp:TextBox></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>

                        </tr>
                        <asp:Repeater ID="rpProyectos" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr style="text-align: center">

                                    <td></td>
                                     <td></td>
                                    <td><%# Eval("nombre") %></td>
                                    <td><%# Eval("descripcion") %></td>
                                    <td><%# Eval("asistente.unidad.nombre") %></td>
                                    <td><%# Eval("fechaInicio") %></td>
                                    <td><%# Eval("fechaFinalizacion") %></td>
                                    <td><%# Eval("finalizado") %></td>

<%--                                    <td></td>
                                    <td style='<%# Convert.ToString(Eval("aprobado")).Equals("True")? "background-color:#008f39":(Convert.ToString(Eval("aprobado")).Equals("False")&&Convert.ToString(Eval("solicitud")).Equals("2")? "background-color:#ff0000": "background-color:#fd8e03") %>'>
                                        <%# Eval("asistente.nombreCompleto") %></td>
                                    <td><%# Eval("asistente.carnet") %></td>
                                    <td><%# Eval("unidad.nombre") %></td>
                                    <td>
                                        <div class="btn-group">
                                            <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("asistente.carnet") %>' />
                                            <asp:LinkButton ID="btnDetalles" runat="server" ToolTip="Detalles" CommandArgument='<%# Eval("idNombramiento") %>' OnClick=" btnVerDetalles"><div class='<%# Eval("aprobado") %>'></div></asp:LinkButton>
                                        </div>
                                    </td>
                                    <td><%# Eval("periodo.semestre") %> Semestre - <%# Eval("periodo.anoPeriodo")%> </td>
                                    <td><%# Eval("cantidadHorasNombrado") %></td>
                                    <td><%# Eval("asistente.cantidadPeriodosNombrado") %></td>
                                    <td>
                                        <div id="btnDocs" class="btn-group">
                                            <asp:HiddenField runat="server" ID="HFIdProyecto" Value='<%# Eval("asistente.carnet") %>' />
                                            <asp:LinkButton ID="btnVerDocs" runat="server" ToolTip="Ver Documentos" OnClick="btnVerArchivos_Click" CommandArgument='<%# Eval("asistente.idAsistente") %>'><span id="cambiar" class="glyphicon glyphicon-list-alt" ></span></asp:LinkButton>
                                        </div>
                                    </td>
                                     <td style="display:none;"><%# Eval("asistente.idAsistente") %></td>
                                    <td style="display:none;"><%# Eval("aprobado") %></td>
                                    <td style="display:none;"><%# Eval("solicitud") %></td>

--%>

                                </tr>

                            </ItemTemplate>

                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
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
            <div id="divDocsAsist" runat="server" visible="false">
                <br />
                <br />
                <br />
                <div class="col-md-12 col-xs-12 col-sm-12">
                    <center>
                            <asp:Label runat="server" Text="Documentos" Font-Size="Large" ForeColor="Black"></asp:Label>
                           
                        </center>
                </div>
                <br />
                <br />

                <!-- ---------------------- FIN tabla proyecto  ------------------------- -->
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12 mt-2">
                    <hr />
                </div>

            </div>

            </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function observacionesAsistentes() {
            $('#modalObservacionesAsistente').modal('show');
        };

         function closeModalAsistentes() {
            $('#modalAsistentes').modal('hide');
        }

      </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
