﻿using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class ForumPost
{
    public int PostId { get; set; }

    public int? UserId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ForumComment> ForumComments { get; set; } = new List<ForumComment>();

    public virtual User? User { get; set; }
}
