using TequilasRestaurant.Data;
using TequilasRestaurant.Models.DbModels;

namespace TequilasRestaurant.Models.Services
{
    public class IngredientService
    {
        private IRepository<Ingredient> _repository;

        public IngredientService(IRepository<Ingredient> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(Ingredient model)
        {
            await _repository.AddAsync(model);
        }

        public async Task DeleteAsync(Ingredient model)
        {
            await _repository.DeleteAsync(model.IngredientId);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Ingredient>> SelectAllAsync()
        {
            return await _repository.SelectAllAsync();
        }

        public async Task<Ingredient> SelectByIdAsync(int id)
        {
            return await _repository.SelectByIdAsync(id);
        }

        public async Task<Ingredient> SelectByIdAsync(int id, QueryOptions<Ingredient> options)
        {
            return await _repository.SelectByIdAsync(id, options);
        }

        public async Task Update(Ingredient model)
        {
            await _repository.UpdateAsync(model);
        }
    }
}
