-- ========================================
-- 统计分析系统数据库初始化脚本
-- 创建时间: 2026-04-26
-- 说明: 创建统计系统所需的所有配置表
-- ========================================

-- 1. 统计菜单配置表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STAT_MENU_CONFIG]') AND type in (N'U'))
BEGIN
    CREATE TABLE STAT_MENU_CONFIG (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        MENU_NAME NVARCHAR(100) NOT NULL,
        MENU_CODE NVARCHAR(50) NOT NULL UNIQUE,
        PARENT_CODE NVARCHAR(50),
        MENU_ORDER INT DEFAULT 0,
        IS_ACTIVE BIT DEFAULT 1,
        ICON_CLASS NVARCHAR(50),
        PAGE_PATH NVARCHAR(255),
        QUERY_TYPE NVARCHAR(50),
        DESCRIPTION NVARCHAR(255),
        CREATED_BY NVARCHAR(50),
        CREATED_TIME DATETIME DEFAULT GETDATE(),
        UPDATED_BY NVARCHAR(50),
        UPDATED_TIME DATETIME DEFAULT GETDATE()
    );
END
GO

-- 2. 统计页面配置表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STAT_PAGE_CONFIG]') AND type in (N'U'))
BEGIN
    CREATE TABLE STAT_PAGE_CONFIG (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        PAGE_CODE NVARCHAR(50) NOT NULL UNIQUE,
        PAGE_NAME NVARCHAR(100) NOT NULL,
        PAGE_TITLE NVARCHAR(100),
        PARAMS_CONFIG NVARCHAR(MAX),
        DISPLAY_CONFIG NVARCHAR(MAX),
        IS_ACTIVE BIT DEFAULT 1,
        DESCRIPTION NVARCHAR(255),
        CREATED_BY NVARCHAR(50),
        CREATED_TIME DATETIME DEFAULT GETDATE(),
        UPDATED_BY NVARCHAR(50),
        UPDATED_TIME DATETIME DEFAULT GETDATE()
    );
END
GO

-- 3. 科室检查类别映射表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DEPT_CATEGORY_MAPPING]') AND type in (N'U'))
BEGIN
    CREATE TABLE DEPT_CATEGORY_MAPPING (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        DEPT_CODE NVARCHAR(50) NOT NULL,
        DEPT_NAME NVARCHAR(100) NOT NULL,
        CATEGORY_NAME NVARCHAR(100) NOT NULL,
        IS_ACTIVE BIT DEFAULT 1,
        CREATED_BY NVARCHAR(50),
        CREATED_TIME DATETIME DEFAULT GETDATE(),
        UPDATED_BY NVARCHAR(50),
        UPDATED_TIME DATETIME DEFAULT GETDATE(),
        UNIQUE(DEPT_CODE, CATEGORY_NAME)
    );
END
GO

-- 4. 每日查询配置表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DAILY_QUERY_CONFIG]') AND type in (N'U'))
BEGIN
    CREATE TABLE DAILY_QUERY_CONFIG (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        QUERY_NAME NVARCHAR(100) NOT NULL,
        QUERY_TYPE NVARCHAR(50) NOT NULL,
        QUERY_SQL NVARCHAR(MAX) NOT NULL,
        PARAMS_MAPPING NVARCHAR(MAX),
        IS_ACTIVE BIT DEFAULT 1,
        DESCRIPTION NVARCHAR(255),
        CREATED_BY NVARCHAR(50),
        CREATED_TIME DATETIME DEFAULT GETDATE(),
        UPDATED_BY NVARCHAR(50),
        UPDATED_TIME DATETIME DEFAULT GETDATE()
    );
END
GO

-- 5. 每日查询历史表
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DAILY_QUERY_HISTORY]') AND type in (N'U'))
BEGIN
    CREATE TABLE DAILY_QUERY_HISTORY (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        QUERY_DATE DATE NOT NULL,
        QUERY_TYPE NVARCHAR(50) NOT NULL,
        QUERY_PARAMS NVARCHAR(MAX),
        EXECUTION_TIME DATETIME DEFAULT GETDATE(),
        EXECUTION_STATUS NVARCHAR(20),
        ROW_COUNT INT,
        ERROR_MESSAGE NVARCHAR(MAX)
    );
