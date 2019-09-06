$projectPath = Join-Path $PSScriptRoot "../FutterlisteNg.Web"

dotnet run --urls=http://127.0.0.1:0 --project $projectPath