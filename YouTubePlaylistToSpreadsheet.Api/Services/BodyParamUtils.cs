namespace YouTubePlaylistToSpreadsheetApi.Services;

using System.Text.RegularExpressions;

public partial class BodyParamUtils : IBodyParamUtils
{
    public bool IsValidUrl(string url, out Uri? uriResult) =>
        Uri.TryCreate(url, UriKind.Absolute, out uriResult) &&
        (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);


    public bool IsYouTubeDomain(Uri uri) => YouTubeHostRegex().IsMatch(uri.Host);

    public bool IsYouTubePlaylist(Uri uri) => uri.AbsolutePath.Contains("playlist");

    public bool TryExtractYouTubePlaylistId(Uri uri, out string? playlistId)
    {
        const string listParam = "list=";

        var startIndex = uri.Query.IndexOf(listParam, StringComparison.Ordinal);

        if (startIndex == -1)
        {
            playlistId = null;
            return false;
        }

        var stopIndex = uri.Query.IndexOf("&", startIndex, StringComparison.Ordinal);

        playlistId = uri.Query[(startIndex + 4)..(stopIndex == -1 ? Index.End : stopIndex)];
        return true;
    }

    [GeneratedRegex("^(https?://)?(www\\.)?(youtube\\.com|youtu\\.be)$", RegexOptions.IgnoreCase)]
    private static partial Regex YouTubeHostRegex();
}
