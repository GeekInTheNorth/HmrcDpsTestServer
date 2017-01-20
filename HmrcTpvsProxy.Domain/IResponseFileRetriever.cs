using System.Xml;

namespace HmrcTpvsProxy.Domain
{
    public interface IResponseFileRetriever
    {
        XmlDocument GetResponse(RequestType requestType);

        string GetResponseAsString(RequestType requestType);
    }
}