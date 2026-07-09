using Restaurant.Core.Domain;

namespace Restaurant.Core.Domain;

public partial class Order
{
    public void AddOrderItem(int itemId, short itemAmount, decimal itemPrice)
    {
        this.OrderItems.Add(new OrderItem()
            {
                DishId = itemId,
                Quantity = itemAmount,
                UnitPrice = itemPrice
            });

        RecalculateOrderTotalPrice();
    }

    private void RecalculateOrderTotalPrice()
    {
        this.TotalPrice = this.OrderItems.Sum(item => item.UnitPrice * item.Quantity);
    }
}