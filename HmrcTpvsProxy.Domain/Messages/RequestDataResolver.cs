using System;
using System.Xml;

namespace HmrcTpvsProxy.Domain.Messages
{
    public class RequestDataResolver : IRequestDataResolver
    {
        public RequestData Get(XmlDocument requestXml)
        {
            if (requestXml == null || requestXml.ChildNodes.Count == 0)
                return new RequestData();

            if (IsAuthorisation(requestXml))
                return GetAuthorisationData(requestXml);

            return GetMessageRequestData(requestXml);
        }

        private RequestData GetAuthorisationData(XmlDocument requestXml)
        {
            var requestData = new RequestData();
            requestData.RequestType = RequestType.Authorisation;
            requestData.VendorId = requestXml.GetElementsByTagName("m:vendorID")[0].InnerText;

            return requestData;
        }

        private RequestData GetMessageRequestData(XmlDocument requestXml)
        {
            var requestData = new RequestData();
            requestData.RequestType = GetRequestType(requestXml);
            requestData.PayeReference = GetNodeValueAsString(requestXml, "m:entity");
            requestData.VendorId = GetNodeValueAsString(requestXml, "m:vendorID");
            requestData.LastSequenceNumberRecieved = GetLastSequenceNumberRecieved(requestXml);

            return requestData;
        }

        private bool IsAuthorisation(XmlDocument requestXml)
        {
            var nodes = requestXml.GetElementsByTagName("wsse:Username");

            return nodes.Count > 0;
        }

        private string GetNodeValueAsString(XmlDocument requestXml, string nodeName)
        {
            var nodes = requestXml.GetElementsByTagName(nodeName);

            return nodes.Count == 0 ? string.Empty : nodes[0].InnerText;
        }

        private RequestType GetRequestType(XmlDocument requestXml)
        {
            RequestType requestType;
            var requestTypeAsString = GetNodeValueAsString(requestXml, "m:dataType");

            if (!Enum.TryParse(requestTypeAsString, out requestType))
                requestType = RequestType.Unknown;

            return requestType;
        }

        private int GetLastSequenceNumberRecieved(XmlDocument requestXml)
        {
            var lastSequenceNumberRecieved = 0;
            var gotAsString = GetNodeValueAsString(requestXml, "m:got");

            if (!int.TryParse(gotAsString, out lastSequenceNumberRecieved))
                lastSequenceNumberRecieved = 0;

            return lastSequenceNumberRecieved;
        }
    }
}