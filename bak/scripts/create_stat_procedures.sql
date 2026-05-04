-- 创建每日统计存储过程
CREATE PROCEDURE [dbo].[sp_GetDailyAnalysisData]
    @StartDate VARCHAR(20),
    @EndDate VARCHAR(20),
    @System VARCHAR(50),
    @Reporter VARCHAR(100),
    @Reviewer VARCHAR(100),
    @Technician VARCHAR(100),
    @Department VARCHAR(100),
    @Category VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- 构建查询SQL
    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = N'
        SELECT 
            SYSTEM_SOURCE_NO AS ''系统'',
            REPORTER_NAME AS ''报告医生'',
            REVIEWER_NAME AS ''审核医生'',
            TECHNICIAN_NAME AS ''技师'',
            ISNULL((SELECT DepartmentName FROM DEPT_CATEGORY_MAPPING WHERE CategoryName = t.EXAM_CATEGORY_NAME), ''其他科室'') AS ''执行科室'',
            EXAM_CATEGORY_NAME AS ''检查类型'',
            COUNT(*) AS ''任务数量'',
            MAX(EXAM_TASK_CREATE_TIME) AS ''最新任务时间''
        FROM EXAM_TASK t
        LEFT JOIN EXAM_REPORT r ON t.ID = r.EXAM_TASK_ID
        WHERE t.IS_DEL = 0'

    -- 添加日期范围条件
    IF @StartDate IS NOT NULL AND @StartDate != ''
    BEGIN
        SET @SQL = @SQL + N' AND t.EXAM_TASK_CREATE_TIME >= ''' + @StartDate + ' 00:00:00'''
    END
    IF @EndDate IS NOT NULL AND @EndDate != ''
    BEGIN
        SET @SQL = @SQL + N' AND t.EXAM_TASK_CREATE_TIME <= ''' + @EndDate + ' 23:59:59'''
    END

    -- 添加系统条件
    IF @System IS NOT NULL AND @System != ''
    BEGIN
        SET @SQL = @SQL + N' AND t.SYSTEM_SOURCE_NO = ''' + @System + ''''
    END

    -- 添加报告医生条件
    IF @Reporter IS NOT NULL AND @Reporter != ''
    BEGIN
        SET @SQL = @SQL + N' AND r.REPORTER_NAME = ''' + @Reporter + ''''
    END

    -- 添加审核医生条件
    IF @Reviewer IS NOT NULL AND @Reviewer != ''
    BEGIN
        SET @SQL = @SQL + N' AND r.REVIEWER_NAME = ''' + @Reviewer + ''''
    END

    -- 添加技师条件
    IF @Technician IS NOT NULL AND @Technician != ''
    BEGIN
        SET @SQL = @SQL + N' AND t.TECHNICIAN_NAME = ''' + @Technician + ''''
    END

    -- 添加执行科室条件
    IF @Department IS NOT NULL AND @Department != ''
    BEGIN
        SET @SQL = @SQL + N' AND ISNULL((SELECT DepartmentName FROM DEPT_CATEGORY_MAPPING WHERE CategoryName = t.EXAM_CATEGORY_NAME), ''其他科室'') = ''' + @Department + ''''
    END

    -- 添加检查类型条件
    IF @Category IS NOT NULL AND @Category != ''
    BEGIN
        SET @SQL = @SQL + N' AND t.EXAM_CATEGORY_NAME = ''' + @Category + ''''
    END

    -- 添加分组和排序
    SET @SQL = @SQL + N'
        GROUP BY t.SYSTEM_SOURCE_NO, r.REPORTER_NAME, r.REVIEWER_NAME, t.TECHNICIAN_NAME, 
        ISNULL((SELECT DepartmentName FROM DEPT_CATEGORY_MAPPING WHERE CategoryName = t.EXAM_CATEGORY_NAME), ''其他科室''), t.EXAM_CATEGORY_NAME
        ORDER BY COUNT(*) DESC'

    -- 执行动态SQL
    EXEC sp_executesql @SQL
END
GO

-- 创建获取报告医生存储过程
CREATE PROCEDURE [dbo].[sp_GetReporters]
    @System VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = N'
        SELECT DISTINCT r.REPORTER_NAME AS code, r.REPORTER_NAME AS name 
        FROM EXAM_REPORT r 
        LEFT JOIN EXAM_TASK t ON r.EXAM_TASK_ID = t.ID 
        WHERE r.IS_DEL = 0 AND r.REPORTER_NAME IS NOT NULL'

    -- 如果指定了系统，添加系统过滤条件
    IF @System IS NOT NULL AND @System != ''
    BEGIN
        SET @SQL = @SQL + N' AND t.SYSTEM_SOURCE_NO = ''' + @System + ''''
    END

    SET @SQL = @SQL + N' ORDER BY r.REPORTER_NAME'

    EXEC sp_executesql @SQL
END
GO

-- 创建获取审核医生存储过程
CREATE PROCEDURE [dbo].[sp_GetReviewers]
    @System VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = N'
        SELECT DISTINCT r.REVIEWER_NAME AS code, r.REVIEWER_NAME AS name 
        FROM EXAM_REPORT r 
        LEFT JOIN EXAM_TASK t ON r.EXAM_TASK_ID = t.ID 
        WHERE r.IS_DEL = 0 AND r.REVIEWER_NAME IS NOT NULL'

    -- 如果指定了系统，添加系统过滤条件
    IF @System IS NOT NULL AND @System != ''
    BEGIN
        SET @SQL = @SQL + N' AND t.SYSTEM_SOURCE_NO = ''' + @System + ''''
    END

    SET @SQL = @SQL + N' ORDER BY r.REVIEWER_NAME'

    EXEC sp_executesql @SQL
END
GO

-- 创建获取检查类型存储过程
CREATE PROCEDURE [dbo].[sp_GetCategories]
    @System VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @SQL NVARCHAR(MAX);
    SET @SQL = N'
        SELECT DISTINCT EXAM_CATEGORY_NAME AS code, EXAM_CATEGORY_NAME AS name 
        FROM EXAM_TASK 
        WHERE IS_DEL = 0 AND EXAM_CATEGORY_NAME IS NOT NULL'

    -- 如果指定了系统，添加系统过滤条件
    IF @System IS NOT NULL AND @System != ''
    BEGIN
        SET @SQL = @SQL + N' AND SYSTEM_SOURCE_NO = ''' + @System + ''''
    END

    SET @SQL = @SQL + N' ORDER BY EXAM_CATEGORY_NAME'

    EXEC sp_executesql @SQL
END
GO
