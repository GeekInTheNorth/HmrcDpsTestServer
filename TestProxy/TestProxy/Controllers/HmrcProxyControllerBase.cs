using System.Web.Http;
using HmrcTpvsProxy.Domain;
using HmrcTpvsProxy.Domain.ConfigurationData;
using HmrcTpvsProxy.Domain.Manipulator;
using HmrcTpvsProxy.Domain.Manipulator.Data;

namespace TestProxy.Controllers
{
    public class HmrcProxyControllerBase : ApiController
    {
        protected ProxyService GetService()
        {
            var configLocation = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Configuration.json");
            var configurationRepo = new ConfigurationRepository(configLocation);

            var employeeIdentityRespository = new EmployeeIdentityRepository();
            var requestTypeResolver = new RequestTypeResolver();
            var hmrcDataManipulator = new HmrcDataManipulator(employeeIdentityRespository, requestTypeResolver);
            var messageSender = new MessageSender();

            return new ProxyService(hmrcDataManipulator, configurationRepo, messageSender);
        }
    }
}
