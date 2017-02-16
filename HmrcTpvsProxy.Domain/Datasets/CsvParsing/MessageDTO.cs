using System;

namespace HmrcTpvsProxy.Domain.Datasets.CsvParsing
{
    public class MessageDTO
    {
        public string FormType { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string NINO { get; set; }

        public string PayId { get; set; }

        public int TaxYearEnd { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime EffectiveDate { get; set; }

        public int SequenceNumber { get; set; }

        public string TaxCode { get; set; }

        public string TaxRegime { get; set; }

        public string TaxBasisNonCumulative { get; set; }

        public decimal GrossTaxable { get; set; }

        public decimal TaxPaid { get; set; }

        public string PlanType { get; set; }
    }
}