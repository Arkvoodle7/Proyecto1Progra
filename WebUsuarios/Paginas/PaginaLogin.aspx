<<<<<<< HEAD
﻿<%@ Page Title="Login" Language="C#" MasterPageFile="~/Paginas/Pagina.master" AutoEventWireup="true" CodeBehind="PaginaLogin.aspx.cs" Inherits="WebUsuarios.Paginas.PaginaLogin" Async="true" %>
=======
<%@ Page Title="Login" Language="C#" MasterPageFile="~/Paginas/Pagina.master" AutoEventWireup="true" CodeBehind="PaginaLogin.aspx.cs" Inherits="WebUsuarios.Paginas.PaginaLogin" Async="true" %>
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-5">
                <div class="card shadow-lg border-0 rounded-lg mt-5">
                    <div class="card-header">
                        <h3 class="text-center font-weight-light my-4">Iniciar Sesión</h3>
                    </div>
                    <div class="card-body">
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" CssClass="form-text text-danger"></asp:Label>
                        <div class="form-group mb-3">
<<<<<<< HEAD
                            <asp:TextBox 
                                ID="txtUsuario" 
                                runat="server" 
                                CssClass="form-control mb-3" 
                                placeholder="Usuario"></asp:TextBox>
                        </div>
                        <div class="form-group mb-3">
                            <asp:TextBox 
                                ID="txtPassword" 
                                runat="server" 
                                CssClass="form-control mb-3" 
                                placeholder="Contraseña" 
                                TextMode="Password"></asp:TextBox>
                        </div>
                        <asp:Button 
                            ID="btnLogin" 
                            runat="server" 
                            CssClass="btn btn-primary mt-3 w-100" 
=======
                            <asp:TextBox
                                ID="txtUsuario"
                                runat="server"
                                CssClass="form-control mb-3"
                                placeholder="Usuario"></asp:TextBox>
                        </div>
                        <div class="form-group mb-3">
                            <asp:TextBox
                                ID="txtPassword"
                                runat="server"
                                CssClass="form-control mb-3"
                                placeholder="Contraseña"
                                TextMode="Password"></asp:TextBox>
                        </div>
                        <asp:Button
                            ID="btnLogin"
                            runat="server"
                            CssClass="btn btn-primary mt-3 w-100"
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
                            Text="Iniciar Sesión"
                            OnClick="btnLogin_Click" />
                        <div class="text-center mt-4">
                            <asp:HyperLink ID="hlRegistro" runat="server" CssClass="text-decoration-none" NavigateUrl="PaginaRegistro.aspx">
                                ¿No tienes cuenta aún? Regístrate aquí
                            </asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
