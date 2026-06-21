using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.Models.Dto
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
