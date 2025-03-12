

using Hangfire;
using HangfireBasicAuthenticationFilter;
using Serilog;
using SurveyBasket.Services.Notification;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddDependencies(builder.Configuration);


builder.Host.UseSerilog((context, configration) =>
    configration
    .ReadFrom.Configuration(context.Configuration)
    //.WriteTo.Console()
    );

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard("/jobs", new DashboardOptions
{
    Authorization = [
        new HangfireCustomBasicAuthenticationFilter
        {
            User = app.Configuration.GetValue<string>("HangfireSettings:Username"),
            Pass = app.Configuration.GetValue<string>("HangfireSettings:Password")
                   }],
    DashboardTitle = "Survey Basket Jobs",
});

var scopeFactory = app.Services
    .GetRequiredService<IServiceScopeFactory>();

using var scope = app.Services
    .CreateScope();

var notificationService = scope.ServiceProvider
    .GetRequiredService<INotificationService>();


RecurringJob
    .AddOrUpdate("SendNewPollNotification", ()=> notificationService.SendNewPollNotification(null) , Cron.Daily);



app.UseSerilogRequestLogging();
app.UseExceptionHandler();   //  Handle exceptions

app.UseCors();     //  Allow all CORS requests
app.UseRouting();            //  Ensure routing happens before auth
app.UseAuthentication();     //  Authentication middleware
app.UseAuthorization();      //  Authorization middleware
app.MapControllers();        //  Map the controller endpoints


app.Run();
