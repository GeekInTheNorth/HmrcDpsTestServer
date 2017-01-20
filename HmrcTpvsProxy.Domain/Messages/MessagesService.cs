using System;
using System.Xml;
using HmrcTpvsProxy.Domain.Messages.Nodes;
using HmrcTpvsProxy.Domain.Messages.Serialization;

namespace HmrcTpvsProxy.Domain.Messages
{
    public class MessagesService : IMessagesService
    {
        private IMessagesRepository repository;
        private IRequestDataResolver requestDataResolver;
        private IResponseBuilder responseBuilder;
        private ISerializer serializer;

        public MessagesService(IMessagesRepository repository, IRequestDataResolver requestDataResolver, IResponseBuilder responseBuilder, ISerializer serializer)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (requestDataResolver == null) throw new ArgumentNullException(nameof(requestDataResolver));
            if (responseBuilder == null) throw new ArgumentNullException(nameof(responseBuilder));
            if (serializer == null) throw new ArgumentNullException(nameof(serializer));

            this.repository = repository;
            this.requestDataResolver = requestDataResolver;
            this.responseBuilder = responseBuilder;
            this.serializer = serializer;
        }

        public string GetResponse(int datasourceId, string requestXml)
        {
            if (datasourceId < 1) throw new ArgumentException("Invalid datasource id", nameof(datasourceId));
            if (string.IsNullOrWhiteSpace(requestXml)) throw new ArgumentException("Invalid XML in request.", nameof(requestXml));

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(requestXml);

            var requestData = requestDataResolver.Get(xmlDocument);

            if (requestData.RequestType == RequestType.Authorisation)
                return CascadeEdgeCaseFiles.Authorisation;

            var envelope = GetEnvelope(datasourceId, requestData);

            return envelope == null ? null : serializer.Serialize(envelope);
        }

        private Envelope GetEnvelope(int datasourceId, RequestData requestData)
        {
            switch (requestData.RequestType)
            {
                case RequestType.P6:
                    return responseBuilder.Build(requestData, repository.GetP6CodingNotices(datasourceId));
                case RequestType.P9:
                    return responseBuilder.Build(requestData, repository.GetP9CodingNotices(datasourceId));
                case RequestType.SL1:
                    return responseBuilder.Build(requestData, repository.GetStudentLoanStartNotices(datasourceId));
                case RequestType.SL2:
                    return responseBuilder.Build(requestData, repository.GetStudentLoanEndNotices(datasourceId));
                default:
                    return null;
            }
        }
    }
}
