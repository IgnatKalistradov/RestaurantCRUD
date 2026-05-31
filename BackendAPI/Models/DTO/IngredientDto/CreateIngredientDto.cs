using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models.DTO.IngredientDto
{
    public class CreateIngredientDto
    {
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