END
GO

-- ========================================
-- 插入 STAT_MENU_CONFIG 数据
-- ========================================
IF NOT EXISTS (SELECT * FROM STAT_MENU_CONFIG WHERE MENU_CODE = 'system')
BEGIN
    INSERT INTO STAT_MENU_CONFIG (MENU_NAME, MENU_CODE, PARENT_CODE, MENU_ORDER, IS_ACTIVE, ICON_CLASS, PAGE_PATH, QUERY_TYPE, DESCRIPTION)
    VALUES
    ('系统信息汇总', 'system', NULL, 1, 1, 'bi bi-server', '/stats/system', 'system', '统计各系统的任务数量'),
    ('执行科室汇总', 'department', NULL, 2, 1, 'bi bi-building', '/stats/department', 'department', '按科室汇总任务数量'),
    ('检查类别汇总', 'category', NULL, 3, 1, 'bi bi-list-check', '/stats/category', 'category', '统计各检查类别的数量'),
    ('报告医生工作量', 'reporter', NULL, 4, 1, 'bi bi-person', '/stats/reporter', 'reporter', '统计报告医生的工作量'),
    ('审核医生工作量', 'reviewer', NULL, 5, 1, 'bi bi-person-check', '/stats/reviewer', 'reviewer', '统计审核医生的工作量'),
    ('技师工作量', 'technician', NULL, 6, 1, 'bi bi-tools', '/stats/technician', 'technician', '统计技师的工作量'),
    ('系统状态汇总', 'status', NULL, 7, 1, 'bi bi-bar-chart', '/stats/status', 'status', '统计各任务状态的数量'),
    ('执行科室工作量排名', 'rank', NULL, 8, 1, 'bi bi-trophy', '/stats/rank', 'rank', '执行科室工作量排名和完成率');
END
GO

-- ========================================
-- 插入 STAT_PAGE_CONFIG 数据
-- ========================================
IF NOT EXISTS (SELECT * FROM STAT_PAGE_CONFIG WHERE PAGE_CODE = 'system')
BEGIN
    INSERT INTO STAT_PAGE_CONFIG (PAGE_CODE, PAGE_NAME, PAGE_TITLE, PARAMS_CONFIG, DISPLAY_CONFIG, IS_ACTIVE, DESCRIPTION)
    VALUES
    ('system', '系统信息汇总', '系统信息汇总',
     '{"dateRange": true, "dateRangeLabel": "日期范围"}',
     '{"table": true, "chart": true, "export": true}',
     1, '系统信息汇总页面'),
    ('department', '执行科室汇总', '执行科室汇总',
     '{"dateRange": true, "dateRangeLabel": "日期范围"}',
     '{"table": true, "chart": true, "export": true}',
     1, '执行科室汇总页面'),
    ('category', '检查类别汇总', '检查类别汇总',
     '{"dateRange": true, "dateRangeLabel": "日期范围"}',
     '{"table": true, "chart": true, "export": true}',
     1, '检查类别汇总页面'),
    ('reporter', '报告医生工作量', '报告医生工作量',
     '{"dateRange": true, "dateRangeLabel": "日期范围"}',
     '{"table": true, "chart": true, "export": true}',
     1, '报告医生工作量页面'),
    ('reviewer', '审核医生工作量', '审核医生工作量',
     '{"dateRange": true, "dateRangeLabel": "日期范围"}',
     '{"table": true, "chart": true, "export": true}',
     1, '审核医生工作量页面'),
    ('technician', '技师工作量', '技师工作量',
     '{"dateRange": true, "dateRangeLabel": "日期范围"}',
     '{"table": true, "chart": true, "export": true}',
     1, '技师工作量页面'),
    ('status', '系统状态汇总', '系统状态汇总',
     '{"dateRange": true, "dateRangeLabel": "日期范围"}',
     '{"table": true, "chart": true, "export": true}',
     1, '系统状态汇总页面'),
    ('rank', '执行科室工作量排名', '执行科室工作量排名',
     '{"dateRange": true, "dateRangeLabel": "日期范围"}',
     '{"table": true, "chart": true, "export": true}',
     1, '执行科室工作量排名页面');
