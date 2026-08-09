using System;
using System.Collections.Generic;

namespace Restaurant.Core.Domain;

public partial class Order
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
