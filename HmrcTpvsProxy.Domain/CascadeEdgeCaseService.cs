using System;
using System.IO;
using System.Text;
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
            var xmlString = string.Empty;

            using (var stream = new MemoryStream())
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    var xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.Encoding = Encoding.UTF8;
                    xmlWriterSettings.Indent = true;

                    using (var xmlWriter = XmlWriter.Create(streamWriter, xmlWriterSettings))
                    {
                        xml.WriteTo(xmlWriter);
                        xmlWriter.Flush();
                        xmlWriter.Close();
                    }

                    using (var streamReader = new StreamReader(stream))
                    {
                        stream.Position = 0;
                        xmlString = streamReader.ReadToEnd();
                        streamReader.Close();
                    }
                }
            }

            return xmlString;
        }
    }
}
