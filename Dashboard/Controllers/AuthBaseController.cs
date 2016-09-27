using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Dashboard.Controllers
{
    public class AuthBaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var user = HttpContext.Session.TryGetValue("Admin.UserModel");
            //if (user == null)
            //    filterContext.Result = RedirectToAction("Login", "Auth", new { area = "Authen", returnUrl = Request.Url.LocalPath });
            //else
            //    base.OnActionExecuting(filterContext);
        }
    }
}
