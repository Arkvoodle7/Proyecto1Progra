<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Paginas/Pagina.master" AutoEventWireup="true" CodeBehind="PaginaRegistro.aspx.cs" Inherits="WebUsuarios.Paginas.PaginaRegistro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-5">
                <div class="card shadow-lg border-0 rounded-lg mt-5">
                    <div class="card-header">
                        <h3 class="text-center font-weight-light my-4">Registrarse</h3>
                    </div>
                    <div class="card-body">
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" CssClass="form-text text-danger"></asp:Label>
                        <div class="form-group mb-3">
                            <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control" placeholder="Identificación" />
                        </div>
                        <div class="form-group mb-3">
                            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" placeholder="Nombre de usuario" />
                        </div>
                        <div class="form-group mb-3">
                            <asp:TextBox ID="txtNombreCompleto" runat="server" CssClass="form-control" placeholder="Nombre completo" />
                        </div>
                        <div class="form-group mb-3">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Contraseña" TextMode="Password" />
                        </div>
                        <div class="form-group mb-3">
                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Número de teléfono" />
                        </div>
                        <div class="d-grid mb-3">
                            <asp:Button ID="btnRegistrar" runat="server" CssClass="btn btn-primary" Text="Aceptar" />
                        </div>
                        <div class="d-grid">
                            <asp:Button ID="btnRegresar" runat="server" CssClass="btn btn-secondary" Text="Regresar" PostBackUrl="~/Paginas/PaginaLogin.aspx" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
