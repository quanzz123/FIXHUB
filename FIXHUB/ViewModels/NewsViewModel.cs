namespace FIXHUB.ViewModels
{
    public class NewsViewModel
    {
        public int NewsId { get; set; }

        public string Title { get; set; } = null!;

        public string? Summary { get; set; }

        public string? Content { get; set; }

        public string? ImageUrl { get; set; }

        public IFormFile? ImageFile { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsPublished { get; set; }

        public int? ViewCount { get; set; }
    }
}
