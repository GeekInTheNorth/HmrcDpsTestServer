using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable()]
    [DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.govtalk.gov.uk/taxation/CodingNoticesP6P6B/2")]
    public class TaxCode
    {
        [XmlAttribute()]
        public string Week1Month1Indicator { get; set; }

        [XmlAttribute()]
        public string TaxRegime { get; set; }

        [XmlText()]
        public string Value { get; set; }
    }
}