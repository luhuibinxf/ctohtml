-- 报告医生工作量
-- 更新日期：2026-04-25

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