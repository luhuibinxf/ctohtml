/*
 * 统计分析系统 - 数据库初始化脚本
 * 版本: 1.0
 * 日期: 2026-04-28
 * 
 * 包含以下内容:
 * 1. 用户表 (TJYHB)
 * 2. 医生信息表 (DOCTOR_INFO)
 * 3. 统计页面配置表 (STAT_PAGE_CONFIG)
 * 4. 统计参数配置表 (STAT_PARAM_CONFIG)
 * 5. 统计显示配置表 (STAT_DISPLAY_CONFIG)
 * 6. 存储过程注册表 (STAT_PROCEDURE_REGISTRY)
 * 7. 科室映射表 (DEPT_CATEGORY_MAPPING)
 * 8. 相关存储过程
 */

-- =============================================
-- 1. 创建用户表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TJYHB]') AND type in (N'U'))
BEGIN
    CREATE TABLE TJYHB (
        ID INT PRIMARY KEY IDENTITY(1,1),
        YHM NVARCHAR(50) NOT NULL UNIQUE,  -- 用户名
        MM NVARCHAR(100) NOT NULL,         -- 密码
        XM NVARCHAR(50),                    -- 姓名
        QX INT DEFAULT 0,                   -- 权限: 0=普通用户, 1=管理员
        SFY INT DEFAULT 1,                  -- 是否启用: 0=禁用, 1=启用
        CREATE_TIME DATETIME DEFAULT GETDATE(),
        UPDATE_TIME DATETIME DEFAULT GETDATE()
    );
    
    -- 插入默认管理员
    INSERT INTO TJYHB (YHM, MM, XM, QX, SFY) VALUES 
    ('lhbdb', '241023', '管理员', 1, 1);
END
GO

-- =============================================
-- 2. 创建医生信息表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DOCTOR_INFO]') AND type in (N'U'))
BEGIN
    CREATE TABLE DOCTOR_INFO (
        ID INT PRIMARY KEY IDENTITY(1,1),
        DOCTOR_CODE NVARCHAR(50) NOT NULL UNIQUE,  -- 医生编码
        DOCTOR_NAME NVARCHAR(100) NOT NULL,        -- 医生姓名
        SYSTEM_TYPE NVARCHAR(50),                   -- 所属系统（RIS/UIS/EIS）
        DEPARTMENT NVARCHAR(100),                   -- 所属科室
        IS_REPORTER BIT DEFAULT 1,                  -- 是否为报告医生
        IS_REVIEWER BIT DEFAULT 0,                  -- 是否为审核医生
        IS_ACTIVE BIT DEFAULT 1,                    -- 是否启用
        CREATE_TIME DATETIME DEFAULT GETDATE(),     -- 创建时间
        UPDATE_TIME DATETIME DEFAULT GETDATE()      -- 更新时间
    );
    
    -- 插入初始化数据
    INSERT INTO DOCTOR_INFO (DOCTOR_CODE, DOCTOR_NAME, SYSTEM_TYPE, DEPARTMENT, IS_REPORTER, IS_REVIEWER) VALUES
    ('DR001', '张医生', 'RIS', '放射科', 1, 0),
    ('DR002', '李医生', 'RIS', '放射科', 1, 0),
    ('DR003', '王医生', 'RIS', '放射科', 1, 0),
    ('DR004', '赵医生', 'RIS', '放射科', 1, 1),
    ('DR005', '钱医生', 'RIS', '放射科', 1, 1),
    ('DR006', '孙医生', 'UIS', '超声科', 1, 1),
    ('DR007', '周医生', 'UIS', '超声科', 1, 0),
    ('DR008', '吴医生', 'UIS', '超声科', 1, 1),
    ('DR009', '郑医生', 'EIS', '内镜科', 1, 1),
    ('DR010', '陈医生', 'EIS', '内镜科', 1, 0),
    ('DR011', '杨医生', 'RIS', '放射科', 0, 1),
    ('DR012', '黄医生', 'RIS', '放射科', 0, 1),
    ('DR013', '刘医生', 'UIS', '超声科', 0, 1),
    ('DR014', '许医生', 'EIS', '内镜科', 1, 1),
    ('DR015', '何医生', 'RIS', '放射科', 1, 1);
END
GO

-- =============================================
-- 3. 创建统计页面配置表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STAT_PAGE_CONFIG]') AND type in (N'U'))
BEGIN
    CREATE TABLE STAT_PAGE_CONFIG (
        ID INT PRIMARY KEY IDENTITY(1,1),
        PAGE_NAME NVARCHAR(200) NOT NULL,     -- 页面名称
        PAGE_CODE NVARCHAR(100) NOT NULL UNIQUE, -- 页面编码
        PROCEDURE_NAME NVARCHAR(200),          -- 主存储过程名
        DETAIL_PROCEDURE NVARCHAR(200),        -- 明细存储过程名
        LINK_FIELD NVARCHAR(100),              -- 关联字段
        DESCRIPTION NVARCHAR(500),             -- 描述
        CONFIG_JSON TEXT,                      -- 配置JSON
        PAGE_TEMPLATE NVARCHAR(500),           -- 页面模板路径
        IS_ACTIVE BIT DEFAULT 1,               -- 是否启用
        CREATE_TIME DATETIME DEFAULT GETDATE(),
        UPDATE_TIME DATETIME DEFAULT GETDATE()
    );
