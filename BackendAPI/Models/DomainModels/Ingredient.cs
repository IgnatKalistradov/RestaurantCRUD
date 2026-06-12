using System;
using System.Collections.Generic;

namespace BackendAPI.Models.DomainModels;

public partial class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Dish> Dishes { get; set; } = new List<Dish>();
}
