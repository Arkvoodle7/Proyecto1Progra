<%@ Page Title="Gestión de Cuentas" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="GestionCuentas.aspx.cs" Inherits="WebAdministracion.Paginas.GestionCuentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gestión de Cuentas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4 text-center">Gestión de Cuentas</h2>
        <div class="d-flex justify-content-between align-items-center mb-3">
            <asp:Button ID="btnAgregarCuenta" runat="server" Text="Nueva" CssClass="btn btn-primary" OnClick="btnAgregarCuenta_Click" />
        </div>
        <asp:GridView ID="gvCuentas" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover">
            <Columns>
                <asp:BoundField DataField="NumeroCuenta" HeaderText="Número de Cuenta" />
                <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre de Usuario" />
                <asp:BoundField DataField="TipoCuenta" HeaderText="Tipo de Cuenta" />
                <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
    </div>
</asp:Content>
