<%@ Page Title="Gestión de Usuarios" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="GestionUsuarios.aspx.cs" Inherits="WebAdministracion.Paginas.GestionUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gestión de Usuarios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
              <h2>Usuarios</h2>
        <asp:Button ID="btnAgregarUsuario" runat="server" Text="Agregar Usuario" CssClass="btn btn-primary mb-3" />
         <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="Identificacion">
        <Columns>
            <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
            <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre de Usuario" />
            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" />
            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnModificar" runat="server" Text="Editar" CommandName="Modificar" CommandArgument='<%# Eval("Identificacion") %>' CssClass="btn btn-warning btn-sm" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("Identificacion") %>' CssClass="btn btn-danger btn-sm" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView> 
    </div>
</asp:Content>
