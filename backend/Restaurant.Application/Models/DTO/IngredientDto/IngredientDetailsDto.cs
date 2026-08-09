namespace Restaurant.Application.Models.Dto
{
    public class IngredientDetailsDto
    {
        public required IngredientBaseDto Ingredient { get; set; }
        public IEnumerable<DishBaseDto> Dishes { get; set; } = new List<DishBaseDto>();
    }
}
