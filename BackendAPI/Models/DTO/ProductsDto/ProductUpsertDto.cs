namespace BackendAPI.Models.DTO.ProductsDto
{
    public class ProductUpsertDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public IEnumerable<int> IngredientIds { get; set; } = new List<int>();
        public int CategoryId { get; set; }
    }
}
