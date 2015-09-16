using System.Web.Http;
using HmrcTpvsProxy.Domain;
using HmrcTpvsProxy.Domain.ConfigurationData;
using HmrcTpvsProxy.Domain.Manipulator;
using HmrcTpvsProxy.Domain.Manipulator.Data;
using System;
using System.Web;
using HmrcTpvsProxy.Domain.Models;

namespace TestProxy.Controllers
{
    public class HmrcProxyControllerBase : ApiController
    {
        protected DateTime LastAccessed
        {
            get
            {
                if (HttpContext.Current.Cache["LastAccessed"] == null)
                    HttpContext.Current.Cache["LastAccessed"] = DateTime.Now;

                return (DateTime)HttpContext.Current.Cache["LastAccessed"];
            }

            set
            {
                HttpContext.Current.Cache["LastAccessed"] = DateTime.Now;
            }
        }

        protected IdentityCache IdentityCache
        {
            get
            {
                if (HttpContext.Current.Cache["IdentityCache"] == null)
                    HttpContext.Current.Cache["IdentityCache"] = new IdentityCache();

                return (IdentityCache)HttpContext.Current.Cache["IdentityCache"];
            }
        }

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
