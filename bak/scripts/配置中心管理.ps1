<#
.SYNOPSIS
    配置中心管理脚本

.DESCRIPTION
    管理配置中心的系统配置、角色和权限

.PARAMETER Action
    操作类型：GetConfig, SetConfig, GetRoles, GetPermissions

.PARAMETER ConfigKey
    配置键名（当Action为GetConfig或SetConfig时使用）

.PARAMETER ConfigValue
    配置值（当Action为SetConfig时使用）

.PARAMETER ConfigType
    配置类型（当Action为SetConfig时使用）

.PARAMETER Description
    配置描述（当Action为SetConfig时使用）

.EXAMPLE
    .\配置中心管理.ps1 -Action GetConfig -ConfigKey "SYSTEM_NAME"

.EXAMPLE
    .\配置中心管理.ps1 -Action SetConfig -ConfigKey "SYSTEM_NAME" -ConfigValue "医疗影像系统" -ConfigType "SYSTEM" -Description "系统名称"

.EXAMPLE
    .\配置中心管理.ps1 -Action GetRoles

.EXAMPLE
    .\配置中心管理.ps1 -Action GetPermissions
#>

param(
    [Parameter(Mandatory=$true, HelpMessage="操作类型")]
    [ValidateSet("GetConfig", "SetConfig", "GetRoles", "GetPermissions")]
    [string]$Action,
    
    [Parameter(HelpMessage="配置键名")]
    [string]$ConfigKey,
    
    [Parameter(HelpMessage="配置值")]
    [string]$ConfigValue,
    
    [Parameter(HelpMessage="配置类型")]
    [string]$ConfigType,
    
    [Parameter(HelpMessage="配置描述")]
    [string]$Description
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
    Write-Host "=== 配置中心管理 ===" -ForegroundColor Green
    
    $connString = Get-ConnectionString
    Write-Host "连接字符串: $connString" -ForegroundColor Cyan
    
    try {
        # 创建数据库连接
        $conn = New-Object System.Data.SqlClient.SqlConnection($connString)
        $conn.Open()
        Write-Host "连接成功" -ForegroundColor Green
        
        switch ($Action) {
            "GetConfig" {
                if (-not $ConfigKey) {
                    Write-Host "错误：GetConfig操作需要指定ConfigKey参数" -ForegroundColor Red
                    return
                }
                
                $cmd = $conn.CreateCommand()
                $cmd.CommandText = "SELECT CONFIG_VALUE, CONFIG_TYPE, DESCRIPTION FROM tjfx_SYS_CONFIG WHERE CONFIG_KEY = @ConfigKey AND IS_DEL = 0"
                $cmd.Parameters.AddWithValue("@ConfigKey", $ConfigKey)
                
                $reader = $cmd.ExecuteReader()
                if ($reader.Read()) {
                    Write-Host "配置键: $ConfigKey" -ForegroundColor Cyan
                    Write-Host "配置值: $($reader["CONFIG_VALUE"])`n" -ForegroundColor Green
                    Write-Host "配置类型: $($reader["CONFIG_TYPE"])" -ForegroundColor Cyan
                    Write-Host "描述: $($reader["DESCRIPTION"])`n" -ForegroundColor Cyan
                } else {
                    Write-Host "错误：配置键 $ConfigKey 不存在" -ForegroundColor Red
                }
                $reader.Close()
            }
            
            "SetConfig" {
                if (-not $ConfigKey -or -not $ConfigValue -or -not $ConfigType) {
                    Write-Host "错误：SetConfig操作需要指定ConfigKey、ConfigValue和ConfigType参数" -ForegroundColor Red
                    return
                }
                
                $cmd = $conn.CreateCommand()
                $cmd.CommandText = @"
                    IF EXISTS (SELECT 1 FROM tjfx_SYS_CONFIG WHERE CONFIG_KEY = @ConfigKey AND IS_DEL = 0)
                    BEGIN
                        UPDATE tjfx_SYS_CONFIG 
                        SET CONFIG_VALUE = @ConfigValue, 
                            CONFIG_TYPE = @ConfigType, 
                            DESCRIPTION = @Description, 
                            UPDATE_TIME = GETDATE()
                        WHERE CONFIG_KEY = @ConfigKey AND IS_DEL = 0;
                    END
                    ELSE
                    BEGIN
                        INSERT INTO tjfx_SYS_CONFIG (CONFIG_KEY, CONFIG_VALUE, CONFIG_TYPE, DESCRIPTION)
                        VALUES (@ConfigKey, @ConfigValue, @ConfigType, @Description);
                    END
"@
                $cmd.Parameters.AddWithValue("@ConfigKey", $ConfigKey)
                $cmd.Parameters.AddWithValue("@ConfigValue", $ConfigValue)
                $cmd.Parameters.AddWithValue("@ConfigType", $ConfigType)
                $cmd.Parameters.AddWithValue("@Description", $Description)
                
                $result = $cmd.ExecuteNonQuery()
                if ($result -gt 0) {
                    Write-Host "成功设置配置：$ConfigKey = $ConfigValue" -ForegroundColor Green
                } else {
                    Write-Host "设置配置失败" -ForegroundColor Red
                }
            }
            
            "GetRoles" {
                $cmd = $conn.CreateCommand()
                $cmd.CommandText = "SELECT ROLE_ID, ROLE_NAME, DESCRIPTION FROM tjfx_SYS_ROLE WHERE IS_DEL = 0 ORDER BY ROLE_ID"
                
                $reader = $cmd.ExecuteReader()
                Write-Host "角色列表:`n" -ForegroundColor Cyan
                while ($reader.Read()) {
                    Write-Host "角色ID: $($reader["ROLE_ID"]), 角色名称: $($reader["ROLE_NAME"]), 描述: $($reader["DESCRIPTION"])`n" -ForegroundColor Green
                }
                $reader.Close()
            }
            
            "GetPermissions" {
                $cmd = $conn.CreateCommand()
                $cmd.CommandText = "SELECT PERMISSION_ID, PERMISSION_NAME, PERMISSION_CODE, DESCRIPTION FROM tjfx_SYS_PERMISSION WHERE IS_DEL = 0 ORDER BY PERMISSION_ID"
                
                $reader = $cmd.ExecuteReader()
                Write-Host "权限列表:`n" -ForegroundColor Cyan
                while ($reader.Read()) {
                    Write-Host "权限ID: $($reader["PERMISSION_ID"]), 权限名称: $($reader["PERMISSION_NAME"]), 权限代码: $($reader["PERMISSION_CODE"]), 描述: $($reader["DESCRIPTION"])`n" -ForegroundColor Green
                }
                $reader.Close()
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