namespace YouTubePlaylistToSpreadsheetApi.Endpoints;

using Contracts.Requests;
using Contracts.Responses;
using Google;
using Microsoft.AspNetCore.Mvc;
using YouTubePlaylistToSpreadsheetApi.Services;

public static class YoutubePlaylist
{
    public static RouteGroupBuilder MapYoutubePlaylistApi(this RouteGroupBuilder group)
    {
        group.MapGet("/version", _getVersion);
        group.MapPost("/playlist", _fetchPlaylist);

        return group;
    }

    private static string _getVersion() => "v1.0.0";

    private static async Task<IResult> _fetchPlaylist([FromBody] PlaylistRequest? body, IBodyParamUtils paramUtils,
        IErrorResponseFactory errorResponseFactory, IYouTubeServiceProvider youTubeServiceProvider)
    {
        if (body?.Ids is null && body?.Urls is null)
        {
            return TypedResults.BadRequest(errorResponseFactory.Create(ErrorStatus.EmptyRequest,
                "The body should have either `ids` or `urls` property"));
        }

        if (body is { Urls.Count: < 1, Ids.Count: < 1 })
        {
            return TypedResults.BadRequest(errorResponseFactory.Create(ErrorStatus.EmptyRequest,
                "At least one id or url is required."));
        }


        var response = new List<object>();
        try
        {
            foreach (var id in body.Ids!)
            {
                var result = await youTubeServiceProvider.GetVideosFromPlaylistAsync(id);
                response.Add(result);
            }

            foreach (var url in body.Urls!)
            {
                if (!paramUtils.IsValidUrl(url, out var uriResult))
                {
                    response.Add(errorResponseFactory.Create(ErrorStatus.NotAValidUrl,
                        "The requested URL is not a valid URL."));

                    continue;
                }

                if (!paramUtils.IsYouTubeDomain(uriResult!))
                {
                    response.Add(errorResponseFactory.Create(ErrorStatus.NotYoutubeUrl,
                        "The requested URL is not a YouTube url."));
                    continue;
                }

                if (!paramUtils.IsYouTubePlaylist(uriResult!))
                {
                    response.Add(errorResponseFactory.Create(ErrorStatus.NotYoutubePlaylist,
                        "The requested URL is not a proper YouTube Playlist URL."));
                    continue;
                }

                if (!paramUtils.TryExtractYouTubePlaylistId(uriResult!, out var playlistId))
                {
                    response.Add(errorResponseFactory.Create(ErrorStatus.NotYoutubePlaylist,
                        "The requested URL does not have required query parameter or value."));
                    continue;
                }

                response.Add(await youTubeServiceProvider.GetVideosFromPlaylistAsync(playlistId!));
            }
        }
        catch (GoogleApiException ex)
        {

        }

        return TypedResults.Ok(response);
    }
}
