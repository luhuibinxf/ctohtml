
-- =============================================
-- 放射科工作量统计系统 - 存储过程创建脚本
-- 基于用户提供的 sp_RadiologyWorkloadStatistics
-- 按照项目规范使用 usp_ 前缀命名
-- =============================================

-- =============================================
-- 存储过程: usp_RadiologyWorkloadStatistics
-- 功能: 按报告医生/审核医生/技师统计工作量
-- =============================================

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_RadiologyWorkloadStatistics')
BEGIN
    DROP PROCEDURE usp_RadiologyWorkloadStatistics;
    PRINT '存储过程 usp_RadiologyWorkloadStatistics 已删除';
END
GO

CREATE PROCEDURE [dbo].[usp_RadiologyWorkloadStatistics]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @StatisticsType VARCHAR(20) = '报告医生'   -- '报告医生', '审核医生', '技师'
AS
BEGIN
    SET NOCOUNT ON;

    WITH CleanCharge AS (
        SELECT EXAM_TASK_ID, EXAM_ITEM_NAME
        FROM EXAM_CHARGE_ITEM
        WHERE IS_DEL = 0 AND EXAM_ITEM_NAME NOT LIKE N'%+%'
    ),
    BaseData AS (
        SELECT
            t.EXAM_TASK_ID,
            t.EXAM_CATEGORY_NAME,
            ci.EXAM_ITEM_NAME,
            CASE @StatisticsType
                WHEN '报告医生' THEN r.REPORTER_NAME
                WHEN '审核医生' THEN r.REVIEWER_NAME
                WHEN '技师'     THEN t.TECHNICIAN_NAME
            END AS WorkerName,
            CASE WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)') THEN 1 ELSE 0 END AS IsGeneralXray,
            CASE WHEN t.EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)') THEN 1 ELSE 0 END AS IsCT,
            CASE WHEN t.EXAM_CATEGORY_NAME = N'核磁共振' THEN 1 ELSE 0 END AS IsMRI,
            CASE WHEN t.EXAM_CATEGORY_NAME = N'钼靶' THEN 1 ELSE 0 END AS IsMammo,
            CASE WHEN t.EXAM_CATEGORY_NAME IN (N'消化道造影', N'消化道造影(新)') THEN 1 ELSE 0 END AS IsContrast,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%脊柱全长%' OR ci.EXAM_ITEM_NAME LIKE N'%下肢全长%' THEN 1 ELSE 0 END AS IsLongLegSpine,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%CTA%' OR ci.EXAM_ITEM_NAME LIKE N'%CTV%' OR
                      ci.EXAM_ITEM_NAME LIKE N'%CTU%' OR ci.EXAM_ITEM_NAME LIKE N'%CTP%' OR
                      ci.EXAM_ITEM_NAME LIKE N'%CCTA%' OR ci.EXAM_ITEM_NAME LIKE N'%三维%' OR
                      ci.EXAM_ITEM_NAME LIKE N'%重建%' THEN 1 ELSE 0 END AS IsCTEnhance,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%增强%' THEN 1 ELSE 0 END AS IsMRIEnhance,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%MRA%' THEN 1 ELSE 0 END AS IsMRA,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%MRU%' THEN 1 ELSE 0 END AS IsMRU,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%MRCP%' THEN 1 ELSE 0 END AS IsMRCP,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%乳腺%' THEN 1 ELSE 0 END AS IsBreast,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%心脏%' THEN 1 ELSE 0 END AS IsHeart,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%腹部%' THEN 1 ELSE 0 END AS IsAbdomen,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%盆腔%' THEN 1 ELSE 0 END AS IsPelvic,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%前列腺%' THEN 1 ELSE 0 END AS IsProstate,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%直肠%' THEN 1 ELSE 0 END AS IsRectum,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%胎儿%' THEN 1 ELSE 0 END AS IsFetus,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%胎盘%' THEN 1 ELSE 0 END AS IsPlacenta
        FROM EXAM_TASK t
        INNER JOIN EXAM_REPORT r ON t.EXAM_TASK_ID = r.EXAM_TASK_ID AND r.IS_DEL = 0
        INNER JOIN CleanCharge ci ON t.EXAM_TASK_ID = ci.EXAM_TASK_ID
        WHERE t.IS_DEL = 0 AND r.IS_DEL = 0
          AND t.SYSTEM_SOURCE_NO = 'RIS' AND t.EXAM_TASK_STATUS = 70
          AND t.CREATED_AT >= @StartDate AND t.CREATED_AT <= @EndDate
          AND (
              (@StatisticsType = '报告医生' AND r.REPORTER_NAME IS NOT NULL)
              OR (@StatisticsType = '审核医生' AND r.REVIEWER_NAME IS NOT NULL)
              OR (@StatisticsType = '技师' AND t.TECHNICIAN_NAME IS NOT NULL)
          )
    )
    SELECT
        WorkerName AS 医生姓名,
        SUM(CASE WHEN IsGeneralXray = 1 AND IsLongLegSpine = 0 THEN 1 ELSE 0 END) AS DR,
        SUM(CASE WHEN IsGeneralXray = 1 AND IsLongLegSpine = 1 THEN 1 ELSE 0 END) AS 下肢及脊柱全长拍片,
        SUM(CASE WHEN IsMammo = 1 THEN 1 ELSE 0 END) AS 钼靶,
        SUM(CASE WHEN IsContrast = 1 THEN 1 ELSE 0 END) AS 造影,
        SUM(CASE WHEN IsCT = 1 AND IsCTEnhance = 0 THEN 1 ELSE 0 END) AS CT平扫,
        SUM(CASE WHEN IsCT = 1 AND IsCTEnhance = 1 THEN 1 ELSE 0 END) AS CT增强,
        SUM(CASE WHEN IsMRI = 1 AND IsMRIEnhance = 0 THEN 1 ELSE 0 END) AS MRI平扫,
        SUM(CASE WHEN IsMRA = 1 THEN 1 ELSE 0 END) AS MRA,
        SUM(CASE WHEN IsMRU = 1 THEN 1 ELSE 0 END) AS MRU,
        SUM(CASE WHEN IsMRCP = 1 THEN 1 ELSE 0 END) AS MRCP,
        SUM(CASE WHEN (IsAbdomen=1 OR IsPelvic=1 OR IsProstate=1 OR IsRectum=1 OR IsFetus=1 OR IsPlacenta=1) THEN 1 ELSE 0 END) AS [腹部盆腔前列腺直肠癌胎儿胎盘],
        SUM(CASE WHEN IsMRI = 1 AND IsMRIEnhance = 1 THEN 1 ELSE 0 END) AS MRI增强,
        SUM(CASE WHEN IsBreast = 1 THEN 1 ELSE 0 END) AS 乳腺,
        SUM(CASE WHEN IsHeart = 1 THEN 1 ELSE 0 END) AS 心脏
    FROM BaseData
    WHERE WorkerName IS NOT NULL
    GROUP BY WorkerName
    HAVING COUNT(DISTINCT EXAM_TASK_ID) > 0
    ORDER BY COUNT(DISTINCT EXAM_TASK_ID) DESC;

    SET NOCOUNT OFF;
