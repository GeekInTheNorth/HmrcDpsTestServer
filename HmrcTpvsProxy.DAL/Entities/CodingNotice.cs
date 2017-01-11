using System;

namespace HmrcTpvsProxy.DAL.Entities
{
    public class CodingNotice
    {
        public int ID { get; set; }

        public int DatasetID { get; set; }

        public string MessageType { get; set; }

        public int SequenceNo { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string NationalInsuranceNo { get; set; }

        public string WorksNumber { get; set; }
        
        public int TaxYear { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string TaxCode { get; set; }

        public string TaxRegime { get; set; }

        public string TaxBasisNonCumulative { get; set; }

        public decimal GrossTaxableInPreviousEmployment { get; set; }

        public decimal TaxPaidInPreviousEmployment { get; set; }

        public virtual Dataset Dataset { get; set; }
    }
}