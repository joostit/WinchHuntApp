$serviceWorkerPath = 'WinchHuntApp\WinchHuntApp\Client\wwwroot\service-worker.published.js'

$clientBuildPath = 'WinchHuntApp\WinchHuntApp\Client\Properties\ClientBuild.cs'
$serverBuildPath = 'WinchHuntApp\WinchHuntApp\Server\Properties\ServerBuild.cs'

$DevBuildPlaceholder = 'DEV000000000#'

Write-Host "Setting build number constants to: $args[0]"

((Get-Content -path $serviceWorkerPath -Raw) -replace "$DevBuildPlaceholder",$args[0]) | Set-Content -Path $serviceWorkerPath

((Get-Content -path $clientBuildPath -Raw) -replace "$DevBuildPlaceholder", $args[0]) | Set-Content -Path $clientBuildPath
((Get-Content -path $serverBuildPath -Raw) -replace "$DevBuildPlaceholder", $args[0]) | Set-Content -Path $serverBuildPath
