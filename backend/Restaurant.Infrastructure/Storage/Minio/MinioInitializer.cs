using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Minio.DataModel.Encryption;

namespace Restaurant.Infrastructure.Storage.Minio;

public class MinioInitializer : IHostedService
{
    private readonly IMinioClient _client;
    private readonly MinioSettings _settings;
    public MinioInitializer(IMinioClient client, IOptions<MinioSettings> options)
    {
        _settings = options.Value;
        _client = client;
    }

    private async Task<bool> CheckBucketExists(CancellationToken cancellationToken)
    {
        var existsArgs = new BucketExistsArgs().WithBucket(_settings.BucketName);

        return await _client.BucketExistsAsync(existsArgs, cancellationToken);
    }

    private async Task CreateBucketIfNotExists(CancellationToken cancellationToken)
    {
        bool exists = await CheckBucketExists(cancellationToken);

        if(exists)
        {
            return;
        }

        var createArgs = new MakeBucketArgs().WithBucket(_settings.BucketName);

        await _client.MakeBucketAsync(createArgs, cancellationToken);

        var policy = $$"""
        {
            "Version": "2012-10-17",
            "Statement": [
                {
                    "Sid": "PublicReadGetObject",
                    "Effect": "Allow",
                    "Principal": "*",
                    "Action": [
                        "s3:GetObject"
                    ],
                    "Resource": [
                        "arn:aws:s3:::{{_settings.BucketName}}/*"
                    ]
                }
            ]
        }

        """;

        var policyArgs = new SetPolicyArgs().WithPolicy(policy).WithBucket(_settings.BucketName);

        await _client.SetPolicyAsync(policyArgs);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await CreateBucketIfNotExists(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}