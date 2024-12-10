<%@ Page Title="Inscribirse" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" Async="true" CodeBehind="Inscribirse.aspx.cs" Inherits="WebUsuarios.Paginas.Inscribirse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Inscribirse
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
        <h1 class="mt-4">Inscribirse</h1>

        <div class="card mb-4">
            <div class="card-header">
                <i class="fas fa-user-plus me-1"></i>
                Inscripción de Cuentas
            </div>
            <div class="card-body">
                <asp:Panel ID="PanelInscribirse" runat="server">
                    <!-- Selección de Cuenta -->
                    <div class="mb-3">
                        <label for="ddlCuentas" class="form-label">Seleccione una cuenta</label>
                        <asp:DropDownList ID="ddlCuentas" runat="server" CssClass="form-control" style="max-width: 300px;">
                            <asp:ListItem Text="Seleccione una cuenta" Value="" />
                        </asp:DropDownList>
                    </div>

                    <!-- Campo para Identificación -->
                    <div class="mb-3">
                        <label for="txtIdentificacion" class="form-label">Identificación</label>
                        <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control" style="max-width: 300px;" />
                    </div>

                    <!-- Campo para Número de Teléfono -->
                    <div class="mb-3">
                        <label for="txtTelefono" class="form-label">Número de Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" style="max-width: 300px;" />
                    </div>

                    <!-- Botón para realizar acción -->
                    <div class="mb-3">
                        <asp:Button ID="btnInscribir" runat="server" Text="Inscribir" CssClass="btn btn-primary" OnClick="btnInscribir_Click" />
                    </div>

                    <!-- Mensaje de Error o Éxito -->
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>


