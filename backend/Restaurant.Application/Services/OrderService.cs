using Restaurant.Core.Domain;
using Restaurant.Data;
using Restaurant.Application.Models.Dto;

namespace Restaurant.Application.Services
{
    public class OrderService
    {
        private readonly IRepository<Order> _repository;

        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(IEnumerable<OrderItemDto> orderItems)
        {
            Order order = new Order()
            {
                CreatedAt = DateTime.UtcNow,

            };
            
            foreach(OrderItemDto itemDto in orderItems)
            {
                order.AddOrderItem(itemDto.Id, itemDto.Amount, itemDto.Price);
            }
            
            await _repository.AddAsync(order);
        }

        public async Task<IEnumerable<OrderDto>> SelectAllAsync()
        {
            QueryOptions<Order> options = new QueryOptions<Order>();
            options.AddInclude("OrderItems.Dish");
            IEnumerable<Order> orders = await _repository.SelectAsync(options);

            return orders.Select(order => new OrderDto()
            {
                Id = order.Id,
                CreateDate = order.CreatedAt,
                orderItems = order.OrderItems.Select(item => new OrderItemDto()
                {
                    Id = item.DishId,
                    Name = item.Dish.Name,
                    Amount = item.Quantity,
                    Price = item.UnitPrice
                })
            });
        }

        public async Task<IEnumerable<Order>> SelectAsync(QueryOptions<Order> options)
        {
            return await _repository.SelectAsync(options);
        }

        public async Task<Order> SelectByIdAsync(int id)
        {
            return await _repository.SelectByIdAsync(id);
        }

        public async Task<Order> SelectByIdAsync(int id, QueryOptions<Order> queryOptions)
        {
            return await _repository.SelectByIdAsync(id, queryOptions);
        }

        public async Task UpdateAsync(Order order)
        {
            await _repository.UpdateAsync(order);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
