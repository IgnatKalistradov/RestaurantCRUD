using BackendAPI.Models;
using BackendAPI.Models.DbModels;
using BackendAPI.Models.DTO.CategoryDto;
using BackendAPI.Models.DTO.IngredientDto;
using BackendAPI.Models.DTO.ProductsDto;

namespace BackendAPI.Services
{
    public class ProductService
    {
        private IRepository<Product> _productRepository;
        public ProductService(IRepository<Product> repository)
        {
            _productRepository = repository;
        }
        public async Task<Product> AddAsync(ProductUpsertDto productDto)
        {
            Product product = new Product()
            {
                ProductId = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Stock = productDto.Stock,
                CategoryId = productDto.CategoryId,
            };

            product.SetIngredients(productDto.IngredientIds);

            await _productRepository.AddAsync(product);

            return product;
        }

        public async Task AddAsync(Product product, int[] ingredientIds)
        {
            product.SetIngredients(ingredientIds);

            await _productRepository.AddAsync(product);
        }

        public async Task<IEnumerable<ProductInfoDto>> SelectAllAsync()
        {
            IEnumerable<Product> products = await _productRepository.SelectAllAsync();
            return products.Select(prod => new ProductInfoDto()
            {
                Id = prod.ProductId,
                Name = prod.Name,
                Description = prod.Description,
                Price = prod.Price,
                Stock = prod.Stock
            });
        }

        public async Task UpdateAsync(ProductUpsertDto productDto)
        {
            QueryOptions<Product> options = new QueryOptions<Product>();
            options.AddInclude("ProductIngredients.Ingredient");

            Product existingProduct = await _productRepository.SelectByIdAsync(productDto.Id, options);
            if(existingProduct == null)
            {
                throw new InvalidDataException("Product was not found");
            }

            existingProduct.Copy(productDto);
            existingProduct.SetIngredients(productDto.IngredientIds);

            await _productRepository.UpdateAsync(existingProduct);
        }

        public async Task DeleteAsync(int id)
        {
            Product product = await _productRepository.SelectByIdAsync(id);

            await _productRepository.DeleteAsync(id);
        }

        public async Task<ProductDetailsDto> SelectByIdAsync(int id)
        {
            QueryOptions<Product> options = new QueryOptions<Product>();
            options.AddInclude("Category");
            options.AddInclude("ProductIngredients.Ingredient");

            Product product = await _productRepository.SelectByIdAsync(id, options);
            ProductDetailsDto productDto = new ProductDetailsDto()
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Category = new CategoryBaseDto()
                {
                    Id = product.CategoryId,
                    Name = product.Category.Name,
                    Description = product.Category.Description
                },
                Ingredients = product.ProductIngredients.Select(pi => new IngredientBaseDto()
                {
                    Id = pi.IngredientId,
                    Name = pi.Ingredient.Name,
                    Description = pi.Ingredient.Description
                })
            };
            return productDto;
        }
    }
}
