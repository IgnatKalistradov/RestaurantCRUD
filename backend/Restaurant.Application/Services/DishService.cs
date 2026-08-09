using Restaurant.Application.Models.Dto;
using Restaurant.Data;
using Restaurant.Infrastructure.Storage;
using Restaurant.Core.Domain;

namespace Restaurant.Application.Services
{
    public class DishService
    {
        private IRepository<Dish> _dishRepository;
        private IngredientService _ingredientService;
        private CategoryService _categoryService;
        private IStorageService _storageService;
        public DishService(IRepository<Dish> repository, IngredientService ingredientService, CategoryService categoryService, IStorageService storageService)
        {
            _dishRepository = repository;
            _ingredientService = ingredientService;
            _categoryService = categoryService;
            _storageService = storageService;
        }

        private async Task<string> UploadImageAsync(AddImageDto imageDto)
        {
            try
            {
                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageDto.FileName)}";
                return await _storageService.UploadImageAsync(fileName, imageDto.Stream, imageDto.ContentType);
            }
            catch (Exception ex)
            {
                throw new Exception("Image upload failed", ex);
            }
        }

        public async Task<Dish> AddAsync(DishUpsertDto dishDto, AddImageDto? imageDto)
        {
            if(_categoryService.ExistsAsync(dishDto.CategoryId).Result == false)
            {
                throw new ArgumentException("Category was not found");
            }
            
            Dish dish = new Dish()
            {
                Name = dishDto.Name,
                Description = dishDto.Description,
                Price = dishDto.Price,
                Stock = dishDto.Stock,
                CategoryId = dishDto.CategoryId,
            };
        
            IEnumerable<Ingredient> ingredients = await _ingredientService.SelectByIdsAsync(dishDto.IngredientIds);
            dish.SetIngredients(ingredients);

            if(imageDto != null)
            {
                dish.ImageUrl = await UploadImageAsync(imageDto);
            }
            
            await _dishRepository.AddAsync(dish);

            return dish;
        }

        public async Task<IEnumerable<DishInfoDto>> SelectAllAsync()
        {
            IEnumerable<Dish> dishes = await _dishRepository.SelectAllAsync();
            return dishes.Select(dish => new DishInfoDto(dish));
        }

        private async Task ApplyDishChangesAsync(Dish dish, DishUpsertDto dishDto, AddImageDto? imageDto)
        {
            dish.Name = dishDto.Name;
            dish.Description = dishDto.Description;
            dish.Price = dishDto.Price;
            dish.Stock = dishDto.Stock;
            dish.CategoryId = dishDto.CategoryId;

            IEnumerable<Ingredient> ingredients = await _ingredientService.SelectByIdsAsync(dishDto.IngredientIds);
            dish.SetIngredients(ingredients);

            await UpdateImageIfPassed(dish, imageDto);
        }
        private async Task UpdateImageIfPassed(Dish dish, AddImageDto? imageDto)
        {
            if(imageDto == null)
            {
                return;
            }

            string newImagePath = await UploadImageAsync(imageDto);
            await DeleteDishImageIfExists(dish);
            dish.ImageUrl = newImagePath;
        }
        public async Task UpdateAsync(DishUpsertDto dishDto, AddImageDto? imageDto)
        {
            QueryOptions<Dish> options = new QueryOptions<Dish>();
            options.AddInclude(nameof(Dish.Ingredients));

            Dish existingDish = await _dishRepository.SelectByIdAsync(dishDto.Id, options);
            if(existingDish == null)
            {
                throw new InvalidDataException("Dish was not found");
            }

            await ApplyDishChangesAsync(existingDish, dishDto, imageDto);

            await _dishRepository.UpdateAsync(existingDish);
        }

        private async Task DeleteDishImageIfExists(Dish dish)
        {
            if(dish.ImageUrl == null)
            {
                return;
            }

            await _storageService.DeleteImageAsync(dish.ImageUrl);
        }

        public async Task DeleteAsync(int id)
        {
            Dish dish = await _dishRepository.SelectByIdAsync(id);

            await DeleteDishImageIfExists(dish);

            await _dishRepository.DeleteAsync(id);
        }

        public async Task<DishDetailsDto> SelectByIdAsync(int id)
        {
            QueryOptions<Dish> options = new QueryOptions<Dish>();
            options.AddInclude(nameof(Dish.Category));
            options.AddInclude(nameof(Dish.Ingredients));

            Dish dish = await _dishRepository.SelectByIdAsync(id, options);
            DishDetailsDto dishDto = new DishDetailsDto(dish);
            return dishDto;
        }
    }
}
