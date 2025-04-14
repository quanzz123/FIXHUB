using FIXHUB.Models;
using Microsoft.AspNetCore.Mvc;

namespace FixHub2.Components
{
    [ViewComponent(Name = "ServiceView")]

    public class ServiceViewComponent : ViewComponent
    {
        private readonly FixHubDbContext _context;

        public ServiceViewComponent(FixHubDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //truy vấn những dịch vụ được phép hiển thị
            var item = (from s in _context.Services
                        where s.IsActive == true
                        select s).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", item));
        }

    }
}