namespace PrePerchaseServer.Models.mediaBank;
public class MediabankResponseFileDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Url { get; set; } = string.Empty;
    public string OriginalName { get; set; } = string.Empty;
}