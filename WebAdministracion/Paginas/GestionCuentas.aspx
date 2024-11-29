<%@ Page Title="Gestión de Cuentas" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="GestionCuentas.aspx.cs" Inherits="WebAdministracion.Paginas.GestionCuentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gestión de Cuentas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Gestión de Cuentas</h2>
        <asp:Button ID="btnAgregarCuenta" runat="server" Text="Agregar Cuenta" OnClick="btnAgregarCuenta_Click" CssClass="btn btn-primary mb-3" />
        <asp:GridView ID="gvCuentas" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="placeholder1" OnRowCommand="gvCuentas_RowCommand">
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
    </div>
</asp:Content>
