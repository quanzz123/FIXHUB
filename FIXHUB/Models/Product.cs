using System;
using System.Collections.Generic;

namespace FIXHUB.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? ProductCategoryId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? StockQuantity { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ProductCategory? ProductCategory { get; set; }
}
