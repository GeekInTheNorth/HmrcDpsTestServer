using System.Net.Http;
using System.Text;
using System.Web.Http;
using HmrcTpvsProxy.DAL.Repositories;
using HmrcTpvsProxy.Domain.Messages;
using HmrcTpvsProxy.Domain.Messages.Serialization;

namespace TestProxy.Controllers.API
{
    public class LegacyPayrollDatasetController : ApiController
    {
        private readonly IMessagesService service;

        public LegacyPayrollDatasetController()
        {
            var repository = new MessagesRepository();
            var requestDataResolver = new RequestDataResolver();
            var responseBuilder = new ResponseBuilder();
            var serialier = new Serializer();
            service = new MessagesService(repository, requestDataResolver, responseBuilder, serialier);
        }

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
