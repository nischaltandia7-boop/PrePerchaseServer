namespace PrePerchaseServer.Models.mediaBank;
public class MediabankResponseDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Bucket { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public string OriginalName { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;

    public string FileType { get; set; } = string.Empty; // IMAGE / DOCUMENT / VIDEO

    public long Size { get; set; }

    public string Module { get; set; } = string.Empty;
    public string? Submodule { get; set; }

    public string? Type { get; set; } // BANNER / GALLERY etc

    public string Status { get; set; } = "INACTIVE";

    public int? Width { get; set; }
    public int? Height { get; set; }
    public string? Orientation { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}