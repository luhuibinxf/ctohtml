-- =============================================
-- 统计分析系统 - 初始化数据脚本
-- 创建日期: 2026-05-01
-- 说明: 初始化基础数据
-- =============================================

USE [WiNEX_PACS_Schema_Full]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- 1. 初始化编码映射数据
-- =============================================
PRINT '============================================='
PRINT '正在初始化编码映射数据...'
PRINT '============================================='

-- 系统映射
IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'SYSTEM' AND SOURCE_VALUE = 'UIS')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('SYSTEM', 'UIS', '超声', 1, '超声系统')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'SYSTEM' AND SOURCE_VALUE = 'RIS')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('SYSTEM', 'RIS', '放射', 2, '放射系统')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'SYSTEM' AND SOURCE_VALUE = 'EIS')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('SYSTEM', 'EIS', '内镜', 3, '内镜系统')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'SYSTEM' AND SOURCE_VALUE = 'PIS')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('SYSTEM', 'PIS', '病理', 4, '病理系统')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'SYSTEM' AND SOURCE_VALUE = 'NMS')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('SYSTEM', 'NMS', '核医学', 5, '核医学系统')

-- 病人类型映射
IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'PATIENT_TYPE' AND SOURCE_VALUE = '1')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('PATIENT_TYPE', '1', '门诊', 1, '门诊病人')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'PATIENT_TYPE' AND SOURCE_VALUE = '2')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('PATIENT_TYPE', '2', '住院', 2, '住院病人')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'PATIENT_TYPE' AND SOURCE_VALUE = '3')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('PATIENT_TYPE', '3', '急诊', 3, '急诊病人')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'PATIENT_TYPE' AND SOURCE_VALUE = '4')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('PATIENT_TYPE', '4', '体检', 4, '体检病人')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'PATIENT_TYPE' AND SOURCE_VALUE = '138138')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('PATIENT_TYPE', '138138', '门诊', 11, '门诊病人')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'PATIENT_TYPE' AND SOURCE_VALUE = '138139')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('PATIENT_TYPE', '138139', '急诊', 13, '急诊病人')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'PATIENT_TYPE' AND SOURCE_VALUE = '138140')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('PATIENT_TYPE', '138140', '体检', 14, '体检病人')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'PATIENT_TYPE' AND SOURCE_VALUE = '145235')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('PATIENT_TYPE', '145235', '住院', 12, '住院病人')

-- 结果状态映射
IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'RESULT_STATUS' AND SOURCE_VALUE = 'P')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('RESULT_STATUS', 'P', '阳性', 1, '阳性结果')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'RESULT_STATUS' AND SOURCE_VALUE = 'N')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('RESULT_STATUS', 'N', '阴性', 2, '阴性结果')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'RESULT_STATUS' AND SOURCE_VALUE = '383927')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('RESULT_STATUS', '383927', '阳性', 1, '阳性结果')

IF NOT EXISTS (SELECT 1 FROM TJFX_CODE_MAPPING WHERE CATEGORY = 'RESULT_STATUS' AND SOURCE_VALUE = '383926')
    INSERT INTO TJFX_CODE_MAPPING (CATEGORY, SOURCE_VALUE, TARGET_VALUE, SORT_ORDER, DESCRIPTION)
    VALUES ('RESULT_STATUS', '383926', '阴性', 2, '阴性结果')

PRINT '✅ 编码映射数据初始化完成'
GO

-- =============================================
-- 2. 初始化角色数据
-- =============================================
PRINT '============================================='
PRINT '正在初始化角色数据...'
PRINT '============================================='

IF NOT EXISTS (SELECT 1 FROM TJFX_ROLE WHERE ROLE_NAME = '管理员')
BEGIN
    INSERT INTO TJFX_ROLE (ROLE_NAME, PERMISSIONS, DESCRIPTION)
    VALUES (
        '管理员',
        '["user:manage","role:manage","config:manage","query:manage","db:manage","token:manage","view:all"]',
        '系统管理员，拥有全部权限'
    )
    PRINT '✅ 管理员角色创建成功'
