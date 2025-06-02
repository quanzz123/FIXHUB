using FIXHUB.Areas.Admin.ViewModel;
using FIXHUB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FIXHUB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GuidesController : Controller
    {
        private readonly FixHubDbContext _context;
        public GuidesController(FixHubDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var guiCate = _context.GuideCategories.Select(m => new GuideCategory
            {
                CategoryId = m.CategoryId,
                CategoryName = m.CategoryName,
                Description = m.Description,
                ImgUrl = m.ImgUrl,
                ParentId = m.ParentId,
                Levels = m.Levels,
                IsActive = m.IsActive,
            }).ToList();
            return View(guiCate);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var guideCategory = (from gu in _context.GuideCategories
                                 where gu.CategoryId == id
                                 select gu).FirstOrDefault();
            if (guideCategory == null)
            {
                return NotFound();
            }
            var parentCategories = _context.GuideCategories
                    .Where(c => c.CategoryId != id && c.ParentId == null)
                    .Select(c => new SelectListItem
                    {
                        Value = c.CategoryId.ToString(),
                        Text = c.CategoryName
                    })
                .ToList();
            var model = new GuideCategoryVM
            {
                CategoryId = guideCategory.CategoryId,
                CategoryName = guideCategory.CategoryName,
                Description = guideCategory.Description,
                ImgUrl = guideCategory.ImgUrl,
                ParentId = guideCategory.ParentId,
                Levels = guideCategory.Levels,
                IsActive = guideCategory.IsActive,
                ParentCategories = parentCategories,
            };
            return View(model);
        }
        public async Task<IActionResult> Edit(GuideCategoryVM model)
        {
            var guideCategory = await _context.GuideCategories.FindAsync(model.CategoryId);
            //var parentCategories = _context.GuideCategories
            //        .Where(c => c.CategoryId != model.CategoryId && c.ParentId == null)
            //        .Select(c => new SelectListItem
            //        {
            //            Value = c.CategoryId.ToString(),
            //            Text = c.CategoryName
            //        })
            //    .ToList();
            if (guideCategory == null) 
                return View(model);
            guideCategory.CategoryName = model.CategoryName;
            guideCategory.Description = model.Description;
            guideCategory.ParentId = model.ParentId;
            guideCategory.Levels = model.Levels;
            guideCategory.IsActive = model.IsActive;
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                var extenstion = Path.GetExtension(model.ImageFile.FileName);
                string newFileName = $"{fileName}_{DateTime.Now.Ticks}{extenstion}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Guide_Category", newFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
                guideCategory.ImgUrl = $"/img/Guide_Category/{newFileName}";
            }

            _context.GuideCategories.Update(guideCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Create()
        {
            var parentCategories = _context.GuideCategories
                .Where(c => c.ParentId == null)
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                })
                .ToList();
            var model = new GuideCategoryVM
            {
                ParentCategories = parentCategories,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(GuideCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var guideCategory = new GuideCategory
                {
                    CategoryName = model.CategoryName,
                    Description = model.Description,
                    ParentId = model.ParentId,
                    Levels = model.Levels,
                    IsActive = model.IsActive,
                };
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    var extenstion = Path.GetExtension(model.ImageFile.FileName);
                    string newFileName = $"{fileName}_{DateTime.Now.Ticks}{extenstion}";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Guide_Category", newFileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }
                    guideCategory.ImgUrl = $"/img/Guide_Category/{newFileName}";
                }
                _context.GuideCategories.Add(guideCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var guideCategory = _context.GuideCategories.Find(id);
            if (guideCategory == null)
            {
                return NotFound();
            }
            guideCategory.IsActive = false; // Thay đổi trạng thái thành không hoạt động
            _context.GuideCategories.Update(guideCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult RepairGuides()
        {
            var listofrepair = (from l in _context.RepairGuides
                                select l).ToList();
            return View(listofrepair);
        }

        
    }
}
