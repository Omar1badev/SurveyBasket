namespace SurveyBasket.Contracts.Auth.RefreshToken;

public record RefreshTokenRequest
(
    string Token,
    string RefreshToken
);
