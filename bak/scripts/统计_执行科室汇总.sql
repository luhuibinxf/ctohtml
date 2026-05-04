-- 执行科室汇总
-- 更新日期：2026-04-25

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
    COUNT(*) AS '任务数量',
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