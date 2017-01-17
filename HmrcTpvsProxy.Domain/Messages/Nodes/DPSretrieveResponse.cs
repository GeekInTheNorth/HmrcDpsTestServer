using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "https://tpvs.hmrc.gov.uk/dps")]
    [XmlRoot(Namespace = "https://tpvs.hmrc.gov.uk/dps", IsNullable = false)]
    public class DPSretrieveResponse
    {
        [XmlElement(Namespace = "http://www.govtalk.gov.uk/taxation/DPSwrapper/1")]
        public DPSdata DPSdata { get; set; }

        public DPSretrieveResponse()
        {
            DPSdata = new DPSdata();
        }
    }
}