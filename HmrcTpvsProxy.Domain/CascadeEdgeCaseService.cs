using System;
using System.IO;
using System.Xml;

namespace HmrcTpvsProxy.Domain
{
    public class CascadeEdgeCaseService : ICascadeEdgeCaseService
    {
        private readonly IRequestTypeResolver requestTypeResolver;
        private readonly IResponseFileRetriever responseFileRetriever;

        public CascadeEdgeCaseService(IRequestTypeResolver requestTypeResolver, IResponseFileRetriever responseFileRetriever)
        {
            if (requestTypeResolver == null) throw new ArgumentNullException("requestTypeResolver");
            if (responseFileRetriever == null) throw new ArgumentNullException("responseFileRetriever");

            this.requestTypeResolver = requestTypeResolver;
            this.responseFileRetriever = responseFileRetriever;
        }

        public XmlDocument GetResponseFor(XmlDocument request)
        {
            var requestType = requestTypeResolver.GetRequestType(request);
            var response = responseFileRetriever.GetResponse(requestType);

            return response;
        }

        public string GetResponseFor(string request)
        {
            var requestXml = new XmlDocument();
            if (!string.IsNullOrWhiteSpace(request))
                requestXml.LoadXml(request);

            var response = GetResponseFor(requestXml);

            return GetXmlDocumentAsString(response);
        }

        private string GetXmlDocumentAsString(XmlDocument xml)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    xml.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    return stringWriter.GetStringBuilder().ToString();
                }
            }
        }
    }
}
