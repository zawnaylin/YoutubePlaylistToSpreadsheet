namespace YouTubePlaylistToSpreadsheetApi.Mappers;

using Contracts.DTO;
using Contracts.Responses;
using Google.Apis.YouTube.v3.Data;

public static class YoutubePlaylistItemToPlaylistVideo
{
    public static PlaylistVideo ConvertToPlaylistVideo(this PlaylistItem playlistItem) => new PlaylistVideo
    {
        Id = playlistItem.Id,
        Title = playlistItem.Snippet.Title,
        ChannelId = playlistItem.Snippet.ChannelId,
        ChannelName = playlistItem.Snippet.ChannelTitle,
        Description = playlistItem.Snippet.Description,
        PublishedAt = playlistItem.Snippet.PublishedAt,
        ThumbnailUrl = playlistItem.Snippet.Thumbnails.Default__.Url,
    };
}
