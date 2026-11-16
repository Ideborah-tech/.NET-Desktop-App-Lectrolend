# Starts the LectroLend web app (React + Vite) on http://localhost:5173
# The backend (start-backend.ps1) must be running; Vite proxies /api to it.
$ErrorActionPreference = 'Stop'
Push-Location (Join-Path $PSScriptRoot '..\frontend')
try {
    if (-not (Test-Path 'node_modules')) { npm install }
    npm run dev
} finally { Pop-Location }
