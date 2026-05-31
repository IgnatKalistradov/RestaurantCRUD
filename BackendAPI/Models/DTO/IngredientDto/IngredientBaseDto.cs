using System.ComponentModel.DataAnnotations;

namespace BackendAPI.Models.DTO.IngredientDto
{
    public class IngredientBaseDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}
