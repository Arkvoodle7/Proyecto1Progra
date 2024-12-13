using Datos;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Negocios
{
    public class NegociosTransferencias
    {
        private readonly DatosTransferencias _datosTransferencias;

        public NegociosTransferencias()
        {
            _datosTransferencias = new DatosTransferencias();
        }

        public async Task<(bool isSuccess, string mensaje)> RealizarTransferencia(string telefonoDestino, decimal monto, string descripcion)
        {
            try
            {
                var response = await _datosTransferencias.RealizarTransferencia(telefonoDestino, monto, descripcion);

                var xml = XDocument.Parse(response);
                var codigo = xml.Descendants().FirstOrDefault(e => e.Name.LocalName == "codigo")?.Value;
                var descripcionRespuesta = xml.Descendants().FirstOrDefault(e => e.Name.LocalName == "descripcion")?.Value;

                if (codigo == "0")
                {
                    return (true, descripcionRespuesta ?? "Transacción realizada con éxito.");
                }
                else
                {
                    return (false, descripcionRespuesta ?? "Ocurrió un error en la transacción.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en NegociosTransferencias: {ex.Message}");
                return (false, "No se pudo procesar la transacción. Intentelo nuevamente en unos minutos.");
            }
        }
    }
}
