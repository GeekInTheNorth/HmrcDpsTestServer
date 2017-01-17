using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HmrcTpvsProxy.Domain.Messages.Nodes
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    public partial class Envelope
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns { get; set; }

        public EnvelopeBody Body { get; set; }

        public Envelope()
        {
            Body = new EnvelopeBody();

            xmlns = new XmlSerializerNamespaces();
            xmlns.Add("env", "http://schemas.xmlsoap.org/soap/envelope/");
            xmlns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xmlns.Add("soapenc", "http://schemas.xmlsoap.org/soap/encoding/");
            xmlns.Add("xsd", "http://www.w3.org/2001/XMLSchema");
        }
    }
}