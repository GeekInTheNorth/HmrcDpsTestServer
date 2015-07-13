using System.Xml;

namespace HmrcTpvsProxy.Domain
{
    public interface IResponseFileResolver
    {
        XmlDocument GetResponse(ResponseType responseType);
    }
}