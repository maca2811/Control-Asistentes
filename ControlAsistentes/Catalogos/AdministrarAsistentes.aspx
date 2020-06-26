<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarAsistentes.aspx.cs" Inherits="ControlAsistentes.Catalogos.AdministrarAsistentes" %>

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
                <asp:Label id="tituloUn" runat="server" Text="Administración de Asistentes" Font-Size="Large" ForeColor="Black"></asp:Label>
                <p class="mt-1">En esta sección podrá aprobar los nombramientos de los asistentes</p>
            </center>
        </div>
        </center>
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <hr />
                </div>
                
                 <div class="col-md-4 col-xs-12 col-sm-12" style="">
                    <h4>Unidad</h4>
                    <asp:DropDownList ID="ddlUnidad" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidad_SelectedIndexChanged"></asp:DropDownList>
                    <br />
                    <h4>Periodo</h4>
                    <asp:DropDownList ID="ddlPeriodo" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidad_SelectedIndexChanged"></asp:DropDownList>
                </div>
               <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                   <br />
                   <br />
                   <br />
                   <label>Simbología</label>
                </div>
                <div class="col-xs-12">
                    <div class="col-xs-1 w3-card-2 w3-orange" style="width: 30px; height: 20px;"></div>
                    <label class="col-xs-5" style="margin-left: 2px">Pendiente</label>
                </div>
                <div class="col-xs-12">
                    <div class=" col-xs-1 w3-card-2 w3-green" style="width: 30px; height: 20px;"></div>
                    <label class="col-xs-5" style="margin-left: 2px">Aprobado</label>
                </div>
                <br />

                <div class="col-xs-12">
                    <div class=" col-xs-1 w3-card-2 w3-red" style="width: 30px; height: 20px;"></div>
                    <label class="col-xs-5" style="margin-left: 2px">Rechazado</label>
                </div>
                
                <div class="col-md-12 col-xs-12 col-sm-12" style="">
                    <div class="col-md-2 col-xs-2 col-sm-2 col-md-offset-10 col-xs-offset-10 col-sm-offset-10" style="text-align: right">
                        <asp:Button ID="btnPendientes" runat="server" Text="Aprobaciones Pendientes" CssClass="btn btn-primary boton-nuevo" OnClick="btnPendientes_Click" />
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
                                <th>Nombramiento Aprobado</th>
                                <th>Último Período Nombrado</th>
                                <th>Cantidad de Horas Nombrado</th>
                                <th>Cantidad de Períodos Nombrado</th>
                                <th>Documentos</th>

                            </tr>
                        </thead>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-primary" OnClick="filtrarAsistentes"><span aria-hidden="true" class="glyphicon glyphicon-search"></span> </asp:LinkButton>

                            </td>
                            <td>

                                <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control chat-input" placeholder="filtro descripción" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txBool" runat="server" CssClass="form-control chat-input" placeholder="filtro descripción" AutoPostBack="true" Visible="false"></asp:TextBox></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>

                        </tr>
                        <asp:Repeater ID="rpAsistentes" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr style="text-align: center">


                                    <td></td>
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
                <!-- ------------------------ Tabla documentos asistente --------------------------- -->
                <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">

                    <table id="tblUnidadesProyecto" class="table table-bordered">
                        <thead>
                            <tr style="text-align: center" class="btn-primary">
                                <th>Nombre Asistente</th>
                                <th>Expediente Académico</th>
                                <th>Informe Matrícula</th>
                                <th>Curriculum</th>
                                <th>Ponderado</th>
                            </tr>
                            </tr>
                        </thead>

                        <asp:Repeater ID="rpDocumentosAsistente" runat="server">
                            <HeaderTemplate>
                            </HeaderTemplate>

                            <ItemTemplate>
                                <tr style="text-align: center">

                                    <td>
                                        <%# Eval("asistente.nombreCompleto") %>
                                    </td>
                                    <td>
                                        <%# Eval("expediente") %>
                                        <td>
                                            <td>
                                                <%# Eval("informe") %>
                                            </td>
                                            <td>
                                                <%# Eval("ponderado") %>
                                            </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <!-- ---------------------- FIN tabla unidades proyecto  ------------------------- -->
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12 mt-2">
                    <hr />
                </div>

            </div>
              <!-- Modal Observacion-->
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <div id="modalObservacionesAsistente" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Aprobar Asistentes</h4>
                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                        <%-- fin titulo accion --%>

                                        <%-- campos a llenar --%>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lblNombreAsistente" runat="server" o Text="Nombre Asistente <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txtNombreAsistente" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lblCarnet" runat="server" Text="Numero de Carné <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                             <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txtNumeroCarné" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lblCantidadHoras" runat="server" Text="Horas nombramiento <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                            <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:TextBox class="form-control" ID="txtCantidadHoras" runat="server" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                            <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="LbEstado" runat="server" Text="Seleccione un estado <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                             <div class="col-md-4 col-xs-4 col-sm-4">
                                                <asp:DropDownList  ID="AsistenteDDL" runat="server" CssClass="form-control" OnSelectedIndexChanged="SeleccionarEstado_OnChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                          <div class="col-md-3 col-xs-3 col-sm-3">
                                                <asp:Label ID="lbObser" runat="server" Text="Observaciones <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                            </div>
                                          <div class="col-md-4 col-xs-4 col-sm-4">
                                            <div  class="input-group">
                                                <asp:TextBox class="form-control" ID="txtObservaciones" TextMode="multiline" Columns="50" Rows="5"  runat="server"  Width="250" ></asp:TextBox>
                                            </div>
                                        </div>
                                            
                                        </div>
                                       
                                

                                        <%-- botones --%>
                                        <div class="col-md-3 col-xs-3 col-sm-3 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary boton-nuevo" OnClick="AprobarAsistente_OnChanged" />
                                           <asp:Button ID="ButtonCerrar" runat="server" Text="Cerrar" CssClass="btn btn-primary boton-nuevo" OnClick="btncerrar" />
                                           <asp:Button ID="BtnCerrar" runat="server" Text="Cerrar" CssClass="btn btn-primary boton-nuevo"    OnClick="botoncerrar"/>
                                        </div>
                                        <%-- fin botones --%>
                                    </div>
                                </div

                            </div>

                        </div>

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
              <!-- Modal AsistentesAprobacionesPendientes-->
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="modalAsistentesAprobacionesPendientes" class="modal fade" role="alertdialog">
                        <div class="modal-dialog modal-lg">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Asistentes pendientes de Aprobación</h4>
                                </div>
                                  

                                <div class="modal-body">
                                      <div class="col-md-6 col-xs-6 col-sm-6">
                                <asp:Label ID="label8" runat="server" Text="Seleccione una unidad" Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlUnidadesAsistente" class="btn btn-default dropdown-toggle" runat="server"  OnSelectedIndexChanged="ddlUnidadAsistente_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                                    <br />

                                    <br />
                                    <br />

                                    <br />
                                    <div class="row">
                                        <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                                            <table class="table table-bordered">
                                                <thead style="text-align: center !important; align-content: center">
                                                    <tr style="text-align: center" class="btn-primary">
                                                        <th>Aprobar Nombramiento</th>
                                                        <th>Nombre</th>
                                                        <th>Carné</th>
                                                        <th>Unidad Asistencia</th>
                                                        <th>Último Período Nombrado</th>
                                                        <th>Cantidad de Horas Nombrado</th>
                                                        <th>Cantidad de Períodos Nombrado</th>
                                                        <th>Documentos</th>

                                                    </tr>
                                                </thead>
                                                <tr>
                                                     
                                                    <td></td>
                                                    <td>
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><i class="fa fa-search"></i></span>
                                                        <asp:TextBox ID="txtBuscarNombre1" runat="server" CssClass="form-control chat-input" placeholder="Filtro nombre asistente" AutoPostBack="true" OnTextChanged="filtrarAsistentesPendintes"></asp:TextBox>
                                                    </div>
                                                        </td>
                                                   
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    
                                                </tr>
                                                
                                                     <asp:Repeater ID="RpAprovaciones" runat="server">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>

                                                    <ItemTemplate>
                                                        <tr style="text-align: center">

                                                             <td>
                                                                <div class="btn-group">
                                                                    <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("asistente.carnet") %>' />
                                                                   <asp:LinkButton ID="btnAsistenteAprobar" runat="server" ToolTip="Seleccionar" CommandArgument='<%# Eval("idNombramiento") %>' OnClick="AsistenteAprobar_OnChanged" CssClass="btn  glyphicon glyphicon-ok" />
                                                                </div>
                                                            </td>
                                                            <td><%# Eval("asistente.nombreCompleto") %></td>
                                                            <td><%# Eval("asistente.carnet") %></td>
                                                            <td><%# Eval("unidad.nombre") %></td>
                                                           
                                                            <td><%# Eval("periodo.semestre") %> Semestre - <%# Eval("periodo.anoPeriodo")%> </td>
                                                            <td><%# Eval("cantidadHorasNombrado") %></td>
                                                            <td><%# Eval("asistente.cantidadPeriodosNombrado") %></td>
                                                            <td style="display:none;"><%# Eval("asistente.idAsistente") %></td>
                                                            <td>
                                                                <div id="btnDocs" class="btn-group">
                                                                    <asp:HiddenField runat="server" ID="HFIdProyecto" Value='<%# Eval("asistente.carnet") %>' />
