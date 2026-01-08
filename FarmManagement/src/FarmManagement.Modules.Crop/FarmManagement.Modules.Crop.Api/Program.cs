using FarmManagement.Modules.Crop.Application;
using FarmManagement.Modules.Crop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddOpenApi();

// Register Application layer (MediatR)
builder.Services.AddCropApplication();

// Register Infrastructure layer (DbContext, Repositories)
builder.Services.AddCropInfrastructure(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
