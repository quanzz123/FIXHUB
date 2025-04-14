using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class Menu
{
    public int MenuId { get; set; }

    public string? Title { get; set; }

    public string? Alias { get; set; }

    public string? Url { get; set; }

    public int? ParentId { get; set; }

    public int? Levels { get; set; }

    public int? Positions { get; set; }

    public string? Icon { get; set; }

    public DateTime? CreateAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Menu> InverseParent { get; set; } = new List<Menu>();

    public virtual Menu? Parent { get; set; }
}
