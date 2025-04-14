using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string? ServiceName { get; set; }

    public string? ServiceDesc { get; set; }

    public string? ImageUrl { get; set; }

    public bool? IsActive { get; set; }
}
