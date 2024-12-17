using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace WebUsuarios
{
    public class RegistrationServiceClient
    {
        private readonly string serviceUrl = "http://localhost:8086/api/register";

        public async Task<RegistrationResponseDto> RegisterUserAsync(RegistrationRequestDto request)
        {
            // Serializar el objeto request a XML
            string xmlData = SerializeToXml(request);

            using (HttpClient client = new HttpClient())
            {
                // Configurar encabezados
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/xml");

                // Crear el contenido de la solicitud
                StringContent content = new StringContent(xmlData, Encoding.UTF8, "application/xml");

                // Enviar la solicitud
                HttpResponseMessage response = await client.PostAsync(serviceUrl, content);

                // Verificar la respuesta
                if (!response.IsSuccessStatusCode)
                {
                    string errorDetails = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine("Error del servicio - Cuerpo: " + errorDetails);
                    throw new Exception($"Error del servicio: {response.StatusCode} - {response.ReasonPhrase} - {errorDetails}");
                }

                // Leer y deserializar la respuesta
                string responseContent = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Respuesta del servicio: " + responseContent);

                return DeserializeFromXml<RegistrationResponseDto>(responseContent);
            }
        }

        private string SerializeToXml<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = false, // Incluir <?xml version="1.0"?>
                Encoding = new UTF8Encoding(false), // UTF-8 sin BOM
                Indent = true                      // Formatear el XML
            };

            using (var memoryStream = new MemoryStream())
            using (var xmlWriter = XmlWriter.Create(memoryStream, settings))
            {
                serializer.Serialize(xmlWriter, obj);
                return Encoding.UTF8.GetString(memoryStream.ToArray()); // Convertir el resultado a cadena UTF-8
            }
        }

        private T DeserializeFromXml<T>(string xmlData)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stringReader = new StringReader(xmlData))
            {
                return (T)serializer.Deserialize(stringReader);
            }
        }
    }

    // DTOs para la solicitud y la respuesta
    [XmlRoot("RegistrationRequestDto")]
    public class RegistrationRequestDto
    {
        [XmlElement("cuenta")]
        public string Cuenta { get; set; }

        [XmlElement("identificacion")]
        public string Identificacion { get; set; }

        [XmlElement("telefono")]
        public string Telefono { get; set; }
    }

    [XmlRoot("RegistrationResponseDto")]
    public class RegistrationResponseDto
    {
        [XmlElement("codigo")]
        public int Codigo { get; set; }

        [XmlElement("mensaje")]
        public string Mensaje { get; set; }
    }
}
