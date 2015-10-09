using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;
using HmrcTpvsProxy.Domain;
using WebApp.Filters;

namespace TestProxy.Controllers
{
    public class TestDataController : ApiController
    {
        [ForceHttps]
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
        public HttpResponseMessage GetTestMessage()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("You have successfully reached the Test Data service using a GET method.  Messages will only be returned using a POST method over HTTPS.");
            stringBuilder.AppendLine(string.Empty);
            stringBuilder.AppendLine("For RTI NVR Messages in Payroll (Windows), run this script first against your database:");
            stringBuilder.AppendLine("UPDATE PYBASIC SET NINumber = 'AA' + DBO.PAD(EmployeeNumber, '0', 6) + 'A'");
            stringBuilder.AppendLine(string.Empty);
            stringBuilder.AppendLine("For RTI NVR Messages in Payroll (Web), run this script first against your database:");
            stringBuilder.AppendLine("UPDATE Employee SET NationalInsuranceNo = 'AA' + DBO.PAD(DisplayEmployeeID,'0',6)+'A' WHERE ISNUMERIC(DisplayEmployeeID) = 1");

            return new HttpResponseMessage
            {
                Content = new StringContent(stringBuilder.ToString(), Encoding.UTF8)
            };
        }

        private class RequestTypeFaker : IRequestTypeResolver
        {
            public RequestType GetRequestType(XmlDocument requestXml)
            {
                return RequestType.P6;
            }

            public RequestType GetRequestTypeForResponse(XmlDocument responseXml)
            {
                return RequestType.P6;
            }
        }
    }
}
