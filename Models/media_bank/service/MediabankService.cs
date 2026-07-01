
using PrePerchaseServer.Models.mediaBank;

public class MediabankService : IMediabankService
{
    private readonly IMediabankRepository _repo;
    private readonly StorageService _storage;
    private readonly IConfiguration _config;
    public MediabankService(
        IMediabankRepository repo,
        StorageService storage,
        IConfiguration config)
    {
        _repo = repo;
        _storage = storage;
        _config = config;
    }

    public async Task<Mediabank> UploadAsync(IFormFile file, CreateMediabankDto dto)
    {
        var bucket = _config["AWS:S3Bucket"];

        if (string.IsNullOrWhiteSpace(bucket))
        {
            throw new InvalidOperationException(
                "AWS:S3Bucket is missing from appsettings.json."
            );
        }

        var upload = await _storage.UploadAsync(
            bucket,
            dto.Module,
            file
        );

        var media = new Mediabank
        {
            Bucket = bucket,
            Key = upload.key,
            Url = upload.url,
            OriginalName = file.FileName,
            FileName = file.FileName,
            MimeType = file.ContentType,
            FileType = file.ContentType.StartsWith("image")
                ? "IMAGE"
                : "DOCUMENT",
            Size = file.Length,
            Module = dto.Module,
            Submodule = dto.Submodule,
            Type = dto.Type,
            Status = dto.Status ?? "INACTIVE"
        };

        return await _repo.CreateAsync(media);
    }
    public Task<List<Mediabank>> GetAllAsync()
    {
        return _repo.GetAllAsync();
    }
    public Task DeleteAsync(Guid id)
    {
        return _repo.DeleteAsync(id);
    }
}