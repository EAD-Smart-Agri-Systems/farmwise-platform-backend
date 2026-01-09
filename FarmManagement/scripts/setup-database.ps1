# PowerShell script to create EF Core migrations and update databases
# Run this script from the FarmManagement directory

Write-Host "=== Farm Management Database Setup ===" -ForegroundColor Green
Write-Host ""

# Check if dotnet ef tool is installed
Write-Host "Checking EF Core tools..." -ForegroundColor Yellow
$efInstalled = dotnet tool list -g | Select-String "dotnet-ef"
if (-not $efInstalled) {
    Write-Host "Installing dotnet-ef tool..." -ForegroundColor Yellow
    dotnet tool install --global dotnet-ef
}

Write-Host ""
Write-Host "=== Creating Migrations ===" -ForegroundColor Green

# Farm Module
Write-Host "Creating migration for Farm module..." -ForegroundColor Cyan
$farmPath = "src\FarmManagement.Modules.Farm\FarmManagement.Modules.Farm.Infrastructure"
$farmContext = "FarmManagement.Modules.Farm.Infrastructure.Persistence.FarmDbContext"
$farmStartup = "src\FarmManagement.WebApi\FarmManagement.WebApi.csproj"

dotnet ef migrations add InitialCreate --project $farmPath --startup-project $farmStartup --context $farmContext

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Farm migration created successfully" -ForegroundColor Green
} else {
    Write-Host "✗ Failed to create Farm migration" -ForegroundColor Red
}

# Crop Module
Write-Host ""
Write-Host "Creating migration for Crop module..." -ForegroundColor Cyan
$cropPath = "src\FarmManagement.Modules.Crop\FarmManagement.Modules.Crop.Infrastructure"
$cropContext = "FarmManagement.Modules.Crop.Infrastructure.Persistence.CropDbContext"

dotnet ef migrations add InitialCreate --project $cropPath --startup-project $farmStartup --context $cropContext

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Crop migration created successfully" -ForegroundColor Green
} else {
    Write-Host "✗ Failed to create Crop migration" -ForegroundColor Red
}

# Advisory Module
Write-Host ""
Write-Host "Creating migration for Advisory module..." -ForegroundColor Cyan
$advisoryPath = "src\FarmManagement.Modules.Advisory\FarmManagement.Modules.Advisory.Infrastructure"
$advisoryContext = "FarmManagement.Modules.Advisory.Infrastructure.Persistence.AdvisoryDbContext"

dotnet ef migrations add InitialCreate --project $advisoryPath --startup-project $farmStartup --context $advisoryContext

if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ Advisory migration created successfully" -ForegroundColor Green
} else {
    Write-Host "✗ Failed to create Advisory migration" -ForegroundColor Red
}

Write-Host ""
Write-Host "=== Migrations Created ===" -ForegroundColor Green
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "1. Start Docker services: docker-compose up -d" -ForegroundColor White
Write-Host "2. Wait for SQL Server to be ready (about 30 seconds)" -ForegroundColor White
Write-Host "3. Run: .\scripts\update-database.ps1" -ForegroundColor White
