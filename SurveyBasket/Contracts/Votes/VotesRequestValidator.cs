using SurveyBasket.Contracts.Votes;

namespace SurveyBasket.Contracts.Votes;

public class VotesRequestValidator : AbstractValidator<VotesRequest>
{
    public VotesRequestValidator()
    {
        RuleFor(x => x.Answers)
            .NotEmpty();

        RuleForEach(x => x.Answers)
            .SetInheritanceValidator(v=>v.Add(new VotesAnswerRequestValidator()));

    }
}
