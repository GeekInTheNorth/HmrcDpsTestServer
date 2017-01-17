using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.govtalk.gov.uk/taxation/DPSwrapper/1")]
    public class DPSheader
    {
        public string Service { get; set; }

        public string EntityType { get; set; }

        public string Entity { get; set; }

        public string DataType { get; set; }

        public int Got { get; set; }

        public int NItems { get; set; }

        public string VendorID { get; set; }

        public bool MoreData { get; set; }

        public int HighWaterMark { get; set; }

        public int NItemsReturned { get; set; }

        public DateTime Timestamp { get; set; }

        public DPSheader()
        {
            Service = "PAYE";
            EntityType = "EmpRef";
            Entity = "123/A6";
            Timestamp = DateTime.Now;
        }
    }
}