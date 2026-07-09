using Restaurant.Core.Domain;

namespace Restaurant.Application.Models.Dto
{
    public class DishDetailsDto
    {
        public DishDetailsDto(Dish dish)
        {
            Id = dish.Id;
            Name = dish.Name;
            Description = dish.Description;
            Price = dish.Price;
            Stock = dish.Stock;
            ImageUrl = dish.ImageUrl;
            Category = new CategoryBaseDto(dish.Category);
            Ingredients = dish.Ingredients.Select(ingredient => new IngredientBaseDto(ingredient));    
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl {get; set;}

        public CategoryBaseDto Category { get; set; }
        public IEnumerable<IngredientBaseDto> Ingredients { get; set; }
    }
}
