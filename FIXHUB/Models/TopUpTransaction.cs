using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class TopUpTransaction
{
    public int TopUpId { get; set; }

    public int? UserId { get; set; }

    public decimal? Amount { get; set; }

    public int? Points { get; set; }

    public string? PaymentMethod { get; set; }

    public string? TransactionCode { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
