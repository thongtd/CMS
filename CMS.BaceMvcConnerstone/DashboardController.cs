using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace CMS.BaceMvcConnerstone
{
    [System.Web.Mvc.Route("dashboard")]
    public class DashboardController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(new object());
        }
    }
}
