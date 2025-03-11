namespace SurveyBasket.Contracts.Results;

public record PollVotesResponse
(
    string PollName,
    IEnumerable<VoteResponse> Votes
    );
