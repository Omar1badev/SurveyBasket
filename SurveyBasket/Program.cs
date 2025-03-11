

using Serilog;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

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

app.UseSerilogRequestLogging();
app.UseExceptionHandler();   //  Handle exceptions

app.UseCors();     //  Allow all CORS requests
app.UseRouting();            //  Ensure routing happens before auth
app.UseAuthentication();     //  Authentication middleware
app.UseAuthorization();      //  Authorization middleware
app.MapControllers();        //  Map the controller endpoints


app.Run();
