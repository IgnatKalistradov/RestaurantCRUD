using Restaurant.Core.Domain;

namespace Restaurant.Core.Domain;

public partial class Dish
{
    public void SetIngredients(IEnumerable<Ingredient> ingredients)
    {
        if(ingredients.Count() == 0)
        {
            throw new ArgumentException("Ingredients cannot be empty");
        }

        this.Ingredients.Clear();

        foreach (var ingredient in ingredients)
        {
            this.Ingredients.Add(ingredient);
        }
    }
}