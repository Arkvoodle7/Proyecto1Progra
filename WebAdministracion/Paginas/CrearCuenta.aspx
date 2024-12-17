<%@ Page Title="Creacion de Cuentas" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="CrearCuenta.aspx.cs" Inherits="WebAdministracion.Paginas.CrearCuenta" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Creacion de Cuentas
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4 text-center">Crear Cuenta</h2>
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block"></asp:Label>

        <div class="mb-3">
            <label for="txtNumeroCuenta">Número de Cuenta:</label>
            <asp:TextBox ID="txtNumeroCuenta" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNumeroCuenta" runat="server" ControlToValidate="txtNumeroCuenta" ErrorMessage="El número de cuenta es requerido" CssClass="text-danger"></asp:RequiredFieldValidator>
        </div>

        <div class="mb-3">
            <label for="txtCedulaUsuario">Cédula del Usuario:</label>
            <asp:TextBox ID="txtCedulaUsuario" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCedulaUsuario_TextChanged"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvCedulaUsuario" runat="server" ControlToValidate="txtCedulaUsuario" ErrorMessage="La cédula es requerida" CssClass="text-danger"></asp:RequiredFieldValidator>
            <asp:Label ID="lblNombreUsuario" runat="server" CssClass="d-block mt-2"></asp:Label>
        </div>

        <div class="mb-3">
            <label for="ddlTipoCuenta">Tipo de Cuenta:</label>
            <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="form-select">
                <asp:ListItem Value="ahorros">Ahorros</asp:ListItem>
                <asp:ListItem Value="empresarial">Empresarial</asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="mb-3">
            <label for="txtSaldoInicial">Saldo Inicial:</label>
            <asp:TextBox ID="txtSaldoInicial" runat="server" CssClass="form-control"></asp:TextBox>
           <asp:RequiredFieldValidator 
                ID="rfvSaldoInicial" 
                runat="server" 
                ControlToValidate="txtSaldoInicial" 
                ErrorMessage="El saldo inicial es requerido" 
                CssClass="text-danger"></asp:RequiredFieldValidator>
        </div>

        <div class="mt-4">
            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary me-2" OnClick="btnAceptar_Click"  />
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-secondary" OnClick="btnRegresar_Click" CausesValidation="false"  />
        </div>
    </div>
</asp:Content>
