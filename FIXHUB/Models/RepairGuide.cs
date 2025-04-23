using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FIXHUB.Models;

public partial class RepairGuide
{
    public int GuideId { get; set; }

    public int? CategoryId { get; set; }

    public string? Title { get; set; }

    public string? Summary { get; set; }

    public string? Content { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UserId { get; set; }

    public string? ImgUrl { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    public virtual GuideCategory? Category { get; set; }

    public virtual ICollection<GuideStep> GuideSteps { get; set; } = new List<GuideStep>();

    public virtual ICollection<Teardown> Teardowns { get; set; } = new List<Teardown>();

    public virtual User? User { get; set; }
}
