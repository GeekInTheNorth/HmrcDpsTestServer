using System.Xml;

namespace HmrcTpvsProxy.Domain
{
    public interface ICascadeEdgeCaseService
    {
        XmlDocument GetResponseFor(XmlDocument request);

        string GetResponseFor(string request);
    }
}