using System;
using System.Xml;

namespace HmrcTpvsProxy.Domain
{
    public class ResponseFileRetriever : IResponseFileRetriever
    {
        public XmlDocument GetResponse(RequestType requestType)
        {
            var response = new XmlDocument();

            switch (requestType)
            {
                case RequestType.Authorisation:
                    response.LoadXml(CascadeEdgeCaseFiles.Authorisation);
                    break;
                case RequestType.P6:
                    response.LoadXml(CascadeEdgeCaseFiles.P6Data);
                    break;
                case RequestType.P9:
                    response.LoadXml(CascadeEdgeCaseFiles.P9Data);
                    break;
                case RequestType.SL1:
                    response.LoadXml(CascadeEdgeCaseFiles.SL1Data);
                    break;
                case RequestType.SL2:
                    response.LoadXml(CascadeEdgeCaseFiles.SL2Data);
                    break;
                case RequestType.NOT:
                    response.LoadXml(CascadeEdgeCaseFiles.NOTData);
                    break;
                case RequestType.AR:
                    response.LoadXml(CascadeEdgeCaseFiles.ARData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("requestType");
            }

            return response;
        }
    }
}