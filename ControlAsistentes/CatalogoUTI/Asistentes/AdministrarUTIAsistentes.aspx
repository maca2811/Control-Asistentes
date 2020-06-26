<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarUTIAsistentes.aspx.cs" Inherits="ControlAsistentes.CatalogoUTI.Asistentes.AdministrarUTIAsistentes" %>
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
                <asp:Label id="titulo" runat="server" Text="Administración de Asistentes UTI" Font-Size="Large" ForeColor="Black"></asp:Label>
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>

                <asp:Label id="tituloAS" runat="server" Text="" Font-Size="Large" ForeColor="Black"></asp:Label>
                <p class="mt-1">En esta sección podrá visualizar información de los asistentes</p>
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

                <%--Filtro Unidades--%>
                <div class="col-md-4 col-xs-12 col-sm-12" style="">
                    <h4>Unidad</h4>
                    <asp:DropDownList ID="ddlUnidad" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidad_SelectedIndexChanged"></asp:DropDownList>
                    <br />
                </div>
                
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                    <br />
                    <br />
                </div>
                <%--fn Filtro--%>     



                <%--Tabla--%>
                <div class="table-responsive col-md-12 col-xs-12 col-sm-12" style="text-align: center; overflow-y: auto;">
                    <table class="table table-bordered">
                        <thead style="text-align: center !important; align-content: center">
                            <tr style="text-align: center" class="btn-primary">
                                <th></th>
                                <th>Nombre</th>
                                <th>Carné</th>
                                <th>Unidad Asistencia</th>
                                <th>Usuario</th>
                                <th>Tarjeta</th>

                            </tr>
                        </thead>
                        <tr>
                            <td>
                                <asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-primary glyphicon glyphicon-search" AutoPostBack="true" OnClick="filtrarAsistentes" aria-hidden="true"></asp:LinkButton></td>
                            <td>
                                <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control chat-input" placeholder="filtro nombre asistente" AutoPostBack="true" onkeypress="enter_click()"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txBool" runat="server" CssClass="form-control chat-input" placeholder="filtro descripción" AutoPostBack="true" Visible="false"></asp:TextBox></td>

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
                                    <td>
                                        <%# Eval("nombreCompleto") %></td>
                                    <td><%# Eval("carnet") %></td>
                                    <td><%# Eval("unidad.nombre") %></td>
                                   

                                     <td>
                                        <div id="btnUser" class="btn-group">
                                            <asp:HiddenField runat="server" ID="HiddenField2" Value='<%# Eval("carnet") %>' />
                                            <asp:LinkButton ID="btnVerUsuario" OnClick="btnVeUsuarioAsistente" class="glyphicon glyphicon-user" runat="server" ToolTip="Ver Usuario" CommandArgument='<%# Eval("carnet") %>'></asp:LinkButton>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="btnTarjeta" class="btn-group">
                                            <asp:HiddenField runat="server" ID="HFIdProyecto" Value='<%# Eval("carnet") %>' />
                                            <asp:LinkButton ID="btnVerTarjeta"  OnClick="btnVerTarjetaAsistente" class="glyphicon glyphicon-credit-card"  runat="server" ToolTip="Ver Tarjeta" CommandArgument='<%# Eval("carnet") %>'></asp:LinkButton>
                                        </div>
                                    </td>

                                </tr>

                            </ItemTemplate>

                            <FooterTemplate>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <%--fn Tabla--%>     

           <%--Botón Atras--%>
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <div style="text-align: right">
                        <asp:Button ID="Button1" runat="server" Text="Atrás" CssClass="btn btn-primary boton-otro" OnClick="btnDevolverse" />
                    </div>
                </div>
            <%--fn Botón--%>


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




            <!-- Modal  Tarjeta Asistente-->
        <div id="modalTarjetaAsistente" class="modal fade" role="alertdialog">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <asp:UpdatePanel ID="UpdatePanel_Tarjetas" runat="server">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Tarjeta Asistente</h4>
                            </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <%-- fin titulo accion --%>

                                            <%-- campos a mostrar --%>
                                            <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                                <div class="col-md-3 col-xs-3 col-sm-3">
                                                    <asp:Label ID="lblNumeroTarjeta" runat="server" Text="Número de tarjeta" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                                </div>
                                                <div class="col-md-8 col-xs-8 col-sm-8">
                                                    <asp:TextBox class="form-control" ID="txtNumeroTarjeta" runat="server" Width="320px"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                                <div class="col-md-3 col-xs-3 col-sm-3">
                                                    <asp:Label ID="lblTarjetaExtraviada" runat="server" Text="Tarjeta Extraviada" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                                </div>
                                                <div class="col-md-3 col-xs-3 col-sm-3">
                                                    <asp:RadioButton id="rdExtraviada" runat="server" GroupName="measurementSystem"></asp:RadioButton>
                                                </div>         
                                            </div>
                                            <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                                <div class="col-md-3 col-xs-3 col-sm-3">
                                                    <asp:Label ID="lblAsistente" runat="server" Text="Asistente" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                                </div>
                                                <div class="col-md-8 col-xs-8 col-sm-8">
                                                    <asp:TextBox class="form-control" ID="txtAsistente" runat="server" Width="320px"></asp:TextBox>
                                                </div>
                                            </div>

                                            <%-- botones --%>
                                            <div class="col-md-3 col-xs-3 col-sm-3 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                                                <asp:Button ID="btnAsignar" runat="server" Text="Asignar" CssClass="btn btn-primary boton-nuevo"   OnClick="btnAsignar_Click"/>
                                                <button type="button" class="btn btn-primary boton-otro" data-dismiss="modal" >Cerrar</button>
                                            </div>
                                            <%-- fin botones --%>
                                        </div>
                                    </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
               </div>
            </div>
        </div>
            <!-- FIN modal Tarjeta Asistente -->


        <!-- Modal Usuario Asistente-->
        <div id="modalUsuarioAsistente" class="modal fade" role="alertdialog">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <asp:UpdatePanel ID="UpdatePanel_Usuarios" runat="server">
                         <ContentTemplate>
                            <%-- Titulo --%>
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Usuario Asistente</h4>
                            </div>
                            <%-- fin titulo --%>

                            <div class="modal-body">
                                <div class="row">
                                    <%-- campos a mostrar --%>
                                    <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                        <div class="col-md-3 col-xs-3 col-sm-3">
                                            <asp:Label ID="lblnombre" runat="server" Text="Nombre" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                        </div>
                                        <div class="col-md-8 col-xs-8 col-sm-8">
                                            <asp:TextBox class="form-control" ID="txtNombre" runat="server" Width="320px"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                        <div class="col-md-3 col-xs-3 col-sm-3">
                                            <asp:Label ID="lblContrasenia" runat="server" Text="Contraseña" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                        </div>
                                        <div class="col-md-8 col-xs-8 col-sm-8">
                                            <asp:TextBox class="form-control" ID="txtContrasenia" runat="server" Width="320px"></asp:TextBox>

                        

                                    

                                        </div>
                                    </div>
                                    <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                        <div class="col-md-3 col-xs-3 col-sm-3">
                                            <asp:Label ID="Label2" runat="server" Text="Usuario Disponible" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                        </div>
                                        <div class="col-md-3 col-xs-3 col-sm-3">
                                            <asp:RadioButton id="rbDisponible" runat="server" GroupName="measurementSystem"></asp:RadioButton>
                                        </div>         
                                    </div>
                                    <div class="col-md-12 col-xs-12 col-sm-12 mt-1">
                                        <div class="col-md-3 col-xs-3 col-sm-3">
                                            <asp:Label ID="lblAsistenteU" runat="server" Text="Asistente" Font-Size="Medium" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
                                        </div>
                                        <div class="col-md-8 col-xs-8 col-sm-8">
                                            <asp:TextBox class="form-control" ID="txtAsistenteU" runat="server" Width="320px"></asp:TextBox>
                                        </div>
                                    </div>

                                    <%-- botones --%>
                                    <div class="col-md-3 col-xs-3 col-sm-3 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
                                        <asp:Button ID="btnAsignarUsuario" runat="server" Text="Asignar" CssClass="btn btn-primary boton-nuevo" OnClick="btnAsignarUsuario_Click"/>
                                        <button type="button" class="btn btn-primary boton-otro" data-dismiss="modal" >Cerrar</button>
                                    </div>
                                    <%-- fin botones --%>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <!--Fin Modal Usuario Asistente-->


        </ContentTemplate>

    </asp:UpdatePanel>





    <!-- Script inicio -->
    <script type="text/javascript">
        function activarModalTarjetaAsistente() {
            $('#modalTarjetaAsistente').modal('show');
        };

        function activarModalUsuarioAsistente() {
            $('#modalUsuarioAsistente').modal('show');
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
