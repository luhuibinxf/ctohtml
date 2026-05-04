$logPath = "D:\AI\tran\DbProcedureCaller\bin\Debug\net8.0-windows\server.log"
$monitorLogPath = "D:\AI\tran\monitor.log"
$backupDir = "D:\AI\tran\log_backup"
$maxRuntime = 0
$startTime = $null
$lastBackupTime = $null

function Write-MonitorLog {
    param([string]$message)
    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    $logEntry = "[$timestamp] $message"
    Add-Content -Path $monitorLogPath -Value $logEntry -Encoding UTF8
}

function Backup-LogFile {
    if (Test-Path $logPath) {
        if (-not (Test-Path $backupDir)) {
            New-Item -ItemType Directory -Path $backupDir -Force | Out-Null
        }
        $backupName = "server_$(Get-Date -Format 'yyyyMMdd_HHmmss').log"
        $backupPath = Join-Path $backupDir $backupName
        Copy-Item -Path $logPath -Destination $backupPath -Force
        Write-MonitorLog "Backup created: $backupName"
        Write-Host "[$(Get-Date -Format 'HH:mm:ss')] Backup: $backupName" -ForegroundColor Magenta
        return $true
    }
    return $false
}

Write-Host "========================================"
Write-Host "         Program Monitor"
Write-Host "========================================"
Write-Host ""

Write-MonitorLog "Monitor started"

while ($true) {
    $currentTime = Get-Date
    
    if (Test-Path $logPath) {
        $content = Get-Content $logPath -Raw
        
        if ($content -match "HTTP服务器启动成功") {
            $lastStart = [regex]::Match($content, '\[(\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2})\].*HTTP服务器启动成功')
            if ($lastStart.Success) {
                $startTime = [DateTime]::Parse($lastStart.Groups[1].Value)
                Write-Host "[$($currentTime.ToString('HH:mm:ss'))] Server started" -ForegroundColor Green
                Write-MonitorLog "Server started"
                Backup-LogFile
                $lastBackupTime = $currentTime
            }
        }
        
        if ($content -match "服务健康检查 - 运行正常") {
            if ($startTime) {
                $runtime = ($currentTime - $startTime).TotalMinutes
                Write-Host "[$($currentTime.ToString('HH:mm:ss'))] Running | Current: $($runtime.ToString('F2')) min" -ForegroundColor Cyan
                
                if ($runtime -gt $maxRuntime) {
                    $maxRuntime = $runtime
                    Write-Host "[$($currentTime.ToString('HH:mm:ss'))] MAX: $($maxRuntime.ToString('F2')) min" -ForegroundColor Yellow
                }
                
                if ($lastBackupTime -and ($currentTime - $lastBackupTime).TotalMinutes -ge 30) {
                    Backup-LogFile
                    $lastBackupTime = $currentTime
                }
            }
        }
        elseif ($content -match "HTTP服务器已停止") {
            if ($startTime) {
                $runtime = ($currentTime - $startTime).TotalMinutes
                Write-Host "[$($currentTime.ToString('HH:mm:ss'))] Stopped | Runtime: $($runtime.ToString('F2')) min" -ForegroundColor Red
                Write-MonitorLog "Server stopped | Runtime: $($runtime.ToString('F2')) min"
                Backup-LogFile
                $startTime = $null
            }
        }
    }
    
    Start-Sleep -Seconds 5
}
