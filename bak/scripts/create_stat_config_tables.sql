-- 统计菜单配置表
CREATE TABLE IF NOT EXISTS STAT_MENU_CONFIG (
    ID INT PRIMARY KEY IDENTITY(1,1),
    MENU_NAME NVARCHAR(100) NOT NULL,        -- 菜单名称
    MENU_ICON NVARCHAR(50),                   -- 菜单图标
    MENU_ORDER INT DEFAULT 0,                 -- 排序号
    PARENT_ID INT DEFAULT 0,                  -- 父菜单ID
    PAGE_URL NVARCHAR(200),                   -- 页面URL
    IS_ACTIVE BIT DEFAULT 1,                  -- 是否启用
    CREATE_TIME DATETIME DEFAULT GETDATE(),   -- 创建时间
    UPDATE_TIME DATETIME DEFAULT GETDATE()    -- 更新时间
);

-- 统计页面配置表（增强版）
CREATE TABLE IF NOT EXISTS STAT_PAGE_CONFIG (
    ID INT PRIMARY KEY IDENTITY(1,1),
    PAGE_NAME NVARCHAR(100) NOT NULL,         -- 页面名称
    PAGE_CODE NVARCHAR(50) UNIQUE NOT NULL,   -- 页面编码（唯一标识）
    PROCEDURE_NAME NVARCHAR(200),             -- 主存储过程名称（固定格式：usp_xxxx）
    DETAIL_PROCEDURE NVARCHAR(200),           -- 明细存储过程名称（用于数据关联）
    LINK_FIELD NVARCHAR(100),                 -- 关联字段（主存储返回的字段，作为明细存储的入参）
    PAGE_TEMPLATE NVARCHAR(200),              -- 页面HTML模板名称（不含扩展名）
    CONFIG_JSON TEXT,                         -- 配置JSON（包含列定义、参数定义等）
    DESCRIPTION NVARCHAR(500),                -- 页面描述
    IS_ACTIVE BIT DEFAULT 1,                  -- 是否启用
    CREATE_TIME DATETIME DEFAULT GETDATE(),   -- 创建时间
    UPDATE_TIME DATETIME DEFAULT GETDATE()    -- 更新时间
);

-- 统计参数配置表（增强版）
CREATE TABLE IF NOT EXISTS STAT_PARAM_CONFIG (
    ID INT PRIMARY KEY IDENTITY(1,1),
    PAGE_ID INT NOT NULL,                     -- 关联页面ID
    PARAM_NAME NVARCHAR(100) NOT NULL,        -- 参数名称
    PARAM_LABEL NVARCHAR(100) NOT NULL,       -- 参数显示标签
    PARAM_TYPE NVARCHAR(50) DEFAULT 'string', -- 参数类型：string, int, datetime, select, multi_select, checkbox, radio
    PARAM_OPTIONS TEXT,                       -- 下拉选项（JSON格式）
    IS_REQUIRED BIT DEFAULT 0,                -- 是否必填
    IS_MULTI_SELECT BIT DEFAULT 0,            -- 是否支持多选
    DEFAULT_VALUE NVARCHAR(200),              -- 默认值（多选时用逗号分隔）
    SORT_ORDER INT DEFAULT 0,                 -- 排序号
    FOREIGN KEY (PAGE_ID) REFERENCES STAT_PAGE_CONFIG(ID)
);

-- 统计结果展示配置表（增强版）
CREATE TABLE IF NOT EXISTS STAT_DISPLAY_CONFIG (
    ID INT PRIMARY KEY IDENTITY(1,1),
    PAGE_ID INT NOT NULL,                     -- 关联页面ID
    COLUMN_NAME NVARCHAR(100) NOT NULL,       -- 列名（存储过程返回的原始列名）
    DISPLAY_NAME NVARCHAR(100) NOT NULL,      -- 显示名称
    COLUMN_WIDTH NVARCHAR(20),                -- 列宽度
    ALIGNMENT NVARCHAR(20) DEFAULT 'left',    -- 对齐方式
    IS_VISIBLE BIT DEFAULT 1,                 -- 是否显示
    IS_LINK BIT DEFAULT 0,                    -- 是否为链接字段（用于关联明细）
    SORT_ORDER INT DEFAULT 0,                 -- 排序号
    FOREIGN KEY (PAGE_ID) REFERENCES STAT_PAGE_CONFIG(ID)
);

-- 存储过程注册表（新增：用于存储过程发现）
CREATE TABLE IF NOT EXISTS STAT_PROCEDURE_REGISTRY (
    ID INT PRIMARY KEY IDENTITY(1,1),
    PROCEDURE_NAME NVARCHAR(200) NOT NULL,    -- 存储过程名称
    PROCEDURE_TYPE NVARCHAR(50) DEFAULT 'query', -- 类型：query（查询）, detail（明细）, summary（汇总）
    DESCRIPTION NVARCHAR(500),                -- 存储过程描述
    PARAMS_JSON TEXT,                         -- 参数列表JSON
    RETURN_COLUMNS TEXT,                      -- 返回列JSON
    IS_ACTIVE BIT DEFAULT 1,                  -- 是否启用
    CREATE_TIME DATETIME DEFAULT GETDATE()    -- 创建时间
);

-- 插入示例数据
INSERT INTO STAT_MENU_CONFIG (MENU_NAME, MENU_ICON, MENU_ORDER, PAGE_URL) VALUES
('每日分析', '📊', 1, '#daily-analysis'),
('科室统计', '🏥', 2, '#department-stats'),
('医生统计', '👨‍⚕️', 3, '#doctor-stats'),
('检查类型统计', '📋', 4, '#category-stats');

