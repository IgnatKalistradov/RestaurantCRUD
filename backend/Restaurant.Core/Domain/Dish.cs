using System;
using System.Collections.Generic;

namespace Restaurant.Core.Domain;

public partial class Dish
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public short Stock { get; set; }

    public int CategoryId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
