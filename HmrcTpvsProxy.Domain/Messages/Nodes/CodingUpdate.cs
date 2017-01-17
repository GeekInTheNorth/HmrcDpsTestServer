using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.govtalk.gov.uk/taxation/CodingNoticesP6P6B/2")]
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