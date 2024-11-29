<%@ Page Title="Consultar Saldo" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="ConsultarSaldo.aspx.cs" Inherits="WebUsuarios.Paginas.ConsultarSaldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Consultar Saldo
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <h1 class="mt-4">Consulta de saldo</h1>

        <div class="card mb-4">
            <div class="card-header">
                <i class="fas fa-wallet me-1"></i>
            </div>
            <div class="card-body">
                <asp:Panel ID="PanelConsultarSaldo" runat="server">
                    <div class="mb-3">
                        <label for="txtTelefono" class="form-label">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" style="max-width: 300px;"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnConsultarSaldo" runat="server" Text="Consultar" CssClass="btn btn-primary" />
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
