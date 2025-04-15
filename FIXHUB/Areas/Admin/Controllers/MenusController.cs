using Microsoft.AspNetCore.Mvc;

namespace FIXHUB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
