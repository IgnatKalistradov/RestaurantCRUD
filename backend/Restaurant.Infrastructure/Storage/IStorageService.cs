namespace Restaurant.Infrastructure.Storage;

public interface IStorageService
{
    Task<string> UploadImageAsync(string objectName, Stream objectData, string contentType);
    Task DeleteImageAsync(string objectPath);
}