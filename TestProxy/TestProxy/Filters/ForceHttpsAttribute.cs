using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;

namespace WebApp.Filters
{
    public class ForceHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            var isInTest = string.Equals(request.RequestUri.Host, "localhost", StringComparison.CurrentCultureIgnoreCase);

            if (!isInTest && request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                var html = "<p>Requests to this service must be made over HTTPS.</p>";

                if (request.Method.Method == "GET")
                {
                    actionContext.Response = request.CreateResponse(HttpStatusCode.Found);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");
                }
                else
                {
                    actionContext.Response = request.CreateResponse(HttpStatusCode.NotFound);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");
                }

            }
        }
    }
}