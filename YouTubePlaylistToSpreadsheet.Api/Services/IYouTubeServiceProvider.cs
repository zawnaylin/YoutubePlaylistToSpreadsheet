namespace YouTubePlaylistToSpreadsheetApi.Services;

using Contracts.DTO;

public interface IYouTubeServiceProvider
{
    public Task<List<PlaylistVideo>> GetVideosFromPlaylistAsync(string playlistId);
}
