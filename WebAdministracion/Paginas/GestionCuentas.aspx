<%@ Page Title="Gestión de Cuentas" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="GestionCuentas.aspx.cs" Inherits="WebAdministracion.Paginas.GestionCuentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gestión de Cuentas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
          <h2>Cuentas</h2>
        <asp:Button ID="btnAgregarCuenta" runat="server" Text="Agregar Cuenta" CssClass="btn btn-primary mb-3" />
      <asp:GridView ID="gvCuentas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="NumeroCuenta">
        <Columns>
            <asp:BoundField DataField="NumeroCuenta" HeaderText="Número de Cuenta" />
            <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
            <asp:BoundField DataField="TipoCuenta" HeaderText="Tipo de Cuenta" />
            <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:C}" />
        </Columns>
    </asp:GridView>
    </div>
</asp:Content>
