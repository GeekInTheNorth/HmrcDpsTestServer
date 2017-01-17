using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Serialization
{
    public interface ISerializer
    {
        string Serialize<T>(T obj);
        string Serialize<T>(T obj, XmlSerializerNamespaces xmlNamespaces);
        string Serialize<T>(T obj, string namespaceAddress);
    }
}