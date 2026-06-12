using BackendAPI.Models.DTO.CategoryDto;
using BackendAPI.Models.DTO.IngredientDto;

namespace BackendAPI.Models.DTO.DishesDto
{
    public class DishDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public CategoryBaseDto Category { get; set; }
        public IEnumerable<IngredientBaseDto> Ingredients { get; set; }
    }
}
