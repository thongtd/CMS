using System.Web.Mvc;
using Autofac;

namespace CMS.Dashboard.Areas.FlatShop.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IComponentContext componentContext) : base(componentContext)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}