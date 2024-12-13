using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DatosTransferencias
    {
        private readonly string _soapTransferenciaEndpoint = "http://localhost:8085/wsusuarios";

        public async Task<string> RealizarTransferencia(string telefonoDestino, decimal monto, string descripcion)
        {
            var soapTransferenciaRequest = $@"
                <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:wsu=""http://pagosmoviles.com/wsusuarios"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <wsu:transaccionRequest>
                         <wsu:telefono>{telefonoDestino}</wsu:telefono>
                         <wsu:monto>{monto}</wsu:monto>
                         <wsu:descripcion>{descripcion}</wsu:descripcion>
                      </wsu:transaccionRequest>
                   </soapenv:Body>
                </soapenv:Envelope>";

            using (var client = new HttpClient())
            {
                try
                {
                    var transferenciaContent = new StringContent(soapTransferenciaRequest, Encoding.UTF8, "text/xml");
                    client.Timeout = TimeSpan.FromSeconds(30);

                    var response = await client.PostAsync(_soapTransferenciaEndpoint, transferenciaContent);
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al realizar la transferencia: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
