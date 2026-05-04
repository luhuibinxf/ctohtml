<#
初始化统计分析系统的数据库配置表
#>

$configFile = "d:\AI\tran\config.dat"
$sqlScriptPath = "d:\AI\tran\scripts\create_param_config_tables.sql"

if (-not (Test-Path $configFile)) {
    Write-Error "配置文件不存在: $configFile"
    exit 1
}

if (-not (Test-Path $sqlScriptPath)) {
    Write-Error "SQL脚本不存在: $sqlScriptPath"
    exit 1
}

$encrypted = Get-Content $configFile -Raw

try {
    $cipherBytes = [Convert]::FromBase64String($encrypted)
    $key = [byte[]](0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF)
    $iv = [byte[]](0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF)
    
    $aes = [System.Security.Cryptography.Aes]::Create()
    $aes.Key = $key
    $aes.IV = $iv
    
    $ms = New-Object System.IO.MemoryStream
    $cs = New-Object System.Security.Cryptography.CryptoStream($ms, $aes.CreateDecryptor(), [System.Security.Cryptography.CryptoStreamMode]::Write)
    $cs.Write($cipherBytes, 0, $cipherBytes.Length)
    $cs.FlushFinalBlock()
    
    $connectionString = [System.Text.Encoding]::UTF8.GetString($ms.ToArray())
    Write-Host "连接字符串解密成功"
}
catch {
    Write-Error "解密连接字符串失败: $_"
    exit 1
}

try {
    Write-Host "正在执行SQL脚本..."
    $sqlContent = Get-Content $sqlScriptPath -Raw
    
    $conn = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $conn.Open()
    Write-Host "数据库连接成功"
    
    $sqlCommands = $sqlContent -split "GO\s*`r?`n" | Where-Object { $_.Trim() }
    
    foreach ($cmdText in $sqlCommands) {
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = $cmdText.Trim()
        $cmd.ExecuteNonQuery()
        Write-Host "执行SQL成功"
    }
    
    $conn.Close()
    Write-Host "配置表初始化完成！"
}
catch {
    Write-Error "执行SQL失败: $_"
    exit 1
}