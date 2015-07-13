using System;
using System.Xml;

namespace HmrcTpvsProxy.Domain
{
    public class ResponseFileResolver : IResponseFileResolver
    {
        public XmlDocument GetResponse(ResponseType responseType)
        {
            var response = new XmlDocument();

            switch (responseType)
            {
                case ResponseType.Authorisation:
                    response.LoadXml(CascadeEdgeCaseFiles.Authorisation);
                    break;
                case ResponseType.P6:
                    response.LoadXml(CascadeEdgeCaseFiles.P6Data);
                    break;
                case ResponseType.P9:
                    response.LoadXml(CascadeEdgeCaseFiles.P9Data);
                    break;
                case ResponseType.SL1:
                    response.LoadXml(CascadeEdgeCaseFiles.SL1Data);
                    break;
                case ResponseType.SL2:
                    response.LoadXml(CascadeEdgeCaseFiles.SL2Data);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("responseType");
            }

            return response;
        }
    }
}