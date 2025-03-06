namespace SurveyBasket.Contracts.Auth;

public record AuthResponse
(
    string Id ,
    string email,
    string FirstName,
    string LastName,
    string Token,
    int ExpiresIn
    );
