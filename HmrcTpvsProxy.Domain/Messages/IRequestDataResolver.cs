using System.Xml;

namespace HmrcTpvsProxy.Domain.Messages
{
    public interface IRequestDataResolver
    {
        RequestData Get(XmlDocument requestXml);
    }
}
