using System.Xml;
using HmrcTpvsProxy.Domain.Messages;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test.Messages
{
    [TestFixture]
    public class RequestDataResolverTest
    {
        private IRequestDataResolver requestDataResolver;

        [SetUp]
        public void SetUp()
        {
            requestDataResolver = new RequestDataResolver();
        }

        [Test]
        public void GivenIDoNotHaveAnXmlDocument_WhenIAskForRequestData_ThenIReturnTheUnknownDataResult()
        {
            var requestData = requestDataResolver.Get(null);

            Assert.That(requestData.RequestType, Is.EqualTo(RequestType.Unknown));
        }

        [Test]
        public void GivenIHaveAnEmptyXmlDocument_WhenIAskForRequestData_ThenIReturnTheUnknownDataResult()
        {
            var requestData = requestDataResolver.Get(new XmlDocument());

            Assert.That(requestData.RequestType, Is.EqualTo(RequestType.Unknown));
        }

        [Test]
        public void GivenIHaveAnAuthorisationRequest_WhenIAskForRequestData_ThenIReturnAuthorisationData()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(TestRequests.Authorisation);

            var requestData = requestDataResolver.Get(xmlDocument);

            Assert.That(requestData.RequestType, Is.EqualTo(RequestType.Authorisation));
            Assert.That(requestData.VendorId, Is.EqualTo("0178"));
        }

        [Test]
        [TestCase("Vendor1", "123/A1", RequestType.P6, 123)]
        [TestCase("Vendor2", "123/A2", RequestType.P9, 66)]
        [TestCase("Vendor3", "123/A3", RequestType.SL1, 99)]
        [TestCase("Vendor4", "123/A4", RequestType.SL2, 987)]
        public void GivenIHaveARequest_WhenIAskForRequestData_ThenIReturnRequestData(string vendorId, string payeReference, RequestType requestType, int lastSequenceNumber)
        {
            var xmlDocument = new XmlDocument();
            var requestAsString = string.Format(TestRequests.RequestWithPlaceholders, vendorId, payeReference, requestType, lastSequenceNumber);
            xmlDocument.LoadXml(requestAsString);

            var requestData = requestDataResolver.Get(xmlDocument);

            Assert.That(requestData.RequestType, Is.EqualTo(requestType));
            Assert.That(requestData.VendorId, Is.EqualTo(vendorId));
            Assert.That(requestData.PayeReference, Is.EqualTo(payeReference));
            Assert.That(requestData.LastSequenceNumberRecieved, Is.EqualTo(lastSequenceNumber));
        }
    }
}