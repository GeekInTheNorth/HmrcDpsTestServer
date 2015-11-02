using System;

namespace HmrcTpvsProxy.Domain.Models
{
    public class StudentLoanNotice
    {
        public string Name { get; set; }

        public string NationalInsuranceNo { get; set; }

        public string WorksNumber { get; set; }

        public string MessageType { get; set; }

        public string PlanType { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime IssueDate { get; set; }

        public int SequenceNumber { get; set; }
    }
}
