namespace YouTubePlaylistToSpreadsheetApi.Services;

using Contracts.DTO;
using Google;
using Google.Apis.Services;
using Google.Apis.Util;
using Google.Apis.YouTube.v3;
using Mappers;

public class YouTubeServiceProvider : IYouTubeServiceProvider
{
    private readonly IConfiguration config;

    public YouTubeServiceProvider(IConfiguration config) => this.config = config;


    public async Task GetVideosFromPlaylistAsync(string playlistId, )
    {
        try
        {
            var youtubeApiKey = this.config["YouTube:ApiKey"];
            var youtubeService = new YouTubeService(new BaseClientService.Initializer { ApiKey = youtubeApiKey, });

            var parts = new Repeatable<string>(new[] { "snippet" });

            var result = new List<PlaylistVideo>();


            var nextPageToken = "";

            while (nextPageToken is not null)
            {
                var playlistItemsRequest = youtubeService.PlaylistItems.List(parts);
                playlistItemsRequest.PlaylistId = playlistId;
                playlistItemsRequest.MaxResults = 50;
                playlistItemsRequest.PageToken = nextPageToken;

                var playlistItemsResponse = await playlistItemsRequest.ExecuteAsync();

                result.AddRange(playlistItemsResponse.Items.Select((playlistItem) =>
                    playlistItem.ConvertToPlaylistVideo()));


                nextPageToken = playlistItemsResponse.NextPageToken;
            }
        }
        catch (GoogleApiException exception)
        {
            return false;
        }
    }
}
