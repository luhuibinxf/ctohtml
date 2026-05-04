CREATE PROCEDURE usp_TJFX_RadiologyWorkloadStatistics
    @startDate DATE,
    @endDate DATE,
    @system VARCHAR(50) = ''
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        ISNULL(CASE SYSTEM_SOURCE_NO 
            WHEN 'UIS' THEN '超声' 
            WHEN 'RIS' THEN '放射' 
            WHEN 'EIS' THEN '内镜' 
            WHEN 'PIS' THEN '病理'
            WHEN 'NMS' THEN '核医学'
            ELSE SYSTEM_SOURCE_NO 
        END, '') AS 系统,
        COUNT(*) AS 任务数量,
        SUM(CASE WHEN RESULT_STATUS = '已审核' THEN 1 ELSE 0 END) AS 已审核数量,
        SUM(CASE WHEN RESULT_STATUS = '未审核' THEN 1 ELSE 0 END) AS 未审核数量,
        ROUND(AVG(DATEDIFF(MINUTE, CREATE_TIME, UPDATE_TIME)), 2) AS 平均处理时长(分钟)
    FROM TJYHB
    WHERE CREATE_TIME >= @startDate AND CREATE_TIME < DATEADD(DAY, 1, @endDate)
        AND (@system = '' OR SYSTEM_SOURCE_NO = @system)
    GROUP BY SYSTEM_SOURCE_NO
    ORDER BY 任务数量 DESC;
END
GO

CREATE PROCEDURE usp_TJFX_RadiologyWorkloadStatistics_Detail
    @startDate DATE,
    @endDate DATE,
    @system VARCHAR(50) = ''
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        ID,
        PATIENT_NAME AS 患者姓名,
        PATIENT_ID AS 患者ID,
        EXAM_NO AS 检查号,
        STUDY_UID AS 检查UID,
        ISNULL(CASE SYSTEM_SOURCE_NO 
            WHEN 'UIS' THEN '超声' 
            WHEN 'RIS' THEN '放射' 
            WHEN 'EIS' THEN '内镜' 
            WHEN 'PIS' THEN '病理'
            WHEN 'NMS' THEN '核医学'
            ELSE SYSTEM_SOURCE_NO 
        END, '') AS 系统,
        DEPARTMENT_NAME AS 科室,
        CATEGORY_NAME AS 检查类型,
        REPORTER_NAME AS 报告医生,
        REVIEWER_NAME AS 审核医生,
        RESULT_STATUS AS 状态,
        CREATE_TIME AS 创建时间,
        UPDATE_TIME AS 更新时间
    FROM TJYHB
    WHERE CREATE_TIME >= @startDate AND CREATE_TIME < DATEADD(DAY, 1, @endDate)
        AND (@system = '' OR SYSTEM_SOURCE_NO = @system)
    ORDER BY CREATE_TIME DESC;
END
GO