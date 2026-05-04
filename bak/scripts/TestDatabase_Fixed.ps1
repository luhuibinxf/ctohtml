# 测试数据库连接和查询执行科室数据
$ErrorActionPreference = "Stop"

function Get-ConnectionString {
    $configFile = "d:\AI\tran\config.dat"
    if (Test-Path $configFile) {
        $encrypted = Get-Content $configFile -Raw
        try {
            $decoded = [System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($encrypted.Trim()))
            return $decoded
        }
        catch {
            Write-Host "解密配置文件失败，使用默认连接字符串" -ForegroundColor Yellow
        }
    }
    return "Data Source=localhost;Initial Catalog=WiNEX_PACS;Integrated Security=True;TrustServerCertificate=True;"
}

Write-Host "=== 测试数据库连接 ===" -ForegroundColor Cyan

$connString = Get-ConnectionString
Write-Host "连接字符串: $connString" -ForegroundColor Gray

try {
    $connection = New-Object System.Data.SqlClient.SqlConnection
    $connection.ConnectionString = $connString
    $connection.Open()
    
    Write-Host "数据库连接成功!" -ForegroundColor Green
    
    # 查询系统信息
    Write-Host "`n=== 系统信息 ===" -ForegroundColor Cyan
    $sql = "SELECT @@VERSION AS Version, DB_NAME() AS DatabaseName"
    $command = $connection.CreateCommand()
    $command.CommandText = $sql
    $reader = $command.ExecuteReader()
    
    if ($reader.Read()) {
        Write-Host "数据库版本: $($reader['Version'])" -ForegroundColor Gray
        Write-Host "当前数据库: $($reader['DatabaseName'])" -ForegroundColor Gray
    }
    $reader.Close()
    
    # 查询系统来源
    Write-Host "`n=== 系统来源 ===" -ForegroundColor Cyan
    $sqlSystem = "SELECT DISTINCT SYSTEM_SOURCE_NO FROM EXAM_TASK WHERE IS_DEL = 0"
    $command.CommandText = $sqlSystem
    $reader = $command.ExecuteReader()
    
    while ($reader.Read()) {
        Write-Host "- $($reader['SYSTEM_SOURCE_NO'])" -ForegroundColor White
    }
    $reader.Close()
    
    # 查询检查类别
    Write-Host "`n=== 检查类别 ===" -ForegroundColor Cyan
    $sqlCategory = "SELECT DISTINCT EXAM_CATEGORY_NAME FROM EXAM_TASK WHERE IS_DEL = 0 ORDER BY EXAM_CATEGORY_NAME"
    $command.CommandText = $sqlCategory
    $reader = $command.ExecuteReader()
    
    while ($reader.Read()) {
        Write-Host "- $($reader['EXAM_CATEGORY_NAME'])" -ForegroundColor White
    }
    $reader.Close()
    
    # 按执行科室统计
    Write-Host "`n=== 执行科室统计 ===" -ForegroundColor Cyan
    $sqlDept = @"
    SELECT 
        CASE 
            WHEN EXAM_CATEGORY_NAME LIKE '%胃镜%' OR EXAM_CATEGORY_NAME LIKE '%肠镜%' OR EXAM_CATEGORY_NAME LIKE '%内镜%' THEN '消化内镜(总)'
            WHEN SYSTEM_SOURCE_NO = 'RIS' THEN '放射科'
            ELSE '其他科室'
        END AS Department,
        COUNT(DISTINCT EXAM_TASK_ID) AS Count
    FROM EXAM_TASK 
    WHERE IS_DEL = 0
    GROUP BY 
        CASE 
            WHEN EXAM_CATEGORY_NAME LIKE '%胃镜%' OR EXAM_CATEGORY_NAME LIKE '%肠镜%' OR EXAM_CATEGORY_NAME LIKE '%内镜%' THEN '消化内镜(总)'
            WHEN SYSTEM_SOURCE_NO = 'RIS' THEN '放射科'
            ELSE '其他科室'
        END
    ORDER BY Count DESC
"@
    
    $command.CommandText = $sqlDept
    $reader = $command.ExecuteReader()
    
    while ($reader.Read()) {
        Write-Host "- $($reader['Department']): $($reader['Count']) 例" -ForegroundColor White
    }
    $reader.Close()
    
    $connection.Close()
    
} catch {
    Write-Host "错误: $_" -ForegroundColor Red
}

Write-Host "`n=== 测试完成 ===" -ForegroundColor Cyan