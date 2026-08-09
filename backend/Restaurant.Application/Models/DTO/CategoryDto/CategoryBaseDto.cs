using System.ComponentModel.DataAnnotations;
using Restaurant.Core.Domain;

namespace Restaurant.Application.Models.Dto
{
    public class CategoryBaseDto
    {
        public CategoryBaseDto(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Description = category.Description;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
