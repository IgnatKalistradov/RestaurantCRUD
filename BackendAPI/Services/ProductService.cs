using BackendAPI.Models;
using BackendAPI.Models.DbModels;
using BackendAPI.Models.DTO.ProductsDto;

namespace BackendAPI.Services
{
    public class ProductService
    {
        private IRepository<Product> _productRepository;
        private IImageService _imageService;
        public ProductService(IRepository<Product> repository, IImageService imageService)
        {
            _productRepository = repository;
            _imageService = imageService;
        }
        public async Task<Product> AddAsync(ProductCreateDto productDto)
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
            if (product.ImageFile != null)
            {
                string imageUrl = await _imageService.SaveImageToRoot(product.ImageFile);

                product.ImageUrl = imageUrl;
            }

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

        public async Task UpdateAsync(Product product, int[] ingredientIds)
        {
            var query = new QueryOptions<Product>();
            query.AddInclude("ProductIngredients.Ingredient");

            Product existingProduct = await _productRepository.SelectByIdAsync(product.ProductId, query);
            if(existingProduct == null)
            {
                throw new InvalidDataException("Product was not found");
            }

            if (product.ImageFile != null)
            {
                string imageUrl = await _imageService.SaveImageToRoot(product.ImageFile);

                if(!string.IsNullOrEmpty(existingProduct.ImageUrl))
                {
                    _imageService.DeleteImageFromRoot(existingProduct.ImageUrl);
                }

                product.ImageUrl = imageUrl;
            }

            existingProduct.Copy(product);
            existingProduct.SetIngredients(ingredientIds);

            await _productRepository.UpdateAsync(existingProduct);
        }

        public async Task DeleteAsync(int id)
        {
            Product product = await _productRepository.SelectByIdAsync(id);
            if(!string.IsNullOrEmpty(product.ImageUrl))
            {
                //_imageService.DeleteImageFromRoot(product.ImageUrl);
            }
            await _productRepository.DeleteAsync(id);
        }

        public async Task<Product> SelectByIdAsync(int id)
        {
            return await _productRepository.SelectByIdAsync(id);
        }
        public async Task<Product> SelectByIdAsync(int id, QueryOptions<Product> options)
        {
            return await _productRepository.SelectByIdAsync(id, options);
        }

    }
}
