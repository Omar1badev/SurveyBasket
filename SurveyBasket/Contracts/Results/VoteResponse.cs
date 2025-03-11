namespace SurveyBasket.Contracts.Results;

public record VoteResponse
(
    string VoterName,
    DateTime VoteTime,
    IEnumerable<QuestionAnswerResponse> SelectedAnswers
    );
