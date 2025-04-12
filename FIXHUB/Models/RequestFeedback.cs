using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class RequestFeedback
{
    public int FeedbackId { get; set; }

    public int? RequestId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ServiceRequest? Request { get; set; }
}
