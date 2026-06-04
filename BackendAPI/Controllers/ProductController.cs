using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models.DbModels;
using BackendAPI.Services;
using BackendAPI.Models.DTO.ProductsDto;

namespace BackendAPIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _productService.SelectAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            try
            {
                var product = await _productService.SelectByIdAsync(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductUpsertDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(productDto);
            }

            try
            {
                Product product = await _productService.AddAsync(productDto);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("edit/{id:int}")]
        public async Task<IActionResult> Edit(ProductUpsertDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
                
            }

            try
            {
                await _productService.UpdateAsync(productDto);
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
                await _productService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
