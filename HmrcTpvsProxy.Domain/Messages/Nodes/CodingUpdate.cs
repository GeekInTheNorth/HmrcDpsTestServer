using System;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable]
    public class CodingUpdate
    {
        public TaxCode TaxCode { get; set; }

        public decimal TotalPreviousPay { get; set; }

        public decimal TotalPreviousTax { get; set; }

        public CodingUpdate()
        {
            TaxCode = new TaxCode();
        }

        public bool ShouldSerializeTotalPreviousPay()
        {
            return TotalPreviousPay != 0;
        }

        public bool ShouldSerializeTotalPreviousTax()
        {
            return TotalPreviousTax != 0;
        }
    }
}