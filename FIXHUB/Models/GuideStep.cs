using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIXHUB.Models;

public partial class GuideStep
{
    public int StepId { get; set; }

    public int? GuideId { get; set; }

    public int? StepNumber { get; set; }

    public string? Instruction { get; set; }

    public string? ImageUrl { get; set; }

    public int? ModifyId { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    public virtual RepairGuide? Guide { get; set; }

    public virtual ICollection<HistoryStep> HistorySteps { get; set; } = new List<HistoryStep>();

    public virtual ICollection<GuideStep> InverseModify { get; set; } = new List<GuideStep>();

    public virtual GuideStep? Modify { get; set; }

    public virtual ICollection<StepComment> StepComments { get; set; } = new List<StepComment>();
}