END
GO
PRINT '存储过程 usp_RadiologyWorkloadStatistics 创建成功';

-- =============================================
-- 存储过程: usp_RadiologyWorkloadStatistics_Group
-- 功能: 按分组统计（普放组/CT组/MRI组）
-- =============================================

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_RadiologyWorkloadStatistics_Group')
BEGIN
    DROP PROCEDURE usp_RadiologyWorkloadStatistics_Group;
    PRINT '存储过程 usp_RadiologyWorkloadStatistics_Group 已删除';
END
GO

CREATE PROCEDURE [dbo].[usp_RadiologyWorkloadStatistics_Group]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @StatisticsType VARCHAR(20) = '报告医生'
AS
BEGIN
    SET NOCOUNT ON;

    WITH CleanCharge AS (
        SELECT EXAM_TASK_ID, EXAM_ITEM_NAME
        FROM EXAM_CHARGE_ITEM
        WHERE IS_DEL = 0 AND EXAM_ITEM_NAME NOT LIKE N'%+%'
    ),
    BaseData AS (
        SELECT
            CASE @StatisticsType
                WHEN '报告医生' THEN r.REPORTER_NAME
                WHEN '审核医生' THEN r.REVIEWER_NAME
                WHEN '技师'     THEN t.TECHNICIAN_NAME
            END AS WorkerName,
            CASE WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放组'
                 WHEN t.EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)') THEN 'CT组'
                 WHEN t.EXAM_CATEGORY_NAME = N'核磁共振' THEN 'MRI组'
                 ELSE '其他' END AS WorkGroup,
            CASE WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)') AND 
                      ci.EXAM_ITEM_NAME NOT LIKE N'%脊柱全长%' AND ci.EXAM_ITEM_NAME NOT LIKE N'%下肢全长%' THEN 1 ELSE 0 END AS DR,
            CASE WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)') AND 
                      (ci.EXAM_ITEM_NAME LIKE N'%脊柱全长%' OR ci.EXAM_ITEM_NAME LIKE N'%下肢全长%') THEN 1 ELSE 0 END AS LongLegSpine,
            CASE WHEN t.EXAM_CATEGORY_NAME = N'钼靶' THEN 1 ELSE 0 END AS Mammo,
            CASE WHEN t.EXAM_CATEGORY_NAME IN (N'消化道造影', N'消化道造影(新)') THEN 1 ELSE 0 END AS Contrast,
            CASE WHEN t.EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)') AND
                      ci.EXAM_ITEM_NAME NOT LIKE N'%CTA%' AND ci.EXAM_ITEM_NAME NOT LIKE N'%CTV%' AND
                      ci.EXAM_ITEM_NAME NOT LIKE N'%CTU%' AND ci.EXAM_ITEM_NAME NOT LIKE N'%CTP%' AND
                      ci.EXAM_ITEM_NAME NOT LIKE N'%CCTA%' AND ci.EXAM_ITEM_NAME NOT LIKE N'%三维%' AND
                      ci.EXAM_ITEM_NAME NOT LIKE N'%重建%' THEN 1 ELSE 0 END AS CTPlain,
            CASE WHEN t.EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)') AND
                      (ci.EXAM_ITEM_NAME LIKE N'%CTA%' OR ci.EXAM_ITEM_NAME LIKE N'%CTV%' OR
                       ci.EXAM_ITEM_NAME LIKE N'%CTU%' OR ci.EXAM_ITEM_NAME LIKE N'%CTP%' OR
                       ci.EXAM_ITEM_NAME LIKE N'%CCTA%' OR ci.EXAM_ITEM_NAME LIKE N'%三维%' OR
                       ci.EXAM_ITEM_NAME LIKE N'%重建%') THEN 1 ELSE 0 END AS CTEnhance,
            CASE WHEN t.EXAM_CATEGORY_NAME = N'核磁共振' AND ci.EXAM_ITEM_NAME NOT LIKE N'%增强%' THEN 1 ELSE 0 END AS MRIPlain,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%MRA%' THEN 1 ELSE 0 END AS MRA,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%MRU%' THEN 1 ELSE 0 END AS MRU,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%MRCP%' THEN 1 ELSE 0 END AS MRCP,
            CASE WHEN t.EXAM_CATEGORY_NAME = N'核磁共振' AND (ci.EXAM_ITEM_NAME LIKE N'%腹部%' OR ci.EXAM_ITEM_NAME LIKE N'%盆腔%' OR
                      ci.EXAM_ITEM_NAME LIKE N'%前列腺%' OR ci.EXAM_ITEM_NAME LIKE N'%直肠%' OR
                      ci.EXAM_ITEM_NAME LIKE N'%胎儿%' OR ci.EXAM_ITEM_NAME LIKE N'%胎盘%') THEN 1 ELSE 0 END AS AbdomenPelvic,
            CASE WHEN t.EXAM_CATEGORY_NAME = N'核磁共振' AND ci.EXAM_ITEM_NAME LIKE N'%增强%' THEN 1 ELSE 0 END AS MRIEnhance,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%乳腺%' THEN 1 ELSE 0 END AS Breast,
            CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%心脏%' THEN 1 ELSE 0 END AS Heart
        FROM EXAM_TASK t
        INNER JOIN EXAM_REPORT r ON t.EXAM_TASK_ID = r.EXAM_TASK_ID AND r.IS_DEL = 0
        INNER JOIN CleanCharge ci ON t.EXAM_TASK_ID = ci.EXAM_TASK_ID
        WHERE t.IS_DEL = 0 AND r.IS_DEL = 0
          AND t.SYSTEM_SOURCE_NO = 'RIS' AND t.EXAM_TASK_STATUS = 70
          AND t.CREATED_AT >= @StartDate AND t.CREATED_AT <= @EndDate
          AND (
              (@StatisticsType = '报告医生' AND r.REPORTER_NAME IS NOT NULL)
              OR (@StatisticsType = '审核医生' AND r.REVIEWER_NAME IS NOT NULL)
              OR (@StatisticsType = '技师' AND t.TECHNICIAN_NAME IS NOT NULL)
          )
    )
    SELECT
        WorkerName AS 医生姓名,
        WorkGroup AS 工作分组,
        SUM(DR) AS DR,
        SUM(LongLegSpine) AS 下肢及脊柱全长拍片,
        SUM(Mammo) AS 钼靶,
        SUM(Contrast) AS 造影,
        SUM(CTPlain) AS CT平扫,
        SUM(CTEnhance) AS CT增强,
        SUM(MRIPlain) AS MRI平扫,
        SUM(MRA) AS MRA,
        SUM(MRU) AS MRU,
        SUM(MRCP) AS MRCP,
        SUM(AbdomenPelvic) AS [腹部盆腔前列腺直肠癌胎儿胎盘],
        SUM(MRIEnhance) AS MRI增强,
        SUM(Breast) AS 乳腺,
        SUM(Heart) AS 心脏,
        SUM(DR + LongLegSpine + Mammo + Contrast) AS 普放组合计,
        SUM(CTPlain + CTEnhance) AS CT组合计,
        SUM(MRIPlain + MRA + MRU + MRCP + AbdomenPelvic + MRIEnhance + Breast + Heart) AS MRI组合计,
        SUM(DR + LongLegSpine + Mammo + Contrast + CTPlain + CTEnhance + MRIPlain + MRA + MRU + MRCP + AbdomenPelvic + MRIEnhance + Breast + Heart) AS 总计
    FROM BaseData
    WHERE WorkerName IS NOT NULL
    GROUP BY WorkerName, WorkGroup
    ORDER BY WorkGroup, 总计 DESC;

    SET NOCOUNT OFF;
