<<<<<<< HEAD
﻿<%@ Page Title="Desinscribirse" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="Desinscribirse.aspx.cs" Inherits="WebUsuarios.Paginas.Desinscribirse" %>
=======
<%@ Page Title="Desinscribirse" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" Async="true" CodeBehind="Desinscribirse.aspx.cs" Inherits="WebUsuarios.Paginas.Desinscribirse" %>
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Desinscribirse
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-4">
<<<<<<< HEAD
        <h1 class="mt-4">Des inscribirse</h1>
=======
        <h1 class="mt-4">Desinscribirse</h1>
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d

        <div class="card mb-4">
            <div class="card-header">
                <i class="fas fa-user-minus me-1"></i>
<<<<<<< HEAD
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
=======
                Desinscripción de Cuentas
            </div>
            <div class="card-body">
                <asp:Panel ID="PanelDesinscribirse" runat="server">
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
                        <asp:Button ID="btnDesinscribir" runat="server" Text="Des inscribir" CssClass="btn btn-danger" OnClick="btnDesinscribir_Click" />
                    </div>

                    <!-- Mensaje de Error o Éxito -->
                    <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
