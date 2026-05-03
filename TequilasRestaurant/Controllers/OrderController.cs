using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TequilasRestaurant.Models;
using TequilasRestaurant.Models.DbModels;
using TequilasRestaurant.Models.Services;
using TequilasRestaurant.Models.ViewModels;

namespace TequilasRestaurant.Controllers
{
    public class OrderController : Controller
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
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(await GetOrderViewModel());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddItem(int productId, int productQuantity)
        {
            Product productToAdd = await _productService.SelectByIdAsync(productId);
            if(productToAdd == null)
            {
                return NotFound();
            }

            OrderViewModel orderViewModel = await GetOrderViewModel();

            orderViewModel.AddOrderItem(productToAdd, productQuantity);

            SetOrderViewModel(orderViewModel);

            return RedirectToAction("Create", orderViewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            OrderViewModel viewModel = await GetOrderViewModel();

            if(viewModel.OrderItems.Count == 0)
            {
                return RedirectToAction("Create");
            }

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            Product productToRemove = await _productService.SelectByIdAsync(productId);
            if(productToRemove == null)
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
            if(orderView.OrderItems.Count == 0)
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

            return View(userOders);
        }
    }
}
