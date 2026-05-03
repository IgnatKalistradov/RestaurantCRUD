namespace TequilasRestaurant.Models
{
    public interface IImageService
    {
        Task<string> SaveImageToRoot(IFormFile file);
        void DeleteImageFromRoot(string fileName);
    }
}
