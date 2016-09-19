using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("dashboard")]
    public class DashBoardController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View("E:/Dropbox/CMS/WebApplication1/Views/DashBoard/Index.cshtml");
        }
    }
}
