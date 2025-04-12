using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class ProductCategory
{
    public int ProductCategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
