using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace TestProxy.Controllers.MVC
{
    public class BaseController : Controller
    {
        private List<string> allowedIpAddresses;

        protected List<string> AllowedIpAddresses
        {
            get
            {
                if (allowedIpAddresses == null || !allowedIpAddresses.Any())
                    allowedIpAddresses = GetIpAddresses();

                return allowedIpAddresses;
            }
        }

        protected bool CanEdit
        {
            get
            {
                var ipAddress = Request.UserHostAddress.Trim();

                return Request.IsLocal || AllowedIpAddresses.Contains(ipAddress, StringComparer.OrdinalIgnoreCase);
            }
        }

        private List<string> GetIpAddresses()
        {
            var appSetting = ConfigurationManager.AppSettings["AllowedIPAddresses"];
            appSetting = string.IsNullOrWhiteSpace(appSetting) ? string.Empty : appSetting;

            return appSetting.Split(',')
                             .Where(x => !string.IsNullOrWhiteSpace(x))
                             .ToList();
        }
    }
}