# PowerShell script to create EF Core migrations for all modules
# Run this script from the FarmManagement directory

Write-Host "=== Creating EF Core Migrations ===" -ForegroundColor Green
Write-Host ""

# Check if dotnet ef tool is installed
Write-Host "Checking EF Core tools..." -ForegroundColor Yellow
$efInstalled = dotnet tool list -g | Select-String "dotnet-ef"
if (-not $efInstalled) {
    Write-Host "Installing dotnet-ef tool..." -ForegroundColor Yellow
    dotnet tool install --global dotnet-ef
} else {
    Write-Host "Updating dotnet-ef tool to latest version..." -ForegroundColor Yellow
    dotnet tool update --global dotnet-ef
}

Write-Host ""
Write-Host "=== Creating Migrations ===" -ForegroundColor Green

$farmStartup = "src\FarmManagement.WebApi\FarmManagement.WebApi.csproj"

# Farm Module
Write-Host "Creating migration for Farm module..." -ForegroundColor Cyan
$farmPath = "src\FarmManagement.Modules.Farm\FarmManagement.Modules.Farm.Infrastructure\FarmManagement.Modules.Farm.Infrastructure.csproj"
$farmContext = "FarmManagement.Modules.Farm.Infrastructure.Persistence.FarmDbContext"

dotnet ef migrations add InitialCreate `
  --project $farmPath `
  --startup-project $farmStartup `
  --context $farmContext `
  --output-dir "Persistence\Migrations"

if ($LASTEXITCODE -eq 0) {
    Write-Host "[OK] Farm migration created successfully" -ForegroundColor Green
} else {
    Write-Host "[FAILED] Failed to create Farm migration" -ForegroundColor Red
}

# Crop Module
Write-Host ""
Write-Host "Creating migration for Crop module..." -ForegroundColor Cyan
$cropPath = "src\FarmManagement.Modules.Crop\FarmManagement.Modules.Crop.Infrastructure\Crop.Infrastructure.csproj"
$cropContext = "FarmManagement.Modules.Crop.Infrastructure.Persistence.CropDbContext"

dotnet ef migrations add InitialCreate `
  --project $cropPath `
  --startup-project $farmStartup `
  --context $cropContext `
  --output-dir "Persistence\Migrations"

if ($LASTEXITCODE -eq 0) {
    Write-Host "[OK] Crop migration created successfully" -ForegroundColor Green
} else {
    Write-Host "[FAILED] Failed to create Crop migration" -ForegroundColor Red
}

# Advisory Module
Write-Host ""
Write-Host "Creating migration for Advisory module..." -ForegroundColor Cyan
$advisoryPath = "src\FarmManagement.Modules.Advisory\FarmManagement.Modules.Advisory.Infrastructure\FarmManagement.Modules.Advisory.Infrastructure.csproj"
$advisoryContext = "FarmManagement.Modules.Advisory.Infrastructure.Persistence.AdvisoryDbContext"

dotnet ef migrations add InitialCreate `
  --project $advisoryPath `
  --startup-project $farmStartup `
  --context $advisoryContext `
  --output-dir "Persistence\Migrations"

if ($LASTEXITCODE -eq 0) {
    Write-Host "[OK] Advisory migration created successfully" -ForegroundColor Green
} else {
    Write-Host "[FAILED] Failed to create Advisory migration" -ForegroundColor Red
}

Write-Host ""
Write-Host "=== Migrations Created ===" -ForegroundColor Green
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "1. Review migration files to ensure OutboxMessages table is included" -ForegroundColor White
Write-Host "2. Start Docker services: docker-compose up -d" -ForegroundColor White
Write-Host "3. Wait for SQL Server to be ready (about 30 seconds)" -ForegroundColor White
Write-Host "4. Run: .\scripts\update-database.ps1" -ForegroundColor White
