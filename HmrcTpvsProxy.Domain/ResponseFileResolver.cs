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
                case ResponseType.NOT:
                    response.LoadXml(CascadeEdgeCaseFiles.NOTData);
                    break;
                case ResponseType.AR:
                    response.LoadXml(CascadeEdgeCaseFiles.ARData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("responseType");
            }

            return response;
        }
    }
}