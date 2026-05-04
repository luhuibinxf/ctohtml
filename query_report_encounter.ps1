$query = @"
SELECT
    c.COLUMN_NAME,
    c.DATA_TYPE,
    c.CHARACTER_MAXIMUM_LENGTH,
    c.IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS c
WHERE c.TABLE_NAME = 'EXAM_REPORT'
AND (c.COLUMN_NAME LIKE '%ENCOUNTER%' OR c.COLUMN_NAME LIKE '%PATIENT%')
ORDER BY c.ORDINAL_POSITION
"@

$connectionString = "Server=localhost;Database=WiNEX_PACS;Integrated Security=True;TrustServerCertificate=True;"
$connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
$adapter = New-Object System.Data.SqlClient.SqlDataAdapter($query, $connection)
$table = New-Object System.Data.DataTable
$adapter.Fill($table) | Out-Null

Write-Host "EXAM_REPORT 表中 ENCOUNTER/PATIENT 相关字段:" -ForegroundColor Cyan
$table | Format-Table -AutoSize
