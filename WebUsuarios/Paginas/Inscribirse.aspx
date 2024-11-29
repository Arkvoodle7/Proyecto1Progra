<%@ Page Title="Inscribirse" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="Inscribirse.aspx.cs" Inherits="WebUsuarios.Paginas.Inscribirse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Inscribirse
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <h1 class="mt-4">Inscribirse</h1>

        <div class="card mb-4">
            <div class="card-header">
                <i class="fas fa-user-plus me-1"></i>
            </div>
            <div class="card-body">
                <asp:Panel ID="PanelInscribirse" runat="server">
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
                    <asp:Button ID="btnInscribir" runat="server" Text="Inscribir" CssClass="btn btn-primary" />
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
