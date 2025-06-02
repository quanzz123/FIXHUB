using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIXHUB.Models;

public partial class HistoryStep
{
    public int HistoryStepId { get; set; }

    public int? StepId { get; set; }

    public string? Title { get; set; }

    public string? Details { get; set; }

    public string? ImgUrl { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreatedDate { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    public virtual GuideStep? Step { get; set; }

    public virtual User? User { get; set; }
}
