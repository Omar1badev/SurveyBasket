namespace SurveyBasket.Contracts.Questions;

public class VotesRequestValidator : AbstractValidator<QuestionRequest>
{
    public VotesRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x.Answers)
            .NotNull();

        RuleFor(x => x.Answers)
            .Must(x => x.Count > 1)
            .WithMessage("There must be at least two answers")
            .When(x=>x.Answers != null);

        RuleFor(x => x.Answers)
            .Must(x => x.Distinct().Count() == x.Count)
            .WithMessage("you can't add duplicated answer for same question")
            .When(x=>x.Answers != null);;
    }
}
