Write-Host "=== 测试服务器功能 ==="

# 1. 清理旧进程
Write-Host "`n[1] 清理旧进程..."
try {
    Get-Process 'DbProcedureCaller' -ErrorAction SilentlyContinue | ForEach-Object { 
        $_.Kill()
        Write-Host "已关闭: " $_.Id
    }
} catch {
    Write-Host "清理进程时出错: " $_.Exception.Message
}

# 2. 启动程序
Write-Host "`n[2] 启动程序..."
try {
    $proc = Start-Process -FilePath "d:\AI\tran\DbProcedureCaller.exe" -PassThru
    Write-Host "程序已启动, PID: " $proc.Id
} catch {
    Write-Host "启动程序失败: " $_.Exception.Message
    exit 1
}

# 3. 等待10秒
Write-Host "`n[3] 等待程序初始化 (10秒)..."
for ($i = 1; $i -le 10; $i++) {
    Write-Host -NoNewline "$i "
    Start-Sleep -Seconds 1
}
Write-Host ""

# 4. 检查8888端口
Write-Host "`n[4] 检查8888端口..."
try {
    $netstat = netstat -an
    if ($netstat -match ":8888") {
        Write-Host "✅ 8888端口正在监听"
        $netstat | Select-String ":8888" | ForEach-Object { Write-Host "   " $_ }
    } else {
        Write-Host "❌ 8888端口未被监听"
    }
} catch {
    Write-Host "检查端口时出错: " $_.Exception.Message
}

# 5. 测试访问8888端口
Write-Host "`n[5] 测试访问8888端口..."
try {
    $response = Invoke-WebRequest -Uri "http://localhost:8888/" -TimeoutSec 5 -UseBasicParsing
    Write-Host "✅ 访问成功!"
    Write-Host "状态码: " $response.StatusCode
    Write-Host "内容长度: " $response.Content.Length "字符"
} catch {
    Write-Host "❌ 访问失败: " $_.Exception.Message
}

# 6. 检查程序状态
Write-Host "`n[6] 检查程序状态..."
if ($proc.HasExited) {
    Write-Host "❌ 程序已退出, 退出码: " $proc.ExitCode
} else {
    Write-Host "✅ 程序仍在运行"
}

# 7. 清理
Write-Host "`n[7] 清理..."
try {
    if (!$proc.HasExited) {
        $proc.Kill()
        Write-Host "已关闭程序"
    }
} catch {
    Write-Host "关闭程序时出错: " $_.Exception.Message
}

Write-Host "`n=== 测试完成 ==="