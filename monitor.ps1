$logPath = "D:\AI\tran\DbProcedureCaller\bin\Debug\net8.0-windows\server.log"
$maxRuntime = 0
$startTime = $null

Write-Host "========================================"
Write-Host "         Program Monitor"
Write-Host "========================================"
Write-Host ""

while ($true) {
    $currentTime = Get-Date
    
    if (Test-Path $logPath) {
        $content = Get-Content $logPath -Raw
        
        if ($content -match "HTTP鏈嶅姟鍣ㄥ惎鍔ㄦ垚鍔?") {
            $lastStart = [regex]::Match($content, '\[(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2})\].*HTTP鏈嶅姟鍣ㄥ惎鍔ㄦ垚鍔?')
            if ($lastStart.Success) {
                $startTime = [DateTime]::Parse($lastStart.Groups[1].Value)
                Write-Host "[$($currentTime.ToString('HH:mm:ss'))] Server started" -ForegroundColor Green
            }
        }
        
        if ($content -match "鏈嶅姟鍋ュ悍妫€鏌?- 杩愯姝ｅ父") {
            if ($startTime) {
                $runtime = ($currentTime - $startTime).TotalMinutes
                Write-Host "[$($currentTime.ToString('HH:mm:ss'))] Running | Current: $($runtime.ToString('F2')) min" -ForegroundColor Cyan
                
                if ($runtime -gt $maxRuntime) {
                    $maxRuntime = $runtime
                    Write-Host "[$($currentTime.ToString('HH:mm:ss'))] MAX: $($maxRuntime.ToString('F2')) min" -ForegroundColor Yellow
                }
            }
        }
        elseif ($content -match "HTTP鏈嶅姟鍣ㄥ凡鍋滄") {
            if ($startTime) {
                $runtime = ($currentTime - $startTime).TotalMinutes
                Write-Host "[$($currentTime.ToString('HH:mm:ss'))] Stopped | Runtime: $($runtime.ToString('F2')) min" -ForegroundColor Red
                $startTime = $null
            }
        }
    }
    
    Start-Sleep -Seconds 5
}