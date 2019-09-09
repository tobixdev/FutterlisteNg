$projectPath = Join-Path $PSScriptRoot "../Api"

Push-Location $projectPath
dotnet watch run
Pop-Location