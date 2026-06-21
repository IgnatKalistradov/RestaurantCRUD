using Restaurant.Core.Domain;
using Restaurant.Data;
using Restaurant.Application.Models.Dto;

namespace Restaurant.Application.Services
{
    public class CategoryService
    {
        private IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> repository)
        {
            _categoryRepository = repository;
        }

        public async Task<Category> AddAsync(CreateCategoryDto categoryDto)
        {
            Category newCategory = new Category()
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            await _categoryRepository.AddAsync(newCategory);

            return newCategory;
        }

        public async Task<IEnumerable<CategoryBaseDto>> SelectAllAsync()
        {
            IEnumerable<Category> categories = await _categoryRepository.SelectAllAsync();

            return categories.Select(cat => new CategoryBaseDto()
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description
            });
        }

        public async Task UpdateAsync(CategoryBaseDto categoryDto)
        {
            Category category = new Category()
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                Description = categoryDto.Description
            };

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<Category> SelectByIdAsync(int id)
        {
            return await _categoryRepository.SelectByIdAsync(id);
        }
        public async Task<CategoryDetailsDto> SelectByIdAsync(int id, QueryOptions<Category> options)
        {
            Category category = await _categoryRepository.SelectByIdAsync(id, options);
            return new CategoryDetailsDto() {
                Category = new CategoryBaseDto()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                },
                Dishes = category.Dishes.Select(dish => new DishBaseDto()
                {
                    Id = dish.Id,
                    Name = dish.Name
                })
            };
        }
    }
}
