using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosConsultarSaldo
    {
        private readonly string _soapSaldoEndpoint = "http://localhost:8084/wsusuarios";

        public async Task<string> ConsultarSaldo(string telefono)
        {
            var soapSaldoRequest = $@"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:wsu='http://pagosmoviles.com/wsusuarios'>
                   <soapenv:Header/>
                   <soapenv:Body>
                      <wsu:saldoRequest>
                         <wsu:telefono>{telefono}</wsu:telefono>
                      </wsu:saldoRequest>
                   </soapenv:Body>
                </soapenv:Envelope>";

            using (var client = new HttpClient())
            {
                var saldoContent = new StringContent(soapSaldoRequest, Encoding.UTF8, "text/xml");
                var saldoResponse = await client.PostAsync(_soapSaldoEndpoint, saldoContent);
                return await saldoResponse.Content.ReadAsStringAsync();

            }
        }
    }
}