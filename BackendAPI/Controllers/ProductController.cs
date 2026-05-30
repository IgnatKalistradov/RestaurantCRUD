using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models.DbModels;
using BackendAPI.Models.Services;

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
        public async Task<IActionResult> Add(Product product, int[] ingredientIds)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(product, ingredientIds);
                return CreatedAtAction("Index", product);
            }

            return BadRequest();
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
