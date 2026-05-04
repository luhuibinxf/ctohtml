param(
    [Parameter(Mandatory=$false)]
    [string]$Server = "localhost",

    [Parameter(Mandatory=$false)]
    [string]$Database = "WiNEX_PACS",

    [Parameter(Mandatory=$false)]
    [string]$Username = "",

    [Parameter(Mandatory=$false)]
    [string]$Password = "",

    [Parameter(Mandatory=$false)]
    [switch]$UseWindowsAuth,

    [Parameter(Mandatory=$false)]
    [switch]$ShowCurrent
)

$ErrorActionPreference = "Stop"

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$configFile = Join-Path $scriptDir "config.dat"

function ConvertTo-Base64 {
    param([string]$Text)
    $bytes = [System.Text.Encoding]::UTF8.GetBytes($Text)
    return [System.Convert]::ToBase64String($bytes)
}

function Get-ConfigConnectionString {
    if (Test-Path $configFile) {
        $encrypted = Get-Content $configFile -Raw
        $decoded = [System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($encrypted.Trim()))
        return $decoded
    }
    return $null
}

function Save-ConnectionString {
    param([string]$ConnString)
    $encoded = ConvertTo-Base64 -Text $ConnString
    Set-Content -Path $configFile -Value $encoded -NoNewline
}

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "       数据库配置保存工具" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

if ($ShowCurrent) {
    $current = Get-ConfigConnectionString
    if ($current) {
        Write-Host "[当前配置]" -ForegroundColor Yellow
        Write-Host $current
    } else {
        Write-Host "[提示] 尚未配置数据库连接" -ForegroundColor Yellow
    }
    Write-Host ""
    return
}

$connString = ""

if ($UseWindowsAuth) {
    $connString = "Server=$Server;Database=$Database;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;"
} else {
    if ([string]::IsNullOrEmpty($Username) -or [string]::IsNullOrEmpty($Password)) {
        Write-Host "[错误] SQL Server认证需要提供用户名和密码" -ForegroundColor Red
        exit 1
    }
    $connString = "Server=$Server;Database=$Database;User ID=$Username;Password=$Password;TrustServerCertificate=True;"
}

try {
    $testConn = New-Object System.Data.SqlClient.SqlConnection
    $testConn.ConnectionString = $connString
    $testConn.Open()

    $version = $testConn.ServerVersion
    $testConn.Close()

    Save-ConnectionString -ConnString $connString

    Write-Host "[成功] 配置已保存到 config.dat" -ForegroundColor Green
    Write-Host "连接字符串: $connString" -ForegroundColor Gray
}
catch {
    Write-Host "[错误] 数据库连接失败: $_" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan