using BackendAPI.Models.DTO;
using System;
using System.Collections.Generic;

namespace BackendAPI.Models.DomainModels;

public partial class Order
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public void SetOrderItems(IEnumerable<OrderItemDto> itemDtos)
    {
        foreach (OrderItemDto itemDto in itemDtos)
        {
            OrderItems.Add(new OrderItem()
            {
                DishId = itemDto.Id,
                Quantity = itemDto.Amount,
                UnitPrice = itemDto.Price
            });
        }

        this.TotalPrice = OrderItems.Select(item => item.UnitPrice * item.Quantity).Sum();
    }
}
