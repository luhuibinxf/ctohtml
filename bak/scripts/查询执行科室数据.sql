-- 查询执行科室真实数据
-- 直接在SQL Server Management Studio中执行

USE WiNEX_PACS;

-- 1. 系统信息
SELECT @@VERSION AS 数据库版本,
       DB_NAME() AS 当前数据库;

-- 2. 系统来源
SELECT DISTINCT SYSTEM_SOURCE_NO AS 系统来源
FROM EXAM_TASK 
WHERE IS_DEL = 0
ORDER BY SYSTEM_SOURCE_NO;

-- 3. 检查类别
SELECT DISTINCT EXAM_CATEGORY_NAME AS 检查类别
FROM EXAM_TASK 
WHERE IS_DEL = 0
ORDER BY EXAM_CATEGORY_NAME;

-- 4. 执行科室统计
SELECT 
    CASE 
        WHEN EXAM_CATEGORY_NAME LIKE '%胃镜%' OR EXAM_CATEGORY_NAME LIKE '%肠镜%' OR EXAM_CATEGORY_NAME LIKE '%内镜%' THEN '消化内镜(总)'
        WHEN SYSTEM_SOURCE_NO = 'RIS' THEN '放射科'
        ELSE '其他科室'
    END AS 执行科室,
    COUNT(DISTINCT EXAM_TASK_ID) AS 检查数量
FROM EXAM_TASK 
WHERE IS_DEL = 0
GROUP BY 
    CASE 
        WHEN EXAM_CATEGORY_NAME LIKE '%胃镜%' OR EXAM_CATEGORY_NAME LIKE '%肠镜%' OR EXAM_CATEGORY_NAME LIKE '%内镜%' THEN '消化内镜(总)'
        WHEN SYSTEM_SOURCE_NO = 'RIS' THEN '放射科'
        ELSE '其他科室'
    END
ORDER BY 检查数量 DESC;

-- 5. 放射科内部分类
SELECT 
    '放射科' AS 执行科室,
    CASE 
        WHEN EXAM_CATEGORY_NAME IN ('普放', '普放(新)', '钼靶', '消化道造影', '消化道造影(新)') THEN '普放类'
        WHEN EXAM_CATEGORY_NAME IN ('CT', 'CT(新)', 'CTA', 'CTA(新)', 'CT重建', 'CT三维重建') THEN 'CT类'
        WHEN EXAM_CATEGORY_NAME IN ('核磁共振', '核磁共振增强', 'MRI增强', 'MRA', 'MRV', 'MRU', 'MRCP', 'MRS') THEN 'MRI类'
        ELSE '其他类'
    END AS 检查类别,
    COUNT(DISTINCT EXAM_TASK_ID) AS 检查数量
FROM EXAM_TASK 
WHERE IS_DEL = 0
  AND SYSTEM_SOURCE_NO = 'RIS'
GROUP BY 
    CASE 
        WHEN EXAM_CATEGORY_NAME IN ('普放', '普放(新)', '钼靶', '消化道造影', '消化道造影(新)') THEN '普放类'
        WHEN EXAM_CATEGORY_NAME IN ('CT', 'CT(新)', 'CTA', 'CTA(新)', 'CT重建', 'CT三维重建') THEN 'CT类'
        WHEN EXAM_CATEGORY_NAME IN ('核磁共振', '核磁共振增强', 'MRI增强', 'MRA', 'MRV', 'MRU', 'MRCP', 'MRS') THEN 'MRI类'
        ELSE '其他类'
    END
ORDER BY 检查数量 DESC;