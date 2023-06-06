namespace YouTubePlaylistToSpreadsheetApi.Contracts.DTO;

public class PlaylistVideo
{
    public string? Id { get; set; }
    public string? ChannelName { get; set; }
    public string? ChannelId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string? ThumbnailUrl { get; set; }
}
