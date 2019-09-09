$projectPath = Join-Path $PSScriptRoot "../Web"

Push-Location $projectPath
dotnet watch run --urls=http://127.0.0.1:80
Pop-Location