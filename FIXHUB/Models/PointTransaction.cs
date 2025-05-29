using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class PointTransaction
{
    public int TransactionId { get; set; }

    public int SenderId { get; set; }

    public int? ReceiverId { get; set; }

    public int Points { get; set; }

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual User? Receiver { get; set; }

    public virtual User Sender { get; set; } = null!;
}
