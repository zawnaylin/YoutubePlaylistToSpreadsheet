using YouTubePlaylistToSpreadsheetApi.Contracts.Responses;

namespace YouTubePlaylistToSpreadsheetApi.Services;

public interface IErrorResponseFactory
{
    public ErrorResponse Create(ErrorStatus status, string message);
}
