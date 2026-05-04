-- 执行科室映射关系（基于实际医院系统）
-- 包含消化内镜等执行科室

USE WiNEX_PACS;

-- 1. 执行科室列表（前台选择）
PRINT '=== 执行科室列表（前台选择）===';
SELECT DISTINCT
    '放射科' AS 执行科室
UNION
SELECT DISTINCT
    '消化内镜(总)' AS 执行科室
UNION
SELECT DISTINCT
    '超声科' AS 执行科室
UNION
SELECT DISTINCT
    '检验科' AS 执行科室
UNION
SELECT DISTINCT
    '病理科' AS 执行科室
ORDER BY 执行科室;

-- 2. 执行科室到检查类别的映射
PRINT '';
PRINT '=== 执行科室到检查类别映射 ===';
SELECT DISTINCT
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'消化胃镜', N'消化肠镜', N'内镜') THEN '消化内镜(总)'
        WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '放射科'
        WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN '放射科'
        WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN '放射科'
        ELSE '其他科室'
    END AS 执行科室,
    EXAM_CATEGORY_NAME AS 检查类别
FROM EXAM_TASK 
WHERE IS_DEL = 0
ORDER BY 执行科室, 检查类别;

-- 3. 消化内镜执行科室统计
PRINT '';
PRINT '=== 消化内镜(总)执行科室统计 ===';
SELECT 
    '消化内镜(总)' AS 执行科室,
    EXAM_CATEGORY_NAME AS 检查类别,
    COUNT(DISTINCT EXAM_TASK_ID) AS 检查数量,
    MAX(CREATED_AT) AS 最新检查时间
FROM EXAM_TASK 
WHERE IS_DEL = 0
  AND EXAM_CATEGORY_NAME IN (N'消化胃镜', N'消化肠镜', N'内镜', N'WJ', N'CJ')
GROUP BY EXAM_CATEGORY_NAME
ORDER BY 检查数量 DESC;

-- 4. 放射科执行科室统计
PRINT '';
PRINT '=== 放射科执行科室统计 ===';
SELECT 
    '放射科' AS 执行科室,
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放类'
        WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN 'CT类'
        WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN 'MRI类'
        ELSE '其他类'
    END AS 检查类别,
    COUNT(DISTINCT EXAM_TASK_ID) AS 检查数量,
    MAX(CREATED_AT) AS 最新检查时间
FROM EXAM_TASK 
WHERE IS_DEL = 0
  AND SYSTEM_SOURCE_NO = 'RIS'
  AND EXAM_CATEGORY_NAME NOT IN (N'消化胃镜', N'消化肠镜', N'内镜', N'WJ', N'CJ')
GROUP BY 
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放类'
        WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN 'CT类'
        WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN 'MRI类'
        ELSE '其他类'
    END
ORDER BY 检查数量 DESC;

-- 5. 各执行科室工作量统计（显示给用户）
PRINT '';
PRINT '=== 各执行科室工作量统计 ===';
SELECT 
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'消化胃镜', N'消化肠镜', N'内镜', N'WJ', N'CJ') THEN '消化内镜(总)'
        WHEN SYSTEM_SOURCE_NO = 'RIS' THEN '放射科'
        ELSE '其他科室'
    END AS 执行科室,
    COUNT(DISTINCT EXAM_TASK_ID) AS 总检查数量,
    MAX(CREATED_AT) AS 最新检查时间
FROM EXAM_TASK 
WHERE IS_DEL = 0
  AND EXAM_TASK_STATUS >= 50
GROUP BY 
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'消化胃镜', N'消化肠镜', N'内镜', N'WJ', N'CJ') THEN '消化内镜(总)'
        WHEN SYSTEM_SOURCE_NO = 'RIS' THEN '放射科'
        ELSE '其他科室'
    END
ORDER BY 总检查数量 DESC;

-- 6. 消化内镜设备分布
PRINT '';
PRINT '=== 消化内镜设备分布 ===';
SELECT 
    '消化内镜(总)' AS 执行科室,
    EXAM_EQUIPMENT_NAME AS 设备名称,
    COUNT(DISTINCT EXAM_TASK_ID) AS 检查数量
FROM EXAM_TASK 
WHERE IS_DEL = 0
  AND EXAM_CATEGORY_NAME IN (N'消化胃镜', N'消化肠镜', N'内镜', N'WJ', N'CJ')
GROUP BY EXAM_EQUIPMENT_NAME
ORDER BY 检查数量 DESC;