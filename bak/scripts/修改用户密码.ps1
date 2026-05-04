param(
    [Parameter(Mandatory=$true)]
    [string]$Username,

    [Parameter(Mandatory=$true)]
    [string]$NewPassword,

    [Parameter(Mandatory=$false)]
    [string]$Server = "localhost",

    [Parameter(Mandatory=$false)]
    [string]$Database = "WiNEX_PACS"
)

$ErrorActionPreference = "Stop"

function Get-ConfigConnectionString {
    $configFile = Join-Path $PSScriptRoot "config.dat"
    if (Test-Path $configFile) {
        $encrypted = Get-Content $configFile -Raw
        $decoded = [System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($encrypted.Trim()))
        return $decoded
    }
    return $null
}

function Update-UserPassword {
    param(
        [string]$ConnString,
        [string]$User,
        [string]$Pass
    )

    try {
        $connection = New-Object System.Data.SqlClient.SqlConnection
        $connection.ConnectionString = $ConnString
        $connection.Open()

        $sql = "UPDATE TJYHB SET YKL = @password WHERE YHM = @username"
        $command = $connection.CreateCommand()
        $command.CommandText = $sql
        $command.Parameters.AddWithValue("@username", $User) | Out-Null
        $command.Parameters.AddWithValue("@password", $Pass) | Out-Null

        $rowsAffected = $command.ExecuteNonQuery()

        $connection.Close()

        return $rowsAffected
    }
    catch {
        Write-Error "更新密码失败: $_"
        throw
    }
}

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "       用户密码修改工具" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

$connString = Get-ConfigConnectionString

if ([string]::IsNullOrEmpty($connString)) {
    Write-Host "[警告] 未找到config.dat，使用集成Windows认证" -ForegroundColor Yellow
    $connString = "Server=$Server;Database=$Database;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;"
}

try {
    $result = Update-UserPassword -ConnString $connString -User $Username -Pass $NewPassword

    if ($result -gt 0) {
        Write-Host "[成功] 用户 '$Username' 的密码已更新" -ForegroundColor Green
    } else {
        Write-Host "[失败] 未找到用户 '$Username'" -ForegroundColor Red
    }
}
catch {
    Write-Host "[错误] $_" -ForegroundColor Red
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan