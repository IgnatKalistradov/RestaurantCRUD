using Microsoft.CodeAnalysis;
using TequilasRestaurant.Models.DbModels;

namespace TequilasRestaurant.Models.ViewModels
{
    public class OrderViewModel
    {
        public decimal TotalAmount { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }

        public IEnumerable<Product> Products { get; set; }

        public void AddOrderItem(Product productToAdd, int productQuantity)
        {
            var existingOrderItem = this.OrderItems.FirstOrDefault(oi => oi.ProductId == productToAdd.ProductId);

            if (existingOrderItem != null)
            {
                existingOrderItem.Quantity += productQuantity;
            }
            else
            {
                this.OrderItems.Add(new OrderItemViewModel()
                {
                    ProductId = productToAdd.ProductId,
                    ProductName = productToAdd.Name,
                    Quantity = productQuantity,
                    PricePerUnit = productToAdd.Price
                });
            }

            this.TotalAmount = this.OrderItems.Sum(oi => oi.PricePerUnit * oi.Quantity);
        }

        public void RemoveOrderItem(int productId)
        {
            OrderItemViewModel? orderItem = OrderItems.FirstOrDefault(oi => oi.ProductId == productId);
            if(orderItem != null)
            {
                OrderItems.Remove(orderItem);
            }
        }
    }
}
