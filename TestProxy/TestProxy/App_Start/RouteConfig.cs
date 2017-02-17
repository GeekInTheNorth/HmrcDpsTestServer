using System.Web.Mvc;
using System.Web.Routing;

namespace TestProxy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DatasetRoute",
                url: "Dataset/{action}/{id}/{messageType}",
                defaults: new { controller = "Dataset", action = "View", id = UrlParameter.Optional, messageType = UrlParameter.Optional }
            );
        }
    }
}