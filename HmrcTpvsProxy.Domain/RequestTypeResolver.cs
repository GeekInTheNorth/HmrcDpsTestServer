using System;
using System.Xml;

namespace HmrcTpvsProxy.Domain
{
    public class RequestTypeResolver : IRequestTypeResolver
    {
        public RequestType GetRequestType(XmlDocument requestXml)
        {
            var requestType = RequestType.Unknown;

            if (IsAuthorisation(requestXml))
                requestType = RequestType.Authorisation;
            else
            {
                var nodes = requestXml.GetElementsByTagName("m:dataType");

                if (nodes.Count == 0)
                    return RequestType.Unknown;

                var dataTypeString = nodes.Item(0).InnerText;

                Enum.TryParse(dataTypeString, true, out requestType);    
            }

            return requestType;
        }

        private bool IsAuthorisation(XmlDocument requestXml)
        {
            var nodes = requestXml.GetElementsByTagName("wsse:Username");

            return nodes.Count > 0;
        }
    }
}
