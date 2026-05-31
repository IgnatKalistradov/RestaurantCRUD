using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models.DTO.CategoryDto
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
