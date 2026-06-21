namespace Restaurant.Application.Models.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<OrderItemDto> orderItems { get; set; }
    }
}
