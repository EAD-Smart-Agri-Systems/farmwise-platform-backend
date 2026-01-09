# PowerShell script to start Docker services for local development

Write-Host "=== Starting Docker Services ===" -ForegroundColor Green
Write-Host ""

# Check if Docker is running
Write-Host "Checking Docker..." -ForegroundColor Yellow
try {
    docker ps | Out-Null
    Write-Host "✓ Docker is running" -ForegroundColor Green
} catch {
    Write-Host "✗ Docker is not running. Please start Docker Desktop." -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Starting services (SQL Server, RabbitMQ, Keycloak)..." -ForegroundColor Yellow
Write-Host "This may take a few minutes on first run..." -ForegroundColor Yellow
Write-Host ""

# Navigate to directory with docker-compose.yml
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
$composePath = Join-Path $scriptPath "..\docker-compose.yml"

docker-compose -f $composePath up -d

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✓ Services started successfully" -ForegroundColor Green
    Write-Host ""
    Write-Host "Waiting for services to be healthy..." -ForegroundColor Yellow
    Start-Sleep -Seconds 10
    
    Write-Host ""
    Write-Host "Service URLs:" -ForegroundColor Cyan
    Write-Host "  SQL Server:    localhost:1433" -ForegroundColor White
    Write-Host "  RabbitMQ:      http://localhost:15672 (guest/guest)" -ForegroundColor White
    Write-Host "  Keycloak:      http://localhost:8080 (admin/admin)" -ForegroundColor White
    Write-Host ""
    Write-Host "Next steps:" -ForegroundColor Yellow
    Write-Host "1. Wait 30-60 seconds for SQL Server to be fully ready" -ForegroundColor White
    Write-Host "2. Run: .\scripts\setup-database.ps1" -ForegroundColor White
    Write-Host "3. Run: .\scripts\update-database.ps1" -ForegroundColor White
} else {
    Write-Host ""
    Write-Host "✗ Failed to start services" -ForegroundColor Red
    Write-Host "Check Docker logs: docker-compose logs" -ForegroundColor Yellow
}
