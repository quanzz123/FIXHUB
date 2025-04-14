using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIXHUB.Components
{
    [ViewComponent(Name = "MenuTopView")]
    public class MenuTopViewComponent : ViewComponent
    {
        private FixHubDbContext _context;

        public MenuTopViewComponent(FixHubDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var lis = (from m in _context.Menus
                       where (m.IsActive == true)
                       select m).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", lis));
        }
    }
}
