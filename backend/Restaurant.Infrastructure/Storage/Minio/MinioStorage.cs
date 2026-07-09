using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace Restaurant.Infrastructure.Storage.Minio;

public class MinioStorage : IStorageService
{
    private readonly IMinioClient _client;
    private readonly MinioSettings _settings;
    public MinioStorage(IMinioClient client, IOptions<MinioSettings> options)
    {
        _client = client;
        _settings = options.Value;
    }

    private string GetObjectNameFromPath(string objectPath)
    {
        return objectPath.Substring(objectPath.LastIndexOf('/') + 1);
    }

    public async Task DeleteImageAsync(string objectPath)
    {
        string objectName = GetObjectNameFromPath(objectPath);

        var removeObjectArgs = new RemoveObjectArgs().WithBucket(_settings.BucketName).WithObject(objectName);
        await _client.RemoveObjectAsync(removeObjectArgs);
    }

    public async Task<string> UploadImageAsync(string objectName, Stream objectData, string contentType)
    {
        var putObjectArgs = new PutObjectArgs().WithBucket(_settings.BucketName).WithContentType(contentType).WithObject(objectName).WithStreamData(objectData).WithObjectSize(objectData.Length);

        await _client.PutObjectAsync(putObjectArgs).ConfigureAwait(false);

        return $"{_settings.BucketName}/{objectName}";
    }
}