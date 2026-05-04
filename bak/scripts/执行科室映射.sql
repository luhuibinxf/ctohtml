-- 执行科室与检查分类映射查询
-- 用于前台选择执行科室时的数据转换

USE WiNEX_PACS;

-- 1. 执行科室映射表（用于前台选择）
PRINT '=== 执行科室列表（前台选择）===';
SELECT 
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放科'
        WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN 'CT科'
        WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN 'MRI科'
        ELSE '其他科'
    END AS 执行科室,
    COUNT(DISTINCT EXAM_TASK_ID) AS 任务数量,
    MAX(CREATED_AT) AS 最新任务时间
FROM EXAM_TASK 
WHERE IS_DEL = 0
  AND SYSTEM_SOURCE_NO = 'RIS'
  AND EXAM_TASK_STATUS >= 50
GROUP BY 
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放科'
        WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN 'CT科'
        WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN 'MRI科'
        ELSE '其他科'
    END
ORDER BY 任务数量 DESC;

-- 2. 执行科室到检查分类的映射关系
PRINT '';
PRINT '=== 执行科室到检查分类映射 ===';
SELECT DISTINCT
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放科'
        WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN 'CT科'
        WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN 'MRI科'
        ELSE '其他科'
    END AS 执行科室,
    EXAM_CATEGORY_NAME AS 检查分类
FROM EXAM_TASK 
WHERE IS_DEL = 0
  AND SYSTEM_SOURCE_NO = 'RIS'
ORDER BY 执行科室, 检查分类;

-- 3. 检查科室统计（显示给用户的科室名称）
PRINT '';
PRINT '=== 检查科室统计（显示给用户）===';
SELECT 
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放科'
        WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN 'CT科'
        WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN 'MRI科'
        ELSE '其他科'
    END AS 检查科室,
    COUNT(DISTINCT EXAM_TASK_ID) AS 检查数量,
    MAX(CREATED_AT) AS 最新检查时间
FROM EXAM_TASK 
WHERE IS_DEL = 0
  AND SYSTEM_SOURCE_NO = 'RIS'
  AND EXAM_TASK_STATUS >= 50
GROUP BY 
    CASE 
        WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放科'
        WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN 'CT科'
        WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN 'MRI科'
        ELSE '其他科'
    END
ORDER BY 检查数量 DESC;

-- 4. 按执行科室的工作量统计（带检查分类明细）
PRINT '';
PRINT '=== 执行科室工作量明细 ===';
WITH DepartmentData AS (
    SELECT 
        CASE 
            WHEN EXAM_CATEGORY_NAME IN (N'普放', N'普放(新)', N'钼靶', N'消化道造影', N'消化道造影(新)') THEN '普放科'
            WHEN EXAM_CATEGORY_NAME IN (N'CT', N'CT(新)', N'CTA', N'CTA(新)', N'CT重建', N'CT三维重建') THEN 'CT科'
            WHEN EXAM_CATEGORY_NAME IN (N'核磁共振', N'核磁共振增强', N'MRI增强', N'MRA', N'MRV', N'MRU', N'MRCP', N'MRS') THEN 'MRI科'
            ELSE '其他科'
        END AS 执行科室,
        EXAM_CATEGORY_NAME AS 检查分类,
        EXAM_TASK_ID
    FROM EXAM_TASK 
    WHERE IS_DEL = 0
      AND SYSTEM_SOURCE_NO = 'RIS'
      AND EXAM_TASK_STATUS >= 50
)
SELECT 
    执行科室,
    检查分类,
    COUNT(DISTINCT EXAM_TASK_ID) AS 数量
FROM DepartmentData
GROUP BY 执行科室, 检查分类
ORDER BY 执行科室, 数量 DESC;