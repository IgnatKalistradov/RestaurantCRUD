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

        [HttpPost]
        public async Task<IActionResult> Add(ProductCreateDto productDto)
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
        public async Task<IActionResult> Edit(Product product, int[] ingredientIds, int productId)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(product, ingredientIds);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
