-- =============================================
-- 统计分析系统 - 存储过程创建脚本
-- =============================================

-- =============================================
-- 影像中心工作量统计
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'proc_RadiologyWorkload')
BEGIN
    EXEC('CREATE PROCEDURE proc_RadiologyWorkload AS BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE proc_RadiologyWorkload
    @StartDate DATE,
    @EndDate DATE,
    @StatisticsType VARCHAR(50) = '报告医生'
AS
BEGIN
    SET NOCOUNT ON;

    IF @StatisticsType = '报告医生'
    BEGIN
        SELECT 
            报告医生,
            COUNT(*) AS 任务数量,
            SUM(CASE WHEN 阳性标识 = 1 THEN 1 ELSE 0 END) AS 阳性数,
            CASE WHEN COUNT(*) > 0 THEN CAST(SUM(CASE WHEN 阳性标识 = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2)) ELSE 0 END AS 阳性率
        FROM 检查记录表 WITH(NOLOCK)
        WHERE 检查日期 BETWEEN @StartDate AND @EndDate
          AND IS_DEL = 0
        GROUP BY 报告医生
        ORDER BY 任务数量 DESC;
    END
    ELSE IF @StatisticsType = '审核医生'
    BEGIN
        SELECT 
            审核医生,
            COUNT(*) AS 任务数量,
            SUM(CASE WHEN 阳性标识 = 1 THEN 1 ELSE 0 END) AS 阳性数,
            CASE WHEN COUNT(*) > 0 THEN CAST(SUM(CASE WHEN 阳性标识 = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2)) ELSE 0 END AS 阳性率
        FROM 检查记录表 WITH(NOLOCK)
        WHERE 检查日期 BETWEEN @StartDate AND @EndDate
          AND IS_DEL = 0
        GROUP BY 审核医生
        ORDER BY 任务数量 DESC;
    END
    ELSE IF @StatisticsType = '技师'
    BEGIN
        SELECT 
            技师,
            COUNT(*) AS 任务数量,
            SUM(CASE WHEN 阳性标识 = 1 THEN 1 ELSE 0 END) AS 阳性数,
            CASE WHEN COUNT(*) > 0 THEN CAST(SUM(CASE WHEN 阳性标识 = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2)) ELSE 0 END AS 阳性率
        FROM 检查记录表 WITH(NOLOCK)
        WHERE 检查日期 BETWEEN @StartDate AND @EndDate
          AND IS_DEL = 0
        GROUP BY 技师
        ORDER BY 任务数量 DESC;
    END
END
GO
PRINT '存储过程 proc_RadiologyWorkload 创建成功';

-- =============================================
-- 科室工作量汇总
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'proc_DepartmentWorkloadSummary')
BEGIN
    EXEC('CREATE PROCEDURE proc_DepartmentWorkloadSummary AS BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE proc_DepartmentWorkloadSummary
    @StartDate DATE,
    @EndDate DATE,
    @SystemType VARCHAR(20) = ''
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        执行科室,
        检查类别,
        COUNT(*) AS 任务数量,
        SUM(CASE WHEN 阳性标识 = 1 THEN 1 ELSE 0 END) AS 阳性数,
        CASE WHEN COUNT(*) > 0 THEN CAST(SUM(CASE WHEN 阳性标识 = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2)) ELSE 0 END AS 阳性率
    FROM 检查记录表 WITH(NOLOCK)
    WHERE 检查日期 BETWEEN @StartDate AND @EndDate
      AND IS_DEL = 0
      AND (@SystemType = '' OR 系统类型 = @SystemType)
    GROUP BY 执行科室, 检查类别
    ORDER BY 执行科室, 检查类别;
END
GO
PRINT '存储过程 proc_DepartmentWorkloadSummary 创建成功';

-- =============================================
-- 每日检查统计
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'proc_DailyCheckStatistics')
BEGIN
    EXEC('CREATE PROCEDURE proc_DailyCheckStatistics AS BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE proc_DailyCheckStatistics
    @StatDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        系统类型,
        检查类别,
        COUNT(*) AS 检查总数,
        SUM(CASE WHEN 阳性标识 = 1 THEN 1 ELSE 0 END) AS 阳性数,
        SUM(CASE WHEN 阳性标识 = 0 THEN 1 ELSE 0 END) AS 阴性数,
        CASE WHEN COUNT(*) > 0 THEN CAST(SUM(CASE WHEN 阳性标识 = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2)) ELSE 0 END AS 阳性率,
        COUNT(DISTINCT 报告医生) AS 医生人数
    FROM 检查记录表 WITH(NOLOCK)
    WHERE 检查日期 = @StatDate
      AND IS_DEL = 0
    GROUP BY 系统类型, 检查类别
    ORDER BY 系统类型, 检查类别;
END
GO
PRINT '存储过程 proc_DailyCheckStatistics 创建成功';

-- =============================================
-- 医生工作量明细
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.procedures WHERE name = 'proc_DoctorWorkloadDetail')
BEGIN
    EXEC('CREATE PROCEDURE proc_DoctorWorkloadDetail AS BEGIN SET NOCOUNT ON; END')
END
GO

ALTER PROCEDURE proc_DoctorWorkloadDetail
    @StartDate DATE,
    @EndDate DATE,
    @DoctorName VARCHAR(50) = '',
    @DoctorType VARCHAR(20) = '报告医生'
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        检查日期,
        CASE WHEN @DoctorType = '报告医生' THEN 报告医生 ELSE 审核医生 END AS 医生姓名,
        检查类别,
        检查编号,
        患者姓名,
        阳性标识,
        报告时间
    FROM 检查记录表 WITH(NOLOCK)
    WHERE 检查日期 BETWEEN @StartDate AND @EndDate
      AND IS_DEL = 0
      AND (@DoctorName = '' OR 报告医生 = @DoctorName OR 审核医生 = @DoctorName)
    ORDER BY 检查日期 DESC, 报告时间 DESC;
END
GO
PRINT '存储过程 proc_DoctorWorkloadDetail 创建成功';

PRINT '=============================================';
PRINT '存储过程创建完成';
PRINT '=============================================';
GO