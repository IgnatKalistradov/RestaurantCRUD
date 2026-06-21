namespace Restaurant.Core.Domain;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    
    public string Description { get; set; } = null!;

    public virtual ICollection<Dish> Dishes { get; private set; } = new List<Dish>();
}