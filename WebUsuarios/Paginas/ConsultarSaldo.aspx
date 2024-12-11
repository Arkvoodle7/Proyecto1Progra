<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="ConsultarSaldo.aspx.cs" Inherits="WebUsuarios.Paginas.ConsultarSaldo" MasterPageFile="~/Paginas/Pagina.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Consulta de Saldo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* General styles (container) */
        .container {
            max-width: 600px;
            margin: auto;
        }

        /* Card styles for the form and result sections */
        .card {
            border-radius: 8px;
            border: 1px solid #ddd;
            background-color: #fff;
            padding: 20px;
            margin-top: 20px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        /* Alert styles */
        .alert {
            border-radius: 8px;
            padding: 15px 20px;
            display: flex;
            align-items: center;
        }

        .alert-icon {
            margin-right: 15px;
            font-size: 2rem;
        }

        .alert-success {
            background-color: #d4edda;
            color: #155724;
        }

        .alert-danger {
            background-color: #f8d7da;
            color: #721c24;
        }

        .btn {
            width: 100%;
        }

        .text-center {
            text-align: center;
        }

        /* Hidden alert (by default) */
        .alert-hidden {
            display: none;
        }
    </style>

    <div class="container">
        <h2 class="text-center">Consulta de Saldo</h2>

        <!-- Formulario para consultar saldo -->
        <asp:Panel ID="panelConsulta" runat="server" CssClass="card">
            <div class="form-group">
                <label for="txtTelefono">Número de Teléfono:</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="text-center">
                <asp:Button ID="btnConsultar" runat="server" CssClass="btn btn-primary mt-2" Text="Consultar" OnClick="btnConsultar_Click" />
            </div>
        </asp:Panel>

        <!-- Mensaje dinámico para éxito o error -->
        <asp:Panel ID="panelMensaje" runat="server" CssClass="alert alert-hidden" Visible="false">
    <span class="alert-icon">
        
        <i id="iconMensaje" runat="server" class="fas fa-info-circle"></i>
    </span>
    <div>
        <strong><asp:Label ID="lblTitulo" runat="server"></asp:Label></strong>
        <p><asp:Label ID="lblDescripcion" runat="server"></asp:Label></p>
    </div>
</asp:Panel>

        <!-- Resultado de la consulta -->
        <asp:Panel ID="panelResultado" runat="server" CssClass="card" Visible="false">
            <h4>Saldo Disponible</h4>
            <asp:Literal ID="saldoAmount" runat="server"></asp:Literal>
            <br />
            <small><asp:Literal ID="telefonoAsociado" runat="server"></asp:Literal></small>
        </asp:Panel>
    </div>
    
</asp:Content>