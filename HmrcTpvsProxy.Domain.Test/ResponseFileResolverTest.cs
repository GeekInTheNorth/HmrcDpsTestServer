using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test
{
    [TestFixture]
    public class ResponseFileResolverTest
    {
        [Test]
        public void WhenRequestingAuthorisation_ReturnsAuthorisationAcceptedResponse()
        {
            var resolver = new ResponseFileResolver();

            var response = resolver.GetResponse(ResponseType.Authorisation);
            var datatypes = response.GetElementsByTagName("ns:DPSrequestTokenResult");

            Assert.IsNotEmpty(datatypes);
        }

        [Test]
        [TestCase(ResponseType.P6)]
        [TestCase(ResponseType.P9)]
        [TestCase(ResponseType.SL1)]
        [TestCase(ResponseType.SL2)]
        [TestCase(ResponseType.NOT)]
        [TestCase(ResponseType.AR)]
        public void ReturnsAMessageWithTheCorrectDataTypeWhenRequestingA(ResponseType responseType)
        {
            var resolver = new ResponseFileResolver();

            var response = resolver.GetResponse(responseType);
            var datatypes = response.GetElementsByTagName("DataType");
            var dataType = "WRONG";

            if (datatypes.Count > 0)
                dataType = datatypes.Item(0).InnerText;

            Assert.AreEqual(responseType.ToString(), dataType);
        }
    }
}
