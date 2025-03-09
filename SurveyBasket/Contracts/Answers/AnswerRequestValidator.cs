

namespace SurveyBasket.Contracts.Answers;

public class AnswerRequestValidator : AbstractValidator<AnswerRequest>
{

    public AnswerRequestValidator()
    {
        //    RuleFor(x => x.Title)
        //        .NotEmpty()
        //        .WithMessage("Title is required")
        //        .Length(3,55);

        //    RuleFor(x => x.Summary)
        //        .NotEmpty()
        //        .WithMessage("Description is required")
        //        .Length(20, 500);

        //    RuleFor(RuleFor => RuleFor.StartsAt)
        //        .NotEmpty()
        //        .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));

        //    RuleFor(RuleFor => RuleFor.StartsAt)
        //        .NotEmpty();

        //    RuleFor(x=>x)
        //        .Must(validdate)
        //        .WithMessage("End date must be greater than start date");

        //}

        //private bool validdate(PollRequest request)
        //{
        //    return request.StartsAt <= request.EndsAt;
        //}
    }
}
