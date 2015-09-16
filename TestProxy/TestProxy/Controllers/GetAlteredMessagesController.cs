using System;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace TestProxy.Controllers
{
    public class GetAlteredMessagesController : HmrcProxyControllerBase
    {
        [AcceptVerbs("POST")]
        public HttpResponseMessage GetData(HttpRequestMessage request)
        {
            if (LastAccessed < DateTime.Now.AddMinutes(-15))
            {
                IdentityCache.Identities.Clear();
            }

            LastAccessed = DateTime.Now;

            var service = GetService();

            return service.GetMessageResponseFor(request, IdentityCache);
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage GetTestMessage()
        {
            return new HttpResponseMessage
            {
                Content = new StringContent("You have successfully reached the Get Messages Proxy which overrides the identities in the test data provided by the HMRC using a GET method.  Messages will be returned using a POST method.", Encoding.UTF8)
            };
        }
    }
}