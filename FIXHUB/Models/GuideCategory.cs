using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class GuideCategory
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public string? ImgUrl { get; set; }

    public int? ParentId { get; set; }

    public int? Levels { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<GuideCategory> InverseParent { get; set; } = new List<GuideCategory>();

    public virtual GuideCategory? Parent { get; set; }

    public virtual ICollection<RepairGuide> RepairGuides { get; set; } = new List<RepairGuide>();
}
