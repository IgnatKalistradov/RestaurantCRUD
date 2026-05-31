namespace BackendAPI.Models.DTO.CategoryDto
{
    public class CategoryDetailsDto
    {
        public required CategoryBaseDto Category { get; set; }
        public required IEnumerable<ProductBaseDto> Products { get; set; }
    }
}
