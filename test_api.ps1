$response = Invoke-WebRequest -Uri http://localhost:9094/daily-analysis -Method POST -ContentType 'application/json' -Body '{"startDate":"2026-04-01","endDate":"2026-04-30","pageSize":5}'
Write-Host "Status: $($response.StatusCode)"
Write-Host "Content:"
$response.Content