<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NuevoAdmin.aspx.cs" Inherits="WebAdministracion.Paginas.NuevoAdmin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../css/adminstyles.css" rel="stylesheet" />
    <style>
        h1 {
            color: #007bff; /* Color azul para los títulos */
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            text-align: center;
            margin-bottom: 20px;
        }
        .btn-primary {
            background-color: #007bff; /* Azul para el botón Aceptar */
            color: white;
            border: none;
            padding: 10px 20px;
            cursor: pointer;
            font-size: 16px;
            border-radius: 5px;
            margin: 10px;
        }
        .btn-primary:hover {
            background-color: #0056b3; /* Azul oscuro para el hover */
        }
        .btn-regresar {
            background-color: #0056b3; /* Azul oscuro para el botón Regresar */
            color: white;
        }
        .btn-regresar:hover {
            background-color: #003d82; /* Azul más oscuro para el hover */
        }
        .form-group {
            text-align: center;
            margin-bottom: 15px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
    </style>
    <title>Nuevo Administrador</title>
</head>
<body>
    <form id="frm_nuevoadmin" runat="server">
        <div>
            <h1>Nuevo Administrador</h1>

            <div id="nombreU" class="form-group">
                <asp:Label ID="lblNombreU" Text="Nombre de usuario:" runat="server" />
                <asp:TextBox ID="txt_nombreU" runat="server" />
            </div>

            <div id="nombreC" class="form-group">
                <asp:Label ID="lblnombreC" Text="Nombre completo:" runat="server" />
                <asp:TextBox ID="txt_nombreC" runat="server" />
            </div>

            <div id="contra" class="form-group">
                <asp:Label ID="lblcontra" Text="Contraseña:" runat="server" />
                <asp:TextBox ID="txt_contra" runat="server" TextMode="Password"/>
            </div>

            <div id="botones" style="text-align: center;">
                <asp:Button ID="btn_aceptar" Text="Aceptar" runat="server" onclick="btnAceptar_Click" CssClass="btn-primary" />
                <asp:Button ID="btn_regresar" Text="Regresar" runat="server" OnClick="btn_regresar_Click" CssClass="btn-regresar" />
            </div>

            <div style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif">
                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