END
GO
PRINT '存储过程 usp_RadiologyWorkloadStatistics_Group 创建成功';

-- =============================================
-- 存储过程: usp_RadiologyWorkloadStatistics_Summary
-- 功能: 获取汇总统计
-- =============================================

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_RadiologyWorkloadStatistics_Summary')
BEGIN
    DROP PROCEDURE usp_RadiologyWorkloadStatistics_Summary;
    PRINT '存储过程 usp_RadiologyWorkloadStatistics_Summary 已删除';
END
GO

CREATE PROCEDURE [dbo].[usp_RadiologyWorkloadStatistics_Summary]
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    EXEC usp_RadiologyWorkloadStatistics @StartDate, @EndDate, '报告医生';

    SET NOCOUNT OFF;
END
GO
PRINT '存储过程 usp_RadiologyWorkloadStatistics_Summary 创建成功';

-- =============================================
-- 存储过程: usp_RadiologyWorkloadStatistics_Detail
-- 功能: 获取指定医生的工作量明细
-- =============================================

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'usp_RadiologyWorkloadStatistics_Detail')
BEGIN
    DROP PROCEDURE usp_RadiologyWorkloadStatistics_Detail;
    PRINT '存储过程 usp_RadiologyWorkloadStatistics_Detail 已删除';
