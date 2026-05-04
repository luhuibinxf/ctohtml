$query = @"
SELECT
    TABLE_NAME,
    COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME = 'encounterTypeName'
   OR COLUMN_NAME = 'ENCOUNTERTYPENAME'
   OR COLUMN_NAME LIKE '%encounterType%'
ORDER BY TABLE_NAME
"@

$connectionString = "Server=localhost;Database=WiNEX_PACS;Integrated Security=True;TrustServerCertificate=True;"
$connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
$adapter = New-Object System.Data.SqlClient.SqlDataAdapter($query, $connection)
$table = New-Object System.Data.DataTable
$adapter.Fill($table) | Out-Null

if ($table.Rows.Count -eq 0) {
    Write-Host "在整个数据库中没有找到 encounterTypeName 相关字段" -ForegroundColor Yellow
} else {
    Write-Host "找到以下 encounterTypeName 相关字段:" -ForegroundColor Green
    $table | Format-Table -AutoSize
}
