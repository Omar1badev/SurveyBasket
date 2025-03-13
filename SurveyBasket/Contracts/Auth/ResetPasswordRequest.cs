namespace SurveyBasket.Contracts.Auth;

public record ResetPasswordRequest
(
    string Email,
    string Code,
    string Password
    );
