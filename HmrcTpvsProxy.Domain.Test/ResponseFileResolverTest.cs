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
        public void WhenRequestingAP6_ReturnsXmlFileWithADataTypeOfP6()
        {
            var resolver = new ResponseFileResolver();

            var response = resolver.GetResponse(ResponseType.P6);
            var datatypes = response.GetElementsByTagName("DataType");
            var dataType = "WRONG";

            if (datatypes.Count > 0)
                dataType = datatypes.Item(0).InnerText;

            Assert.AreEqual("P6", dataType);
        }

        [Test]
        public void WhenRequestingAP9_ReturnsXmlFileWithADataTypeOfP9()
        {
            var resolver = new ResponseFileResolver();

            var response = resolver.GetResponse(ResponseType.P9);
            var datatypes = response.GetElementsByTagName("DataType");
            var dataType = "WRONG";

            if (datatypes.Count > 0)
                dataType = datatypes.Item(0).InnerText;

            Assert.AreEqual("P9", dataType);
        }

        [Test]
        public void WhenRequestingAnSL1_ReturnsXmlFileWithADataTypeOfSL1()
        {
            var resolver = new ResponseFileResolver();

            var response = resolver.GetResponse(ResponseType.SL1);
            var datatypes = response.GetElementsByTagName("DataType");
            var dataType = "WRONG";

            if (datatypes.Count > 0)
                dataType = datatypes.Item(0).InnerText;

            Assert.AreEqual("SL1", dataType);
        }

        [Test]
        public void WhenRequestingAnSL2_ReturnsXmlFileWithADataTypeOfSL2()
        {
            var resolver = new ResponseFileResolver();

            var response = resolver.GetResponse(ResponseType.SL2);
            var datatypes = response.GetElementsByTagName("DataType");
            var dataType = "WRONG";

            if (datatypes.Count > 0)
                dataType = datatypes.Item(0).InnerText;

            Assert.AreEqual("SL2", dataType);
        }
    }
}
