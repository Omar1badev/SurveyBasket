
namespace SurveyBasket;

public static class DependencyInjection
{

    public static IServiceCollection AddDependencies(this IServiceCollection Services)
    {
        Services.AddControllers();

        Services.AddEndpointsApiExplorer();

        Services.AddScoped<IPollsService, PollsService>();



        Services.AddMappester()
                .AddFluentValidation()
                .AddSwagger();


        return Services;
    } 

    public static IServiceCollection AddSwagger(this IServiceCollection Services)
    {
        Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "SurveyBasket", Version = "v1" });
        });
        return Services;
    }


    public static IServiceCollection AddFluentValidation(this IServiceCollection Services)
    {
        Services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return Services;
    }


    public static IServiceCollection AddMappester(this IServiceCollection Services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());

        Services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        return Services;
    }


}
