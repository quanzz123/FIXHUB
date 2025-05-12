using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class HistoryStep
{
    public int HistoryStepId { get; set; }

    public int? StepId { get; set; }

    public string? Title { get; set; }

    public string? Details { get; set; }

    public string? ImgUrl { get; set; }

    public int? UserId { get; set; }

    public virtual GuideStep? Step { get; set; }

    public virtual User? User { get; set; }
}