END
GO

-- =============================================
-- 4. 创建统计参数配置表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STAT_PARAM_CONFIG]') AND type in (N'U'))
BEGIN
    CREATE TABLE STAT_PARAM_CONFIG (
        ID INT PRIMARY KEY IDENTITY(1,1),
        PAGE_ID INT NOT NULL,                     -- 关联页面ID
        PARAM_NAME NVARCHAR(100) NOT NULL,        -- 参数名称
        PARAM_LABEL NVARCHAR(100) NOT NULL,       -- 参数显示标签
        PARAM_TYPE NVARCHAR(50) DEFAULT 'string', -- 参数类型
        PARAM_OPTIONS TEXT,                       -- 下拉选项（JSON格式）
        IS_REQUIRED BIT DEFAULT 0,                -- 是否必填
        IS_MULTI_SELECT BIT DEFAULT 0,            -- 是否支持多选
        DEFAULT_VALUE NVARCHAR(200),              -- 默认值
        SORT_ORDER INT DEFAULT 0,                 -- 排序号
        FOREIGN KEY (PAGE_ID) REFERENCES STAT_PAGE_CONFIG(ID)
    );
END
GO

-- =============================================
-- 5. 创建统计显示配置表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STAT_DISPLAY_CONFIG]') AND type in (N'U'))
BEGIN
    CREATE TABLE STAT_DISPLAY_CONFIG (
        ID INT PRIMARY KEY IDENTITY(1,1),
        PAGE_ID INT NOT NULL,                     -- 关联页面ID
        COLUMN_NAME NVARCHAR(100) NOT NULL,       -- 列名
        DISPLAY_NAME NVARCHAR(100) NOT NULL,      -- 显示名称
        COLUMN_WIDTH INT DEFAULT 100,             -- 列宽度
        ALIGNMENT NVARCHAR(20) DEFAULT 'left',    -- 对齐方式
        IS_LINK BIT DEFAULT 0,                    -- 是否为链接字段
        SORT_ORDER INT DEFAULT 0,                 -- 排序号
        FOREIGN KEY (PAGE_ID) REFERENCES STAT_PAGE_CONFIG(ID)
    );
END
GO

-- =============================================
-- 6. 创建存储过程注册表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STAT_PROCEDURE_REGISTRY]') AND type in (N'U'))
BEGIN
    CREATE TABLE STAT_PROCEDURE_REGISTRY (
        ID INT PRIMARY KEY IDENTITY(1,1),
        PROCEDURE_NAME NVARCHAR(200) NOT NULL UNIQUE, -- 存储过程名
        PROCEDURE_TYPE NVARCHAR(50) DEFAULT 'query',  -- 类型: query/detail/summary
        DESCRIPTION NVARCHAR(500),                    -- 描述
        PARAMS_JSON TEXT,                             -- 参数JSON
        RETURN_COLUMNS TEXT,                          -- 返回列JSON
        IS_ACTIVE BIT DEFAULT 1,                      -- 是否启用
        CREATE_TIME DATETIME DEFAULT GETDATE(),
        UPDATE_TIME DATETIME DEFAULT GETDATE()
    );
END
GO

-- =============================================
-- 7. 创建科室映射表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DEPT_CATEGORY_MAPPING]') AND type in (N'U'))
BEGIN
    CREATE TABLE DEPT_CATEGORY_MAPPING (
        ID INT PRIMARY KEY IDENTITY(1,1),
        CategoryName NVARCHAR(100) NOT NULL UNIQUE,  -- 检查类别名称
        DepartmentName NVARCHAR(100) NOT NULL,      -- 执行科室名称
        CREATE_TIME DATETIME DEFAULT GETDATE(),
        UPDATE_TIME DATETIME DEFAULT GETDATE()
    );
    
    -- 插入初始化数据
    INSERT INTO DEPT_CATEGORY_MAPPING (CategoryName, DepartmentName) VALUES
    ('CT', '放射科'),
    ('CT(新)', '放射科'),
    ('核磁共振', '放射科'),
    ('钼靶', '放射科'),
    ('普放', '放射科'),
    ('普放(新)', '放射科'),
    ('消化道造影', '放射科'),
    ('消化道造影(新)', '放射科'),
    ('超声', '超声科'),
    ('超声(新)', '超声科'),
    ('心电图', '心内科'),
    ('内镜', '内镜科'),
    ('病理', '病理科');
END
GO

