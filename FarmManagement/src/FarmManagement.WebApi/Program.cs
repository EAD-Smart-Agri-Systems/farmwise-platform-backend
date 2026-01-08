using FarmManagement.WebApi.Extensions;
using FarmManagement.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Core services
// --------------------
builder.Services.AddControllers();

// --------------------
// Swagger
// --------------------
builder.Services.AddSwaggerDocumentation();

// --------------------
// Authentication & Authorization
// --------------------
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorizationPolicies();

// --------------------
// Modules
// --------------------
builder.Services.AddModules(builder.Configuration);

var app = builder.Build();

// --------------------
// Middleware pipeline
// --------------------
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
