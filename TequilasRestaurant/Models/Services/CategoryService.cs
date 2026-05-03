using TequilasRestaurant.Models.DbModels;

namespace TequilasRestaurant.Models.Services
{
    public class CategoryService
    {
        private IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> repository)
        {
            _categoryRepository = repository;
        }

        public async Task AddAsync(Category product)
        {
            await _categoryRepository.AddAsync(product);
        }

        public async Task<IEnumerable<Category>> SelectAllAsync()
        {
            return await _categoryRepository.SelectAllAsync();
        }

        public async Task UpdateAsync(Category product)
        {
            await _categoryRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<Category> SelectByIdAsync(int id)
        {
            return await _categoryRepository.SelectByIdAsync(id);
        }
        public async Task<Category> SelectByIdAsync(int id, QueryOptions<Category> options)
        {
            return await _categoryRepository.SelectByIdAsync(id, options);
        }
    }
}
