<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/MenuPanel.Master" AutoEventWireup="true" CodeBehind="PanelAdministrador.aspx.cs" Inherits="Totem.UI.Paginas.PanelAdministrador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <!--#include file="Header.html"-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h2 class="text-info">Carga de reportes diarios</h2>

    <div class="panel panel-primary">
        <div class="panel-heading">
            <i class="fa fa-file-excel-o"></i>
            Carga de planillas
        </div>
        <div class="panel-body">

            <div class="well">
                <div class="row">
                    <div class="col-md-6 col-md-offset-3">

                        <label id="cargando">
                            <img src="../Fw/imagenes/ajax-loader.gif" /> cargando
                        </label>
                        
                        <div class="panel panel-default">
                            <div class="panel-body">

                                <asp:FileUpload ID="fileUp" runat="server" /><br />
                            
                                <asp:Button ID="btnCargaExcel" runat="server" Text="Cargar" CssClass="btn btn-warning btn-block" OnClick="btnCargaExcel_Click" ClientIDMode="static" />
                            </div>
                            <div class="panel-footer"></div>
                        </div>


                    </div>
                </div>
            </div>

        </div>
    </div>

    <!--#include file="Footer.html"-->
    <script src="js/utilPanel.js"></script>
</asp:Content>