END
GO

CREATE PROCEDURE [dbo].[usp_RadiologyWorkloadStatistics_Detail]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @WorkerName VARCHAR(50),
    @StatisticsType VARCHAR(20) = '报告医生'
AS
BEGIN
    SET NOCOUNT ON;

    WITH CleanCharge AS (
        SELECT EXAM_TASK_ID, EXAM_ITEM_NAME
        FROM EXAM_CHARGE_ITEM
        WHERE IS_DEL = 0 AND EXAM_ITEM_NAME NOT LIKE N'%+%'
    )
    SELECT
        t.EXAM_TASK_ID,
        t.PATIENT_NAME AS 患者姓名,
        t.PATIENT_ID AS 患者ID,
        t.EXAM_CATEGORY_NAME AS 检查类别,
        ci.EXAM_ITEM_NAME AS 检查项目,
        CASE @StatisticsType
            WHEN '报告医生' THEN r.REPORTER_NAME
            WHEN '审核医生' THEN r.REVIEWER_NAME
            WHEN '技师'     THEN t.TECHNICIAN_NAME
        END AS 医生姓名,
        t.EXEC_DEPT_NAME AS 执行科室,
        t.CREATED_AT AS 创建时间,
        t.EXAM_TIME AS 检查时间,
        r.REPORT_TIME AS 报告时间,
        CASE t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)') THEN '普放组'
             WHEN t.EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)') THEN 'CT组'
             WHEN t.EXAM_CATEGORY_NAME = N'核磁共振' THEN 'MRI组'
             WHEN t.EXAM_CATEGORY_NAME = N'钼靶' THEN '普放组'
             WHEN t.EXAM_CATEGORY_NAME IN (N'消化道造影', N'消化道造影(新)') THEN '普放组'
             ELSE '其他' END AS 工作分组,
        CASE WHEN ci.EXAM_ITEM_NAME LIKE N'%脊柱全长%' OR ci.EXAM_ITEM_NAME LIKE N'%下肢全长%' THEN '下肢及脊柱全长拍片'
             WHEN ci.EXAM_ITEM_NAME LIKE N'%CTA%' OR ci.EXAM_ITEM_NAME LIKE N'%CTV%' OR
                  ci.EXAM_ITEM_NAME LIKE N'%CTU%' OR ci.EXAM_ITEM_NAME LIKE N'%CTP%' OR
                  ci.EXAM_ITEM_NAME LIKE N'%CCTA%' OR ci.EXAM_ITEM_NAME LIKE N'%三维%' OR
                  ci.EXAM_ITEM_NAME LIKE N'%重建%' THEN 'CT增强'
             WHEN ci.EXAM_ITEM_NAME LIKE N'%增强%' THEN 'MRI增强'
             WHEN ci.EXAM_ITEM_NAME LIKE N'%MRA%' THEN 'MRA'
             WHEN ci.EXAM_ITEM_NAME LIKE N'%MRU%' THEN 'MRU'
             WHEN ci.EXAM_ITEM_NAME LIKE N'%MRCP%' THEN 'MRCP'
             WHEN ci.EXAM_ITEM_NAME LIKE N'%乳腺%' THEN '乳腺'
             WHEN ci.EXAM_ITEM_NAME LIKE N'%心脏%' THEN '心脏'
             WHEN ci.EXAM_ITEM_NAME LIKE N'%腹部%' OR ci.EXAM_ITEM_NAME LIKE N'%盆腔%' OR
                  ci.EXAM_ITEM_NAME LIKE N'%前列腺%' OR ci.EXAM_ITEM_NAME LIKE N'%直肠%' OR
                  ci.EXAM_ITEM_NAME LIKE N'%胎儿%' OR ci.EXAM_ITEM_NAME LIKE N'%胎盘%' THEN '腹部盆腔前列腺直肠胎儿胎盘'
             WHEN t.EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)') THEN 'CT平扫'
             WHEN t.EXAM_CATEGORY_NAME = N'核磁共振' THEN 'MRI平扫'
             WHEN t.EXAM_CATEGORY_NAME = N'钼靶' THEN '钼靶'
             WHEN t.EXAM_CATEGORY_NAME IN (N'消化道造影', N'消化道造影(新)') THEN '造影'
             WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)') THEN 'DR'
             ELSE '其他' END AS 细分类别
    FROM EXAM_TASK t
    INNER JOIN EXAM_REPORT r ON t.EXAM_TASK_ID = r.EXAM_TASK_ID AND r.IS_DEL = 0
    INNER JOIN CleanCharge ci ON t.EXAM_TASK_ID = ci.EXAM_TASK_ID
    WHERE t.IS_DEL = 0 AND r.IS_DEL = 0
      AND t.SYSTEM_SOURCE_NO = 'RIS' AND t.EXAM_TASK_STATUS = 70
      AND t.CREATED_AT >= @StartDate AND t.CREATED_AT <= @EndDate
      AND (
          (@StatisticsType = '报告医生' AND r.REPORTER_NAME = @WorkerName)
          OR (@StatisticsType = '审核医生' AND r.REVIEWER_NAME = @WorkerName)
          OR (@StatisticsType = '技师' AND t.TECHNICIAN_NAME = @WorkerName)
      )
    ORDER BY t.CREATED_AT DESC;

    SET NOCOUNT OFF;
END
GO
PRINT '存储过程 usp_RadiologyWorkloadStatistics_Detail 创建成功';

PRINT '=============================================';
PRINT '存储过程创建完成';
PRINT '=============================================';
GO