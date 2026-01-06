using FarmManagement.Modules.Advisory.Application.Interfaces;
using FarmManagement.Modules.Advisory.Infrastructure.Persistence;
using FarmManagement.Modules.Advisory.Infrastructure.Repositories;
using FarmManagement.Modules.Advisory.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Services.AddDbContext<AdvisoryDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AdvisoryDb")));


builder.Services.AddScoped<IAdvisoryReportRepository, AdvisoryReportRepository>();
builder.Services.AddScoped<IAdvisoryRiskAssessmentService, AdvisoryRiskAssessmentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();
app.Run();
