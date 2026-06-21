namespace Restaurant.Application.Models.Dto
{
    public class IngredientDetailsDto
    {
        public IngredientBaseDto Ingredient { get; set; } = new IngredientBaseDto();
        public IEnumerable<DishBaseDto> Dishes { get; set; } = new List<DishBaseDto>();
    }
}
