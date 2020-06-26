<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="ControlAsistentes.Menuaspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <%------------------------------MENUS------------------------------%>
    <div id="MenuControl">
             <%------------------------------MENU SECRETARIA--------------------------%>
            <div id="MenuSecretaria" runat="server">
                <div class="gridcontainer clearfix">
                    <div class="grid_3">
                        <a id="unidadesS"  runat="server">
                            <div class="fmcircle_out">
                                <div class="fmcircle_border">
                                    <div class="fmcircle_in fmcircle_red">
                                        <h3 class="fa fa-building-o fa-2x"></h3>
                                        <h3 style=" color: black;font-family:'Bookman Old Style'; font-size:25px;font-weight:bold"">Unidadeds</h3>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    
                    <div class="grid_3">
                        <a id="A1"  runat="server">
                            <div class="fmcircle_out">
                                <div class="fmcircle_border">
                                    <div class="fmcircle_in fmcircle_red">
                                        <h3 class="fa fa-building-o fa-2x"></h3>
                                        <h3 style=" color: black;font-family:'Bookman Old Style'; font-size:25px;font-weight:bold"">P</h3>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                      

                    <div class="grid_3">
                        <a id="asistentesS"  runat="server">
                            <div class="fmcircle_out">
                                <div class="fmcircle_border">
                                    <div class="fmcircle_in fmcircle_red">
                                        <h3 class="fa fa-users fa-2x"></h3>
                                        <h3 style=" color: black;font-family:'Bookman Old Style'; font-size:25px;font-weight:bold"">Asistentes</h3>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
             <%------------------------------FIN MENU SECRETARIA--------------------------%>

             <%------------------------------MENU ENCARGADO--------------------------%>
            <div id="MenuEncargado" runat="server">
                <div class="gridcontainer clearfix">
                    
                    <div class="grid_3">
                        <a id="asistentesE"  runat="server">
                            <div class="fmcircle_out">
                                <div class="fmcircle_border">
                                    <div class="fmcircle_in fmcircle_red">
                                        <h3 class="fa fa-users fa-2x"></h3>
                                        <h3 style=" color: black;font-family:'Bookman Old Style'; font-size:25px;font-weight:bold"">Asistentes</h3>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
            <%------------------------------FIN MENU ENCARGADO--------------------------%>
           
             <%------------------------------MENU ENCARGADO--------------------------%>
            <div id="MenuUTI" runat="server">
                <div class="gridcontainer clearfix">
                    <div class="grid_3">
                        <a id="asistentesUTI"  runat="server">
                            <div class="fmcircle_out">
                                <div class="fmcircle_border">
                                    <div class="fmcircle_in fmcircle_red">
                                        <h3 class="fa fa-users fa-2x"></h3>
                                        <h3 style=" color: black;font-family:'Bookman Old Style'; font-size:25px;font-weight:bold"">Asistentes</h3>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                      <div class="grid_3">
                        <a id="tarjetasU"  runat="server">
                            <div class="fmcircle_out">
                                <div class="fmcircle_border">
                                    <div class="fmcircle_in fmcircle_red">
                                        <h3 class="fa fa-credit-card fa-2x"></h3>
                                        <h3 style=" color: black;font-family:'Bookman Old Style'; font-size:25px;font-weight:bold"">Tarjetas</h3>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                    <div class="grid_3">
                        <a id="usuariosU"  runat="server">
                            <div class="fmcircle_out">
                                <div class="fmcircle_border">
                                    <div class="fmcircle_in fmcircle_red">
                                        <h3 class="fa fa-user-plus fa-2x"></h3>
                                        <h3 style=" color: black;font-family:'Bookman Old Style'; font-size:25px;font-weight:bold"">Usuarios</h3>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>

                </div>
            </div>
        </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>