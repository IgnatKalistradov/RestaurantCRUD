using BackendAPI.Data;
using BackendAPI.Models;
using BackendAPI.Models.DbModels;
using BackendAPI.Models.DTO;
using BackendAPI.Models.DTO.IngredientDto;

namespace BackendAPI.Services
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
            await _repository.DeleteAsync(model.IngredientId);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<IngredientBaseDto>> SelectAllAsync()
        {
            IEnumerable<Ingredient> ingredients = await _repository.SelectAllAsync();

            return ingredients.Select(ing => new IngredientBaseDto()
            {
                Id = ing.IngredientId,
                Name = ing.Name,
                Description = ing.Description
            });
        }

        public async Task<Ingredient> SelectByIdAsync(int id)
        {
            return await _repository.SelectByIdAsync(id);
        }

        public async Task<IngredientDetailsDto> SelectByIdAsync(int id, QueryOptions<Ingredient> options)
        {
            Ingredient ingredient = await _repository.SelectByIdAsync(id, options);

            IngredientDetailsDto ingredientDetails = new IngredientDetailsDto
            {
                Ingredient = new IngredientBaseDto()
                {
                    Id = id,
                    Name = ingredient.Name,
                    Description = ingredient.Description
                },
                Products = ingredient.ProductIngredients.Select(pi => new ProductBaseDto
                {
                    Id = pi.ProductId,
                    Name = pi.Product.Name
                })
            };

            return ingredientDetails;
        }

        public async Task Update(Ingredient model)
        {
            await _repository.UpdateAsync(model);
        }
    }
}
