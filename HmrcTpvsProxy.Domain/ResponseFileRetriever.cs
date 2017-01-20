using System;
using System.Xml;

namespace HmrcTpvsProxy.Domain
{
    public class ResponseFileRetriever : IResponseFileRetriever
    {
        public XmlDocument GetResponse(RequestType requestType)
        {
            var response = new XmlDocument();
            response.LoadXml(GetResponseAsString(requestType));

            return response;
        }

        public string GetResponseAsString(RequestType requestType)
        {
            switch (requestType)
            {
                case RequestType.Authorisation:
                    return CascadeEdgeCaseFiles.Authorisation;
                case RequestType.P6:
                    return CascadeEdgeCaseFiles.P6Data;
                case RequestType.P9:
                    return CascadeEdgeCaseFiles.P9Data;
                case RequestType.SL1:
                    return CascadeEdgeCaseFiles.SL1Data;
                case RequestType.SL2:
                    return CascadeEdgeCaseFiles.SL2Data;
                case RequestType.NOT:
                    return CascadeEdgeCaseFiles.NOTData;
                case RequestType.AR:
                    return CascadeEdgeCaseFiles.ARData;
                case RequestType.RTI:
                    return CascadeEdgeCaseFiles.NVRData;
                default:
                    throw new ArgumentOutOfRangeException("requestType");
            }
        }
    }
}