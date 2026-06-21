using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Domain;
using Restaurant.Application.Services;
using Restaurant.Application.Models.Dto;
using Restaurant.Data;

namespace Restaurant.Api.Controllers
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
            queryOptions.AddInclude("Dishes");
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

            return CreatedAtAction(nameof(this.Details), new {id = ingredient.Id}, ingredient);
        }

        [HttpPost("edit/{id:int}")]
        public async Task<IActionResult> Edit(IngredientBaseDto ingredientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ingredientDto);
            }

            try
            {
                await _ingredientService.Update(ingredientDto);

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
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
