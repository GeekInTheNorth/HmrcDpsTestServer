using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace TestProxy.Controllers
{
    public class ClearCacheController : HmrcProxyControllerBase
    {
        [AcceptVerbs("POST","GET")]
        public HttpResponseMessage ClearCache()
        {
            IdentityCache.Identities.Clear();

            return new HttpResponseMessage
            {
                Content = new StringContent("The Identity Cache for Altered Messages has now been cleared.", Encoding.UTF8)
            };
        }
    }
}
