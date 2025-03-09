namespace SurveyBasket.Contracts.Votes;

public record VotesRequest
(
    IEnumerable<VotesAnswerRequest> Answers

    );
