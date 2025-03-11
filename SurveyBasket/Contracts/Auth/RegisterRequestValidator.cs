using SurveyBasket.Abstraction.Consts;

namespace SurveyBasket.Contracts.Auth;

public class RegisterRequestValidator :  AbstractValidator<RegisterRequest>
{

    public RegisterRequestValidator()
{
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .Length(8, 50);

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .Matches(RegexPatterns.Password)
            .WithMessage("Password should be 8 digits and should contains Lowercase,Uppercase,Number and Special character ");
            
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("LastName is required")
            .Length(3, 100); ;
    } }

