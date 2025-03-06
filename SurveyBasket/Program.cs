

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddDependencies(builder.Configuration);


var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();            //  Ensure routing happens before auth
app.UseAuthentication();     //  Authentication middleware
app.UseAuthorization();      //  Authorization middleware
app.MapControllers();        //  Map the controller endpoints


app.Run();
