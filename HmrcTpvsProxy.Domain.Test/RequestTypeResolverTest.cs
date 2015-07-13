using System.Xml;
using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test
{
    [TestFixture]
    public class RequestTypeResolverTest
    {
        [Test]
        public void WhenRequestingAuthorisation_CorrectlyReturnsTypeOfAuthorisation()
        {
            var request = TestRequests.Authorisation;
            var requestXml = new XmlDocument();
            requestXml.LoadXml(request);

            var requestTypeResolver = new RequestTypeResolver();
            var requestType = requestTypeResolver.GetRequestType(requestXml);

            Assert.AreEqual(RequestType.Authorisation, requestType);
        }

        [Test]
        public void WhenRequestingP6s_CorrectlyReturnsTypeOfP6()
        {
            var request = TestRequests.P6Request;
            var requestXml = new XmlDocument();
            requestXml.LoadXml(request);

            var requestTypeResolver = new RequestTypeResolver();
            var requestType = requestTypeResolver.GetRequestType(requestXml);

            Assert.AreEqual(RequestType.P6, requestType);
        }

        [Test]
        public void WhenRequestingP9s_CorrectlyReturnsTypeOfP9()
        {
            var request = TestRequests.P9Request;
            var requestXml = new XmlDocument();
            requestXml.LoadXml(request);

            var requestTypeResolver = new RequestTypeResolver();
            var requestType = requestTypeResolver.GetRequestType(requestXml);

            Assert.AreEqual(RequestType.P9, requestType);
        }

        [Test]
        public void WhenRequestingSL1s_CorrectlyReturnsTypeOfSL1()
        {
            var request = TestRequests.SL1Request;
            var requestXml = new XmlDocument();
            requestXml.LoadXml(request);

            var requestTypeResolver = new RequestTypeResolver();
            var requestType = requestTypeResolver.GetRequestType(requestXml);

            Assert.AreEqual(RequestType.SL1, requestType);
        }

        [Test]
        public void WhenRequestingSL2s_CorrectlyReturnsTypeOfSL2()
        {
            var request = TestRequests.SL2Request;
            var requestXml = new XmlDocument();
            requestXml.LoadXml(request);

            var requestTypeResolver = new RequestTypeResolver();
            var requestType = requestTypeResolver.GetRequestType(requestXml);

            Assert.AreEqual(RequestType.SL2, requestType);
        }

        [Test]
        public void WhenRequestingARs_CorrectlyReturnsTypeOfAR()
        {
            var request = TestRequests.ARRequest;
            var requestXml = new XmlDocument();
            requestXml.LoadXml(request);

            var requestTypeResolver = new RequestTypeResolver();
            var requestType = requestTypeResolver.GetRequestType(requestXml);

            Assert.AreEqual(RequestType.AR, requestType);
        }

        [Test]
        public void WhenRequestingNOTs_CorrectlyReturnsTypeOfNOT()
        {
            var request = TestRequests.NOTRequest;
            var requestXml = new XmlDocument();
            requestXml.LoadXml(request);

            var requestTypeResolver = new RequestTypeResolver();
            var requestType = requestTypeResolver.GetRequestType(requestXml);

            Assert.AreEqual(RequestType.NOT, requestType);
        }
    }
}