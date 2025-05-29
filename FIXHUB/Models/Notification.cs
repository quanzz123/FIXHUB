using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? UserId { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }

    public bool? IsRead { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? TransactionId { get; set; }

    public virtual PointTransaction? Transaction { get; set; }

    public virtual User? User { get; set; }
}
