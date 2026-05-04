-- 审核医生工作量
-- 更新日期：2026-04-25

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