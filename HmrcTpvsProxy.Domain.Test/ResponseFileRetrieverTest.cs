using NUnit.Framework;

namespace HmrcTpvsProxy.Domain.Test
{
    [TestFixture]
    public class ResponseFileRetrieverTest
    {
        [Test]
        public void WhenRequestingAuthorisation_ReturnsAuthorisationAcceptedResponse()
        {
            var retriever = new ResponseFileRetriever();

            var response = retriever.GetResponse(RequestType.Authorisation);
            var datatypes = response.GetElementsByTagName("ns:DPSrequestTokenResult");

            Assert.IsNotEmpty(datatypes);
        }

        [Test]
        [TestCase(RequestType.P6)]
        [TestCase(RequestType.P9)]
        [TestCase(RequestType.SL1)]
        [TestCase(RequestType.SL2)]
        [TestCase(RequestType.NOT)]
        [TestCase(RequestType.AR)]
        public void ReturnsAMessageWithTheCorrectDataTypeWhenRequestingA(RequestType requestType)
        {
            var retriever = new ResponseFileRetriever();

            var response = retriever.GetResponse(requestType);
            var datatypes = response.GetElementsByTagName("DataType");
            var dataType = "WRONG";

            if (datatypes.Count > 0)
                dataType = datatypes.Item(0).InnerText;

            Assert.AreEqual(requestType.ToString(), dataType);
        }
    }
}