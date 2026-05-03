using Microsoft.AspNetCore.Mvc;
using TequilasRestaurant.Models;
using TequilasRestaurant.Models.DbModels;
using TequilasRestaurant.Models.Services;

namespace TequilasRestaurant.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly IngredientService _ingredientService;
        private readonly CategoryService _categoryService;
        
        public ProductController(ProductService productService, IngredientService ingredientService, CategoryService categoryService)
        {
            this._productService = productService;
            this._ingredientService = ingredientService;
            this._categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _productService.SelectAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.Ingredients = await _ingredientService.SelectAllAsync();
            ViewBag.Categories = await _categoryService.SelectAllAsync();

            return View(new Product());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product product, int[] ingredientIds)
        {
            if(ModelState.IsValid)
            {
                await _productService.AddAsync(product, ingredientIds);
                return RedirectToAction("Index", "Product");
            }

            ViewBag.Ingredients = await _ingredientService.SelectAllAsync();
            ViewBag.Categories = await _categoryService.SelectAllAsync();
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            QueryOptions<Product> query = new QueryOptions<Product>();
            query.Includes.Add("ProductIngredients.Ingredient");
            Product product = await _productService.SelectByIdAsync(id, query);

            if(product == null)
            {
                return RedirectToAction("Index", "Product");
            }

            ViewBag.Ingredients = await _ingredientService.SelectAllAsync();
            ViewBag.Categories = await _categoryService.SelectAllAsync();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product, int[] ingredientIds)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(product, ingredientIds);
                return RedirectToAction("Index", "Product");
            }

            ViewBag.Ingredients = await _ingredientService.SelectAllAsync();
            ViewBag.Categories = await _categoryService.SelectAllAsync();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product product)
        {
            if(ModelState.IsValid)
            {
                await _productService.DeleteAsync(product.ProductId);
                return RedirectToAction("Index", "Product");
            }

            return View(product);
        }
    }
}
