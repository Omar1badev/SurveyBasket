
namespace SurveyBasket.Contracts.Auth;

public class AuthRequestValidator : AbstractValidator<AuthRequest>
{

    public AuthRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress();
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .Length(8,50);

    } }
