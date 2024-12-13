<%@ Page Title="Transferencias" Language="C#" Async="true" MasterPageFile="~/Paginas/Pagina.master" AutoEventWireup="true" CodeBehind="Transferencias.aspx.cs" Inherits="WebUsuarios.Paginas.Transferencias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Transferencias
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        /* Estilos generales */
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }
         .alert-container {
            position: fixed;
            top: 50px; /* Bajamos la posición */
            right: 20px;
            z-index: 1050;
            width: 400px; /* Más ancho */
            max-height: 100px; /* Menos alto */
        }

        .alert-custom-icon {
            margin-right: 10px;
            font-size: 1.5rem;
        }

        .alert-title {
            font-weight: bold;
        }

        .container {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 95vh;
        }

        .card {
            background: #ffffff;
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
            max-width: 500px;
            width: 100%;
            padding: 30px;
        }

        .card h2 {
            font-size: 1.8rem;
            font-weight: 600;
            color: #333333;
            text-align: center;
            margin-bottom: 20px;
        }

        .form-label {
            font-size: 0.9rem;
            font-weight: 500;
            color: #555555;
            margin-bottom: 8px;
        }

        .form-control {
            border-radius: 10px;
            border: 1px solid #dddddd;
            padding: 10px 15px;
            font-size: 1rem;
            color: #333333;
            box-shadow: inset 0 2px 4px rgba(0, 0, 0, 0.05);
            transition: all 0.3s ease;
        }

        .form-control:focus {
            border-color: #007bff;
            box-shadow: 0 0 8px rgba(0, 123, 255, 0.2);
        }

        .btn-primary {
            display: flex;
            align-items: center;
            justify-content: center;
            background-color: #007bff;
            border: none;
            border-radius: 10px;
            color: #ffffff;
            font-size: 1rem;
            font-weight: 600;
            padding: 12px 20px;
            gap: 0.2rem;
            cursor: pointer;
            transition: all 0.3s ease;
        }

        .btn-primary:hover {
            background-color: #0056b3;
        }

        
        
    </style>

    <div class="container">

         <div class="alert-container">
            <asp:Literal ID="lblMensaje" runat="server" EnableViewState="false"></asp:Literal>
        </div>
        <!-- Formulario -->
        <div class="card">
            <h2>Realizar Transferencia</h2>
            <div class="mb-3">
                <label for="txtTelefonoDestino" class="form-label">Teléfono Destino</label>
                <asp:TextBox ID="txtTelefonoDestino" CssClass="form-control" runat="server" placeholder="Ingrese el número de teléfono"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtMonto" class="form-label">Monto</label>
                <asp:TextBox ID="txtMonto" CssClass="form-control" runat="server" placeholder="Ingrese el monto" TextMode="Number"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtDescripcion" class="form-label">Descripción</label>
                <asp:TextBox ID="txtDescripcion" CssClass="form-control" runat="server" placeholder="Ingrese una descripción"></asp:TextBox>
            </div>
            <button type="button" class="btn btn-primary w-100">
                <i class="fas fa-paper-plane"></i> Enviar
            </button>
        </div>
    </div>

    <!-- Modal de Confirmación -->
    <div class="modal fade" id="modalConfirmacion" tabindex="-1" aria-labelledby="modalConfirmacionLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalConfirmacionLabel">Confirmar Transferencia</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>¿Realmente desea realizar la transferencia al teléfono <span id="telefonoConfirmacion"></span>?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">No</button>
                    <asp:Button ID="btnConfirmar" CssClass="btn btn-primary" runat="server" Text="Si" />
                </div>
            </div>
        </div>
    </div>

    <script>
        function validarFormulario() {
            const telefonoDestino = document.getElementById('<%= txtTelefonoDestino.ClientID %>').value.trim();
            const monto = document.getElementById('<%= txtMonto.ClientID %>').value.trim();
            const descripcion = document.getElementById('<%= txtDescripcion.ClientID %>').value.trim();

            if (!telefonoDestino || !monto || !descripcion) {
                // Invocar la función desde el servidor
                __doPostBack('<%= lblMensaje.ClientID %>', 'Campos vacíos');
                return;
            }

            // Mostrar el número de teléfono en el modal
            document.getElementById('telefonoConfirmacion').innerText = telefonoDestino;

            // Abrir el modal
            var modal = new bootstrap.Modal(document.getElementById('modalConfirmacion'));
            modal.show();
        }
    </script>
</asp:Content>