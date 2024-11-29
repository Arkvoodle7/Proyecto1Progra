<%@ Page Title="Desinscribirse" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="Desinscribirse.aspx.cs" Inherits="WebUsuarios.Paginas.Desinscribirse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Desinscribirse
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <h1 class="mt-4">Des inscribirse</h1>

        <div class="card mb-4">
            <div class="card-header">
                <i class="fas fa-user-minus me-1"></i>
            </div>
            <div class="card-body">
                <asp:Panel ID="PanelDesinscribirse" runat="server">
                    <div class="mb-3">
                        <label for="txtCuenta" class="form-label">Cuenta</label>
                        <asp:TextBox ID="txtCuenta" runat="server" CssClass="form-control" style="max-width: 300px;"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtIdentificacion" class="form-label">Identificación</label>
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control" style="max-width: 300px;"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <label for="txtTelefono" class="form-label">Número de teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" style="max-width: 300px;"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnDesinscribir" runat="server" Text="Des inscribir" CssClass="btn btn-danger" />
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
