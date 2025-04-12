using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class TeardownComponent
{
    public int ComponentId { get; set; }

    public int? TeardownId { get; set; }

    public string? ComponentName { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Teardown? Teardown { get; set; }
}
