using BackendAPI.Models;
using BackendAPI.Models.DbModels;
using BackendAPI.Models.DTO.IngredientDto;
using BackendAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private IngredientService _ingredientService;
        public IngredientController(IngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _ingredientService.SelectAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            QueryOptions<Ingredient> queryOptions = new QueryOptions<Ingredient>();
            queryOptions.AddInclude("ProductIngredients.Product");
            return Ok(await _ingredientService.SelectByIdAsync(id, queryOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name", "Description")] CreateIngredientDto ingredientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ingredientDto);
            }

            Ingredient ingredient = await _ingredientService.AddAsync(ingredientDto);

            return CreatedAtAction(nameof(this.Details), new {id = ingredient.IngredientId}, ingredient);
        }

        [HttpPost("edit/{id:int}")]
        public async Task<IActionResult> Edit([Bind("IngredientId", "Name", "Description")] Ingredient ingredient, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ingredient);
            }

            await _ingredientService.Update(ingredient);

            return RedirectToAction("Index");
        }


        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _ingredientService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
            return NoContent();
        }
    }
}
