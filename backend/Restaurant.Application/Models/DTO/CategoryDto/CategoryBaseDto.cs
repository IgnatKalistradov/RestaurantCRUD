using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.Models.Dto
{
    public class CategoryBaseDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