-- =============================================
-- 存储过程: sp_GetReporters - 获取报告医生
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetReporters]') AND type in (N'P', N'PC'))
BEGIN
    EXEC dbo.sp_executesql @statement = N'
        CREATE PROCEDURE sp_GetReporters
            @System NVARCHAR(50)
        AS
        BEGIN
            SELECT 
                DOCTOR_CODE AS code,
                DOCTOR_NAME AS name
            FROM DOCTOR_INFO
            WHERE IS_REPORTER = 1 
              AND IS_ACTIVE = 1
              AND (@System IS NULL OR @System = '''' OR SYSTEM_TYPE = @System)
            ORDER BY DOCTOR_NAME;
        END
    '
END
GO

-- =============================================
-- 存储过程: sp_GetReviewers - 获取审核医生
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetReviewers]') AND type in (N'P', N'PC'))
BEGIN
    EXEC dbo.sp_executesql @statement = N'
        CREATE PROCEDURE sp_GetReviewers
            @System NVARCHAR(50)
        AS
        BEGIN
            SELECT 
                DOCTOR_CODE AS code,
                DOCTOR_NAME AS name
            FROM DOCTOR_INFO
            WHERE IS_REVIEWER = 1 
              AND IS_ACTIVE = 1
              AND (@System IS NULL OR @System = '''' OR SYSTEM_TYPE = @System)
            ORDER BY DOCTOR_NAME;
        END
    '
END
GO

-- =============================================
-- 存储过程: sp_GetCategories - 获取检查类型
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetCategories]') AND type in (N'P', N'PC'))
BEGIN
    EXEC dbo.sp_executesql @statement = N'
        CREATE PROCEDURE sp_GetCategories
            @System NVARCHAR(50)
        AS
        BEGIN
            SELECT DISTINCT 
                EXAM_CATEGORY_NAME AS code,
                EXAM_CATEGORY_NAME AS name
            FROM EXAM_TASK_INFO
            WHERE (@System IS NULL OR @System = '''' OR SYSTEM_SOURCE_NO = @System)
            ORDER BY EXAM_CATEGORY_NAME;
        END
    '
END
GO

-- =============================================
-- 存储过程: sp_GetDepartments - 获取执行科室
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetDepartments]') AND type in (N'P', N'PC'))
BEGIN
    EXEC dbo.sp_executesql @statement = N'
        CREATE PROCEDURE sp_GetDepartments
            @System NVARCHAR(50)
        AS
        BEGIN
            SELECT DISTINCT 
                EXECUTE_DEPARTMENT AS code,
                EXECUTE_DEPARTMENT AS name
            FROM EXAM_TASK_INFO
            WHERE (@System IS NULL OR @System = '''' OR SYSTEM_SOURCE_NO = @System)
            ORDER BY EXECUTE_DEPARTMENT;
        END
    '
END
GO

-- =============================================
-- 存储过程: usp_tjfx_DailyAnalysis - 每日分析
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_tjfx_DailyAnalysis]') AND type in (N'P', N'PC'))
BEGIN
    EXEC dbo.sp_executesql @statement = N'
        CREATE PROCEDURE usp_tjfx_DailyAnalysis
            @StartDate DATE,
            @EndDate DATE,
            @System NVARCHAR(50) = NULL,
            @Reporter NVARCHAR(100) = NULL,
            @Reviewer NVARCHAR(100) = NULL,
            @Technician NVARCHAR(100) = NULL,
            @Department NVARCHAR(100) = NULL,
            @Category NVARCHAR(100) = NULL
        AS
        BEGIN
            SELECT 
                SYSTEM_SOURCE_NO AS ''系统'',
                REPORTER_NAME AS ''报告医生'',
                REVIEWER_NAME AS ''审核医生'',
                TECHNICIAN_NAME AS ''技师'',
                ISNULL((SELECT DepartmentName FROM DEPT_CATEGORY_MAPPING WHERE CategoryName = EXAM_CATEGORY_NAME), ''其他科室'') AS ''执行科室'',
                EXAM_CATEGORY_NAME AS ''检查类型'',
                COUNT(*) AS ''任务数量'',
                MAX(EXAM_TASK_CREATE_TIME) AS ''最新任务时间''
            FROM EXAM_TASK_INFO
            WHERE EXAM_TASK_CREATE_TIME BETWEEN @StartDate AND DATEADD(DAY, 1, @EndDate)
            AND (@System IS NULL OR @System = '''' OR SYSTEM_SOURCE_NO = @System)
            AND (@Reporter IS NULL OR @Reporter = '''' OR REPORTER_NAME = @Reporter)
            AND (@Reviewer IS NULL OR @Reviewer = '''' OR REVIEWER_NAME = @Reviewer)
            AND (@Technician IS NULL OR @Technician = '''' OR TECHNICIAN_NAME = @Technician)
            AND (@Department IS NULL OR @Department = '''' OR EXECUTE_DEPARTMENT = @Department)
            AND (@Category IS NULL OR @Category = '''' OR EXAM_CATEGORY_NAME = @Category)
            GROUP BY SYSTEM_SOURCE_NO, REPORTER_NAME, REVIEWER_NAME, TECHNICIAN_NAME, EXAM_CATEGORY_NAME
            ORDER BY SYSTEM_SOURCE_NO, REPORTER_NAME;
        END
    '
END
GO

PRINT '数据库初始化完成！'
GO