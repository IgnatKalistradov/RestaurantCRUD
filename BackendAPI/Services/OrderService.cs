using BackendAPI.Models;
using BackendAPI.Models.DbModels;



namespace BackendAPI.Services
{
    public class OrderService
    {
        private readonly IRepository<Order> _repository;

        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }

        //public Order CreateOrder(OrderViewModel orderView, string userId)
        //{
        //    Order order = new Order()
        //    {
        //        OrderDate = DateTime.Now,
        //        TotalAmount = orderView.TotalAmount,
        //        UserId = userId
        //    };

        //    foreach(var orderViewItem in orderView.OrderItems)
        //    {
        //        order.OrderItems.Add(new OrderItem()
        //        {
        //            ProductId = orderViewItem.ProductId,
        //            Quantity = orderViewItem.Quantity,
        //            Price = orderViewItem.PricePerUnit
        //        });
        //    }

        //    return order;
        //}
        public async Task AddAsync(Order order)
        {
            await _repository.AddAsync(order);
        }

        public async Task<IEnumerable<Order>> SelectAllAsync()
        {
            return await _repository.SelectAllAsync();
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