END
GO

-- ========================================
-- 插入 DEPT_CATEGORY_MAPPING 数据（示例数据）
-- 说明: 此数据为示例，实际使用时根据医院实际情况添加
-- ========================================
IF NOT EXISTS (SELECT * FROM DEPT_CATEGORY_MAPPING)
BEGIN
    INSERT INTO DEPT_CATEGORY_MAPPING (DEPT_CODE, DEPT_NAME, CATEGORY_NAME, IS_ACTIVE, DESCRIPTION)
    VALUES
    ('DIGESTIVE', '消化内镜(总)', '肠镜(老)', 1, '消化内镜'),
    ('DIGESTIVE', '消化内镜(总)', '肠镜(新)', 1, '消化内镜'),
    ('DIGESTIVE', '消化内镜(总)', '超声内镜', 1, '消化内镜'),
    ('DIGESTIVE', '消化内镜(总)', '胃镜(老)', 1, '消化内镜'),
    ('DIGESTIVE', '消化内镜(总)', '胃镜(新)', 1, '消化内镜'),
    ('DIGESTIVE', '消化内镜(总)', '消化肠镜', 1, '消化内镜'),
    ('DIGESTIVE', '消化内镜(总)', '消化胃镜', 1, '消化内镜'),
    ('RESPIRATORY', '呼吸内镜科', '支气管镜(新)', 1, '呼吸内镜'),
    ('RESPIRATORY', '呼吸内镜科', '支气管镜(总)', 1, '呼吸内镜'),
    ('RADIOLOGY', '放射科', 'CT', 1, '放射科'),
    ('RADIOLOGY', '放射科', 'CT(新)', 1, '放射科'),
    ('RADIOLOGY', '放射科', '核磁共振', 1, '放射科'),
    ('RADIOLOGY', '放射科', '钼靶', 1, '放射科'),
    ('RADIOLOGY', '放射科', '普放', 1, '放射科'),
    ('RADIOLOGY', '放射科', '普放(新)', 1, '放射科'),
    ('RADIOLOGY', '放射科', '消化道造影', 1, '放射科'),
    ('RADIOLOGY', '放射科', '消化道造影(新)', 1, '放射科'),
    ('ULTRASOUND', '超声科', '介入超声', 1, '超声科'),
    ('NEUROLOGY', '神经内科', '脑电', 1, '神经内科'),
    ('HEALTH', '体检科', '体检彩超', 1, '体检科'),
    ('HEALTH', '体检科', '新城体检彩超', 1, '体检科');
END
GO

