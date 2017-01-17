using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Serialization
{
    public class Serializer : ISerializer
    {
        public string Serialize<T>(T obj, XmlSerializerNamespaces xmlNamespaces)
        {
            // remove the XML node from the top
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            var serializer = new XmlSerializer(obj.GetType());

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, obj, xmlNamespaces);
                return stream.ToString();
            }
        }

        public string Serialize<T>(T obj, string namespaceAddress)
        {
            var xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add(string.Empty, namespaceAddress);

            return Serialize(obj, xmlNamespaces);
        }

        public string Serialize<T>(T obj)
        {
            // remove the XML node from the top
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            var serializer = new XmlSerializer(obj.GetType());

            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, obj);
                return stream.ToString();
            }
        }
    }
}