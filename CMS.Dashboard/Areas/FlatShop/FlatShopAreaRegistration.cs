using System.Web.Mvc;

namespace CMS.Dashboard.Areas.FlatShop
{
    public class FlatShopAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FlatShop";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Root",
                "",
                new { controller = "Home", action = "Index" },
                new[] { "CMS.Dashboard.Areas.FlatShop.Controllers" }
            );

            context.MapRoute(
                "FlatShop_default",
                "FlatShop/{controller}/{action}/{id}",
                new { conntroller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "CMS.Dashboard.Areas.FlatShop.Controllers" }
            );
        }
    }
}