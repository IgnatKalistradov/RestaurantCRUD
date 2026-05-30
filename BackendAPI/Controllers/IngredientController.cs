using Microsoft.AspNetCore.Mvc;
using BackendAPI.Models;
using BackendAPI.Models.DbModels;
using BackendAPI.Models.Services;

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
        public async Task<IActionResult> Create([Bind("Name", "Description")] Ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ingredient);
            }

            await _ingredientService.AddAsync(ingredient);

            return CreatedAtAction(nameof(this.Details), ingredient);
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
        public async Task<IActionResult> Delete([Bind("IngredientId", "Name", "Description")] Ingredient ingredient, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ingredient);
            }

            await _ingredientService.DeleteAsync(ingredient.IngredientId);

            return RedirectToAction("Index");
        }
    }
}
