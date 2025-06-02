using Microsoft.AspNetCore.Mvc.Rendering;

namespace FIXHUB.Areas.Admin.ViewModel
{
    public class GuideCategoryVM
    {
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public string? Description { get; set; }

        public string? ImgUrl { get; set; }

        public int? ParentId { get; set; }

        public int? Levels { get; set; }

        public bool IsActive { get; set; }

        public IFormFile? ImageFile { get; set; }

        
        public List<SelectListItem>? ParentCategories { get; set; } // thêm mới

    }
}
