using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class GuideCategory
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<RepairGuide> RepairGuides { get; set; } = new List<RepairGuide>();
}
