# Build script for Simab (سامانه سیماب)
# Run from solution root: .\scripts\build.ps1

$ErrorActionPreference = "Stop"
$SolutionRoot = Split-Path -Parent (Split-Path -Parent $PSScriptRoot)
if (-not (Test-Path (Join-Path $SolutionRoot "MercurySystem.sln"))) {
    $SolutionRoot = $PSScriptRoot
}
Set-Location $SolutionRoot

Write-Host "Restoring packages..." -ForegroundColor Cyan
dotnet restore

Write-Host "Building solution..." -ForegroundColor Cyan
dotnet build --no-incremental -c Release

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed." -ForegroundColor Red
    exit 1
}

Write-Host "Build succeeded." -ForegroundColor Green
