<%@ Page Title="Transferencias" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="Transferencias.aspx.cs" Inherits="WebUsuarios.Paginas.Transferencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Transferencias
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <h1 class="mt-4">Transferencias</h1>

        <div class="card mb-4">
            <div class="card-header">
                <i class="fas fa-exchange-alt me-1"></i>
            </div>
            <div class="card-body">
                <asp:Panel ID="PanelTransferencias" runat="server">
                    <div class="mb-3">
                        <label for="txtTelefonoDestino" class="form-label">Teléfono destino</label>
                        <asp:TextBox ID="txtTelefonoDestino" runat="server" CssClass="form-control" style="max-width: 300px;"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtMonto" class="form-label">Monto de la transacción</label>
                        <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control" style="max-width: 300px;" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtDescripcion" class="form-label">Descripción de la transacción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" style="max-width: 300px;"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnTransferir" runat="server" Text="Enviar" CssClass="btn btn-primary" />
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
