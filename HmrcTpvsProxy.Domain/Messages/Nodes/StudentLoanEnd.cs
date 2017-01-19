using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.govtalk.gov.uk/taxation/StudentLoanEnd/2")]
    [XmlRoot(Namespace = "http://www.govtalk.gov.uk/taxation/StudentLoanEnd/2", IsNullable = false)]
    public class StudentLoanEnd
    {
        [XmlAttribute(DataType = "date")]
        public DateTime IssueDate { get; set; }

        [XmlAttribute]
        public int SequenceNumber { get; set; }

        [XmlAttribute]
        public int TaxYearEnd { get; set; }

        public string EmployerRef { get; set; }

        public string NINO { get; set; }

        public Name Name { get; set; }

        public string WorksNumber { get; set; }

        [XmlElement(ElementName = "StopDate", DataType = "date")]
        public DateTime EffectiveDate { get; set; }
    }
}