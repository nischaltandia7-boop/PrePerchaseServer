using Amazon.S3;
using Amazon.S3.Model;
public class StorageService
{
    private readonly IAmazonS3 _s3;
    private readonly IConfiguration _config;
    public StorageService(IAmazonS3 s3, IConfiguration config)
    {
        _s3 = s3;
        _config = config;
    }

    public async Task<(string url, string key)> UploadAsync(
        string bucket,
        string keyPrefix,
        IFormFile file)
    {
        var key = $"{keyPrefix}/{Guid.NewGuid()}-{file.FileName}";

        using var stream = file.OpenReadStream();

        var request = new PutObjectRequest
        {
            BucketName = bucket,
            Key = key,
            InputStream = stream,
            ContentType = file.ContentType
        };

        await _s3.PutObjectAsync(request);

        var url = $"https://{bucket}.s3.amazonaws.com/{key}";
        return (url, key);
    }
}