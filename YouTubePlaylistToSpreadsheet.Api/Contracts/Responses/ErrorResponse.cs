namespace YouTubePlaylistToSpreadsheetApi.Contracts.Responses;

public class ErrorResponse : BaseResponse
{
    public string ErrorStatus { get; init; } = null!;
    public string Message { get; init; } = null!;
}

public enum ErrorStatus
{
    EmptyRequest,
    NotAValidUrl,
    NotYoutubeUrl,
    NotYoutubePlaylist,
    IdNotFound,
}
