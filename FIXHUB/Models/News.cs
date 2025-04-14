using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class News
{
    public int NewsId { get; set; }

    public string Title { get; set; } = null!;

    public string? Summary { get; set; }

    public string? Content { get; set; }

    public string? ImageUrl { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsPublished { get; set; }

    public int? ViewCount { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<NewsComment> NewsComments { get; set; } = new List<NewsComment>();
}
