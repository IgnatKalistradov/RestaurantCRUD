using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models.DomainModels;
using BackendAPI.Services;
using BackendAPI.Models.DTO.DishesDto;

namespace BackendAPIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly DishService _dishService;

        public DishController(DishService dishService)
        {
            this._dishService = dishService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _dishService.SelectAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            try
            {
                var dish = await _dishService.SelectByIdAsync(id);
                return Ok(dish);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(DishUpsertDto dishDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(dishDto);
            }

            try
            {
                Dish dish = await _dishService.AddAsync(dishDto);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("edit/{id:int}")]
        public async Task<IActionResult> Edit(DishUpsertDto dishDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
                
            }

            try
            {
                await _dishService.UpdateAsync(dishDto);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _dishService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