END

IF NOT EXISTS (SELECT 1 FROM TJFX_ROLE WHERE ROLE_NAME = '科室管理员')
BEGIN
    INSERT INTO TJFX_ROLE (ROLE_NAME, PERMISSIONS, DESCRIPTION)
    VALUES (
        '科室管理员',
        '["query:dept","view:dept"]',
        '科室管理员，可查看本科室数据'
    )
    PRINT '✅ 科室管理员角色创建成功'
END

IF NOT EXISTS (SELECT 1 FROM TJFX_ROLE WHERE ROLE_NAME = '普通用户')
BEGIN
    INSERT INTO TJFX_ROLE (ROLE_NAME, PERMISSIONS, DESCRIPTION)
    VALUES (
        '普通用户',
        '["query:basic","view:basic"]',
        '普通用户，可查看基础统计'
    )
    PRINT '✅ 普通用户角色创建成功'
END
GO

-- =============================================
-- 3. 初始化菜单配置数据
-- =============================================
PRINT '============================================='
PRINT '正在初始化菜单配置数据...'
PRINT '============================================='

IF NOT EXISTS (SELECT 1 FROM TJFX_MENU_CONFIG WHERE MENU_NAME = '首页')
BEGIN
    INSERT INTO TJFX_MENU_CONFIG (MENU_NAME, MENU_URL, PARENT_ID, PERMISSION_LEVEL, SORT_ORDER, ICON)
    VALUES ('首页', '/dashboard', NULL, 0, 1, '🏠')
    PRINT '✅ 首页菜单创建成功'
END

IF NOT EXISTS (SELECT 1 FROM TJFX_MENU_CONFIG WHERE MENU_NAME = '统计分析')
BEGIN
    INSERT INTO TJFX_MENU_CONFIG (MENU_NAME, MENU_URL, PARENT_ID, PERMISSION_LEVEL, SORT_ORDER, ICON)
    VALUES ('统计分析', NULL, NULL, 0, 2, '📊')
    PRINT '✅ 统计分析菜单创建成功'
END

DECLARE @AnalysisMenuId INT
SELECT @AnalysisMenuId = ID FROM TJFX_MENU_CONFIG WHERE MENU_NAME = '统计分析'

IF @AnalysisMenuId IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM TJFX_MENU_CONFIG WHERE MENU_NAME = '每日分析' AND PARENT_ID = @AnalysisMenuId)
    BEGIN
        INSERT INTO TJFX_MENU_CONFIG (MENU_NAME, MENU_URL, PARENT_ID, PERMISSION_LEVEL, SORT_ORDER, ICON)
        VALUES ('每日分析', '/daily-analysis', @AnalysisMenuId, 0, 1, '📈')
        PRINT '✅ 每日分析菜单创建成功'
    END
    
    IF NOT EXISTS (SELECT 1 FROM TJFX_MENU_CONFIG WHERE MENU_NAME = '科室统计' AND PARENT_ID = @AnalysisMenuId)
    BEGIN
        INSERT INTO TJFX_MENU_CONFIG (MENU_NAME, MENU_URL, PARENT_ID, PERMISSION_LEVEL, SORT_ORDER, ICON)
        VALUES ('科室统计', '/department-statistics', @AnalysisMenuId, 0, 2, '🏥')
        PRINT '✅ 科室统计菜单创建成功'
    END
    
    IF NOT EXISTS (SELECT 1 FROM TJFX_MENU_CONFIG WHERE MENU_NAME = '医生统计' AND PARENT_ID = @AnalysisMenuId)
    BEGIN
        INSERT INTO TJFX_MENU_CONFIG (MENU_NAME, MENU_URL, PARENT_ID, PERMISSION_LEVEL, SORT_ORDER, ICON)
        VALUES ('医生统计', '/doctor-statistics', @AnalysisMenuId, 0, 3, '👨‍⚕️')
        PRINT '✅ 医生统计菜单创建成功'
    END
    
    IF NOT EXISTS (SELECT 1 FROM TJFX_MENU_CONFIG WHERE MENU_NAME = '检查类型统计' AND PARENT_ID = @AnalysisMenuId)
    BEGIN
        INSERT INTO TJFX_MENU_CONFIG (MENU_NAME, MENU_URL, PARENT_ID, PERMISSION_LEVEL, SORT_ORDER, ICON)
        VALUES ('检查类型统计', '/category-statistics', @AnalysisMenuId, 0, 4, '🔬')
        PRINT '✅ 检查类型统计菜单创建成功'
    END
