namespace YouTubePlaylistToSpreadsheetApi.Contracts.Requests;

public class PlaylistRequest
{
    public ICollection<string>? Ids { get; init; }

    public ICollection<string>? Urls { get; init; }
}
