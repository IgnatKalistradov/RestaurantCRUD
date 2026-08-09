namespace Restaurant.Application.Models.Dto;

public class AddImageDto : IDisposable
{
    public required string ContentType { get; init; }
    public required long Length { get; init; }
    public required string FileName { get; init; }
    public required Stream Stream { get; init; }

    public void Dispose()
    {
        Stream.Dispose();
    }
}