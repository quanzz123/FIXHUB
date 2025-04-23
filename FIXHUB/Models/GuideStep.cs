using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class GuideStep
{
    public int StepId { get; set; }

    public int? GuideId { get; set; }

    public int? StepNumber { get; set; }

    public string? Instruction { get; set; }

    public string? ImageUrl { get; set; }

    public int? ModifyId { get; set; }

    public int? UserId { get; set; }

    public virtual RepairGuide? Guide { get; set; }

    public virtual ICollection<GuideStep> InverseModify { get; set; } = new List<GuideStep>();

    public virtual GuideStep? Modify { get; set; }

    public virtual User? User { get; set; }
}
