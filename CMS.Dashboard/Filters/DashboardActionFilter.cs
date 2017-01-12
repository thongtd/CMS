using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CMS.Dashboard.Code.Models;

namespace CMS.Dashboard.Filters
{
    public class DashboardActionFilter: ActionFilterAttribute
    {
        public string IndexPageTile { get; set; }
        public string EditPageTile { get; set; }
        public string CreatePageTile { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var breadcurmbs = new List<Breadcurmb>();

            var requestContext = HttpContext.Current.Request.RequestContext;
            
            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = "/",
                Lable = "Home"
            });

            breadcurmbs.Add(new Breadcurmb
            {
                ActionLink = "#",
                Lable = "Dashboard"
            });

            var action = filterContext.RouteData.Values["action"].ToString();
            var controller = filterContext.RouteData.Values["controller"].ToString();

            if (action.Equals("Index"))
            {
                breadcurmbs.Add(new Breadcurmb
                {
                    ActionLink = new UrlHelper(requestContext).Action("Index", controller, null),
                    Lable = IndexPageTile
                });

                filterContext.Controller.ViewBag.Title = IndexPageTile;
            }
            else if (action.Equals("Edit"))
            {
                breadcurmbs.Add(new Breadcurmb
                {
                    ActionLink = new UrlHelper(requestContext).Action("Index", controller, null),
                    Lable = IndexPageTile
                });

                breadcurmbs.Add(new Breadcurmb
                {
                    ActionLink = "",
                    Lable = EditPageTile
                });

                filterContext.Controller.ViewBag.Title = EditPageTile;
            }
            else if (action.Equals("Create"))
            {
                breadcurmbs.Add(new Breadcurmb
                {
                    ActionLink = new UrlHelper(requestContext).Action("Index", controller, null),
                    Lable = IndexPageTile
                });

                breadcurmbs.Add(new Breadcurmb
                {
                    ActionLink = "",
                    Lable = CreatePageTile
                });

                filterContext.Controller.ViewBag.Title = CreatePageTile;
            }

            filterContext.Controller.ViewBag.Breadcurmbs = breadcurmbs;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}