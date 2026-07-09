using System.ComponentModel.DataAnnotations;
using Restaurant.Core.Domain;

namespace Restaurant.Application.Models.Dto
{
    public class IngredientBaseDto
    {
        public IngredientBaseDto(Ingredient ingredient)
        {
            Id = ingredient.Id;
            Name = ingredient.Name;
            Description = ingredient.Description;
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}
