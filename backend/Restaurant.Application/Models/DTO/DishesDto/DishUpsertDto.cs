namespace Restaurant.Application.Models.Dto
{
    public class DishUpsertDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public short Stock { get; set; }

        public IEnumerable<int> IngredientIds { get; set; } = new List<int>();
        public int CategoryId { get; set; }
    }
}
