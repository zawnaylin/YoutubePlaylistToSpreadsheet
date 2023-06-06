namespace YouTubePlaylistToSpreadsheetApi.Services;

public interface IBodyParamUtils
{
    public bool IsValidUrl(string url, out Uri? uriResult);

    public bool IsYouTubeDomain(Uri uri);

    public bool IsYouTubePlaylist(Uri uri);

    public bool TryExtractYouTubePlaylistId(Uri uri, out string? playlistId);
}
