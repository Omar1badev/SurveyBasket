namespace SurveyBasket.Contracts.Auth;

public record ForgetPasswordRequest
(
    [EmailAddress]
    [Required]
    string Email
    );
