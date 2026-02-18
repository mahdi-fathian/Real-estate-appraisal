# Database scripts for Simab (سامانه سیماب)
# Run from solution root. Requires: dotnet ef (dotnet tool install --global dotnet-ef)

$ErrorActionPreference = "Stop"
$SolutionRoot = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
if (-not (Test-Path (Join-Path $SolutionRoot "MercurySystem.sln"))) {
    $SolutionRoot = $PSScriptRoot
}
Set-Location $SolutionRoot

$Infra = "src\Simab.Infrastructure\Simab.Infrastructure.csproj"
$Api = "src\Simab.Api\Simab.Api.csproj"

$action = $args[0]
if (-not $action) { $action = "update" }

switch ($action) {
    "add" {
        $name = $args[1]
        if (-not $name) { $name = "InitialCreate" }
        Write-Host "Adding migration: $name" -ForegroundColor Cyan
        dotnet ef migrations add $name --project $Infra --startup-project $Api
    }
    "update" {
        Write-Host "Applying migrations to database SimabDb..." -ForegroundColor Cyan
        dotnet ef database update --project $Infra --startup-project $Api
    }
    "drop" {
        Write-Host "Dropping database..." -ForegroundColor Yellow
        dotnet ef database drop --project $Infra --startup-project $Api --force
    }
    default {
        Write-Host "Usage: .\scripts\database.ps1 [add|update|drop] [MigrationName]" -ForegroundColor Gray
        Write-Host "  add    - Add a new migration (optional name, default: InitialCreate)" -ForegroundColor Gray
        Write-Host "  update - Apply pending migrations to SimabDb" -ForegroundColor Gray
        Write-Host "  drop   - Drop the database" -ForegroundColor Gray
    }
}

if ($LASTEXITCODE -ne 0) { exit 1 }