END

IF NOT EXISTS (SELECT 1 FROM TJFX_MENU_CONFIG WHERE MENU_NAME = '系统配置')
BEGIN
    INSERT INTO TJFX_MENU_CONFIG (MENU_NAME, MENU_URL, PARENT_ID, PERMISSION_LEVEL, SORT_ORDER, ICON)
    VALUES ('系统配置', NULL, NULL, 2, 99, '⚙️')
    PRINT '✅ 系统配置菜单创建成功'
END

DECLARE @ConfigMenuId INT
SELECT @ConfigMenuId = ID FROM TJFX_MENU_CONFIG WHERE MENU_NAME = '系统配置'

IF @ConfigMenuId IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM TJFX_MENU_CONFIG WHERE MENU_NAME = '用户管理' AND PARENT_ID = @ConfigMenuId)
    BEGIN
        INSERT INTO TJFX_MENU_CONFIG (MENU_NAME, MENU_URL, PARENT_ID, PERMISSION_LEVEL, SORT_ORDER, ICON)
        VALUES ('用户管理', '/user-manage', @ConfigMenuId, 2, 1, '👥')
        PRINT '✅ 用户管理菜单创建成功'
    END
    
    IF NOT EXISTS (SELECT 1 FROM TJFX_MENU_CONFIG WHERE MENU_NAME = '查询配置' AND PARENT_ID = @ConfigMenuId)
    BEGIN
        INSERT INTO TJFX_MENU_CONFIG (MENU_NAME, MENU_URL, PARENT_ID, PERMISSION_LEVEL, SORT_ORDER, ICON)
        VALUES ('查询配置', '/query-config', @ConfigMenuId, 2, 2, '🔧')
        PRINT '✅ 查询配置菜单创建成功'
    END
    
    IF NOT EXISTS (SELECT 1 FROM TJFX_MENU_CONFIG WHERE MENU_NAME = '数据库配置' AND PARENT_ID = @ConfigMenuId)
    BEGIN
        INSERT INTO TJFX_MENU_CONFIG (MENU_NAME, MENU_URL, PARENT_ID, PERMISSION_LEVEL, SORT_ORDER, ICON)
        VALUES ('数据库配置', '/db-config', @ConfigMenuId, 2, 3, '💾')
        PRINT '✅ 数据库配置菜单创建成功'
    END
    
    IF NOT EXISTS (SELECT 1 FROM TJFX_MENU_CONFIG WHERE MENU_NAME = '令牌管理' AND PARENT_ID = @ConfigMenuId)
    BEGIN
        INSERT INTO TJFX_MENU_CONFIG (MENU_NAME, MENU_URL, PARENT_ID, PERMISSION_LEVEL, SORT_ORDER, ICON)
        VALUES ('令牌管理', '/token-manage', @ConfigMenuId, 2, 4, '🎫')
        PRINT '✅ 令牌管理菜单创建成功'
    END
END
GO

-- =============================================
-- 4. 初始化查询配置数据
-- =============================================
PRINT '============================================='
PRINT '正在初始化查询配置数据...'
PRINT '============================================='

