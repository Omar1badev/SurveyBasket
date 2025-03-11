namespace SurveyBasket.Contracts.Results;

public record VotesPerAnswerResponse
(
    string answer,
    int count
    
    );