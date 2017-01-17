using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.govtalk.gov.uk/taxation/DPSwrapper/1")]
    [XmlRoot(Namespace = "http://www.govtalk.gov.uk/taxation/DPSwrapper/1", IsNullable = false)]
    public partial class DPSdata
    {
        public DPSheader DPSheader { get; set; }

        /// <remarks/>
        [XmlElement("CodingNoticesP6P6B", Namespace = "http://www.govtalk.gov.uk/taxation/CodingNoticesP6P6B/2")]
        public List<CodingNoticesP6P6B> CodingNoticesP6P6B { get; set; }

        public DPSdata()
        {
            DPSheader = new DPSheader();
        }

        public bool ShouldSerializeCodingNoticesP6P6B()
        {
            return CodingNoticesP6P6B != null && CodingNoticesP6P6B.Any();
        }
    }
}