using BackendAPI.Models;
using BackendAPI.Models.DomainModels;
using BackendAPI.Models.DTO.CategoryDto;
using BackendAPI.Models.DTO.IngredientDto;
using BackendAPI.Models.DTO.DishesDto;

namespace BackendAPI.Services
{
    public class DishService
    {
        private IRepository<Dish> _dishRepository;
        private IngredientService _ingredientService;
        public DishService(IRepository<Dish> repository, IngredientService ingredientService)
        {
            _dishRepository = repository;
            _ingredientService = ingredientService;
        }
        public async Task<Dish> AddAsync(DishUpsertDto dishDto)
        {
            Dish dish = new Dish()
            {
                Id = dishDto.Id,
                Name = dishDto.Name,
                Description = dishDto.Description,
                Price = dishDto.Price,
                Stock = dishDto.Stock,
                CategoryId = dishDto.CategoryId,
            };

            var ingredients = await _ingredientService.SelectByIdsAsync(dishDto.IngredientIds);
            dish.SetIngredients(ingredients);

            await _dishRepository.AddAsync(dish);

            return dish;
        }

        public async Task AddAsync(Dish dish, int[] ingredientIds)
        {
            var ingredients = await _ingredientService.SelectByIdsAsync(ingredientIds);
            dish.SetIngredients(ingredients);

            await _dishRepository.AddAsync(dish);
        }

        public async Task<IEnumerable<DishInfoDto>> SelectAllAsync()
        {
            IEnumerable<Dish> dishes = await _dishRepository.SelectAllAsync();
            return dishes.Select(dish => new DishInfoDto()
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Stock = dish.Stock
            });
        }

        public async Task UpdateAsync(DishUpsertDto dishDto)
        {
            QueryOptions<Dish> options = new QueryOptions<Dish>();
            options.AddInclude(nameof(Dish.Ingredients));

            Dish existingDish = await _dishRepository.SelectByIdAsync(dishDto.Id, options);
            if(existingDish == null)
            {
                throw new InvalidDataException("Dish was not found");
            }

            existingDish.Copy(dishDto);

            var ingredients = await _ingredientService.SelectByIdsAsync(dishDto.IngredientIds);
            existingDish.SetIngredients(ingredients);

            await _dishRepository.UpdateAsync(existingDish);
        }

        public async Task DeleteAsync(int id)
        {
            Dish dish = await _dishRepository.SelectByIdAsync(id);

            await _dishRepository.DeleteAsync(id);
        }

        public async Task<DishDetailsDto> SelectByIdAsync(int id)
        {
            QueryOptions<Dish> options = new QueryOptions<Dish>();
            options.AddInclude("Category");
            options.AddInclude("Ingredients");

            Dish dish = await _dishRepository.SelectByIdAsync(id, options);
            DishDetailsDto dishDto = new DishDetailsDto()
            {
                Id = id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Stock = dish.Stock,
                Category = new CategoryBaseDto()
                {
                    Id = dish.CategoryId,
                    Name = dish.Category.Name,
                    Description = dish.Category.Description
                },
                Ingredients = dish.Ingredients.Select(ingredient => new IngredientBaseDto()
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Description = ingredient.Description
                })
            };
            return dishDto;
        }
    }
}
