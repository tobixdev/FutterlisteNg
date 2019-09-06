$projectPath = Join-Path $PSScriptRoot "../FutterlisteNg.Api"

Push-Location $projectPath
dotnet watch run
Pop-Location