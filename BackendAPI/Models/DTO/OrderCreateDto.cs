namespace BackendAPI.Models.DTO
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public short Amount { get; set; }
        public decimal Price { get; set; }
    }

}
