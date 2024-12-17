<<<<<<< HEAD
﻿<%@ Page Title="Gestión de Administradores" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="GestionAdministradores.aspx.cs" Inherits="WebAdministracion.Paginas.GestionAdministradores" %>
=======
<%@ Page Title="Gestión de Administradores" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="GestionAdministradores.aspx.cs" Inherits="WebAdministracion.Paginas.GestionAdministradores" Async="true" %>
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gestión de Administradores
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<<<<<<< HEAD
    <div>
        <h2>Administradores</h2>
        <asp:Button ID="btnAgregarAdministrador" runat="server" Text="Agregar Administrador" CssClass="btn btn-primary mb-3" />
        <asp:GridView ID="gvAdministradores" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="placeholder1">
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
=======
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

>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
</asp:Content>
