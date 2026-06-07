using BackendAPI.Models.DTO;

namespace BackendAPI.Models.DbModels
{
    public class Order
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; private set; }
        public ICollection<OrderItem> OrderItems { get; private set; }

        public void SetOrderItems(IEnumerable<OrderItemDto> itemDtos)
        {
            foreach(OrderItemDto itemDto in itemDtos)
            {
                OrderItems.Add(new OrderItem()
                {
                    ProductId = itemDto.Id,
                    Quantity = itemDto.Amount,
                    Price = itemDto.Price
                });
            }

            this.TotalAmount = OrderItems.Select(item => item.Price * item.Quantity).Sum();
        }
    }
}