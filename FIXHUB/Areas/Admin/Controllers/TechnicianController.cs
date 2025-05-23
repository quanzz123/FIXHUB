using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIXHUB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TechnicianController : Controller
    {
        private readonly FixHubDbContext _context;
        public TechnicianController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
