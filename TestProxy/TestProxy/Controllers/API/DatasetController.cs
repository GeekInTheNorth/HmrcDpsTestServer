using System.Net.Http;
using System.Text;
using System.Web.Http;
using HmrcTpvsProxy.DAL.Repositories;
using HmrcTpvsProxy.Domain.Messages;
using HmrcTpvsProxy.Domain.Messages.Serialization;
using WebApp.Filters;

namespace TestProxy.Controllers.API
{
    public class DatasetController : ApiController
    {
        private readonly IMessagesService service;

        public DatasetController()
        {
            var repository = new MessagesRepository();
            var requestDataResolver = new RequestDataResolver();
            var responseBuilder = new ResponseBuilder();
            var serialier = new Serializer();
            service = new MessagesService(repository, requestDataResolver, responseBuilder, serialier);
        }

        [ForceHttps]
        [AcceptVerbs("POST")]
        public HttpResponseMessage GetData([FromUri]int id, HttpRequestMessage request)
        {
            var content = request.Content;
            var xmlContent = content.ReadAsStringAsync().Result;

            var messages = service.GetResponse(id, xmlContent);

            return new HttpResponseMessage
            {
                Content = new StringContent(messages, Encoding.UTF8, "application/xml")
            };
        }
    }
}