using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models.DTO.CategoryDto
{
    public class CategoryBaseDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
