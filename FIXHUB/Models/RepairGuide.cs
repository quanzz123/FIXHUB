using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class RepairGuide
{
    public int GuideId { get; set; }

    public int? CategoryId { get; set; }

    public string? Title { get; set; }

    public string? Summary { get; set; }

    public string? Content { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual GuideCategory? Category { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<GuideStep> GuideSteps { get; set; } = new List<GuideStep>();

    public virtual ICollection<Teardown> Teardowns { get; set; } = new List<Teardown>();
}
