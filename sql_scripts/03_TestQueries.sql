
-- =============================================
-- 放射科工作量统计系统 - 验证查询脚本
-- =============================================
-- 执行顺序: 在创建表和存储过程之后执行
-- 说明: 所有数据均来自实际业务表(EXAM_TASK/EXAM_REPORT)

PRINT '=============================================';
PRINT '        放射科工作量统计系统 - 验证脚本';
PRINT '=============================================';
GO

-- =============================================
-- 验证1: 检查业务表是否存在
-- =============================================
PRINT '';
PRINT '【验证1】检查业务表是否存在';
PRINT '---------------------------------------------';

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'EXAM_TASK')
BEGIN
    PRINT '✓ EXAM_TASK 表存在';
    SELECT COUNT(*) AS RecordCount FROM EXAM_TASK WHERE IS_DEL = 0;
END
ELSE
BEGIN
    PRINT '✗ EXAM_TASK 表不存在，请确认数据库配置';
END

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'EXAM_REPORT')
BEGIN
    PRINT '✓ EXAM_REPORT 表存在';
    SELECT COUNT(*) AS RecordCount FROM EXAM_REPORT;
END
ELSE
BEGIN
    PRINT '✗ EXAM_REPORT 表不存在，请确认数据库配置';
END

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Pacs_SysDict')
BEGIN
    PRINT '✓ Pacs_SysDict 表存在';
END
ELSE
BEGIN
    PRINT '✗ Pacs_SysDict 表不存在（可选）';
END
GO

-- =============================================
-- 验证2: 检查统计数据表
-- =============================================
PRINT '';
PRINT '【验证2】检查统计数据表';
PRINT '---------------------------------------------';

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'EXAM_WORKLOAD_STAT')
BEGIN
    PRINT '✓ EXAM_WORKLOAD_STAT 表存在';
    SELECT COUNT(*) AS RecordCount FROM EXAM_WORKLOAD_STAT WHERE IS_DEL = 0;
END
ELSE
BEGIN
    PRINT '✗ EXAM_WORKLOAD_STAT 表不存在';
END

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'EXAM_WORKLOAD_CONFIG')
BEGIN
    PRINT '✓ EXAM_WORKLOAD_CONFIG 表存在';
END
ELSE
BEGIN
    PRINT '✗ EXAM_WORKLOAD_CONFIG 表不存在';
END
GO

-- =============================================
-- 验证3: 检查存储过程
-- =============================================
PRINT '';
PRINT '【验证3】检查存储过程';
PRINT '---------------------------------------------';

SELECT 
    name AS ProcedureName, 
    create_date AS CreateDate,
    '✓ 存在' AS Status
FROM sys.procedures 
WHERE name LIKE 'USP_EXAM_%'
ORDER BY create_date DESC;
GO

-- =============================================
-- 操作1: 同步实际业务数据
-- =============================================
PRINT '';
PRINT '【操作1】同步实际业务数据';
PRINT '---------------------------------------------';
PRINT '请根据需要执行以下同步命令:';
PRINT '';
PRINT '-- 同步单日数据（以当前日期为例）';
PRINT '-- EXEC USP_EXAM_SyncWorkloadData GETDATE();';
PRINT '';
PRINT '-- 同步指定日期数据';
PRINT '-- EXEC USP_EXAM_SyncWorkloadData ''2026-04-30'';';
PRINT '';
PRINT '-- 同步日期范围数据';
PRINT '-- EXEC USP_EXAM_SyncWorkloadDataRange ''2026-04-01'', ''2026-04-30'';';
GO

-- =============================================
-- 操作2: 查询统计数据
-- =============================================
PRINT '';
PRINT '【操作2】查询统计数据示例';
PRINT '---------------------------------------------';

-- 查询现有统计数据
IF EXISTS (SELECT * FROM EXAM_WORKLOAD_STAT WHERE IS_DEL = 0)
BEGIN
    PRINT '统计数据记录数:';
    SELECT COUNT(*) AS TotalRecords FROM EXAM_WORKLOAD_STAT WHERE IS_DEL = 0;
    
    PRINT '';
    PRINT '最近5条统计数据:';
    SELECT TOP 5 * FROM EXAM_WORKLOAD_STAT WHERE IS_DEL = 0 ORDER BY STAT_DATE DESC;
END
ELSE
BEGIN
    PRINT '暂无可查询数据，请先执行数据同步';
END
GO

-- =============================================
-- 操作3: 调用统计存储过程
-- =============================================
PRINT '';
PRINT '【操作3】调用统计存储过程示例';
PRINT '---------------------------------------------';

-- 获取日期范围（最近7天）
DECLARE @StartDate DATE = DATEADD(DAY, -7, CAST(GETDATE() AS DATE));
DECLARE @EndDate DATE = CAST(GETDATE() AS DATE);

PRINT '日期范围: ' + CONVERT(VARCHAR, @StartDate, 23) + ' 至 ' + CONVERT(VARCHAR, @EndDate, 23);
PRINT '';

IF EXISTS (SELECT * FROM EXAM_WORKLOAD_STAT WHERE IS_DEL = 0 AND STAT_DATE BETWEEN @StartDate AND @EndDate)
BEGIN
    PRINT '调用 USP_EXAM_GetWorkloadByDate:';
    EXEC USP_EXAM_GetWorkloadByDate @StartDate, @EndDate, NULL, NULL, NULL, NULL, NULL, NULL, 5, 1;
    
    PRINT '';
    PRINT '调用 USP_EXAM_GetWorkloadSummary (按科室):';
    EXEC USP_EXAM_GetWorkloadSummary @StartDate, @EndDate, 'DEPARTMENT';
    
    PRINT '';
    PRINT '调用 USP_EXAM_GetDailyTrend:';
    EXEC USP_EXAM_GetDailyTrend @StartDate, @EndDate;
END
ELSE
BEGIN
    PRINT '该日期范围内无数据，请先同步数据';
END
GO

-- =============================================
-- 验证完成
-- =============================================
PRINT '';
PRINT '=============================================';
PRINT '                 验证完成';
PRINT '=============================================';
PRINT '';
PRINT '数据来源说明:';
PRINT '  - 统计数据: EXAM_WORKLOAD_STAT 表';
PRINT '  - 原始数据: EXAM_TASK + EXAM_REPORT 业务表';
PRINT '';
PRINT '同步数据命令:';
PRINT '  1. 同步今日数据: EXEC USP_EXAM_SyncWorkloadData GETDATE();';
PRINT '  2. 同步指定日期: EXEC USP_EXAM_SyncWorkloadData ''YYYY-MM-DD'';';
PRINT '  3. 同步日期范围: EXEC USP_EXAM_SyncWorkloadDataRange ''开始日期'', ''结束日期'';';
PRINT '';
PRINT '查询数据命令:';
PRINT '  1. 查询工作量: EXEC USP_EXAM_GetWorkloadByDate ''开始日期'', ''结束日期'';';
PRINT '  2. 获取汇总: EXEC USP_EXAM_GetWorkloadSummary ''开始日期'', ''结束日期'', ''DEPARTMENT'';';
PRINT '  3. 获取趋势: EXEC USP_EXAM_GetDailyTrend ''开始日期'', ''结束日期'';';
GO