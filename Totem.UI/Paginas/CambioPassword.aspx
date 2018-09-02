<%@ Page Title="" Language="C#" MasterPageFile="~/Paginas/MenuPanel.Master" AutoEventWireup="true" CodeBehind="CambioPassword.aspx.cs" Inherits="Totem.UI.Paginas.CambioPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <!--#include file="Header.html"-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-warning">Cambio de clave</h2>

    <div class="row">
        <div class="col-md-4 col-md-offset-4">
            <div id="containerMsg"></div>
        </div>
    </div>


    <div class="panel panel-default">
        <div class="panel-body">

            <div class="col-md-offset-3">
                <div class="form-group row">
                    <label class="col-sm-2 col-form-label">Clave actual</label>
                    <div class="col-sm-5">
                        <asp:TextBox ID="txtClaveActual" class="form-control" TextMode="Password" runat="server" placeholder="Clave actual"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group row">
                    <label class="col-sm-2 col-form-label">Nueva clave</label>
                    <div class="col-sm-5">
                        <asp:TextBox ID="txtNuevClave" class="form-control" runat="server" TextMode="Password" placeholder="Nueva clave"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group row">
                    <label class="col-sm-2 col-form-label">Confirmar clave</label>
                    <div class="col-sm-5">
                        <asp:TextBox ID="txtNuevaClaveConfirm" class="form-control" runat="server" TextMode="Password" placeholder="Confirmar clave"></asp:TextBox>
                    </div>
                </div>

                
                <div class="form-group row">
                   <div class="col-md-offset-2">
                        <div class="col-sm-5">
                         <asp:Button ID="btnCambiarClave" runat="server" Text="Confirmar" CssClass="btn btn-warning btn-block" OnClick="btnCambiarClave_Click" />
                    </div>
                   </div>
                </div>

            </div>

        </div>
        <div class="panel-footer">
        </div>
    </div>


    <!--#include file="Footer.html"-->
</asp:Content>
