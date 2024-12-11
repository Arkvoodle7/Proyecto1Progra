using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace Negocios
{
    public class NegociosConsultarSaldo
    {
        private readonly DatosConsultarSaldo _datosConsultarSaldo;

        public NegociosConsultarSaldo()
        {
            _datosConsultarSaldo = new DatosConsultarSaldo();
        }

        public async Task<(bool isSuccess, string title, string message)> ConsultarSaldo(string telefono)
        {
            var response = await _datosConsultarSaldo.ConsultarSaldo(telefono);

            // Parsear el XML para extraer los datos
            var xml = XDocument.Parse(response);
            var codigo = xml.Descendants("codigo").FirstOrDefault()?.Value;
            var descripcion = xml.Descendants("descripcion").FirstOrDefault()?.Value;

            if (codigo == "0")
            {
                var saldo = xml.Descendants("saldo").FirstOrDefault()?.Value;
                return (true, "Consulta Exitosa", $"El saldo disponible en su cuenta es de: {saldo} colones");
            }
            else
            {
                return (false, "Error", descripcion ?? "Ha ocurrido un error desconocido.");
            }
        }
    }
}
