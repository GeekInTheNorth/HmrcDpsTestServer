using System.Net.Http;
using System.Text;
using System.Web.Http;
using HmrcTpvsProxy.Domain;

namespace TestProxy.Controllers
{
    public class GetMessagesProxyController : ApiController
    {
        [AcceptVerbs("POST")]
        public HttpResponseMessage GetData(HttpRequestMessage request)
        {
            var service = new ProxyService();

            return service.GetMessageResponseFor(request);
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage GetTestMessage()
        {
            return new HttpResponseMessage
            {
                Content = new StringContent("You have successfully reached the Get Messages Proxy using a GET method.  Messages will be returned using a POST method.", Encoding.UTF8)
            };
        }
    }
}
