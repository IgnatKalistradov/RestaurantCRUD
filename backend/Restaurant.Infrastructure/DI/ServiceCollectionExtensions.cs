using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Restaurant.Infrastructure.Storage;
using Restaurant.Infrastructure.Storage.Minio;

namespace Restaurant.Infrastructure.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<MinioSettings>(config.GetSection("Minio"));

        var settings = config.GetSection("Minio").Get<MinioSettings>();
        if(settings == null)
        {
            throw new Exception("Does not found Minio section in appsettings.json");
        }
        services.AddSingleton<IMinioClient>(_ => new MinioClient().WithCredentials(settings.AccessKey, settings.SecretKey).WithEndpoint(settings.Endpoint).Build());

        services.AddScoped<IStorageService, MinioStorage>();

        services.AddHostedService<MinioInitializer>();

        return services;
    }
}