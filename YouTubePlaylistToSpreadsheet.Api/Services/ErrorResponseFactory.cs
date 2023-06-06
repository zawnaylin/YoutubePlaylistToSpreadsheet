namespace YouTubePlaylistToSpreadsheetApi.Services;

using Contracts.Responses;

public class ErrorResponseFactory : IErrorResponseFactory
{
    public ErrorResponse Create(ErrorStatus status, string message) => new ErrorResponse
    {
        ErrorStatus = status.ToString(), Message = message,
    };
}
