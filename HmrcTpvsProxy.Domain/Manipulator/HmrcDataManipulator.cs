using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using HmrcTpvsProxy.Domain.Manipulator.Data;

namespace HmrcTpvsProxy.Domain.Manipulator
{
    public class HmrcDataManipulator : IHmrcDataManipulator
    {
        private readonly IEmployeeIdentityRepository repository;
        private readonly IRequestTypeResolver requestTypeResolver;

        public HmrcDataManipulator(IEmployeeIdentityRepository repository, IRequestTypeResolver requestTypeResolver)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (requestTypeResolver == null) throw new ArgumentNullException(nameof(requestTypeResolver));

            this.repository = repository;
            this.requestTypeResolver = requestTypeResolver;
        }

        public string ApplyEmployeeIdentities(string response)
        {
            var xmlResponse = new XmlDocument();
            xmlResponse.LoadXml(response);

            var validRequestTypes = new List<RequestType> {RequestType.P6, RequestType.P9, RequestType.SL1, RequestType.SL2};
            var requestType = requestTypeResolver.GetRequestTypeForResponse(xmlResponse);

            if (!validRequestTypes.Contains(requestType))
                return response;

            UpdateP6P9Identities(xmlResponse);

            return GetXmlDocumentAsString(xmlResponse);
        }

        private void UpdateP6P9Identities(XmlDocument xmlResponse)
        {
            var identities = repository.Get().ToList();
            var nodes = xmlResponse.GetElementsByTagName("CodingNoticeP9");

            for (var loop = 0; loop < nodes.Count; loop++)
            {
                if (loop >= identities.Count)
                    break;

                var node = nodes.Item(loop);
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name == "NINO") childNode.InnerText = identities[loop].NationalInsuranceNo;
                    if (childNode.Name == "WorksNumber") childNode.InnerText = identities[loop].EmployeePayId.ToString();
                }
            }
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
