using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIXHUB.Controllers
{
    public class NewsController : Controller
    {
        private readonly FixHubDbContext _context;
        public NewsController(FixHubDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var news = _context.News.ToList();
            return View(news);
        }

        public IActionResult Details(int id)
        {
            var newsItem = _context.News.FirstOrDefault(n => n.NewsId == id);
            if (newsItem == null)
            {
                return NotFound();
            }
            return View(newsItem);
        }
    }
}
