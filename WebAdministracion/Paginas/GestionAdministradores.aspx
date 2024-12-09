<%@ Page Title="Gestión de Administradores" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="GestionAdministradores.aspx.cs" Inherits="WebAdministracion.Paginas.GestionAdministradores" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gestión de Administradores
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4 text-center">Gestión de Administradores</h2>
        <div class="d-flex justify-content-between align-items-center mb-3">
            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-primary" OnClick="btnNuevo_Click" />
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
        </div>
     <asp:GridView ID="gvAdministradores" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover" DataKeyNames="NombreUsuario" OnRowCommand="gvAdministradores_RowCommand">
        <Columns>
            <asp:BoundField DataField="NombreUsuario" HeaderText="Administradores"/>
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("NombreUsuario") %>' CssClass="btn btn-warning btn-sm me-1" />
                  <asp:Button 
                    ID="btnEliminar" 
                    runat="server" 
                    Text="Eliminar" 
                    CommandName="Eliminar" 
                    CommandArgument='<%# Eval("NombreUsuario") %>' 
                    CssClass="btn btn-danger btn-sm" />

                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <asp:Label ID="lblEliminarUsuario" runat="server" CssClass="text-info" Visible="false"></asp:Label>
</div>

</asp:Content>
