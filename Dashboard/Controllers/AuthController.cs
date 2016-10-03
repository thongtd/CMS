using System.Security.Policy;
using Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.ProjectModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Dashboard.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
