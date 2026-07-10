#!/bin/bash
set -e

# Volta para a raiz do projeto backend, onde fica o .csproj
cd "$(dirname "$0")/../.."

echo "Aplicando migrations..."
dotnet ef database update

echo "Populando banco com dados de exemplo..."
sqlite3 app.db < Scripts/Seeds/seed.sql

echo "Banco populado com sucesso!"