<%--                                                                    <asp:LinkButton ID="btnVerDocs" runat="server" ToolTip="Ver Documentos" CommandArgument='<%# Eval("carnet") %>'><span id="cambiar" class="glyphicon glyphicon-list-alt"></span></asp:LinkButton>--%>
                                                                     <asp:LinkButton ID="btnVerDocs" runat="server" ToolTip="Ver Documentos" CommandArgument='<%# Eval("asistente.idAsistente") %>' OnClick="btnVerArchivo_Click" CssClass="glyphicon glyphicon-list-alt" />
                                                                </div>
                                                            </td>



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
                                 <asp:LinkButton ID="lbPrimero2" runat="server" CssClass="btn btn-primary" OnClick="lbPrimero2_Click"><span class="glyphicon glyphicon-fast-backward"></span></asp:LinkButton>
                             </td>
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbAnterior2" runat="server" CssClass="btn btn-default" OnClick="lbAnterior_Click"><span class="glyphicon glyphicon-backward"></asp:LinkButton>
                             </td>
                              <td style="padding:1px !important">
                                  <asp:DataList ID="rptAsistenteAprobado" runat="server"
                                    OnItemCommand="rptPaginacion2_ItemCommand"
                                    OnItemDataBound="rptPaginacion2_ItemDataBound" RepeatDirection="Horizontal">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="lbPaginacion" runat="server" CssClass="btn btn-default"
                                            CommandArgument='<%# Eval("IndexPagina") %>' CommandName="nuevaPagina"
                                            Text='<%# Eval("PaginaText") %>' ></asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:DataList>
                              </td>
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbSiguiente2" CssClass="btn btn-default" runat="server" OnClick="lbSiguiente2_Click"><span class="glyphicon glyphicon-forward"></asp:LinkButton>
                             </td>
                             <td style="padding:1px !important">
                                 <asp:LinkButton ID="lbUltimo2" CssClass="btn btn-primary" runat="server" OnClick="lbUltimo_Click"><span class="glyphicon glyphicon-fast-forward"></asp:LinkButton>
                             </td>
                             <td style="padding:1px !important">
                                 <asp:Label ID="lblpagina2" runat="server" Text=""></asp:Label>
                             </td>
                         </tr>
                     </table>
                 </center>
                </div>

                <%--fn paginación--%>

                                    </div>

                                </div>

                            </div>

                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function observacionesAsistentes() {
            $('#modalObservacionesAsistente').modal('show');
        };
        function activarModalAsistentesAprobacionesPendientes() {
            $('#modalAsistentesAprobacionesPendientes').modal('show');
        };
        function closeModalAsistentes() {
            $('#modalAsistentes').modal('hide');
        }

      </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
