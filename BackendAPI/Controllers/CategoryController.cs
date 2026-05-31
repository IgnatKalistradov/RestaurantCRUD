using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models;
using BackendAPI.Models.DbModels;
using BackendAPI.Services;
using BackendAPI.Models.DTO.CategoryDto;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _categoryService.SelectAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            QueryOptions<Category> queryOptions = new QueryOptions<Category>();
            queryOptions.AddInclude("Products");

            try
            {
                CategoryDetailsDto categoryDto = await _categoryService.SelectByIdAsync(id, queryOptions);

                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(categoryDto);
            }

            try
            {
                Category category = await _categoryService.AddAsync(categoryDto);

                return CreatedAtAction(nameof(this.Details), new {id = category.CategoryId}, category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("edit/{id:int}")]
        public async Task<IActionResult> Edit([Bind("CategoryId", "Name", "Description")] Category category, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(category);
            }

            await _categoryService.UpdateAsync(category);

            return RedirectToAction("Index");
        }


        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete([Bind("CategoryId", "Name", "Description")] Category category, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(category);
            }

            await _categoryService.DeleteAsync(category.CategoryId);

            return RedirectToAction("Index");
        }
    }
}