IF NOT EXISTS (SELECT 1 FROM TJFX_QUERY_CONFIG WHERE QUERY_NAME = '每日分析')
BEGIN
    INSERT INTO TJFX_QUERY_CONFIG (QUERY_NAME, QUERY_TYPE, QUERY_SQL, PARAMS_MAPPING, PERMISSION_REQUIRED, DESCRIPTION)
    VALUES (
        '每日分析',
        'ANALYSIS',
        'SELECT 
            ISNULL(
                CASE t.SYSTEM_SOURCE_NO 
                    WHEN ''UIS'' THEN ''超声'' 
                    WHEN ''RIS'' THEN ''放射'' 
                    WHEN ''EIS'' THEN ''内镜'' 
                    WHEN ''PIS'' THEN ''病理''
                    WHEN ''NMS'' THEN ''核医学''
                    ELSE t.SYSTEM_SOURCE_NO 
                END,
                ''''
            ) AS 系统,
            ISNULL(r.REPORTER_NAME, '''') AS 报告医生,
            ISNULL(r.REVIEWER_NAME, '''') AS 审核医生,
            ISNULL(t.TECHNICIAN_NAME, '''') AS 技师,
            ISNULL(t.EXEC_DEPT_NAME, '''') AS 执行科室,
            ISNULL(t.EXAM_CATEGORY_NAME, '''') AS 检查类型,
            ISNULL(
                CASE t.ENCOUNTER_TYPE_NO 
                    WHEN ''1'' THEN ''门诊'' 
                    WHEN ''2'' THEN ''住院'' 
                    WHEN ''3'' THEN ''急诊'' 
                    WHEN ''4'' THEN ''体检''
                    WHEN ''138138'' THEN ''门诊'' 
                    WHEN ''138139'' THEN ''急诊''
                    WHEN ''138140'' THEN ''体检'' 
                    WHEN ''145235'' THEN ''住院''
                    WHEN ''OPD'' THEN ''门诊'' 
                    WHEN ''IPD'' THEN ''住院'' 
                    WHEN ''EMER'' THEN ''急诊'' 
                    WHEN ''CHECKUP'' THEN ''体检''
                    ELSE ISNULL(d.cValue, t.ENCOUNTER_TYPE_NO) 
                END,
                ''''
            ) AS 病人类型,
            ISNULL(
                CASE r.NEG_POS_CODE 
                    WHEN ''383927'' THEN ''阳性'' 
                    WHEN ''383926'' THEN ''阴性'' 
                    WHEN ''P'' THEN ''阳性'' 
                    WHEN ''N'' THEN ''阴性'' 
                    WHEN ''Y'' THEN ''阳性'' 
                    WHEN ''POS'' THEN ''阳性'' 
                    WHEN ''NEG'' THEN ''阴性'' 
                    ELSE ''未知'' 
                END,
                ''''
            ) AS 结果状态,
            COUNT(*) AS 任务数量,
            SUM(CASE WHEN r.NEG_POS_CODE IN (''383927'',''P'',''Y'',''POS'') THEN 1 ELSE 0 END) AS 阳性数量,
            SUM(CASE WHEN r.NEG_POS_CODE IN (''383926'',''N'',''NEG'') THEN 1 ELSE 0 END) AS 阴性数量,
            ROUND(
                CASE WHEN COUNT(*) > 0 
                    THEN SUM(CASE WHEN r.NEG_POS_CODE IN (''383927'',''P'',''Y'',''POS'') THEN 1.0 ELSE 0.0 END) * 100.0 / COUNT(*) 
                    ELSE 0.0 
                END,
                2
            ) AS 阳性率
        FROM EXAM_TASK t WITH(NOLOCK)
        INNER JOIN EXAM_REPORT r WITH(NOLOCK) ON t.EXAM_TASK_ID = r.EXAM_TASK_ID
        LEFT JOIN Pacs_SysDict d WITH(NOLOCK) ON d.TableName = ''EXAM_TASK'' 
            AND d.FieldName = ''ENCOUNTER_TYPE_NO'' 
            AND (CAST(d.nValue AS VARCHAR(50)) = t.ENCOUNTER_TYPE_NO OR d.cValue = t.ENCOUNTER_TYPE_NO)
        WHERE t.IS_DEL = 0
            AND (@StartDate IS NULL OR t.CREATED_AT >= @StartDate)
            AND (@EndDate IS NULL OR t.CREATED_AT < DATEADD(DAY, 1, @EndDate))
            AND (@System IS NULL OR @System = '''' OR t.SYSTEM_SOURCE_NO = @System)
            AND (@Reporter IS NULL OR @Reporter = '''' OR r.REPORTER_NAME = @Reporter)
            AND (@Reviewer IS NULL OR @Reviewer = '''' OR r.REVIEWER_NAME = @Reviewer)
            AND (@Technician IS NULL OR @Technician = '''' OR t.TECHNICIAN_NAME = @Technician)
            AND (@Department IS NULL OR @Department = '''' OR t.EXEC_DEPT_NAME = @Department)
            AND (@Category IS NULL OR @Category = '''' OR t.EXAM_CATEGORY_NAME = @Category)
            AND (@PatientType IS NULL OR @PatientType = '''' OR t.ENCOUNTER_TYPE_NO = @PatientType)
            AND (@ResultStatus IS NULL OR @ResultStatus = '''' OR r.NEG_POS_CODE = @ResultStatus)
        GROUP BY
            ISNULL(CASE t.SYSTEM_SOURCE_NO WHEN ''UIS'' THEN ''超声'' WHEN ''RIS'' THEN ''放射'' WHEN ''EIS'' THEN ''内镜'' WHEN ''PIS'' THEN ''病理'' WHEN ''NMS'' THEN ''核医学'' ELSE t.SYSTEM_SOURCE_NO END,''''),
            ISNULL(r.REPORTER_NAME,''''),
            ISNULL(r.REVIEWER_NAME,''''),
            ISNULL(t.TECHNICIAN_NAME,''''),
            ISNULL(t.EXEC_DEPT_NAME,''''),
            ISNULL(t.EXAM_CATEGORY_NAME,''''),
            ISNULL(CASE t.ENCOUNTER_TYPE_NO WHEN ''1'' THEN ''门诊'' WHEN ''2'' THEN ''住院'' WHEN ''3'' THEN ''急诊'' WHEN ''4'' THEN ''体检'' WHEN ''138138'' THEN ''门诊'' WHEN ''138139'' THEN ''急诊'' WHEN ''138140'' THEN ''体检'' WHEN ''145235'' THEN ''住院'' WHEN ''OPD'' THEN ''门诊'' WHEN ''IPD'' THEN ''住院'' WHEN ''EMER'' THEN ''急诊'' WHEN ''CHECKUP'' THEN ''体检'' ELSE ISNULL(d.cValue, t.ENCOUNTER_TYPE_NO) END,''''),
            ISNULL(CASE r.NEG_POS_CODE WHEN ''383927'' THEN ''阳性'' WHEN ''383926'' THEN ''阴性'' WHEN ''P'' THEN ''阳性'' WHEN ''N'' THEN ''阴性'' WHEN ''Y'' THEN ''阳性'' WHEN ''POS'' THEN ''阳性'' WHEN ''NEG'' THEN ''阴性'' ELSE ''未知'' END,'''')
        ORDER BY 任务数量 DESC',
        '[{"name":"StartDate","display":"开始日期","type":"date","required":true},{"name":"EndDate","display":"结束日期","type":"date","required":true},{"name":"System","display":"系统","type":"select","required":false},{"name":"Reporter","display":"报告医生","type":"select","required":false},{"name":"Reviewer","display":"审核医生","type":"select","required":false},{"name":"Technician","display":"技师","type":"select","required":false},{"name":"Department","display":"执行科室","type":"select","required":false},{"name":"Category","display":"检查类型","type":"select","required":false},{"name":"PatientType","display":"病人类型","type":"select","required":false},{"name":"ResultStatus","display":"结果状态","type":"select","required":false}]',
        0,
        '每日分析统计查询'
    )
    PRINT '✅ 每日分析查询配置创建成功'
END
GO

PRINT '============================================='
PRINT '🎉 所有初始化数据完成！'
PRINT '============================================='
GO
