using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FixHub2.Components
{
    [ViewComponent(Name = "GuidesCategoryView")]
    public class GuidesCategoryViewComponent : ViewComponent
    {
        private readonly FixHubDbContext _context;

        public GuidesCategoryViewComponent(FixHubDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int levels)
        {
            var item = (from m in _context.GuideCategories
                        where (m.IsActive == true) && (m.Levels == levels)
                        select m).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", item));
        }
    }
}