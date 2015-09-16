using HmrcTpvsProxy.Domain.Manipulator.Data;
using HmrcTpvsProxy.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

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

        public string ApplyEmployeeIdentities(string response, IdentityCache identityCache)
        {
            var xmlResponse = new XmlDocument();
            xmlResponse.LoadXml(response);

            var validRequestTypes = new List<RequestType> {RequestType.P6, RequestType.P9, RequestType.SL1, RequestType.SL2};
            var requestType = requestTypeResolver.GetRequestTypeForResponse(xmlResponse);

            if (!validRequestTypes.Contains(requestType))
                return response;

            switch (requestType)
            {
                case RequestType.P6:
                    UpdateIdentities(xmlResponse, "CodingNoticesP6P6B", identityCache);
                    break;
                case RequestType.P9:
                    UpdateIdentities(xmlResponse, "CodingNoticeP9", identityCache);
                    break;
                case RequestType.SL1:
                    UpdateIdentities(xmlResponse, "StudentLoanStart", identityCache);
                    break;
                case RequestType.SL2:
                    UpdateIdentities(xmlResponse, "StudentLoanEnd", identityCache);
                    break;
            }

            return GetXmlDocumentAsString(xmlResponse);
        }

        private void UpdateIdentities(XmlDocument xmlResponse, string messageNodeName, IdentityCache identityCache)
        {
            var identities = repository.Get().ToList();
            var nodes = xmlResponse.GetElementsByTagName(messageNodeName);

            foreach (XmlNode messageNode in nodes)
            {
                var messageNino = GetNationalInsuranceNoFromMessage(messageNode);
                var cacheIdentity = identityCache.Identities.Where(x => x.Key == messageNino).Select(x => x.Value).FirstOrDefault();
                if (cacheIdentity == null)
                {
                    cacheIdentity = GetNextFreeIdentity(identities, identityCache);
                    if (cacheIdentity == null)
                        continue;

                    identityCache.Identities.Add(messageNino, cacheIdentity);
                }

                ApplyIdentityToMessage(messageNode, cacheIdentity);
            }
        }

        private static string GetNationalInsuranceNoFromMessage(XmlNode messageNode)
        {
            var messageNino = string.Empty;
            foreach (XmlNode childNode in messageNode.ChildNodes)
            {
                if (childNode.Name == "NINO")
                {
                    messageNino = childNode.InnerText.Trim();
                    break;
                }
            }

            return messageNino;
        }

        private static void ApplyIdentityToMessage(XmlNode messageNode, EmployeeIdentity cacheIdentity)
        {
            foreach (XmlNode childNode in messageNode.ChildNodes)
            {
                if (childNode.Name == "NINO") childNode.InnerText = cacheIdentity.NationalInsuranceNo;
                if (childNode.Name == "WorksNumber") childNode.InnerText = cacheIdentity.EmployeePayId.ToString("F0");
                if (childNode.Name == "Name")
                {
                    foreach (XmlNode nameNode in childNode.ChildNodes)
                    {
                        if (nameNode.Name == "Forename") nameNode.InnerText = cacheIdentity.Forename ?? string.Empty;
                        if (nameNode.Name == "Surname") nameNode.InnerText = cacheIdentity.Surname ?? string.Empty;
                        if (nameNode.Name == "Title") nameNode.InnerText = cacheIdentity.Title ?? string.Empty;
                    }
                }
            }
        }

        private EmployeeIdentity GetNextFreeIdentity(List<EmployeeIdentity> identities, IdentityCache identityCaches)
        {
            var ids = (from identity in identities
                       join cachedIdentities in identityCaches.Identities.Values on identity.EmployeePayId equals cachedIdentities.EmployeePayId into iug
                       from idUsage in iug.DefaultIfEmpty()
                       select new
                       {
                           Identity = identity,
                           Usages = iug.Count()
                       }).ToList();

            return ids.OrderBy(x => x.Usages).ThenBy(x => x.Identity.EmployeePayId).Select(x => x.Identity).FirstOrDefault();
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
