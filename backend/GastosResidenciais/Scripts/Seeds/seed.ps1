
chcp 65001 | Out-Null
$OutputEncoding = [System.Text.Encoding]::UTF8

Set-Location (Join-Path $PSScriptRoot "../..")

Write-Host "Aplicando migrations..."
dotnet ef database update

Write-Host "Populando banco com dados de exemplo..."
Get-Content -Encoding UTF8 -Raw Scripts/Seed/seed.sql | sqlite3 gastos.db

Write-Host "Banco populado com sucesso!"