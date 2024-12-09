<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NuevoAdmin.aspx.cs" Inherits="WebAdministracion.Paginas.NuevoAdmin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../css/styles.css" rel="stylesheet" />
    <title>Nuevo Administrador</title>
</head>
<body>
    <form id="frm_nuevoadmin" runat="server">
        <div>
            <h1>Nuevo Administrador</h1>

            <div id="nombreU">
                <asp:Label ID="lblNombreU" Text="Nombre de usuario:" runat="server" />
                <asp:TextBox ID="txt_nombreU" runat="server" />
            </div>

            <div id="nombreC">
                <asp:Label ID="lblnombreC" Text="Nombre completo:" runat="server" />
                <asp:TextBox ID="txt_nombreC" runat="server" />
            </div>

            <div id="contra">
                <asp:Label ID="lblcontra" Text="Contraseña:" runat="server" />
                <asp:TextBox ID="txt_contra" runat="server" />
            </div>

            <div id="botones">
                <asp:Button ID="btn_aceptar" Text="Aceptar" runat="server" onclick="btnAceptar_Click" CssClass="btn-primary" />
                <asp:Button ID="btn_regresar" Text="Regresar" runat="server" OnClick="btn_regresar_Click" CssClass="btn-primary" />
            </div>

            <div>
                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
