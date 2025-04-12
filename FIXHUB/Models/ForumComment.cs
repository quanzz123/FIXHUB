using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class ForumComment
{
    public int CommentId { get; set; }

    public int? PostId { get; set; }

    public int? UserId { get; set; }

    public string? CommentText { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ForumPost? Post { get; set; }

    public virtual User? User { get; set; }
}
