using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Dashboard.Controllers
{
    public class BaseDashboardController : Controller
    {
        // GET: BaseDashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}