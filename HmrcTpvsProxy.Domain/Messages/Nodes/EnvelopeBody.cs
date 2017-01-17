using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class EnvelopeBody
    {
        [XmlElement(Namespace = "https://tpvs.hmrc.gov.uk/dps")]
        public DPSretrieveResponse DPSretrieveResponse { get; set; }

        public EnvelopeBody()
        {
            DPSretrieveResponse = new DPSretrieveResponse();
        }
    }
}