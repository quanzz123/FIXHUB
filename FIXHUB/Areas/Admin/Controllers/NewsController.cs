using FIXHUB.Models;
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
        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Create( News news)
        {
            return View();
        }
    }
}
