﻿using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Datos
{
    public class DatosRegistro
    {
        private readonly string url = "http://localhost:8083/usuario/crear";

        public async Task<bool> RegistrarUsuarioAsync(Usuario usuario)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //serializar el objeto Usuario a XML
                    var xmlSerializer = new XmlSerializer(typeof(Usuario));
                    var ns = new XmlSerializerNamespaces();
                    ns.Add("", "");

                    var settings = new XmlWriterSettings
                    {
                        OmitXmlDeclaration = true,
                        Encoding = Encoding.UTF8
                    };

                    string xmlContent;
                    using (var stringWriter = new StringWriter())
                    using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                    {
                        xmlSerializer.Serialize(xmlWriter, usuario, ns);
                        xmlContent = stringWriter.ToString();
                    }

                    //crear el contenido de la solicitud
                    var content = new StringContent(xmlContent, Encoding.UTF8, "application/xml");

                    //enviar la solicitud POST al WS
                    var response = await client.PostAsync(url, content);

                    //verificar el codigo de estado HTTP
                    if (response.IsSuccessStatusCode)
                    {
                        return true; //registro exitoso
                    }
                    else
                    {
                        return false; //error en el registro
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    //clase Usuario para serializacion
    [XmlRoot("usuario")]
    public class Usuario
    {
        [XmlElement("identificacion")]
        public string Identificacion { get; set; }

        [XmlElement("nombreUsuario")]
        public string NombreUsuario { get; set; }

        [XmlElement("nombreCompleto")]
        public string NombreCompleto { get; set; }

        [XmlElement("contrasena")]
        public string Contrasena { get; set; }

        [XmlElement("telefono")]
        public string Telefono { get; set; }
    }
}