INSERT INTO STAT_PAGE_CONFIG (PAGE_NAME, PAGE_CODE, PROCEDURE_NAME, DETAIL_PROCEDURE, LINK_FIELD, PAGE_TEMPLATE, DESCRIPTION) VALUES
('科室工作量统计', 'DEPARTMENT_WORKLOAD', 'usp_GetDepartmentWorkload', 'usp_GetDepartmentDetail', 'DEPARTMENT_CODE', 'department_stats', '按科室统计工作量汇总'),
('医生工作量统计', 'DOCTOR_WORKLOAD', 'usp_GetDoctorWorkload', 'usp_GetDoctorDetail', 'DOCTOR_ID', 'doctor_stats', '按医生统计工作量汇总'),
('检查类型统计', 'CATEGORY_STATS', 'usp_GetCategoryStats', 'usp_GetCategoryDetail', 'CATEGORY_CODE', 'category_stats', '按检查类型统计');

INSERT INTO STAT_PARAM_CONFIG (PAGE_ID, PARAM_NAME, PARAM_LABEL, PARAM_TYPE, IS_REQUIRED, DEFAULT_VALUE) VALUES
(1, 'startDate', '开始日期', 'datetime', 1, '2024-01-01'),
(1, 'endDate', '结束日期', 'datetime', 1, '2024-12-31'),
(1, 'system', '系统类型', 'select', 0, 'RIS'),
(2, 'startDate', '开始日期', 'datetime', 1, '2024-01-01'),
(2, 'endDate', '结束日期', 'datetime', 1, '2024-12-31'),
(2, 'departmentCode', '科室编码', 'string', 0, ''),
(3, 'startDate', '开始日期', 'datetime', 1, '2024-01-01'),
(3, 'endDate', '结束日期', 'datetime', 1, '2024-12-31');

INSERT INTO STAT_DISPLAY_CONFIG (PAGE_ID, COLUMN_NAME, DISPLAY_NAME, COLUMN_WIDTH, ALIGNMENT, IS_LINK) VALUES
(1, 'DEPARTMENT_CODE', '科室编码', '100px', 'left', 1),
(1, 'DEPARTMENT_NAME', '科室名称', '200px', 'left', 0),
(1, 'COUNT', '数量', '100px', 'center', 0),
(1, 'AMOUNT', '金额', '120px', 'right', 0),
(2, 'DOCTOR_ID', '医生ID', '100px', 'left', 1),
(2, 'DOCTOR_NAME', '医生姓名', '150px', 'left', 0),
(2, 'REPORT_COUNT', '报告数', '100px', 'center', 0),
(2, 'REVIEW_COUNT', '审核数', '100px', 'center', 0),
(3, 'CATEGORY_CODE', '类型编码', '100px', 'left', 1),
(3, 'CATEGORY_NAME', '检查类型', '150px', 'left', 0),
(3, 'COUNT', '数量', '100px', 'center', 0),
(3, 'RATE', '占比', '100px', 'center', 0);

-- 注册示例存储过程
INSERT INTO STAT_PROCEDURE_REGISTRY (PROCEDURE_NAME, PROCEDURE_TYPE, DESCRIPTION, PARAMS_JSON, RETURN_COLUMNS) VALUES
('usp_GetDepartmentWorkload', 'query', '获取科室工作量汇总', '[{"name":"startDate","type":"datetime","required":true},{"name":"endDate","type":"datetime","required":true},{"name":"system","type":"string","required":false}]', '[{"name":"DEPARTMENT_CODE","type":"string"},{"name":"DEPARTMENT_NAME","type":"string"},{"name":"COUNT","type":"int"},{"name":"AMOUNT","type":"decimal"}]'),
('usp_GetDepartmentDetail', 'detail', '获取科室工作量明细', '[{"name":"departmentCode","type":"string","required":true},{"name":"startDate","type":"datetime","required":true},{"name":"endDate","type":"datetime","required":true}]', '[{"name":"ID","type":"int"},{"name":"PATIENT_NAME","type":"string"},{"name":"EXAM_DATE","type":"datetime"},{"name":"AMOUNT","type":"decimal"}]'),
('usp_GetDoctorWorkload', 'query', '获取医生工作量汇总', '[{"name":"startDate","type":"datetime","required":true},{"name":"endDate","type":"datetime","required":true},{"name":"departmentCode","type":"string","required":false}]', '[{"name":"DOCTOR_ID","type":"string"},{"name":"DOCTOR_NAME","type":"string"},{"name":"REPORT_COUNT","type":"int"},{"name":"REVIEW_COUNT","type":"int"}]'),
('usp_GetDoctorDetail', 'detail', '获取医生工作量明细', '[{"name":"doctorId","type":"string","required":true},{"name":"startDate","type":"datetime","required":true},{"name":"endDate","type":"datetime","required":true}]', '[{"name":"ID","type":"int"},{"name":"PATIENT_NAME","type":"string"},{"name":"EXAM_TYPE","type":"string"},{"name":"REPORT_TIME","type":"datetime"}]'),
('usp_GetCategoryStats', 'query', '获取检查类型统计', '[{"name":"startDate","type":"datetime","required":true},{"name":"endDate","type":"datetime","required":true}]', '[{"name":"CATEGORY_CODE","type":"string"},{"name":"CATEGORY_NAME","type":"string"},{"name":"COUNT","type":"int"},{"name":"RATE","type":"decimal"}]'),
('usp_GetCategoryDetail', 'detail', '获取检查类型明细', '[{"name":"categoryCode","type":"string","required":true},{"name":"startDate","type":"datetime","required":true},{"name":"endDate","type":"datetime","required":true}]', '[{"name":"ID","type":"int"},{"name":"PATIENT_NAME","type":"string"},{"name":"EXAM_DATE","type":"datetime"},{"name":"AMOUNT","type":"decimal"}]');