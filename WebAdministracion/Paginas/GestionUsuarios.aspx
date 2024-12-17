<<<<<<< HEAD
﻿<%@ Page Title="Gestión de Usuarios" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="GestionUsuarios.aspx.cs" Inherits="WebAdministracion.Paginas.GestionUsuarios" %>
=======
﻿<%@ Page Title="Gestión de Usuarios" Language="C#" MasterPageFile="~/Paginas/Pagina.Master" AutoEventWireup="true" CodeBehind="GestionUsuarios.aspx.cs" Inherits="WebAdministracion.Paginas.GestionUsuarios" Async="true" %>
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gestión de Usuarios
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<<<<<<< HEAD
    <div>
        <h2>Usuarios</h2>
        <asp:Button ID="btnAgregarUsuario" runat="server" Text="Agregar Usuario" CssClass="btn btn-primary mb-3" />
        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="placeholder1">
            <Columns>
                <asp:BoundField DataField="placeholder1" HeaderText="placeholder1" />
                <asp:BoundField DataField="placeholder2" HeaderText="placeholder2" />
                <asp:BoundField DataField="placeholder3" HeaderText="placeholder3" />
                <asp:BoundField DataField="placeholder4" HeaderText="placeholder4" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="Modificar" CommandArgument='<%# Eval("placeholder1") %>' CssClass="btn btn-warning btn-sm" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("placeholder1") %>' CssClass="btn btn-danger btn-sm" />
=======
    <div class="container mt-5">
        <h2 class="mb-4 text-center">Gestión de Usuarios</h2>
        <div class="d-flex justify-content-between align-items-center mb-3">
            <asp:Button ID="btnAgregarUsuario" runat="server" Text="Nuevo" CssClass="btn btn-primary" OnClick="btnAgregarUsuario_Click" />
            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger mt-3 d-block"></asp:Label>
        </div>
        <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" DataKeyNames="Identificacion" OnRowCommand="gvUsuarios_RowCommand">
            <Columns>
                <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre de Usuario" />
                <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar" CommandArgument='<%# Eval("Identificacion") %>' CssClass="btn btn-warning btn-sm me-1" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Eval("Identificacion") %>' CssClass="btn btn-danger btn-sm" OnClientClick='<%# "return confirmarEliminacion(\"" + Eval("Identificacion") + "\");" %>' />
>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
<<<<<<< HEAD
    </div>
</asp:Content>
=======

    </div>
<script type="text/javascript">
    function confirmarEliminacion(identificacion) {
        console.log("Intentando eliminar usuario con Identificación:", identificacion);
        Swal.fire({
            title: '¿Realmente desea eliminar el usuario seleccionado?',
            showCancelButton: true,
            confirmButtonText: 'Sí',
            cancelButtonText: 'No',
            icon: 'warning',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                console.log("Confirmación de eliminación recibida para Identificación:", identificacion);
                // realiza el postback al servidor con la identificación
                __doPostBack('<%= gvUsuarios.UniqueID %>', 'Eliminar$' + identificacion);
            } else {
                console.log("Eliminación cancelada para Identificación:", identificacion);
            }
        });

        return false;
    }
</script>






</asp:Content>

>>>>>>> bac7423082a8dfee49ab73d45a08abd65507d25d
