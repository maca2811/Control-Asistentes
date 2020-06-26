<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="AdministrarUsuarios.aspx.cs" Inherits="ControlAsistentes.CatalogoUTI.Usuarios.AdministrarUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="MainScriptManager" runat="server" EnableCdn="true"></asp:ScriptManager>
	<asp:UpdatePanel ID="PanelUsuarios" runat="server">
		<ContentTemplate>
			<div class="row">


				<div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
					<br />
				</div>
				<%--page title--%>
				<div class="col-md-12 col-xs-12 col-sm-12 center">
					<asp:Label runat="server" Font-Size="Large" ForeColor="Black">Administración de usuarios para asistentes</asp:Label>
					<p class="mt-1">En esta sección podrá administrar los usuarios de los asistentes</p>
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
                       <asp:Button ID="btnNuevo" runat="server" Text="Nuevo Usuario" CssClass="btn btn-primary boton-nuevo" OnClick ="btnNuevoUsuario_Click" />
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
								<th></th>
								<th>Usuario</th>
								<th>Disponibilidad</th>
								<th>Nombre de Asistente</th>
								<th>Carné de Asistente</th>
							</tr>
						</thead>
						<tbody>
							<tr>
								<td>
									<asp:LinkButton ID="btnFiltrar" runat="server" CssClass="btn btn-primary" ><span aria-hidden="true" class="glyphicon glyphicon-search"></span> </asp:LinkButton>
								</td>
								<td>
									<asp:TextBox ID="txtBuscarUsuario" runat="server" CssClass="form-control chat-input" placeholder="filtro usuarios" onkeypress="enter_click()"></asp:TextBox>
								</td>
								<td></td>
								<td></td>
								<td></td>
							</tr>
							<asp:Repeater ID="rpUsuarios" runat="server">
								<HeaderTemplate></HeaderTemplate>
								<ItemTemplate>
									<asp:UpdatePanel ID="PanelRepeater" runat="server">
										<ContentTemplate>
											<tr>
												<td>
													<asp:LinkButton ID="btnEditar" runat="server" ToolTip="Editar" CommandArgument='<%# Eval("idUsuario") %>' CssClass="glyphicon glyphicon-pencil" OnClick="btnEditarUsuario_Click"></asp:LinkButton>
													<asp:LinkButton ID="btnEliminar" runat="server" ToolTip="Eliminar" CommandArgument='<%# Eval("idUsuario") %>' CssClass="glyphicon glyphicon-trash" OnClick="btnEliminarUsuario_Click"></asp:LinkButton>
												</td>
												<td><%# Eval("nombre") %></td>
												<td style="color: #337ab7;">
													<asp:LinkButton ID="LBDisponibilidad" runat="server" ToolTip="Cambiar disponibilidad" CommandArgument='<%# Eval("idUsuario") %>'><span class='<%# Eval("disponible") %>'></span></asp:LinkButton>
												</td>
												<td><%# Eval("asistente.nombreCompleto") %></td>
												<td><%# Eval("asistente.carnet") %></td>
											</tr>
										</ContentTemplate>
										<Triggers>
											<asp:AsyncPostBackTrigger ControlID="btnEliminar" EventName="Click" />
											<asp:AsyncPostBackTrigger ControlID="btnEditar" EventName="Click" />
										</Triggers>
									</asp:UpdatePanel>
								</ItemTemplate>
								<FooterTemplate></FooterTemplate>

							</asp:Repeater>
						</tbody>
					</table>
				</div>

				<%--boton atras--%>
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
			<!--cierre de div principal -->
		</ContentTemplate>
	</asp:UpdatePanel>
	<!--Cierre de centana principal-->


	<!-- Modal Nueva Unidad-->
	<asp:UpdatePanel ID="PanelUsuariosInsertar" runat="server">
		<ContentTemplate>
			<div id="modalNuevoUsuario" class="modal fade" role="alertdialog">
				<div class="modal-dialog modal-lg">
					<!-- Modal content-->
					<div class="modal-content">
						<div class="modal-header">
							<button type="button" class="close" data-dismiss="modal">&times;</button>
							
							<asp:Label ID="lblModal" runat="server" Text="Contraseña <span style='color:red'></span> " Font-Size="Medium"  ForeColor="Black" CssClass="label" Font-Bold="false" ></asp:Label>
						</div>

						<div class="modal-body">
							<div class="row">
								<%-- fin titulo accion --%>

								<%-- campos a llenar --%>
								<div class="col-md-9 col-xs-9 col-sm-9 mt-1">
									<div class="col-md-4 col-xs-4 col-sm-4">
										<asp:Label ID="lblNuevoUsuario" runat="server" Text="Usuario <span style='color:red'>*</span> " Font-Size="Medium" Font-Names="Bookman Old Style" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
									</div>
									<div class="col-md-8 col-xs-8 col-sm-8">
                                      <div class="input-group">
										<asp:TextBox class="form-control" ID="txtNuevoUsuario" runat="server" Width="240"></asp:TextBox>
									</div>
									</div>
								</div>
								<div class="col-md-9 col-xs-9 col-sm-9 mt-1">
									 <div class="col-md-4 col-xs-4 col-sm-4">
										<asp:Label ID="lblContraseñaNueva" runat="server" Text="Contraseña <span style='color:red'>*</span> " Font-Size="Medium" Font-Names="Bookman Old Style" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
									</div>
									<div class="col-md-5 col-xs-5 col-sm-5">
                                      <div class="input-group">
											<asp:TextBox ID="txtContrasena" type="password" runat="server" CssClass="form-control"></asp:TextBox>
											<asp:LinkButton ID="btnVerContrasena"
												CssClass="input-group-addon btn btn-primary glyphicon glyphicon-eye-open icon"
												runat="server"
												
												OnClick="verContraseña">
												
											</asp:LinkButton>
										</div>

									
									</div>
								</div>
							
								<div class="col-md-12 col-xs-12 col-sm-12 mt-1">
									<div class="col-md-3 col-xs-3 col-sm-3">
										<asp:Label ID="lblSeleccionAsistenteNuevo" runat="server" Text="Seleccione un Asistente " Font-Size="Medium" Font-Names="Bookman Old Style" ForeColor="Black" CssClass="label" Font-Bold="false"></asp:Label>
									</div>
									<div class="col-md-8 col-xs-8 col-sm-8">
										<asp:DropDownList ID="ddlSeleccionAsistenteNuevo" class="btn btn-default dropdown-toggle" runat="server" ></asp:DropDownList>
									</div>
								</div>

								<%-- botones --%>
								<div class="col-md-3 col-xs-3 col-sm-3 col-md-offset-9 col-xs-offset-9 col-sm-offset-9">
									<asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
									<button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
								</div>
								<%-- fin botones --%>
							</div>
						</div>

					</div>

				</div>

			
		</ContentTemplate>
	</asp:UpdatePanel>
	<!-- Fin Modal Nueva Unidad-->

	<!--script-->
	<script type="text/javascript">
        function activarModalNuevoUsuario() {
            $('#modalNuevoUsuario').modal('show');
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