-- ========================================
-- 插入 DAILY_QUERY_CONFIG 数据
-- 说明: SQL语句使用动态映射，通过JOIN DEPT_CATEGORY_MAPPING表实现
-- ========================================
IF NOT EXISTS (SELECT * FROM DAILY_QUERY_CONFIG WHERE QUERY_TYPE = 'system')
BEGIN
    INSERT INTO DAILY_QUERY_CONFIG (QUERY_NAME, QUERY_TYPE, QUERY_SQL, PARAMS_MAPPING, IS_ACTIVE, DESCRIPTION)
    VALUES
    ('系统信息汇总', 'system',
     'SELECT SYSTEM_SOURCE_NO AS "系统标识", COUNT(*) AS "任务数量", MAX(EXAM_TASK_CREATE_TIME) AS "最新任务时间" FROM EXAM_TASK WHERE IS_DEL = 0 GROUP BY SYSTEM_SOURCE_NO ORDER BY COUNT(*) DESC',
     '{"date": "date"}',
     1, '统计各系统的任务数量'),
    ('执行科室汇总', 'department',
     'SELECT m.DEPT_NAME AS "执行科室", COUNT(*) AS "任务数量" FROM EXAM_TASK t LEFT JOIN DEPT_CATEGORY_MAPPING m ON t.EXAM_CATEGORY_NAME = m.CATEGORY_NAME AND m.IS_ACTIVE = 1 WHERE t.IS_DEL = 0 GROUP BY m.DEPT_NAME ORDER BY COUNT(*) DESC',
     '{"date": "date"}',
     1, '按科室汇总任务数量'),
    ('检查类别汇总', 'category',
     'SELECT EXAM_CATEGORY_NAME AS "检查类别", COUNT(*) AS "数量", MAX(EXAM_TASK_CREATE_TIME) AS "最新检查时间" FROM EXAM_TASK WHERE IS_DEL = 0 GROUP BY EXAM_CATEGORY_NAME ORDER BY COUNT(*) DESC',
     '{"date": "date"}',
     1, '统计各检查类别的数量'),
    ('报告医生工作量', 'reporter',
     'SELECT REPORTER_NAME AS "医生姓名", COUNT(*) AS "报告数量", MAX(REPORT_TIME) AS "最新报告时间" FROM EXAM_REPORT WHERE IS_DEL = 0 GROUP BY REPORTER_NAME ORDER BY COUNT(*) DESC',
     '{"date": "date"}',
     1, '统计报告医生的工作量'),
    ('审核医生工作量', 'reviewer',
     'SELECT REVIEWER_NAME AS "医生姓名", COUNT(*) AS "审核数量", MAX(REVIEW_TIME) AS "最新审核时间" FROM EXAM_REPORT WHERE IS_DEL = 0 GROUP BY REVIEWER_NAME ORDER BY COUNT(*) DESC',
     '{"date": "date"}',
     1, '统计审核医生的工作量'),
    ('技师工作量', 'technician',
     'SELECT TECHNICIAN_NAME AS "技师姓名", COUNT(*) AS "检查数量", MAX(EXAM_TASK_CREATE_TIME) AS "最新检查时间" FROM EXAM_TASK WHERE IS_DEL = 0 GROUP BY TECHNICIAN_NAME ORDER BY COUNT(*) DESC',
     '{"date": "date"}',
     1, '统计技师的工作量'),
    ('系统状态汇总', 'status',
     'SELECT EXAM_TASK_STATUS AS "任务状态", COUNT(*) AS "数量", MAX(EXAM_TASK_CREATE_TIME) AS "最新时间" FROM EXAM_TASK WHERE IS_DEL = 0 GROUP BY EXAM_TASK_STATUS ORDER BY COUNT(*) DESC',
     '{"date": "date"}',
     1, '统计各任务状态的数量'),
    ('执行科室工作量排名', 'rank',
     'SELECT m.DEPT_NAME AS "执行科室", COUNT(*) AS "总任务数", SUM(CASE WHEN t.EXAM_TASK_STATUS = ''已完成'' THEN 1 ELSE 0 END) AS "已完成数", ROUND(SUM(CASE WHEN t.EXAM_TASK_STATUS = ''已完成'' THEN 1 ELSE 0 END) * 100.0 / COUNT(*), 2) AS "完成率", MAX(t.EXAM_TASK_CREATE_TIME) AS "最新任务时间" FROM EXAM_TASK t LEFT JOIN DEPT_CATEGORY_MAPPING m ON t.EXAM_CATEGORY_NAME = m.CATEGORY_NAME AND m.IS_ACTIVE = 1 WHERE t.IS_DEL = 0 GROUP BY m.DEPT_NAME ORDER BY COUNT(*) DESC',
     '{"date": "date"}',
     1, '执行科室工作量排名和完成率');
END
GO

PRINT '统计分析系统数据库初始化完成！'
