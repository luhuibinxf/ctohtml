<#
.SYNOPSIS
    添加或修改用户数据

.DESCRIPTION
    支持添加新用户或修改现有用户，用户ID必须是数字类型

.PARAMETER UserId
    用户ID（数字类型）

.PARAMETER Username
    用户名

.PARAMETER Password
    用户密码

.PARAMETER Action
    操作类型：Add（添加）或 Update（修改）

.EXAMPLE
    .\添加用户数据.ps1 -UserId 1001 -Username "测试用户" -Password "123456" -Action Add

.EXAMPLE
    .\添加用户数据.ps1 -UserId 1001 -Username "更新用户" -Password "654321" -Action Update
#>

param(
    [Parameter(Mandatory=$true, HelpMessage="用户ID（数字类型）")]
    [ValidatePattern("^[0-9]+$")]
    [string]$UserId,
    
    [Parameter(Mandatory=$true, HelpMessage="用户名")]
    [string]$Username,
    
    [Parameter(Mandatory=$true, HelpMessage="用户密码")]
    [string]$Password,
    
    [Parameter(Mandatory=$true, HelpMessage="操作类型：Add或Update")]
    [ValidateSet("Add", "Update")]
    [string]$Action
)

# 读取配置文件
function Get-ConnectionString {
    $configFile = "d:\AI\tran\config.dat"
    if (Test-Path $configFile) {
        try {
            $encrypted = Get-Content $configFile -Encoding UTF8 | Select-Object -First 1
            $decoded = [System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($encrypted))
            return $decoded
        } catch {
            Write-Host "解密配置文件失败，使用默认连接字符串" -ForegroundColor Yellow
        }
    }
    return "Data Source=localhost;Initial Catalog=WiNEX_PACS;Integrated Security=True;"
}

# 主函数
function Main {
    Write-Host "=== 添加/修改用户数据 ===" -ForegroundColor Green
    
    # 验证用户ID是否为数字
    if (-not ($UserId -match "^[0-9]+$")) {
        Write-Host "错误：用户ID必须是数字类型" -ForegroundColor Red
        return
    }
    
    $connString = Get-ConnectionString
    Write-Host "连接字符串: $connString" -ForegroundColor Cyan
    
    try {
        # 创建数据库连接
        $conn = New-Object System.Data.SqlClient.SqlConnection($connString)
        $conn.Open()
        Write-Host "连接成功" -ForegroundColor Green
        
        if ($Action -eq "Add") {
            # 检查用户ID是否已存在
            $checkCmd = $conn.CreateCommand()
            $checkCmd.CommandText = "SELECT COUNT(*) FROM TJYHB WHERE YHID = @YHID"
            $checkCmd.Parameters.AddWithValue("@YHID", $UserId)
            $count = $checkCmd.ExecuteScalar()
            
            if ($count -gt 0) {
                Write-Host "错误：用户ID $UserId 已存在" -ForegroundColor Red
                $conn.Close()
                return
            }
            
            # 添加新用户
            $cmd = $conn.CreateCommand()
            $cmd.CommandText = "INSERT INTO TJYHB (YHID, YHMC, YKL, IS_DEL) VALUES (@YHID, @YHMC, @YKL, 0)"
            $cmd.Parameters.AddWithValue("@YHID", $UserId)
            $cmd.Parameters.AddWithValue("@YHMC", $Username)
            $cmd.Parameters.AddWithValue("@YKL", $Password)
            
            $result = $cmd.ExecuteNonQuery()
            if ($result -gt 0) {
                Write-Host "成功添加用户：ID=$UserId, 用户名=$Username" -ForegroundColor Green
            } else {
                Write-Host "添加用户失败" -ForegroundColor Red
            }
        } elseif ($Action -eq "Update") {
            # 检查用户是否存在
            $checkCmd = $conn.CreateCommand()
            $checkCmd.CommandText = "SELECT COUNT(*) FROM TJYHB WHERE YHID = @YHID"
            $checkCmd.Parameters.AddWithValue("@YHID", $UserId)
            $count = $checkCmd.ExecuteScalar()
            
            if ($count -eq 0) {
                Write-Host "错误：用户ID $UserId 不存在" -ForegroundColor Red
                $conn.Close()
                return
            }
            
            # 更新用户
            $cmd = $conn.CreateCommand()
            $cmd.CommandText = "UPDATE TJYHB SET YHMC = @YHMC, YKL = @YKL WHERE YHID = @YHID"
            $cmd.Parameters.AddWithValue("@YHID", $UserId)
            $cmd.Parameters.AddWithValue("@YHMC", $Username)
            $cmd.Parameters.AddWithValue("@YKL", $Password)
            
            $result = $cmd.ExecuteNonQuery()
            if ($result -gt 0) {
                Write-Host "成功更新用户：ID=$UserId, 用户名=$Username" -ForegroundColor Green
            } else {
                Write-Host "更新用户失败" -ForegroundColor Red
            }
        }
        
        $conn.Close()
        
    } catch {
        Write-Host "错误：$($_.Exception.Message)" -ForegroundColor Red
    }
}

# 执行主函数
Main

Write-Host "\n=== 操作完成 ===" -ForegroundColor Green