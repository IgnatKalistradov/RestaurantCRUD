
using Microsoft.AspNetCore.Hosting;
using BackendAPI.Models.DbModels;
using BackendAPI.Models;

namespace BackendAPI.Services
{
    public class ImageService : IImageService
    {
        private IWebHostEnvironment _webHostEnvironment;
        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void DeleteImageFromRoot(string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                throw new InvalidDataException("File path is null or empty");
            }

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string filePath = Path.Combine(uploadsFolder, fileName);
            if(!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File by path: {filePath}, does not exist");
            }

            File.Delete(filePath);
        }
        public async Task<string> SaveImageToRoot(IFormFile file)
        {
            if (file == null)
            {
                throw new InvalidDataException("IFormFile is null");
            }

            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string uploadFilePath = Path.Combine(uploadsFolder, fileName);

            using (FileStream fileStream = new FileStream(uploadFilePath, FileMode.CreateNew))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}
