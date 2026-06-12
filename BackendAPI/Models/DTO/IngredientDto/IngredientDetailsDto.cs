using BackendAPI.Models.DTO.DishesDto;

namespace BackendAPI.Models.DTO.IngredientDto
{
    public class IngredientDetailsDto
    {
        public IngredientBaseDto Ingredient { get; set; } = new IngredientBaseDto();
        public IEnumerable<DishBaseDto> Dishes { get; set; } = new List<DishBaseDto>();
    }
}
