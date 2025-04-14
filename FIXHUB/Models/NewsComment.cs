using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class NewsComment
{
    public int CommentId { get; set; }

    public int NewsId { get; set; }

    public int UserId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public bool? IsApproved { get; set; }

    public int? ParentId { get; set; }

    public virtual ICollection<NewsComment> InverseParent { get; set; } = new List<NewsComment>();

    public virtual News News { get; set; } = null!;

    public virtual NewsComment? Parent { get; set; }

    public virtual User User { get; set; } = null!;
}
