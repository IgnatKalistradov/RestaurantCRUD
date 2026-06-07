using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models;
using BackendAPI.Models.DbModels;
using BackendAPI.Services;
using BackendAPI.Models.DTO;


namespace BackendAPIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ProductService _productService;
        private OrderService _orderService;

        public OrderController(ProductService productService, OrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IEnumerable<OrderItemDto> orderItems)
        {
            try
            {
                await _orderService.AddAsync(orderItems);

                return Created();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                return Ok(await _orderService.SelectAllAsync());
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _orderService.DeleteAsync(id);

            return NoContent();
        }
    }
}
