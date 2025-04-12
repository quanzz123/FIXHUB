using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class Technician
{
    public int TechnicianId { get; set; }

    public int? UserId { get; set; }

    public string? Expertise { get; set; }

    public double? Rating { get; set; }

    public virtual ICollection<RequestAssignment> RequestAssignments { get; set; } = new List<RequestAssignment>();

    public virtual User? User { get; set; }
}
