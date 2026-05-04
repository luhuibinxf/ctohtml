-- 4月1号检查工作量统计
-- 更新日期：2026-04-27

-- ==============================================
-- 1. 4月1号总体工作量
-- ==============================================
SELECT 
    COUNT(*) AS '总检查数'
FROM 
    EXAM_TASK
WHERE 
    IS_DEL = 0
    AND CONVERT(DATE, EXAM_TASK_CREATE_TIME) = '2026-04-01';

-- ==============================================
-- 2. 4月1号按执行科室统计
-- ==============================================
SELECT 
    CASE 
        WHEN EXAM_CATEGORY_NAME IN ('肠镜(老)', '肠镜(新)', '超声内镜', '胃镜(老)', '胃镜(新)', '消化肠镜', '消化胃镜') THEN '消化内镜(总)'
        WHEN EXAM_CATEGORY_NAME IN ('支气管镜(新)', '支气管镜(总)') THEN '呼吸内镜科'
        WHEN EXAM_CATEGORY_NAME IN ('CT', 'CT(新)', '核磁共振', '钼靶', '普放', '普放(新)', '消化道造影', '消化道造影(新)') THEN '放射科'
        WHEN EXAM_CATEGORY_NAME = '介入超声' THEN '超声科'
        WHEN EXAM_CATEGORY_NAME = '脑电' THEN '神经内科'
        WHEN EXAM_CATEGORY_NAME IN ('体检彩超', '新城体检彩超') THEN '体检科'
        ELSE '其他科室'
    END AS '执行科室',
    COUNT(*) AS '检查数量'
FROM 
    EXAM_TASK
WHERE 
    IS_DEL = 0
    AND CONVERT(DATE, EXAM_TASK_CREATE_TIME) = '2026-04-01'
GROUP BY 
    CASE 
        WHEN EXAM_CATEGORY_NAME IN ('肠镜(老)', '肠镜(新)', '超声内镜', '胃镜(老)', '胃镜(新)', '消化肠镜', '消化胃镜') THEN '消化内镜(总)'
        WHEN EXAM_CATEGORY_NAME IN ('支气管镜(新)', '支气管镜(总)') THEN '呼吸内镜科'
        WHEN EXAM_CATEGORY_NAME IN ('CT', 'CT(新)', '核磁共振', '钼靶', '普放', '普放(新)', '消化道造影', '消化道造影(新)') THEN '放射科'
        WHEN EXAM_CATEGORY_NAME = '介入超声' THEN '超声科'
        WHEN EXAM_CATEGORY_NAME = '脑电' THEN '神经内科'
        WHEN EXAM_CATEGORY_NAME IN ('体检彩超', '新城体检彩超') THEN '体检科'
        ELSE '其他科室'
    END
ORDER BY 
    COUNT(*) DESC;

-- ==============================================
-- 3. 4月1号按检查类型统计
-- ==============================================
SELECT 
    EXAM_CATEGORY_NAME AS '检查类型',
    COUNT(*) AS '检查数量'
FROM 
    EXAM_TASK
WHERE 
    IS_DEL = 0
    AND CONVERT(DATE, EXAM_TASK_CREATE_TIME) = '2026-04-01'
GROUP BY 
    EXAM_CATEGORY_NAME
ORDER BY 
    COUNT(*) DESC;

-- ==============================================
-- 4. 4月1号按系统统计
-- ==============================================
SELECT 
    SYSTEM_SOURCE_NO AS '系统',
    COUNT(*) AS '检查数量'
FROM 
    EXAM_TASK
WHERE 
    IS_DEL = 0
    AND CONVERT(DATE, EXAM_TASK_CREATE_TIME) = '2026-04-01'
GROUP BY 
    SYSTEM_SOURCE_NO
ORDER BY 
    COUNT(*) DESC;

-- ==============================================
-- 5. 4月1号按状态统计
-- ==============================================
SELECT 
    EXAM_TASK_STATUS AS '任务状态',
    COUNT(*) AS '检查数量'
FROM 
    EXAM_TASK
WHERE 
    IS_DEL = 0
    AND CONVERT(DATE, EXAM_TASK_CREATE_TIME) = '2026-04-01'
GROUP BY 
    EXAM_TASK_STATUS
ORDER BY 
    COUNT(*) DESC;

-- ==============================================
-- 6. 4月1号报告医生工作量
-- ==============================================
SELECT 
    r.REPORTER_NAME AS '医生姓名',
    COUNT(*) AS '报告数量'
FROM 
    EXAM_REPORT r
JOIN 
    EXAM_TASK t ON r.EXAM_TASK_ID = t.EXAM_TASK_ID
WHERE 
    r.IS_DEL = 0
    AND t.IS_DEL = 0
    AND CONVERT(DATE, t.EXAM_TASK_CREATE_TIME) = '2026-04-01'
GROUP BY 
    r.REPORTER_NAME
ORDER BY 
    COUNT(*) DESC;

-- ==============================================
-- 7. 4月1号审核医生工作量
-- ==============================================
SELECT 
    r.REVIEWER_NAME AS '医生姓名',
    COUNT(*) AS '审核数量'
FROM 
    EXAM_REPORT r
JOIN 
    EXAM_TASK t ON r.EXAM_TASK_ID = t.EXAM_TASK_ID
WHERE 
    r.IS_DEL = 0
    AND t.IS_DEL = 0
    AND CONVERT(DATE, t.EXAM_TASK_CREATE_TIME) = '2026-04-01'
GROUP BY 
    r.REVIEWER_NAME
ORDER BY 
    COUNT(*) DESC;

-- ==============================================
-- 8. 4月1号技师工作量
-- ==============================================
SELECT 
    TECHNICIAN_NAME AS '技师姓名',
    COUNT(*) AS '检查数量'
FROM 
    EXAM_TASK
WHERE 
    IS_DEL = 0
    AND CONVERT(DATE, EXAM_TASK_CREATE_TIME) = '2026-04-01'
GROUP BY 
    TECHNICIAN_NAME
ORDER BY 
    COUNT(*) DESC;