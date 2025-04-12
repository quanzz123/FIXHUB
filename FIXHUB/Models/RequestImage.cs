using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class RequestImage
{
    public int ImageId { get; set; }

    public int? RequestId { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public virtual ServiceRequest? Request { get; set; }
}
