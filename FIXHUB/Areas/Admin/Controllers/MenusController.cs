using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FIXHUB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenusController : Controller
    {
        private readonly FixHubDbContext _context;
        public MenusController(FixHubDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var menus = _context.Menus
                .Select(m => new Menu
                {
                    MenuId = m.MenuId,
                    Title = m.Title,
                    Alias = m.Alias,
                    Url = m.Url,
                    ParentId = m.ParentId,
                    Levels = m.Levels,
                    Positions = m.Positions,
                    Icon = m.Icon,
                    CreateAt = m.CreateAt,
                    IsActive = m.IsActive
                }).ToList(); 
            return View(menus);
        }
    }
}
