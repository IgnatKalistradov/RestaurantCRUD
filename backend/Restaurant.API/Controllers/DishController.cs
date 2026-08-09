using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Services;
using Restaurant.Application.Models.Dto;
using Restaurant.Core.Domain;

namespace Restaurant.Api.Controllers
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

        private AddImageDto? CreateImageDtoIfImagePassed(IFormFile? image)
        {
            if(image != null && image.Length > 0)
            {
                Stream imageStream = image.OpenReadStream();
                return new AddImageDto()
                {
                    FileName = image.FileName,
                    Length = image.Length,
                    ContentType = image.ContentType,
                    Stream = imageStream
                };
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] DishUpsertDto dishDto, IFormFile? image)
        {
            try
            {   
                using AddImageDto? addImageDto = CreateImageDtoIfImagePassed(image);
                
                Dish dish = await _dishService.AddAsync(dishDto, addImageDto);
                
                return Created();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("edit/{id:int}")]
        public async Task<IActionResult> Edit([FromForm] DishUpsertDto dishDto, IFormFile? image)
        {
            try
            {
                using AddImageDto? imageDto = CreateImageDtoIfImagePassed(image);
                await _dishService.UpdateAsync(dishDto, imageDto);
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
