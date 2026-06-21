namespace Restaurant.Core.Domain;

public partial class Order
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public void AddOrderItem(int itemId, short itemAmount, decimal itemPrice)
    {
        OrderItems.Add(new OrderItem()
            {
                DishId = itemId,
                Quantity = itemAmount,
                UnitPrice = itemPrice
            });

        RecalculateOrderTotalPrice();
    }

    private void RecalculateOrderTotalPrice()
    {
        this.TotalPrice = OrderItems.Select(item => item.UnitPrice * item.Quantity).Sum();
    }
}