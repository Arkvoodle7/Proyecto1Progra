<<<<<<< HEAD
﻿<%@ Page Title="Login" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="PaginaLogin.aspx.cs" Inherits="WebAdministracion.Paginas.PaginaLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
=======
<%@ Page Title="Login" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="PaginaLogin.aspx.cs" Inherits="WebAdministracion.Paginas.PaginaLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../css/styles.css" rel="stylesheet" />
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-5">
                <div class="card shadow-lg border-0 rounded-lg mt-5">
                    <div class="card-header">
                        <h3 class="text-center font-weight-light my-4">Iniciar Sesión</h3>
                    </div>
                    <div class="card-body">
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" CssClass="form-text text-danger"></asp:Label>
                        <asp:TextBox 
                            ID="txtUsuario" 
                            runat="server" 
                            CssClass="form-control mb-3" 
                            placeholder="Usuario"></asp:TextBox>
                        <asp:TextBox 
                            ID="txtPassword" 
                            runat="server" 
                            CssClass="form-control mb-3" 
                            placeholder="Contraseña" 
                            TextMode="Password"></asp:TextBox>
                        <asp:Button 
                            ID="btnLogin" 
                            runat="server" 
                            CssClass="btn btn-primary mt-3 w-100" 
<<<<<<< HEAD
                            Text="Iniciar Sesión" />
=======
                            Text="Iniciar Sesión" onclick="btnLogin_Click"/>
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
