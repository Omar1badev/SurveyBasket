using SurveyBasket.Contracts.Questions;

namespace SurveyBasket.Mapping;

public class MappingConfigration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<QuestionRequest, Question>()
            .Map(dest=>dest.Answers , src => src.Answers.Select(answer=>new Answer { Content = answer}));
    }
}
