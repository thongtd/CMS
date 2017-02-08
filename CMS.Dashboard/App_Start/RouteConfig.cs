using System.Web.Mvc;
using System.Web.Routing;

namespace CMS.Dashboard
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Product", action = "Index", id = UrlParameter.Optional },
                new[] { "CMS.Dashboard" }
            );
        }
    }
}
