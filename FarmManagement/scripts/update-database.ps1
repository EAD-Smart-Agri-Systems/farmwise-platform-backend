# PowerShell script to apply EF Core migrations to databases
# Make sure Docker services are running first: docker-compose up -d

Write-Host "=== Applying Database Migrations ===" -ForegroundColor Green
Write-Host ""

# Check if SQL Server is accessible
Write-Host "Checking SQL Server connection..." -ForegroundColor Yellow
$connectionString = "Server=localhost,1433;Database=master;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;Connect Timeout=5"

try {
    $connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $connection.Open()
    $connection.Close()
    Write-Host "[OK] SQL Server is accessible" -ForegroundColor Green
} catch {
    Write-Host "[ERROR] Cannot connect to SQL Server. Make sure Docker services are running:" -ForegroundColor Red
    Write-Host "  docker-compose up -d" -ForegroundColor Yellow
    exit 1
}

Write-Host ""
Write-Host "=== Applying Migrations ===" -ForegroundColor Green

$farmStartup = "src\FarmManagement.WebApi\FarmManagement.WebApi.csproj"

# Farm Module
Write-Host "Applying Farm module migration..." -ForegroundColor Cyan
$farmPath = "src\FarmManagement.Modules.Farm\FarmManagement.Modules.Farm.Infrastructure"
$farmContext = "FarmManagement.Modules.Farm.Infrastructure.Persistence.FarmDbContext"

dotnet ef database update --project $farmPath --startup-project $farmStartup --context $farmContext

if ($LASTEXITCODE -eq 0) {
    Write-Host "[OK] Farm database updated successfully" -ForegroundColor Green
} else {
    Write-Host "[ERROR] Failed to update Farm database" -ForegroundColor Red
}

# Crop Module
Write-Host ""
Write-Host "Applying Crop module migration..." -ForegroundColor Cyan
$cropPath = "src\FarmManagement.Modules.Crop\FarmManagement.Modules.Crop.Infrastructure"
$cropContext = "FarmManagement.Modules.Crop.Infrastructure.Persistence.CropDbContext"

dotnet ef database update --project $cropPath --startup-project $farmStartup --context $cropContext

if ($LASTEXITCODE -eq 0) {
    Write-Host "[OK] Crop database updated successfully" -ForegroundColor Green
} else {
    Write-Host "[ERROR] Failed to update Crop database" -ForegroundColor Red
}

# Advisory Module
Write-Host ""
Write-Host "Applying Advisory module migration..." -ForegroundColor Cyan
$advisoryPath = "src\FarmManagement.Modules.Advisory\FarmManagement.Modules.Advisory.Infrastructure"
$advisoryContext = "FarmManagement.Modules.Advisory.Infrastructure.Persistence.AdvisoryDbContext"

dotnet ef database update --project $advisoryPath --startup-project $farmStartup --context $advisoryContext

if ($LASTEXITCODE -eq 0) {
    Write-Host "[OK] Advisory database updated successfully" -ForegroundColor Green
} else {
    Write-Host "[ERROR] Failed to update Advisory database" -ForegroundColor Red
}

Write-Host ""
Write-Host "=== Database Setup Complete ===" -ForegroundColor Green
Write-Host ""
Write-Host "You can now run the application:" -ForegroundColor Yellow
Write-Host "  dotnet run --project src\FarmManagement.WebApi\FarmManagement.WebApi.csproj" -ForegroundColor White
