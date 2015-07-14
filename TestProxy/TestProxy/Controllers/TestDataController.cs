using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;
using HmrcTpvsProxy.Domain;

namespace TestProxy.Controllers
{
    public class TestDataController : ApiController
    {
        [AcceptVerbs("POST")]
        public HttpResponseMessage GetData(HttpRequestMessage request)
        {
            var content = request.Content;
            var xmlContent = content.ReadAsStringAsync().Result;

            var requestTypeResolver = new RequestTypeResolver();
            var responseFileRetriever = new ResponseFileRetriever();
            var service = new CascadeEdgeCaseService(requestTypeResolver, responseFileRetriever);

            var message = service.GetResponseFor(xmlContent);

            return new HttpResponseMessage
            {
                Content = new StringContent(message, Encoding.UTF8, "application/xml")
            };
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage Test()
        {
            var requestTypeResolver = new RequestTypeFaker();
            var responseFileRetriever = new ResponseFileRetriever();
            var service = new CascadeEdgeCaseService(requestTypeResolver, responseFileRetriever);

            var message = service.GetResponseFor(string.Empty);

            return new HttpResponseMessage
            {
                Content = new StringContent(message, Encoding.UTF8, "application/xml")
            };
        }

        private class RequestTypeFaker : IRequestTypeResolver
        {
            public RequestType GetRequestType(XmlDocument requestXml)
            {
                return RequestType.P6;
            }
        }
    }
}
