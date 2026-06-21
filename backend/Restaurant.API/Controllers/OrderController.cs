using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Services;
using Restaurant.Application.Models.Dto;

namespace Restaurant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private DishService _dishService;
        private OrderService _orderService;

        public OrderController(DishService dishService, OrderService orderService)
        {
            _dishService = dishService;
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
