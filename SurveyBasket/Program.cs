

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


app.UseAuthorization();

app.MapControllers();

app.Run();
