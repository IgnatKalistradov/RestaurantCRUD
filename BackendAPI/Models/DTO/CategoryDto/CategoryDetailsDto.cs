using BackendAPI.Models.DTO.DishesDto;

namespace BackendAPI.Models.DTO.CategoryDto
{
    public class CategoryDetailsDto
    {
        public required CategoryBaseDto Category { get; set; }
        public required IEnumerable<DishBaseDto> Dishes { get; set; }
    }
}
