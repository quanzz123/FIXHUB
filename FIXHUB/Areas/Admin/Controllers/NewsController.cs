using FIXHUB.Models;
using FIXHUB.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIXHUB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NewsController : Controller
    {
        private readonly FixHubDbContext _context;
        public NewsController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var news = _context.News.Select(m => new News
            {
                NewsId = m.NewsId,
                Title = m.Title,
                ImageUrl = m.ImageUrl,
                Summary = m.Summary,
                IsPublished = m.IsPublished,
            }).ToList();


            return View(news);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imgpath = null;
                if (model.ImageFile != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    var extenstion = Path.GetExtension(model.ImageFile.FileName);
                    string newFileName = $"{fileName}_{DateTime.Now.Ticks}{extenstion}";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img/news", newFileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }
                    //news.img = "/img/HistoryStep" + newFileName;
                    imgpath = "/admin/img/news/" + newFileName;
                }
                var news = new News
                {
                    Title = model.Title,
                    Summary = model.Summary,
                    Content = model.Content,
                    ImageUrl = imgpath,
                    CreatedBy = int.Parse(User.FindFirst("UserId")?.Value ?? "0"),
                    CreatedAt = DateTime.Now,
                    IsPublished = model.IsPublished,
                };
                _context.News.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var news = _context.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }
            var model = new NewsViewModel
            {
                NewsId = news.NewsId,
                Title = news.Title,
                Summary = news.Summary,
                Content = news.Content,
                ImageUrl = news.ImageUrl,
                IsPublished = news.IsPublished ?? false,
            };
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var news = _context.News.Find(model.NewsId);
                if (news == null)
                {
                    return NotFound();
                }
                news.Title = model.Title;
                news.Summary = model.Summary;
                news.Content = model.Content;
                news.IsPublished = model.IsPublished;
                if (model.ImageFile != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    var extenstion = Path.GetExtension(model.ImageFile.FileName);
                    string newFileName = $"{fileName}_{DateTime.Now.Ticks}{extenstion}";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img/news", newFileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }
                    news.ImageUrl = "/admin/img/news/" + newFileName;
                }
                news.UpdatedAt = DateTime.Now;
                _context.News.Update(news);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
