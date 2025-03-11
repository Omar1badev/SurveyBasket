namespace SurveyBasket.Contracts.Results;

public record VotesPerQuestionResponse
(
    string Quesstion,
    IEnumerable<VotesPerAnswerResponse> selectedanswers
    );
