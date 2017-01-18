using System;
using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable]
    public class TaxCode
    {
        [XmlIgnore]
        public bool Week1Month1 { get; set; }

        [XmlAttribute()]
        public string Week1Month1Indicator
        {
            get { return Week1Month1 ? "X" : null; }
            set { Week1Month1 = value == "X"; }
        }

        [XmlIgnore]
        public bool IsScottishEmployee { get; set; }

        [XmlAttribute()]
        public string TaxRegime
        {
            get { return IsScottishEmployee ? "S" : null; }
            set { IsScottishEmployee = value == "S"; }
        }

        [XmlText()]
        public string Value { get; set; }

        public bool ShouldSerializeWeek1Month1Indicator()
        {
            return Week1Month1;
        }

        public bool ShouldSerializeTaxRegime()
        {
            return IsScottishEmployee;           
        }
    }
}