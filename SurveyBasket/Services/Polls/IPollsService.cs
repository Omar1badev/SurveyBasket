namespace SurveyBasket.Services.Polls;

public interface IPollsService
{
    Task<PollResponse> CreatePollAsync(PollRequest pollRequest);
    Task<PollResponse> GetPollByIdAsync(int pollId);
    Task<IEnumerable<PollResponse>> GetPollsAsync();
    Task<PollResponse> UpdatePollAsync(int pollId, PollRequest pollRequest);
    bool DeletePollAsync(int pollId,CancellationToken cancellationToken = default);

    Task<bool> ToggleStatus(int Id);
}
