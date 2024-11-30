using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Datos
{
    public class DatosLogin
    {
        private readonly string url = "http://localhost:8082/auth/login";

        public async Task<AuthResponse> ValidarCredencialesAsync(string nombreUsuario, string contrasena)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Crear la trama XML del authRequestDto
                    var authRequest = new AuthRequestDto
                    {
                        NombreUsuario = nombreUsuario,
                        Contrasena = contrasena
                    };

                    var xmlSerializer = new XmlSerializer(typeof(AuthRequestDto));
                    var ns = new XmlSerializerNamespaces();
                    ns.Add("", ""); // Elimina los espacios de nombres

                    var settings = new XmlWriterSettings
                    {
                        OmitXmlDeclaration = true,
                        Encoding = Encoding.UTF8
                    };

                    string xmlContent;
                    using (var stringWriter = new StringWriter())
                    using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                    {
                        xmlSerializer.Serialize(xmlWriter, authRequest, ns);
                        xmlContent = stringWriter.ToString();
                    }

                    // Registrar el XML enviado
                    System.Diagnostics.Debug.WriteLine("XML Enviado:");
                    System.Diagnostics.Debug.WriteLine(xmlContent);

                    // Enviar la solicitud al WS con Content-Type correcto
                    var content = new StringContent(xmlContent, Encoding.UTF8, "application/xml");
                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer la respuesta del WS
                        var responseXml = await response.Content.ReadAsStringAsync();
                        var xmlDeserializer = new XmlSerializer(typeof(AuthResponse));
                        using (var stringReader = new StringReader(responseXml))
                        {
                            var authResponse = (AuthResponse)xmlDeserializer.Deserialize(stringReader);
                            return authResponse;
                        }
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        return new AuthResponse
                        {
                            Resultado = -1,
                            Mensaje = $"Error al conectar con el servicio. Código de estado: {response.StatusCode}, Motivo: {response.ReasonPhrase}, Detalles: {errorContent}"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    Resultado = -1,
                    Mensaje = $"Error: {ex.Message}. Detalles: {ex.StackTrace}"
                };
            }
        }
    }

    // Clases para serializar y deserializar las tramas
    [XmlRoot("authRequestDto")]
    public class AuthRequestDto
    {
        [XmlElement("nombreUsuario")]
        public string NombreUsuario { get; set; }

        [XmlElement("contrasena")]
        public string Contrasena { get; set; }
    }

    [XmlRoot("authResponse")]
    public class AuthResponse
    {
        [XmlElement("resultado")]
        public int Resultado { get; set; }

        [XmlElement("mensaje")]
        public string Mensaje { get; set; }
    }
}
