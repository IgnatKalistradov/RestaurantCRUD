using Microsoft.AspNetCore.Mvc;
using TequilasRestaurant.Models;
using TequilasRestaurant.Models.DbModels;
using TequilasRestaurant.Models.Services;

namespace TequilasRestaurant.Controllers
{
    public class IngredientController : Controller
    {
        private IngredientService _ingredientService;
        public IngredientController(IngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _ingredientService.SelectAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            QueryOptions<Ingredient> queryOptions = new QueryOptions<Ingredient>();
            queryOptions.AddInclude("ProductIngredients.Product");
            return View(await _ingredientService.SelectByIdAsync(id, queryOptions));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name", "Description")] Ingredient ingredient)
        {
            if(!ModelState.IsValid)
            {
                return View(ingredient);
            }

            await _ingredientService.AddAsync(ingredient);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Ingredient ingredient = await _ingredientService.SelectByIdAsync(id);

            return View(ingredient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("IngredientId", "Name", "Description")] Ingredient ingredient)
        {
            if(!ModelState.IsValid)
            {
                return View(ingredient);
            }

            await _ingredientService.Update(ingredient);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _ingredientService.SelectByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([Bind("IngredientId", "Name", "Description")] Ingredient ingredient)
        {
            if(!ModelState.IsValid)
            {
                return View(ingredient);
            }

            await _ingredientService.DeleteAsync(ingredient.IngredientId);

            return RedirectToAction("Index");
        }
    }
}
