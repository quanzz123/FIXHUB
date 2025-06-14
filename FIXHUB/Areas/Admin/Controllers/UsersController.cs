using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIXHUB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        public FixHubDbContext _context;
        public UsersController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var users = _context.Users.ToList();  
            return View();
        }
    }
}
