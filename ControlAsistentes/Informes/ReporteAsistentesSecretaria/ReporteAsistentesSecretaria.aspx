<%@ Page Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ReporteAsistentesSecretaria.aspx.cs" Inherits="ControlAsistentes.Informes.ReporteAsistentesSecretaria.ReporteAsistentesSecretaria" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                    <br />
                </div>
                <center>
                    <div class="col-md-12 col-xs-12 col-sm-12">
                        <center>
                            <asp:Label id="tituloUn" runat="server" Text="Reporte Asistentes" Font-Size="Large" ForeColor="Black"></asp:Label>
                            <p class="mt-1">En esta sección podrá visualizar todos los nombramientos de los asistentes a través de los períodos</p>
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

                <div class="col-md-4 col-xs-12 col-sm-12" style="">
                    <h4>Unidad</h4>
                    <asp:DropDownList ID="ddlUnidad" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidad_SelectedIndexChanged"></asp:DropDownList>
                    <br />
                    <h4>Periodo</h4>
                    <asp:DropDownList ID="ddlPeriodo" class="btn btn-default dropdown-toggle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUnidad_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <br />
            <br />

        <div style="overflow-x: auto; width: 1490px">
        <rsweb:ReportViewer ID="ReporteSecretaria" runat="server" ClientIDMode="AutoID" AsyncRendering="False" Width="100%" Height="100%" SizeToReportContent="True" BackColor="" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226"></rsweb:ReportViewer>
    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    </asp:Content>

       