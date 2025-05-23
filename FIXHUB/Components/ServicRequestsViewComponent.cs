using FIXHUB.Models;
using FIXHUB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeFixes;

namespace FIXHUB.Components
{
    [ViewComponent(Name = "ServicRequestsView")]
    public class ServicRequestsViewComponent : ViewComponent
    {
        private readonly FixHubDbContext _context;
        public ServicRequestsViewComponent(FixHubDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            /* var item = (from t in _context.Technicians
                         join u in _context.Users on t.UserId equals u.UserId
                         join p in _context.UserProfiles on u.UserId equals p.UserId
                         select new TechnicianVM
                         {
                             UserId = u.UserId,
                             UserName = u.UserName,
                             FullName = p.FullName,
                             Bio = p.Bio,
                             AvatarUrl = p.AvatarUrl,
                             Expertise = t.Expertise,
                             Rating = t.Rating
                         }).ToList();*/
            var items = (from t in _context.Technicians
                         join u in _context.Users on t.UserId equals u.UserId
                         join p in _context.UserProfiles on u.UserId equals p.UserId
                         group t by new { u.UserId, u.UserName, p.FullName, p.Bio, p.AvatarUrl } into g
                         select new TechnicianVM
                         {
                             UserId = g.Key.UserId,
                             UserName = g.Key.UserName,
                             FullName = g.Key.FullName,
                             Bio = g.Key.Bio,
                             AvatarUrl = g.Key.AvatarUrl,
                             Rating = g.Max(x => x.Rating), // hoặc tính trung bình
                             Expertise = g.Select(x => x.Expertise).Distinct().ToList()
                         }).ToList();


            return await Task.FromResult((IViewComponentResult)View("Default", items));
        }
    }
}
