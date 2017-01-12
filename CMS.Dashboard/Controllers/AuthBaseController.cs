using System.Web.Mvc;

namespace CMS.Dashboard.Controllers
{
    public class AuthBaseController : ActionFilterAttribute, IActionFilter
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
