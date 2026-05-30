using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models;
using BackendAPI.Models.DbModels;
using BackendAPI.Models.Services;
using BackendAPI.Models.ViewModels;

namespace BackendAPIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ProductService _productService;
        private OrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ProductService productService, OrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _orderService = orderService;
            _userManager = userManager;
        }

        private async Task<OrderViewModel> GetOrderViewModel()
        {
            return HttpContext.Session.Get<OrderViewModel>("OrderViewModel") ?? new OrderViewModel
            {
                OrderItems = new List<OrderItemViewModel>(),
                Products = await _productService.SelectAllAsync()
            };
        }

        private void SetOrderViewModel(OrderViewModel orderViewModel)
        {
            HttpContext.Session.Set("OrderViewModel", orderViewModel);
        }

        private void RemoveOrderViewModel()
        {
            HttpContext.Session.Remove("OrderViewModel");
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddItem(int productId, int productQuantity)
        {
            Product productToAdd = await _productService.SelectByIdAsync(productId);
            if (productToAdd == null)
            {
                return NotFound();
            }

            OrderViewModel orderViewModel = await GetOrderViewModel();

            orderViewModel.AddOrderItem(productToAdd, productQuantity);

            SetOrderViewModel(orderViewModel);

            return Ok();
        }

        [Authorize]
        [HttpGet("cart")]
        public async Task<IActionResult> GetCart()
        {
            OrderViewModel viewModel = await GetOrderViewModel();

            return Ok(viewModel);
        }

        [Authorize]
        [HttpPost("remove")]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            Product productToRemove = await _productService.SelectByIdAsync(productId);
            if (productToRemove == null)
            {
                return NotFound();
            }

            OrderViewModel orderViewModel = await GetOrderViewModel();

            orderViewModel.RemoveOrderItem(productToRemove.ProductId);

            SetOrderViewModel(orderViewModel);

            return RedirectToAction("Cart", orderViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Place()
        {
            OrderViewModel orderView = await GetOrderViewModel();
            if (orderView.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }

            string userId = _userManager.GetUserId(User);

            Order order = _orderService.CreateOrder(orderView, userId);
            await _orderService.AddAsync(order);

            RemoveOrderViewModel();

            return RedirectToAction("ViewOrders");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ViewOrders()
        {
            string userId = _userManager.GetUserId(User);
            QueryOptions<Order> queryOptions = new QueryOptions<Order>();
            queryOptions.AddInclude("OrderItems.Product");
            queryOptions.Where = order => order.UserId == userId;

            var userOders = await _orderService.SelectAsync(queryOptions);

            return Ok(userOders);
        }
    }
}
