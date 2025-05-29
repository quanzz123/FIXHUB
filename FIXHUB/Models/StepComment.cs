using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class StepComment
{
    public int CommentId { get; set; }

    public int StepId { get; set; }

    public int UserId { get; set; }

    public string CommentText { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int? ParentCommentId { get; set; }

    public int? Rate { get; set; }

    public virtual ICollection<StepComment> InverseParentComment { get; set; } = new List<StepComment>();

    public virtual StepComment? ParentComment { get; set; }

    public virtual GuideStep Step { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
