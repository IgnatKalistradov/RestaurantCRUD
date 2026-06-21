namespace Restaurant.Core.Domain;

public partial class Dish
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

    public virtual ICollection<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();

    public void SetIngredients(IEnumerable<Ingredient> ingredients)
    {
        this.Ingredients.Clear();

        foreach (var ingredient in ingredients)
        {
            this.Ingredients.Add(ingredient);
        }
    }
}