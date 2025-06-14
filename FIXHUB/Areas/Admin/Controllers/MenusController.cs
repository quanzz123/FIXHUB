using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [HttpGet]
        public IActionResult Create()
        {
            var parentMenus = _context.Menus
                .Where(m => m.ParentId == null)
                .Select(m => new SelectListItem
                {
                    Value = m.MenuId.ToString(),
                    Text = m.Title
                }).ToList();
            ViewBag.ParentMenus = parentMenus;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Menu model)
        {
            if (ModelState.IsValid)
            {
                model.CreateAt = DateTime.Now;
                _context.Menus.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var menu = _context.Menus.Find(id);
            if (menu == null)
            {
                return NotFound();
            }
            var parentMenus = _context.Menus
                .Where(m => m.MenuId != id && m.ParentId == null)
                .Select(m => new SelectListItem
                {
                    Value = m.MenuId.ToString(),
                    Text = m.Title
                }).ToList();
            ViewBag.ParentMenus = parentMenus;
            return View(menu);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Menu model)
        {
            if (ModelState.IsValid)
            {
                var existingMenu = _context.Menus.Find(model.MenuId);
                if (existingMenu == null)
                {
                    return NotFound();
                }
                existingMenu.Title = model.Title;
                existingMenu.Alias = model.Alias;
                existingMenu.Url = model.Url;
                existingMenu.ParentId = model.ParentId;
                existingMenu.Levels = model.Levels;
                existingMenu.Positions = model.Positions;
                existingMenu.Icon = model.Icon;
                existingMenu.IsActive = model.IsActive;
                _context.Menus.Update(existingMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var menu = _context.Menus.Find(id);
            if (menu == null)
            {
                return NotFound();
            }
            menu.IsActive = false; 
            _context.Menus.Update(menu);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
