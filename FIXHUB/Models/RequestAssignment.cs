using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class RequestAssignment
{
    public int AssignmentId { get; set; }

    public int? RequestId { get; set; }

    public int? TechnicianId { get; set; }

    public DateTime? AssignedAt { get; set; }

    public string? Notes { get; set; }

    public virtual ServiceRequest? Request { get; set; }

    public virtual Technician? Technician { get; set; }
}
