-- 数据库汇总信息（包含执行科室映射）
-- 更新日期：2026-04-25

-- ==============================================
-- 1. 系统信息汇总
-- ==============================================
SELECT 
    SYSTEM_SOURCE_NO AS '系统标识',
    COUNT(*) AS '任务数量',
    MAX(EXAM_TASK_CREATE_TIME) AS '最新任务时间'
FROM 
    EXAM_TASK
WHERE 
    IS_DEL = 0
GROUP BY 
    SYSTEM_SOURCE_NO
ORDER BY 
    COUNT(*) DESC;

-- ==============================================
-- 2. 执行科室汇总（按系统）
-- ==============================================
SELECT 
    SYSTEM_SOURCE_NO AS '系统',
    CASE 
        WHEN EXAM_CATEGORY_NAME IN ('肠镜(老)', '肠镜(新)', '超声内镜', '胃镜(老)', '胃镜(新)', '消化肠镜', '消化胃镜') THEN '消化内镜(总)'
        WHEN EXAM_CATEGORY_NAME IN ('支气管镜(新)', '支气管镜(总)') THEN '呼吸内镜科'
        WHEN EXAM_CATEGORY_NAME IN ('CT', 'CT(新)', '核磁共振', '钼靶', '普放', '普放(新)', '消化道造影', '消化道造影(新)') THEN '放射科'
        WHEN EXAM_CATEGORY_NAME = '介入超声' THEN '超声科'
        WHEN EXAM_CATEGORY_NAME = '脑电' THEN '神经内科'
        WHEN EXAM_CATEGORY_NAME IN ('体检彩超', '新城体检彩超') THEN '体检科'
        ELSE '其他科室'
    END AS '执行科室',
    COUNT(*) AS '任务数量',
    MAX(EXAM_TASK_CREATE_TIME) AS '最新任务时间'
FROM 
    EXAM_TASK
WHERE 
    IS_DEL = 0
GROUP BY 
    SYSTEM_SOURCE_NO,
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
    SYSTEM_SOURCE_NO, '执行科室';

-- ==============================================
-- 3. 执行科室详细映射
-- ==============================================
SELECT 
    SYSTEM_SOURCE_NO AS '系统',
    EXAM_CATEGORY_NAME AS '检查类型',
    CASE 
        WHEN EXAM_CATEGORY_NAME IN ('肠镜(老)', '肠镜(新)', '超声内镜', '胃镜(老)', '胃镜(新)', '消化肠镜', '消化胃镜') THEN '消化内镜(总)'
        WHEN EXAM_CATEGORY_NAME IN ('支气管镜(新)', '支气管镜(总)') THEN '呼吸内镜科'
        WHEN EXAM_CATEGORY_NAME IN ('CT', 'CT(新)', '核磁共振', '钼靶', '普放', '普放(新)', '消化道造影', '消化道造影(新)') THEN '放射科'
        WHEN EXAM_CATEGORY_NAME = '介入超声' THEN '超声科'
        WHEN EXAM_CATEGORY_NAME = '脑电' THEN '神经内科'
        WHEN EXAM_CATEGORY_NAME IN ('体检彩超', '新城体检彩超') THEN '体检科'
        ELSE '其他科室'
    END AS '执行科室',
    COUNT(*) AS '任务数量'
FROM 
    EXAM_TASK
WHERE 
    IS_DEL = 0
GROUP BY 
    SYSTEM_SOURCE_NO,
    EXAM_CATEGORY_NAME
ORDER BY 
    SYSTEM_SOURCE_NO, '执行科室', EXAM_CATEGORY_NAME;

-- ==============================================
-- 4. 检查类别汇总
-- ==============================================
SELECT 
    EXAM_CATEGORY_NAME AS '检查类别',
    COUNT(*) AS '数量',
    MAX(EXAM_TASK_CREATE_TIME) AS '最新检查时间'
FROM 
    EXAM_TASK
WHERE 
    IS_DEL = 0
GROUP BY 
    EXAM_CATEGORY_NAME
ORDER BY 
    COUNT(*) DESC;

-- ==============================================
-- 5. 人员属性汇总 - 报告医生
-- ==============================================
SELECT 
    REPORTER_NAME AS '医生姓名',
    COUNT(*) AS '报告数量',
    MAX(REPORT_TIME) AS '最新报告时间'
FROM 
    EXAM_REPORT
WHERE 
    IS_DEL = 0
GROUP BY 
    REPORTER_NAME
ORDER BY 
    COUNT(*) DESC;

-- ==============================================
-- 6. 人员属性汇总 - 审核医生
-- ==============================================
SELECT 
    REVIEWER_NAME AS '医生姓名',
    COUNT(*) AS '审核数量',
    MAX(REVIEW_TIME) AS '最新审核时间'
FROM 
    EXAM_REPORT
WHERE 
    IS_DEL = 0
GROUP BY 
    REVIEWER_NAME
ORDER BY 
    COUNT(*) DESC;

-- ==============================================
-- 7. 人员属性汇总 - 技师
-- ==============================================
SELECT 
    TECHNICIAN_NAME AS '技师姓名',
    COUNT(*) AS '检查数量',
    MAX(EXAM_TASK_CREATE_TIME) AS '最新检查时间'
FROM 
    EXAM_TASK
WHERE 
    IS_DEL = 0
GROUP BY 
    TECHNICIAN_NAME
ORDER BY 
    COUNT(*) DESC;

-- ==============================================
-- 8. 系统状态汇总
-- ==============================================
SELECT 
    EXAM_TASK_STATUS AS '任务状态',
    COUNT(*) AS '数量',
    MAX(EXAM_TASK_CREATE_TIME) AS '最新时间'
FROM 
    EXAM_TASK
WHERE 
    IS_DEL = 0
GROUP BY 
    EXAM_TASK_STATUS
ORDER BY 
    COUNT(*) DESC;

-- ==============================================
-- 9. 执行科室工作量排名
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
    COUNT(*) AS '总任务数',
    SUM(CASE WHEN EXAM_TASK_STATUS = '已完成' THEN 1 ELSE 0 END) AS '已完成数',
    ROUND(SUM(CASE WHEN EXAM_TASK_STATUS = '已完成' THEN 1 ELSE 0 END) * 100.0 / COUNT(*), 2) AS '完成率',
    MAX(EXAM_TASK_CREATE_TIME) AS '最新任务时间'
FROM 
    EXAM_TASK
WHERE 
    IS_DEL = 0
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