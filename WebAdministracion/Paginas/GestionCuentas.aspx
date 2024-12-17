<%@ Page Title="Gestión de Cuentas" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="GestionCuentas.aspx.cs" Inherits="WebAdministracion.Paginas.GestionCuentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gestión de Cuentas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<<<<<<< HEAD
    <div>
        <h2>Cuentas</h2>
        <asp:Button ID="btnAgregarCuenta" runat="server" Text="Agregar Cuenta" CssClass="btn btn-primary mb-3" />
        <asp:GridView ID="gvCuentas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="placeholder1">
            <Columns>
                <asp:BoundField DataField="placeholder1" HeaderText="placeholder1" />
                <asp:BoundField DataField="placeholder2" HeaderText="placeholder2" />
                <asp:BoundField DataField="placeholder3" HeaderText="placeholder3" />
                <asp:BoundField DataField="placeholder4" HeaderText="placeholder4" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="Modificar" CommandArgument='<%# Eval("placeholder1") %>' CssClass="btn btn-warning btn-sm" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("placeholder1") %>' CssClass="btn btn-danger btn-sm" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
=======
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
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
    </div>
</asp:Content>
