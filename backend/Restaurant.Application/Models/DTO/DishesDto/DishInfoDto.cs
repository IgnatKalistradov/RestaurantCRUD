using Restaurant.Core.Domain;

namespace Restaurant.Application.Models.Dto
{
    public class DishInfoDto
    {
        public DishInfoDto(Dish dish)
        {
            Id = dish.Id;
            Name = dish.Name;
            Description = dish.Description;
            Price = dish.Price;
            Stock = dish.Stock;
            ImageUrl = dish.ImageUrl;  
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageUrl {get; set;}
    }
}
