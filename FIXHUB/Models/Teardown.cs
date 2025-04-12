using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class Teardown
{
    public int TeardownId { get; set; }

    public string? DeviceName { get; set; }

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public string? Overview { get; set; }

    public int? GuideId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual RepairGuide? Guide { get; set; }

    public virtual ICollection<TeardownComponent> TeardownComponents { get; set; } = new List<TeardownComponent>();
}
