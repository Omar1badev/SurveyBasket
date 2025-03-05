namespace SurveyBasket.Mapping;

public class MappingConfigration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.NewConfig<PollRequest, PollResponse>()
        //    .Map(dest => dest.Id, src => src.Summary);
    }
}
