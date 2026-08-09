using Restaurant.Data;
using Restaurant.Application.Models.Dto;
using Restaurant.Core.Domain;

namespace Restaurant.Application.Services
{
    public class IngredientService
    {
        private IRepository<Ingredient> _repository;

        public IngredientService(IRepository<Ingredient> repository)
        {
            _repository = repository;
        }

        public async Task<Ingredient> AddAsync(CreateIngredientDto model)
        {
            Ingredient ingredient = new Ingredient()
            {
                Name = model.Name,
                Description = model.Description
            };

            await _repository.AddAsync(ingredient);

            return ingredient;
        }

        public async Task DeleteAsync(Ingredient model)
        {
            await _repository.DeleteAsync(model.Id);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<IngredientBaseDto>> SelectAllAsync()
        {
            IEnumerable<Ingredient> ingredients = await _repository.SelectAllAsync();

            return ingredients.Select(ing => new IngredientBaseDto(ing));
        }

        public async Task<Ingredient> SelectByIdAsync(int id)
        {
            return await _repository.SelectByIdAsync(id);
        }

        public async Task<IEnumerable<Ingredient>> SelectByIdsAsync(IEnumerable<int> ingredientIds)
        {
            return await _repository.SelectByIdsAsync(ingredientIds);
        }

        public async Task<IngredientDetailsDto> SelectByIdAsync(int id, QueryOptions<Ingredient> options)
        {
            Ingredient ingredient = await _repository.SelectByIdAsync(id, options);

            IngredientDetailsDto ingredientDetails = new IngredientDetailsDto
            {
                Ingredient = new IngredientBaseDto(ingredient),
                Dishes = ingredient.Dishes.Select(dish => new DishBaseDto
                {
                    Id = dish.Id,
                    Name = dish.Name
                })
            };

            return ingredientDetails;
        }

        public async Task Update(IngredientBaseDto ingredientDto)
        {
            Ingredient ingredient = new Ingredient()
            {
                Id = ingredientDto.Id,
                Name = ingredientDto.Name,
                Description = ingredientDto.Description
            };
            await _repository.UpdateAsync(ingredient);
        }
    }
}
