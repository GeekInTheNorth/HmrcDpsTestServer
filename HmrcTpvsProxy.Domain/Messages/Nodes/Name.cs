using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.govtalk.gov.uk/taxation/CodingNoticesP6P6B/2")]
    public class Name
    {
        public string Forename { get; set; }

        public string Surname { get; set; }
    }
}