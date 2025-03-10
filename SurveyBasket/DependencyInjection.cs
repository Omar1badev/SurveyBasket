﻿
namespace SurveyBasket;

public static class DependencyInjection
{

    public static IServiceCollection AddDependencies(this IServiceCollection Services , IConfiguration configuration)
    {

        Services.AddControllers();

        Services.AddEndpointsApiExplorer();

        Services.AddScoped<IPollsService, PollsService>();
        Services.AddScoped<IVotesService, VotesService>();
        Services.AddScoped<IQuestionService, QuestionService>();
        Services.AddScoped<IAuthService,AuthService>();
        Services.AddScoped<IJwtProvider, JwtProvider>();

        Services.AddExceptionHandler<GlobalExceptionHandler>();
        Services.AddProblemDetails();

        Services.AddAuth(configuration)
                .AddMappester()
                .AddFluentValidation()
                .AddSwagger()
                .AddDatabase(configuration)
                ;


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
    
    public static IServiceCollection AddDatabase(this IServiceCollection Services, IConfiguration c)
    {
        var ConnectionString = c.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string is not found in the configuration file");

        Services.AddDbContext<ApplicationDbcontext>(options =>
            options.UseSqlServer(ConnectionString));

        return Services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection Services, IConfiguration configuration)
    {

        Services.AddIdentity<ApplicataionUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbcontext>();

        Services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        var Jwtsetting = configuration.GetSection("Jwt").Get<JwtOptions>();

        Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {


                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = Jwtsetting?.Audience,
                ValidIssuer = Jwtsetting?.Issuer,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Jwtsetting?.Key!))
            };
        });

        return Services;
    }
    public static IServiceCollection AddCORS(this IServiceCollection Services)
    {
        Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder=>
                builder
                        //.WithMethods("GET", "POST", "PUT", "DELETE")
                        //.WithOrigins("http://localhost:3000")
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader() );
        });
        return Services;
    }


        
    }



