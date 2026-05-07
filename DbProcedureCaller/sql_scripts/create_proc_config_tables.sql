-- =============================================
-- 存储过程配置表 - 创建脚本
-- =============================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'proc_configs')
BEGIN
    CREATE TABLE proc_configs (
        id VARCHAR(50) PRIMARY KEY,
        name VARCHAR(100) NOT NULL,
        icon VARCHAR(50) DEFAULT 'fa-chart-bar',
        procName VARCHAR(200) NOT NULL,
        templateName VARCHAR(100),
        parameters TEXT,
        enabled BIT DEFAULT 1,
        sortOrder INT DEFAULT 100,
        description VARCHAR(500),
        permission VARCHAR(100),
        createdAt DATETIME DEFAULT GETDATE(),
        updatedAt DATETIME DEFAULT GETDATE()
    );
    
    CREATE INDEX IX_proc_configs_enabled ON proc_configs(enabled);
    CREATE INDEX IX_proc_configs_sortOrder ON proc_configs(sortOrder);
    
    PRINT '表 proc_configs 创建成功';
END
GO

-- =============================================
-- 示例数据
-- =============================================

IF NOT EXISTS (SELECT * FROM proc_configs WHERE id = 'rad_workload')
BEGIN
    INSERT INTO proc_configs (id, name, icon, procName, templateName, parameters, enabled, sortOrder, description)
    VALUES (
        'rad_workload',
        '影像中心工作量',
        'fa-solid fa-chart-bar',
        'proc_RadiologyWorkload',
        'rad_workload.html',
        '[{"name":"@StartDate","displayName":"开始日期","type":"datetime","isRequired":true},{"name":"@EndDate","displayName":"结束日期","type":"datetime","isRequired":true},{"name":"@StatisticsType","displayName":"统计类型","type":"varchar","options":"报告医生,审核医生,技师","defaultValue":"报告医生"}]',
        1,
        10,
        '统计影像中心各医生工作量及阳性率'
    );
    PRINT '示例配置 rad_workload 已添加';
END
GO

IF NOT EXISTS (SELECT * FROM proc_configs WHERE id = 'department_summary')
BEGIN
    INSERT INTO proc_configs (id, name, icon, procName, templateName, parameters, enabled, sortOrder, description)
    VALUES (
        'department_summary',
        '科室工作量汇总',
        'fa-solid fa-building',
        'proc_DepartmentWorkloadSummary',
        '',
        '[{"name":"@StartDate","displayName":"开始日期","type":"datetime","isRequired":true},{"name":"@EndDate","displayName":"结束日期","type":"datetime","isRequired":true},{"name":"@SystemType","displayName":"系统类型","type":"varchar","options":"放射,超声,内镜,病理"}]',
        1,
        20,
        '按科室汇总检查工作量'
    );
    PRINT '示例配置 department_summary 已添加';
END
GO

IF NOT EXISTS (SELECT * FROM proc_configs WHERE id = 'daily_check')
BEGIN
    INSERT INTO proc_configs (id, name, icon, procName, templateName, parameters, enabled, sortOrder, description)
    VALUES (
        'daily_check',
        '每日检查统计',
        'fa-solid fa-calendar-check',
        'proc_DailyCheckStatistics',
        '',
        '[{"name":"@StatDate","displayName":"统计日期","type":"datetime","isRequired":true}]',
        1,
        30,
        '统计指定日期的检查数据'
    );
    PRINT '示例配置 daily_check 已添加';
END
GO

PRINT '=============================================';
PRINT '存储过程配置表创建完成';
PRINT '=============================================';
GO