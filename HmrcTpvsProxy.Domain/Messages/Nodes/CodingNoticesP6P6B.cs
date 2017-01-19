using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.govtalk.gov.uk/taxation/CodingNoticesP6P6B/2")]
    [XmlRoot(Namespace = "http://www.govtalk.gov.uk/taxation/CodingNoticesP6P6B/2", IsNullable = false)]
    public class CodingNoticesP6P6B : INotice
    {
        public string EmployerRef { get; set; }

        public Name Name { get; set; }

        public string NINO { get; set; }

        public string WorksNumber { get; set; }

        [XmlElement(DataType = "date")]
        public DateTime EffectiveDate { get; set; }

        public CodingUpdate CodingUpdate { get; set; }

        [XmlAttribute()]
        public string FormType { get; set; }

        [XmlAttribute(DataType = "date")]
        public DateTime IssueDate { get; set; }

        [XmlAttribute()]
        public int SequenceNumber { get; set; }

        [XmlAttribute()]
        public int TaxYearEnd { get; set; }

        public CodingNoticesP6P6B()
        {
            Name = new Name();
            CodingUpdate = new CodingUpdate();
        }
    }
}