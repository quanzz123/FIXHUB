using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FIXHUB.ViewComponents
{
    public class MenuTopViewComponent :ViewComponent
    {
        private readonly FixHubDbContext _context;

        public MenuTopViewComponent(FixHubDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View());
        }
    }
}
