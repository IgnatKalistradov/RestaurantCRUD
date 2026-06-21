namespace Restaurant.Application.Models.Dto
{
    public class CategoryDetailsDto
    {
        public required CategoryBaseDto Category { get; set; }
        public required IEnumerable<DishBaseDto> Dishes { get; set; }
    }
}
