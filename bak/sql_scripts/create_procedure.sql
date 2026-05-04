-- 创建放射科工作量统计存储过程
CREATE PROCEDURE sp_RadiologyWorkloadStatistics
    @StartDate DATETIME,
    @EndDate DATETIME,
    @StatisticsType VARCHAR(20) = 'reporter' -- reporter:报告医生, reviewer:审核医生, technician:技师
AS
BEGIN
    SET NOCOUNT ON;

    -- 验证参数
    IF @StartDate IS NULL OR @EndDate IS NULL
    BEGIN
        RAISERROR('开始日期和结束日期不能为空', 16, 1);
        RETURN;
    END

    IF @StatisticsType NOT IN ('reporter', 'reviewer', 'technician')
    BEGIN
        RAISERROR('统计类型必须是 reporter、reviewer 或 technician', 16, 1);
        RETURN;
    END

    -- 主统计查询：根据统计类型执行相应的工作量统计
    IF @StatisticsType = 'reporter'
    BEGIN
        SELECT 
            r.REPORTER_NAME AS 医生姓名, 
            -- DR统计（普放类别中排除下肢及脊柱全长拍片）
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)') AND 
                         NOT (ri.EXAM_ITEM_NAME LIKE N'%脊柱全长%' OR ri.EXAM_ITEM_NAME LIKE N'%下肢全长%') THEN 1 ELSE 0 END) AS DR,
            -- 下肢及脊柱全长拍片统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)') AND 
                         (ri.EXAM_ITEM_NAME LIKE N'%脊柱全长%' OR ri.EXAM_ITEM_NAME LIKE N'%下肢全长%') THEN 1 ELSE 0 END) AS 下肢及脊柱全长拍片,
            -- 钼靶统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'钼靶' THEN 1 ELSE 0 END) AS 钼靶,
            -- 造影统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME IN (N'消化道造影', N'消化道造影(新)') THEN 1 ELSE 0 END) AS 造影,
            -- 普放组统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN 1 ELSE 0 END) AS 普放组,
            -- CT平扫统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'CT' THEN 1 ELSE 0 END) AS CT平扫,
            -- CT增强统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'CT(新)' THEN 1 ELSE 0 END) AS CT增强,
            -- CCTA统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%CTA%' THEN 1 ELSE 0 END) AS CCTA,
            -- CT重建统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%重建%' THEN 1 ELSE 0 END) AS CT重建,
            -- CT组统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME LIKE N'CT%' THEN 1 ELSE 0 END) AS CT组,
            -- MRI平扫统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'核磁共振' THEN 1 ELSE 0 END) AS MRI平扫,
            -- MRI增强统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%增强%' AND t.EXAM_CATEGORY_NAME = N'核磁共振' THEN 1 ELSE 0 END) AS MRI增强,
            -- MRA统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRA%' THEN 1 ELSE 0 END) AS MRA,
            -- MRV统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRV%' THEN 1 ELSE 0 END) AS MRV,
            -- MRU统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRU%' THEN 1 ELSE 0 END) AS MRU,
            -- MRCP统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRCP%' THEN 1 ELSE 0 END) AS MRCP,
            -- MRS统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRS%' THEN 1 ELSE 0 END) AS MRS,
            -- MRI组统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'核磁共振' THEN 1 ELSE 0 END) AS MRI组,
            -- 总工作量
            COUNT(DISTINCT t.EXAM_TASK_ID) AS 总工作量 
        FROM EXAM_REPORT r
        INNER JOIN EXAM_TASK t ON r.EXAM_TASK_ID = t.EXAM_TASK_ID AND t.IS_DEL = 0
        LEFT JOIN EXAM_CHARGE_ITEM ri ON t.EXAM_TASK_ID = ri.EXAM_TASK_ID AND ri.IS_DEL = 0
        WHERE r.IS_DEL = 0
          AND t.SYSTEM_SOURCE_NO = 'RIS'
          AND t.EXAM_TASK_STATUS >= 50
          AND t.CREATED_AT >= @StartDate
          AND t.CREATED_AT <= @EndDate
          AND r.REPORTER_NAME IS NOT NULL
        GROUP BY r.REPORTER_NAME
        ORDER BY 总工作量 DESC;
    END
    ELSE IF @StatisticsType = 'reviewer'
    BEGIN
        SELECT 
            r.REVIEWER_NAME AS 医生姓名, 
            -- DR统计（普放类别中排除下肢及脊柱全长拍片）
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)') AND 
                         NOT (ri.EXAM_ITEM_NAME LIKE N'%脊柱全长%' OR ri.EXAM_ITEM_NAME LIKE N'%下肢全长%') THEN 1 ELSE 0 END) AS DR,
            -- 下肢及脊柱全长拍片统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)') AND 
                         (ri.EXAM_ITEM_NAME LIKE N'%脊柱全长%' OR ri.EXAM_ITEM_NAME LIKE N'%下肢全长%') THEN 1 ELSE 0 END) AS 下肢及脊柱全长拍片,
            -- 钼靶统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'钼靶' THEN 1 ELSE 0 END) AS 钼靶,
            -- 造影统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME IN (N'消化道造影', N'消化道造影(新)') THEN 1 ELSE 0 END) AS 造影,
            -- 普放组统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN 1 ELSE 0 END) AS 普放组,
            -- CT平扫统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'CT' THEN 1 ELSE 0 END) AS CT平扫,
            -- CT增强统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'CT(新)' THEN 1 ELSE 0 END) AS CT增强,
            -- CCTA统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%CTA%' THEN 1 ELSE 0 END) AS CCTA,
            -- CT重建统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%重建%' THEN 1 ELSE 0 END) AS CT重建,
            -- CT组统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME LIKE N'CT%' THEN 1 ELSE 0 END) AS CT组,
            -- MRI平扫统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'核磁共振' THEN 1 ELSE 0 END) AS MRI平扫,
            -- MRI增强统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%增强%' AND t.EXAM_CATEGORY_NAME = N'核磁共振' THEN 1 ELSE 0 END) AS MRI增强,
            -- MRA统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRA%' THEN 1 ELSE 0 END) AS MRA,
            -- MRV统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRV%' THEN 1 ELSE 0 END) AS MRV,
            -- MRU统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRU%' THEN 1 ELSE 0 END) AS MRU,
            -- MRCP统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRCP%' THEN 1 ELSE 0 END) AS MRCP,
            -- MRS统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRS%' THEN 1 ELSE 0 END) AS MRS,
            -- MRI组统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'核磁共振' THEN 1 ELSE 0 END) AS MRI组,
            -- 总工作量
            COUNT(DISTINCT t.EXAM_TASK_ID) AS 总工作量 
        FROM EXAM_REPORT r
        INNER JOIN EXAM_TASK t ON r.EXAM_TASK_ID = t.EXAM_TASK_ID AND t.IS_DEL = 0
        LEFT JOIN EXAM_CHARGE_ITEM ri ON t.EXAM_TASK_ID = ri.EXAM_TASK_ID AND ri.IS_DEL = 0
        WHERE r.IS_DEL = 0
          AND t.SYSTEM_SOURCE_NO = 'RIS'
          AND t.EXAM_TASK_STATUS >= 50
          AND t.CREATED_AT >= @StartDate
          AND t.CREATED_AT <= @EndDate
          AND r.REVIEWER_NAME IS NOT NULL
        GROUP BY r.REVIEWER_NAME
        ORDER BY 总工作量 DESC;
    END
    ELSE IF @StatisticsType = 'technician'
    BEGIN
        SELECT 
            t.TECHNICIAN_NAME AS 技师姓名, 
            -- DR统计（普放类别中排除下肢及脊柱全长拍片）
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)') AND 
                         NOT (ri.EXAM_ITEM_NAME LIKE N'%脊柱全长%' OR ri.EXAM_ITEM_NAME LIKE N'%下肢全长%') THEN 1 ELSE 0 END) AS DR,
            -- 下肢及脊柱全长拍片统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)') AND 
                         (ri.EXAM_ITEM_NAME LIKE N'%脊柱全长%' OR ri.EXAM_ITEM_NAME LIKE N'%下肢全长%') THEN 1 ELSE 0 END) AS 下肢及脊柱全长拍片,
            -- 钼靶统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'钼靶' THEN 1 ELSE 0 END) AS 钼靶,
            -- 造影统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME IN (N'消化道造影', N'消化道造影(新)') THEN 1 ELSE 0 END) AS 造影,
            -- 普放组统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN 1 ELSE 0 END) AS 普放组,
            -- CT平扫统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'CT' THEN 1 ELSE 0 END) AS CT平扫,
            -- CT增强统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'CT(新)' THEN 1 ELSE 0 END) AS CT增强,
            -- CCTA统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%CTA%' THEN 1 ELSE 0 END) AS CCTA,
            -- CT重建统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%重建%' THEN 1 ELSE 0 END) AS CT重建,
            -- CT组统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME LIKE N'CT%' THEN 1 ELSE 0 END) AS CT组,
            -- MRI平扫统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'核磁共振' THEN 1 ELSE 0 END) AS MRI平扫,
            -- MRI增强统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%增强%' AND t.EXAM_CATEGORY_NAME = N'核磁共振' THEN 1 ELSE 0 END) AS MRI增强,
            -- MRA统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRA%' THEN 1 ELSE 0 END) AS MRA,
            -- MRV统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRV%' THEN 1 ELSE 0 END) AS MRV,
            -- MRU统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRU%' THEN 1 ELSE 0 END) AS MRU,
            -- MRCP统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRCP%' THEN 1 ELSE 0 END) AS MRCP,
            -- MRS统计
            SUM(CASE WHEN ri.EXAM_ITEM_NAME LIKE N'%MRS%' THEN 1 ELSE 0 END) AS MRS,
            -- MRI组统计
            SUM(CASE WHEN t.EXAM_CATEGORY_NAME = N'核磁共振' THEN 1 ELSE 0 END) AS MRI组,
            -- 总工作量
            COUNT(DISTINCT t.EXAM_TASK_ID) AS 总工作量 
        FROM EXAM_TASK t
        INNER JOIN EXAM_REPORT r ON t.EXAM_TASK_ID = r.EXAM_TASK_ID AND t.IS_DEL = 0
        LEFT JOIN EXAM_CHARGE_ITEM ri ON t.EXAM_TASK_ID = ri.EXAM_TASK_ID AND ri.IS_DEL = 0
        WHERE t.IS_DEL = 0
          AND r.IS_DEL = 0
          AND t.SYSTEM_SOURCE_NO = 'RIS'
          AND t.EXAM_TASK_STATUS >= 50
          AND t.CREATED_AT >= @StartDate
          AND t.CREATED_AT <= @EndDate
          AND t.TECHNICIAN_NAME IS NOT NULL
        GROUP BY t.TECHNICIAN_NAME
        ORDER BY 总工作量 DESC;
    END

    SET NOCOUNT OFF;
END;
