<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarNombramientoAsistente.aspx.cs" Inherits="ControlAsistentes.CatalogoEncargado.AdministrarNombramientoAsistente" %>

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
                <h2 class="mt-1">En esta sección podrá registrar nombramientos de las horas asistentes</h2>
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
                         <asp:Button ID="btnNombramiento" runat="server" Text="Nuevo Nombramiento" CssClass="btn btn-primary boton-nuevo" OnClick="btnNombramiento_Click" />
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
                                <th>Período Nombramiento</th>
                                <th>Horas Nombramiento</th>
                                <th>Períodos Nombrado</th>
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
                                    <td>
                                        <asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar" CommandArgument='<%# Eval("idNombramiento") %>' class="btn glyphicon glyphicon-pencil" OnClick="btnEditarNombramiento"></asp:LinkButton>
                                        <asp:LinkButton ID="btnEliminar" runat="server" ToolTip="Eliminar" CommandArgument='<%# Eval("idNombramiento") %>' class="btn glyphicon glyphicon-trash" OnClick="btnEliminarNombramiento"></asp:LinkButton>
                                    </td>
                                    <td style='<%# Convert.ToString(Eval("aprobado")).Equals("True")? "background-color:#008f39": (Convert.ToString(Eval("aprobado")).Equals("False")&&Convert.ToString(Eval("solicitud")).Equals("2")? "background-color:#fd8e03": "background-color:#ff0000") %>'>
                                        <%# Eval("asistente.nombreCompleto") %></td>
                                    <td><%# Eval("asistente.carnet") %></td>
                                    <td><%# Eval("unidad.nombre") %></td>
                                    <td>
                                        <div class="btn-group">
                                            <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Eval("asistente.carnet") %>' />
                                            <asp:LinkButton ID="btnDetalles" runat="server" OnClick="btnDetallesNombramiento" ToolTip="Detalles" CommandArgument='<%# Eval("idNombramiento") %>'><div class='<%# Eval("aprobado") %>' ></div></asp:LinkButton>
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
                                        <%# Eval("nombreCompleto") %>
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Modal nuevo asistente -->

    <%--modal nuevo--%>
    <div id="modalNuevoNombramiento" class="modal fade" role="alertdialog" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Nombramiento Asistente</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">

                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>

                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="Label5" runat="server" Text="Asistente <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-xs-4 input-group">
                                        <asp:LinkButton ID="btnEliminarAsistente" runat="server" CssClass="input-group-addon boton-eliminar" OnClick="btnEliminarAsistenteNombramiento_Click">
                                            <i class="glyphicon glyphicon-trash"></i>
                                        </asp:LinkButton>
                                        <asp:TextBox class="form-control" ID="txtAsistente" runat="server" Width="245 px"></asp:TextBox>
                                        <span id="spanAgregarAsistenes" runat="server" style="cursor: pointer" data-toggle="modal" data-target="#modalAsistentesNombramiento" class="input-group-addon boton-nuevo"><i class="glyphicon glyphicon-plus-sign"></i></span>
                                    </div>
                                </div>

                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="Label1" runat="server" Text="Unidad Nombramiento <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                        <div class="input-group">
                                            <asp:TextBox class="form-control" ID="txtU" runat="server" ReadOnly="true" Width="320px"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="lbHoras" runat="server" Text="Horas Nombramiento <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                        <div class="input-group">
                                            <asp:TextBox class="form-control" ID="txtHorasN" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="lbPeriodoNO" runat="server" Text="Período Nombramiento <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                        <div class="input-group">
                                            <asp:DropDownList ID="periodosDDL" runat="server" CssClass="form-control" Width="200px">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="lblInduccion" runat="server" Text="Recibe Inducción " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                        <div class="input-group">
                                            <asp:CheckBox ID="ChckBxInduccion" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <!-- Archivo Expediente -->
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="lbExpediente" runat="server" Text="Expediente Académico <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                    </div>
                                    <asp:UpdatePanel ID="updExpediente" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:FileUpload ID="fileExpediente" runat="server" AllowMultiple="true" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- Fin Archivos Expediente -->
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>

                                <!-- Archivo Informe-->
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="lbInforme" runat="server" Text="Informe de Matrícula " Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                    </div>
                                    <asp:UpdatePanel ID="UpInforme" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:FileUpload ID="fileInforme" runat="server" AllowMultiple="true" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- Fin Archivos   Informe -->

                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>

                                <!-- Archivo CV-->
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="lbCV" runat="server" Text="Curriculum VITAE" Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                    </div>
                                    <asp:UpdatePanel ID="upCV" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:FileUpload ID="fileCV" runat="server" AllowMultiple="true" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- Fin Archivos CV -->

                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>

                                <!-- Archivo Cuenta-->
                                <div id="prueba" class="col-md-12 col-xs-12 col-sm-12" visible="false">
                                    <div class="col-xs-3">
                                        <asp:Label ID="lbCuenta" runat="server" Text="Cuenta de Banco" Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                    </div>
                                    <asp:UpdatePanel ID="upCuenta" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:FileUpload ID="fileCuenta" runat="server" AllowMultiple="true" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- Fin Archivos Cuenta -->
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>

                            </div>
                        </div>


                        <div class="modal-footer">
                            <asp:Button ID="btnNuevoNombramientoModal" runat="server" Text="Guardar" CssClass="btn btn-primary boton-nuevo" OnClick="guardarNombramiento_Click" />
                            <button type="button" class="btn btn-primary boton-otro" data-dismiss="modal">Cerrar</button>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnNuevoNombramientoModal" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>




    <%--modal asistentes--%>
    <div id="modalAsistentesNombramiento" class="modal fade" role="alertdialog" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="PanelAsistentesNombramiento" runat="server">
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
                                                        <th>Carné</th>
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
                                                <asp:Repeater ID="rpAsistentesNombramiento" runat="server">
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
    <!-- Fin modal nuevo Nombramiento -->

    <!-- Modal Eliminar -->
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="modalEliminarNombramiento" class="modal fade" role="alertdialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Eliminar Nombramiento</h4>
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
                                        <asp:Label ID="Label2" runat="server" Text="Horas nombramiento: " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                    </div>

                                    <div class="col-md-4 col-xs-4 col-sm-4">
                                        <asp:TextBox class="form-control" ID="txtHorasNE" runat="server" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <br />
                                </div>


                                <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                    <div class="col-md-3 col-xs-3 col-sm-3">
                                        <asp:Label ID="Label4" runat="server" Text="Período Nombramiento: " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                    </div>

                                    <div class="col-md-4 col-xs-4 col-sm-4">
                                        <asp:TextBox class="form-control" ID="txtPeriodoNE" ReadOnly="true" runat="server" Font-Bold="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <br />
                                </div>

                                <div class="col-md-12 col-xs-12 col-sm-12" style="text-align: center">
                                    <div class="col-md-3 col-xs-3 col-sm-3">
                                        <asp:Label ID="Label3" runat="server" Text="Períodos Nombrado: " Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                    </div>

                                    <div class="col-md-4 col-xs-4 col-sm-4">
                                        <asp:TextBox class="form-control" ID="txtPeriodosNE" ReadOnly="true" runat="server" Font-Bold="false"></asp:TextBox>
                                    </div>
                                </div>

                                <%-- fin campos a llenar --%>

                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <br />
                                </div>


                            </div>

                            <%-- botones --%>
                            <div class="modal-footer">
                                <asp:Button ID="btnEliminarAsistenteE" runat="server" Text="Eliminar" CssClass="btn btn-primary boton-eliminar" OnClick="btnConfirmarEliminarNombramiento" />
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
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
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
                            <asp:Button ID="Button2" runat="server" Text="Confirmar" CssClass="btn btn-primary boton-eliminar" OnClick="eliminarNombramiento" />
                            <button type="button" class="btn btn-default boton-otro" data-dismiss="modal">Cancelar</button>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- FIN Modal Confirmar Eliminar Asistente -->
    <%--modal nuevo--%>
    <div id="modalEditarNombramiento" class="modal fade" role="alertdialog" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Editar Nombramiento Asistente</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">

                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>

                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="Label6" runat="server" Text="Asistente <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                        <div class="input-group">
                                            <asp:TextBox class="form-control" ID="txtAsistenteM" runat="server" ReadOnly="true" Width="320px"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="Label8" runat="server" Text="Unidad Nombramiento <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                        <div class="input-group">
                                            <asp:TextBox class="form-control" ID="txtUnidadM" runat="server" ReadOnly="true" Width="320px"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="Label9" runat="server" Text="Horas Nombramiento <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                        <div class="input-group">
                                            <asp:TextBox class="form-control" ID="txtHorasM" runat="server"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="Label10" runat="server" Text="Período Nombramiento <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                        <div class="input-group">
                                            <asp:TextBox class="form-control" ID="txtPeriodoM" runat="server" ReadOnly="true"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="Label11" runat="server" Text="Recibe Inducción " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                    </div>
                                    <div class="col-md-6 col-xs-4 col-sm-4">
                                        <div class="input-group">
                                            <asp:CheckBox ID="checkBM" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>
                                <!-- Archivo Expediente -->
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="Label12" runat="server" Text="Expediente Académico <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:FileUpload ID="fileExpedienteM" runat="server" AllowMultiple="true" CssClass="form-control" />
                                                    <asp:LinkButton ID="btnExpediente"
                                                        runat="server"
                                                        ToolTip="Ver Archivo"
                                                        CssClass="input-group-addon boton-otro"
                                                        Style="cursor: pointer"
                                                        CommandArgument='<%# Eval("") %>'
                                                        OnClick="btnVerArchivo_Click">
												    <i class="glyphicon glyphicon-floppy-save"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- Fin Archivos Expediente -->
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>

                                <!-- Archivo Informe-->
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="Label13" runat="server" Text="Informe de Matrícula " Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:FileUpload ID="fileInformeM" runat="server" AllowMultiple="true" CssClass="form-control" />
                                                    <asp:LinkButton ID="btnInforme"
                                                        runat="server"
                                                        CssClass="input-group-addon boton-otro"
                                                        Style="cursor: pointer"
                                                        ToolTip="Ver Archivo"
                                                        CommandArgument='<%# Eval("") %>'
                                                        OnClick="btnVerArchivo_Click">
                                                        
												        <i class="glyphicon glyphicon-floppy-save"></i>
                                                    </asp:LinkButton>
                                                </div>

                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- Fin Archivos   Informe -->

                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>

                                <!-- Archivo CV-->
                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <div class="col-xs-3">
                                        <asp:Label ID="Label14" runat="server" Text="Curriculum VITAE" Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:FileUpload ID="fileCVM" runat="server" AllowMultiple="true" CssClass="form-control" />
                                                    <asp:LinkButton ID="btnCV"
                                                        runat="server"
                                                        CssClass="input-group-addon boton-otro"
                                                        Style="cursor: pointer"
                                                        ToolTip="Ver Archivo"
                                                        CommandArgument='<%# Eval("") %>'
                                                        OnClick="btnVerArchivo_Click">
												        <i class="glyphicon glyphicon-floppy-save"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- Fin Archivos CV -->

                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>

                                <!-- Archivo Cuenta-->
                                <div class="col-md-12 col-xs-12 col-sm-12" visible="false">
                                    <div class="col-xs-3">
                                        <asp:Label ID="Label15" runat="server" Text="Cuenta de Banco" Font-Size="Medium" ForeColor="Black" Font-Bold="true" CssClass="label"></asp:Label>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:FileUpload ID="fileCuentaM" runat="server" AllowMultiple="true" CssClass="form-control" />
                                                    <asp:LinkButton ID="btnCuenta"
                                                        runat="server"
                                                        CssClass="input-group-addon boton-otro"
                                                        Style="cursor: pointer"
                                                        ToolTip="Ver Archivo"
                                                        CommandArgument='<%# Eval("") %>'
                                                        OnClick="btnVerArchivo_Click">
												        <i class="glyphicon glyphicon-floppy-save"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- Fin Archivos Cuenta -->
                                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                    <br />
                                </div>

                            </div>
                        </div>


                        <div class="modal-footer">
                            <asp:Button ID="btnEditarNombramientoModal" runat="server" Text="Editar" CssClass="btn btn-primary boton-editar" OnClick="editarNombramiento_Click" />
                            <button type="button" class="btn btn-primary boton-otro" data-dismiss="modal">Cerrar</button>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnEditarNombramientoModal" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!-- Modal Detalles Nombramiento-->
    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
        <ContentTemplate>
            <div id="modalDetallesNombramiento" class="modal" role="alertdialog">
                <div class="modal-dialog modal-lg">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Detalles del Nombramiento</h4>
                        </div>
                        <div class="modal-body">
                            <%-- campos a llenar --%>
                            <div class="row">

                                <%-- fin campos a llenar --%>

                                <div class="col-md-12 col-xs-12 col-sm-12">
                                    <br />
                                    <div class="col-md-12 col-xs-12 col-sm-12">
                                        <div class="col-xs-4">
                                            <asp:Label ID="Label16" runat="server" Text="Asistente <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                        </div>
                                        <div class="col-md-6 col-xs-4 col-sm-4">
                                            <div class="input-group">
                                                <asp:TextBox class="form-control" ID="txtAsistenteD" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                        <br />
                                    </div>
                                    <div>
                                        <div class="col-md-12 col-xs-12 col-sm-12">
                                            <div class="col-xs-4">
                                                <asp:Label ID="Label18" runat="server" Text="Solicitud <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                            </div>
                                            <div class="col-md-6 col-xs-4 col-sm-4">
                                                <div class="input-group">
                                                    <asp:TextBox class="form-control" ID="txtSolicitudD" runat="server" ReadOnly="true" Style="text-align: center"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                                        <br />
                                    </div>

                                    <div class="col-md-12 col-xs-12 col-sm-12">
                                        <div class="col-xs-4">
                                            <asp:Label ID="Label17" runat="server" Text="Detalles Nombramiento <span style='color:red'></span> " Font-Size="Medium" ForeColor="Black" CssClass="label"></asp:Label>
                                        </div>
                                        <div class="col-md-6 col-xs-4 col-sm-4">
                                            <div class="input-group">
                                                <asp:TextBox class="form-control" ID="txtDetalles" TextMode="multiline" Columns="50" Rows="5" runat="server" ReadOnly="true" Width="200"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-12 col-xs-12 col-sm-12">
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">

                                <button type="button" class="btn btn-default boton-otro" data-dismiss="modal">Salir</button>
                            </div>
                        </div>

                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- FIN Modal Confirmar Eliminar Asistente -->
    <script type="text/javascript">
        function activarModalNuevoNombramiento() {
            $('#modalNuevoNombramiento').modal('show');
        };
        function cerrarModalAsistenteNombramiento() {
            $('#modalAsistentesNombramiento').modal('hide');
        };
        function activarModalEliminarNombramiento() {
            $('#modalEliminarNombramiento').modal('show');
        };

        function activarModalConfirmarEliminarNombramiento() {
            $('#modalConfirmarEliminarNombramiento').modal('show');
        };

        function activarModalEditarNombramiento() {
            $('#modalEditarNombramiento').modal('show');
        };
        function activarModalDetallesNombramiento() {
            $('#modalDetallesNombramiento').modal('show');
        };
    </script>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
