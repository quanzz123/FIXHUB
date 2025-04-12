using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class ServiceRequest
{
    public int RequestId { get; set; }

    public int? UserId { get; set; }

    public int? ServiceTypeId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public DateTime? PreferredDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<RequestAssignment> RequestAssignments { get; set; } = new List<RequestAssignment>();

    public virtual ICollection<RequestFeedback> RequestFeedbacks { get; set; } = new List<RequestFeedback>();

    public virtual ICollection<RequestImage> RequestImages { get; set; } = new List<RequestImage>();

    public virtual ServiceType? ServiceType { get; set; }

    public virtual User? User { get; set; }
}
