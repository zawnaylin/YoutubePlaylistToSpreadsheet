namespace YouTubePlaylistToSpreadsheetApi.Contracts.DTO;

public class Playlist
{
    public string? Id { get; set; }
    public List<PlaylistVideo>? Videos { get; set; }
}
