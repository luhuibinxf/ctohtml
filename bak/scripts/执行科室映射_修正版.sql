-- 执行科室映射关系
-- 明确区分执行科室、检查类别和检查项目

USE WiNEX_PACS;

-- 1. 执行科室定义（前台选择用）
PRINT '=== 执行科室列表（前台选择）===';
SELECT DISTINCT
    '放射科' AS 执行科室
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

-- 2. 放射科内部科室分类
PRINT '';
PRINT '=== 放射科内部科室分类 ===';
SELECT 
    '放射科' AS 执行科室,
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放室'
        WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN 'CT室'
        WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN 'MRI室'
        ELSE '其他室'
    END AS 检查科室,
    COUNT(DISTINCT EXAM_TASK_ID) AS 检查数量
FROM EXAM_TASK 
WHERE IS_DEL = 0
  AND SYSTEM_SOURCE_NO = 'RIS'
  AND EXAM_TASK_STATUS >= 50
GROUP BY 
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放室'
        WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN 'CT室'
        WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN 'MRI室'
        ELSE '其他室'
    END
ORDER BY 检查数量 DESC;

-- 3. 执行科室到检查类别的映射
PRINT '';
PRINT '=== 执行科室到检查类别映射 ===';
SELECT DISTINCT
    '放射科' AS 执行科室,
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放'
        WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN 'CT'
        WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN 'MRI'
        ELSE '其他'
    END AS 检查类别,
    EXAM_CATEGORY_NAME AS 具体检查项目
FROM EXAM_TASK 
WHERE IS_DEL = 0
  AND SYSTEM_SOURCE_NO = 'RIS'
ORDER BY 执行科室, 检查类别, 具体检查项目;

-- 4. 按执行科室统计（显示给用户）
PRINT '';
PRINT '=== 执行科室工作量统计（显示给用户）===';
SELECT 
    '放射科' AS 执行科室,
    COUNT(DISTINCT EXAM_TASK_ID) AS 总检查数量,
    MAX(CREATED_AT) AS 最新检查时间
FROM EXAM_TASK 
WHERE IS_DEL = 0
  AND SYSTEM_SOURCE_NO = 'RIS'
  AND EXAM_TASK_STATUS >= 50
UNION ALL
SELECT 
    '其他科室' AS 执行科室,
    COUNT(DISTINCT EXAM_TASK_ID) AS 总检查数量,
    MAX(CREATED_AT) AS 最新检查时间
FROM EXAM_TASK 
WHERE IS_DEL = 0
  AND SYSTEM_SOURCE_NO != 'RIS'
  AND EXAM_TASK_STATUS >= 50
ORDER BY 总检查数量 DESC;

-- 5. 放射科各检查科室统计
PRINT '';
PRINT '=== 放射科各检查科室统计 ===';
SELECT 
    '放射科' AS 执行科室,
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放室'
        WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN 'CT室'
        WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN 'MRI室'
        ELSE '其他室'
    END AS 检查科室,
    COUNT(DISTINCT EXAM_TASK_ID) AS 检查数量,
    MAX(CREATED_AT) AS 最新检查时间
FROM EXAM_TASK 
WHERE IS_DEL = 0
  AND SYSTEM_SOURCE_NO = 'RIS'
  AND EXAM_TASK_STATUS >= 50
GROUP BY 
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放室'
        WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN 'CT室'
        WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN 'MRI室'
        ELSE '其他室'
    END
ORDER BY 检查数量 DESC;