using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class UserPoint
{
    public int UserId { get; set; }

    public int? PointBalance { get; set; }

    public virtual User User { get; set; } = null!;
}
