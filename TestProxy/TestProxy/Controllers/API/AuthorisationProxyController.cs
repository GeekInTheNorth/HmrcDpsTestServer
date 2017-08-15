using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebApp.Filters;

namespace TestProxy.Controllers.API
{
    public class AuthorisationProxyController : HmrcProxyControllerBase
    {
        [ForceHttps]
        [AcceptVerbs("POST")]
        public HttpResponseMessage GetData(HttpRequestMessage request)
        {
            var service = GetService();

            return service.GetAuthorisationResponseFor(request);
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage GetTestMessage()
        {
            return new HttpResponseMessage
            {
                Content = new StringContent("You have successfully reached the Authorisation Proxy using a GET method.  Messages will only be returned using a POST method over HTTPS.", Encoding.UTF8)
            };
        }  
    }
}