using System;
using System.Collections.Generic;

namespace Restaurant.Core.Domain;

public partial class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int DishId { get; set; }

    public short Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public virtual Dish Dish { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
