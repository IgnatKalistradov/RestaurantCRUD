namespace Restaurant.Infrastructure.Storage.Minio;

public record MinioSettings
{
    public string Endpoint {get; set;} = string.Empty;
    public string AccessKey {get; set;} = string.Empty;
    public string SecretKey {get; set;} = string.Empty;
    public string BucketName {get; set;} = string.Empty;
}