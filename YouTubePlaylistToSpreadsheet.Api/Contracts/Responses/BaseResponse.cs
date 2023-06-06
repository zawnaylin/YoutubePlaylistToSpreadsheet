namespace YouTubePlaylistToSpreadsheetApi.Contracts.Responses;

public abstract class BaseResponse
{
    public Guid ResponseId { get; } = Guid.NewGuid();
}
