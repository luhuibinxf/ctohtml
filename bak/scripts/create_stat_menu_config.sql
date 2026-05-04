-- 创建统计菜单配置表
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

-- 创建统计页面配置表
CREATE TABLE STAT_PAGE_CONFIG (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    PAGE_CODE NVARCHAR(50) NOT NULL UNIQUE,
    PAGE_NAME NVARCHAR(100) NOT NULL,
    PAGE_TITLE NVARCHAR(100),
    QUERY_TYPE NVARCHAR(50) NOT NULL,
    PARAMS_CONFIG NVARCHAR(MAX),
    DISPLAY_CONFIG NVARCHAR(MAX),
    IS_ACTIVE BIT DEFAULT 1,
    DESCRIPTION NVARCHAR(255),
    CREATED_BY NVARCHAR(50),
    CREATED_TIME DATETIME DEFAULT GETDATE(),
    UPDATED_BY NVARCHAR(50),
    UPDATED_TIME DATETIME DEFAULT GETDATE()
);

-- 插入默认统计菜单
INSERT INTO STAT_MENU_CONFIG (MENU_NAME, MENU_CODE, MENU_ORDER, ICON_CLASS, PAGE_PATH, QUERY_TYPE, DESCRIPTION)
VALUES 
('系统信息汇总', 'sys_summary', 1, 'bi bi-server', '/stats/system', 'system', '统计各系统的任务数量'),
('执行科室汇总', 'dept_summary', 2, 'bi bi-building', '/stats/department', 'department', '按科室汇总任务数量'),
('检查类别汇总', 'category_summary', 3, 'bi bi-list-check', '/stats/category', 'category', '统计各检查类别的数量'),
('报告医生工作量', 'reporter_stats', 4, 'bi bi-person', '/stats/reporter', 'reporter', '统计报告医生的工作量'),
('审核医生工作量', 'reviewer_stats', 5, 'bi bi-person-check', '/stats/reviewer', 'reviewer', '统计审核医生的工作量'),
('技师工作量', 'technician_stats', 6, 'bi bi-tools', '/stats/technician', 'technician', '统计技师的工作量'),
('系统状态汇总', 'status_summary', 7, 'bi bi-bar-chart', '/stats/status', 'status', '统计各任务状态的数量'),
('执行科室工作量排名', 'dept_rank', 8, 'bi bi-trophy', '/stats/rank', 'rank', '执行科室工作量排名和完成率');

-- 插入默认统计页面配置
INSERT INTO STAT_PAGE_CONFIG (PAGE_CODE, PAGE_NAME, PAGE_TITLE, QUERY_TYPE, PARAMS_CONFIG, DISPLAY_CONFIG, DESCRIPTION)
VALUES 
('system', '系统信息汇总', '系统信息汇总', 'system', '{"date": true, "timeRange": false}', '{"table": true, "chart": true, "export": true}', '系统信息汇总页面'),
('department', '执行科室汇总', '执行科室汇总', 'department', '{"date": true, "timeRange": false}', '{"table": true, "chart": true, "export": true}', '执行科室汇总页面'),
('category', '检查类别汇总', '检查类别汇总', 'category', '{"date": true, "timeRange": false}', '{"table": true, "chart": true, "export": true}', '检查类别汇总页面'),
('reporter', '报告医生工作量', '报告医生工作量', 'reporter', '{"date": true, "timeRange": true}', '{"table": true, "chart": true, "export": true}', '报告医生工作量页面'),
('reviewer', '审核医生工作量', '审核医生工作量', 'reviewer', '{"date": true, "timeRange": true}', '{"table": true, "chart": true, "export": true}', '审核医生工作量页面'),
('technician', '技师工作量', '技师工作量', 'technician', '{"date": true, "timeRange": false}', '{"table": true, "chart": true, "export": true}', '技师工作量页面'),
('status', '系统状态汇总', '系统状态汇总', 'status', '{"date": true, "timeRange": false}', '{"table": true, "chart": true, "export": true}', '系统状态汇总页面'),
('rank', '执行科室工作量排名', '执行科室工作量排名', 'rank', '{"date": true, "timeRange": false}', '{"table": true, "chart": true, "export": true}', '执行科室工作量排名页面');
