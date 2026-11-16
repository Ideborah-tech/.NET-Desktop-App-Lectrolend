# Starts the LectroLend API (Spring Boot) on http://localhost:8081
# Usage:  .\scripts\start-backend.ps1
# It asks for your PostgreSQL password (or reads DB_PASSWORD), creates the "lectrolend" database if
# it does not exist, then runs the app. Flyway builds the tables and demo data is loaded on first start.

$ErrorActionPreference = 'Stop'

# Spring Boot 3 needs Java 17+, and this project targets Java 21.
$jdk = @('C:\Program Files\Eclipse Adoptium', 'C:\Program Files\Java') |
    Where-Object { Test-Path $_ } |
    ForEach-Object { Get-ChildItem $_ -Directory -Filter 'jdk-2*' } |
    Sort-Object Name | Select-Object -First 1
if ($jdk) {
    $env:JAVA_HOME = $jdk.FullName
    $env:Path = "$($jdk.FullName)\bin;$env:Path"
}
Write-Host "Using Java: $(& java -version 2>&1 | Select-Object -First 1)"

if (-not $env:DB_USER) { $env:DB_USER = 'postgres' }
if (-not $env:DB_PASSWORD) {
    $secure = Read-Host "PostgreSQL password for user '$($env:DB_USER)'" -AsSecureString
    $env:DB_PASSWORD = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($secure))
}
if (-not $env:DB_NAME) { $env:DB_NAME = 'lectrolend' }

$psql = Get-ChildItem 'C:\Program Files\PostgreSQL' -Directory -ErrorAction SilentlyContinue |
    Sort-Object Name -Descending | ForEach-Object { Join-Path $_.FullName 'bin\psql.exe' } |
    Where-Object { Test-Path $_ } | Select-Object -First 1

if ($psql) {
    $env:PGPASSWORD = $env:DB_PASSWORD
    $exists = & $psql -U $env:DB_USER -h localhost -tAc "SELECT 1 FROM pg_database WHERE datname='$($env:DB_NAME)'"
    if ($LASTEXITCODE -ne 0) { throw 'Could not connect to PostgreSQL. Check the password and that the service is running.' }
    if (-not $exists) {
        & $psql -U $env:DB_USER -h localhost -c "CREATE DATABASE $($env:DB_NAME)"
        Write-Host "Created database $($env:DB_NAME)"
    }
    Remove-Item Env:\PGPASSWORD
} else {
    Write-Host "psql not found: make sure a database named '$($env:DB_NAME)' already exists."
}

Push-Location (Join-Path $PSScriptRoot '..\backend')
try { mvn spring-boot:run } finally { Pop-Location }
