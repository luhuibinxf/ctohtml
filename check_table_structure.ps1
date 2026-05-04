$ErrorActionPreference = "Stop"

try {
    Add-Type -AssemblyName "System.Data"

    $connectionString = "Server=localhost;Database=WiNEX_PACS;Integrated Security=True;TrustServerCertificate=True;"
    $connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $connection.Open()

    $command = $connection.CreateCommand()
    $command.CommandText = @"
SELECT
    c.COLUMN_NAME,
    c.DATA_TYPE,
    c.CHARACTER_MAXIMUM_LENGTH,
    c.NUMERIC_PRECISION,
    c.NUMERIC_SCALE,
    c.IS_NULLABLE,
    c.COLUMN_DEFAULT,
    p.VALUE AS DESCRIPTION
FROM INFORMATION_SCHEMA.COLUMNS c
LEFT JOIN sys.extended_properties p
    ON p.major_id = OBJECT_ID(c.TABLE_NAME)
    AND p.minor_id = c.ORDINAL_POSITION
    AND p.name = 'MS_Description'
WHERE c.TABLE_NAME = 'EXAM_TASK'
ORDER BY c.ORDINAL_POSITION
"@

    $adapter = New-Object System.Data.SqlClient.SqlDataAdapter($command)
    $dataset = New-Object System.Data.DataSet
    $adapter.Fill($dataset) | Out-Null

    Write-Host "EXAM_TASK 表结构:" -ForegroundColor Cyan
    Write-Host "=" * 100
    $dataset.Tables[0] | Format-Table -AutoSize

    $connection.Close()
} catch {
    Write-Host "错误: $($_.Exception.Message)" -ForegroundColor Red
}
