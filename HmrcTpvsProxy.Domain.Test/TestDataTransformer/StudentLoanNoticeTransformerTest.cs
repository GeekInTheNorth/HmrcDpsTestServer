using System;
using System.Linq;
using System.Xml;
using HmrcTpvsProxy.Domain.TestDataTransformer;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test.TestDataTransformer
{
    [TestFixture]
    public class StudentLoanNoticeTransformerTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenGivenANullDocument_ThrowsAnArguementNullException()
        {
            var transformer = new StudentLoanNoticeTransformer();

            transformer.Transform(null);
        }

        [Test]
        public void WhenGivenAnInvalidMessage_ThenNoRecordsAreReturned()
        {
            var transformer = new StudentLoanNoticeTransformer();
            var fileToTransform = TestResponses.P6Response;
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileToTransform);
            
            var results = transformer.Transform(xmlDocument);

            Assert.IsFalse(results.Any());
        }

        [Test]
        public void WhenGivenAValidSL1Message_ThenDataIsTransformed()
        {
            var transformer = new StudentLoanNoticeTransformer();
            var fileToTransform = TestResponses.SL1Response;
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileToTransform);

            var results = transformer.Transform(xmlDocument);

            Assert.IsTrue(results.Any());
        }

        [Test]
        public void WhenGivenAValidSL2Message_ThenDataIsTransformed()
        {
            var transformer = new StudentLoanNoticeTransformer();
            var fileToTransform = TestResponses.SL2Response;
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileToTransform);

            var results = transformer.Transform(xmlDocument);

            Assert.IsTrue(results.Any());
        }
    }
}
