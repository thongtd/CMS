using System.Web.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.Dashboard.Controllers
{
    public class AuthBaseController : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
