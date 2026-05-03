using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace TequilasRestaurant.Models.DbModels
{
    public class Product
    {
        public Product()
        {
            ProductIngredients = new List<ProductIngredient>();
        }

        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category? Category { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string ImageUrl { get; set; } = "https://placehold.co/600x400";
        [ValidateNever]
        public ICollection<OrderItem>? OrderItems { get; set; }
        [ValidateNever]
        public ICollection<ProductIngredient> ProductIngredients { get; set; }

        public void Copy(Product product)
        {
            this.Name = product.Name;
            this.Description = product.Description;
            this.Price = product.Price;
            this.Stock = product.Stock;
            this.CategoryId = product.CategoryId;
            this.ImageUrl = product.ImageUrl;
        }

        public void SetIngredients(IEnumerable<int> ingredientIds)
        {
            this.ProductIngredients.Clear();

            foreach (int ingredientId in ingredientIds)
            {
                this.ProductIngredients.Add(new ProductIngredient() { ProductId = this.ProductId, IngredientId = ingredientId });
            }
        }
    }
}