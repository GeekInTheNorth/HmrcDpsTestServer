using HmrcTpvsProxy.Domain.Models;
using System.Net.Http;

namespace HmrcTpvsProxy.Domain
{
    public interface IProxyService
    {
        HttpResponseMessage GetAuthorisationResponseFor(HttpRequestMessage request);

        HttpResponseMessage GetMessageResponseFor(HttpRequestMessage request);

        HttpResponseMessage GetMessageResponseFor(HttpRequestMessage request, IdentityCache identityCache);
    }
}