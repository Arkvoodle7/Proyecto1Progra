<%@ Page Title="Creacion de Usuarios" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="CrearUsuario.aspx.cs" Inherits="WebAdministracion.Paginas.CrearUsuario" Async="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Creacion de Usuarios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4 text-center">Creacion de Usuarios</h2>
        <div class="d-flex justify-content-between align-items-center mb-3">
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
        </div>
        <div>
            <div>
            <label>Identificación:</label>
            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvIdentificacion" runat="server" ControlToValidate="txtIdentificacion" ErrorMessage="La identificación es requerida" CssClass="text-danger" />
            </div>
            
            <div>
            <label>Nombre de Usuario:</label>
            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvNombreUsuario" runat="server" ControlToValidate="txtNombreUsuario" ErrorMessage="El nombre de usuario es requerido" CssClass="text-danger" />
            </div>

            <div>
            <label>Nombre Completo:</label>
            <asp:TextBox ID="txtNombreCompleto" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvNombreCompleto" runat="server" ControlToValidate="txtNombreCompleto" ErrorMessage="El nombre completo es requerido" CssClass="text-danger" />
            </div>

            <div>
            <label>Contraseña:</label>
            <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvContrasena" runat="server" ControlToValidate="txtContrasena" ErrorMessage="La contraseña es requerida" CssClass="text-danger" />
            </div>

            <div>
            <label>Teléfono:</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator ID="rfvTelefono" runat="server" ControlToValidate="txtTelefono" ErrorMessage="El teléfono es requerido" CssClass="text-danger" />
            </div>

            <div class="mt-3">
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary" OnClick="btnRegresar_Click" CausesValidation="false" />
            </div>
        </div>
    </div>
</asp:Content>

