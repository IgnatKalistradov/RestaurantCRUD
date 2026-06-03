using BackendAPI.Models.DTO.ProductsDto;

namespace BackendAPI.Models.DTO.IngredientDto
{
    public class IngredientDetailsDto
    {
        public IngredientBaseDto Ingredient { get; set; } = new IngredientBaseDto();
        public IEnumerable<ProductBaseDto> Products { get; set; } = new List<ProductBaseDto>();
    }
}
