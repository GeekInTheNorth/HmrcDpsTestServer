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

        [XmlElement("CodingNoticesP6P6B", Namespace = "http://www.govtalk.gov.uk/taxation/CodingNoticesP6P6B/2")]
        public List<CodingNoticesP6P6B> CodingNoticesP6P6B { get; set; }

        [XmlElement("CodingNoticesP9", Namespace = "http://www.govtalk.gov.uk/taxation/CodingNoticeP9/2")]
        public List<CodingNoticesP9> CodingNoticesP9 { get; set; }

        [XmlElement("StudentLoanStart", Namespace = "http://www.govtalk.gov.uk/taxation/StudentLoanStart/2")]
        public List<StudentLoanStart> StudentLoanStart { get; set; }

        public DPSdata()
        {
            DPSheader = new DPSheader();
        }

        public bool ShouldSerializeCodingNoticesP6P6B()
        {
            return CodingNoticesP6P6B != null && CodingNoticesP6P6B.Any();
        }

        public bool ShouldSerializeCodingNoticesP9()
        {
            return CodingNoticesP9 != null && CodingNoticesP9.Any();
        }

        public bool ShouldSerializeStudentLoanStart()
        {
            return StudentLoanStart != null && StudentLoanStart.Any();
        }
    }
}