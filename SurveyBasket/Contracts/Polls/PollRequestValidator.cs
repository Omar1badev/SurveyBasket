
namespace SurveyBasket.Contracts.Polls;

public class PollRequestValidator : AbstractValidator<PollRequest>
{

    public PollRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .Length(3,15);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required");
        
    }
}
