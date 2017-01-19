using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HmrcTpvsProxy.Domain.Messages
{
    public class MessagesService : IMessagesService
    {
        private IMessagesRepository repository;
        private IRequestDataResolver requestDataResolver;

        public MessagesService(IMessagesRepository repository, IRequestDataResolver requestDataResolver)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (requestDataResolver == null) throw new ArgumentNullException(nameof(requestDataResolver));

            this.repository = repository;
            this.requestDataResolver = requestDataResolver;
        }

        public string GetResponse(int datasourceId, string requestXml)
        {
            if (datasourceId < 1) throw new ArgumentException("Invalid datasource id", nameof(datasourceId));
            if (string.IsNullOrWhiteSpace(requestXml)) throw new ArgumentException("Invalid XML in request.", nameof(requestXml));

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(requestXml);

            var requestData = requestDataResolver.Get(xmlDocument);

            return null;
        }
    }
}
