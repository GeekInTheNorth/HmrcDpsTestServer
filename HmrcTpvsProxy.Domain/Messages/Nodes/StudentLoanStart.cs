using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.govtalk.gov.uk/taxation/StudentLoanStart/2")]
    [XmlRoot(Namespace = "http://www.govtalk.gov.uk/taxation/StudentLoanStart/2", IsNullable = false)]
    public class StudentLoanStart
    {
        [XmlAttribute(DataType = "date")]
        public DateTime IssueDate { get; set; }

        [XmlAttribute()]
        public int SequenceNumber { get; set; }

        [XmlAttribute()]
        public int TaxYearEnd { get; set; }

        public string EmployerRef { get; set; }

        public string NINO { get; set; }

        public Name Name { get; set; }

        public string WorksNumber { get; set; }

        [XmlElement(DataType = "date")]
        public DateTime LoanStartDate { get; set; }

        public string PlanType { get; set; }

        public bool ShouldSerializePlanType()
        {
            return !string.IsNullOrWhiteSpace(PlanType);
        }
    }
}