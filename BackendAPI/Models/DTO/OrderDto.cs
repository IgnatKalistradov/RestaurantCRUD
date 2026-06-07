namespace BackendAPI.Models.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<OrderItemDto> orderItems { get; set; }
    }
}
