-- 用户表创建脚本
-- 数据库：WiNEX_PACS
-- 执行前请确保已连接到正确的数据库

USE WiNEX_PACS;
GO

-- 创建用户表 TJYHB
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TJYHB' AND xtype='U')
BEGIN
    CREATE TABLE TJYHB (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) UNIQUE NOT NULL,
        Password NVARCHAR(100) NOT NULL,
        CanViewStats BIT DEFAULT 1,
        IsLocked BIT DEFAULT 0,
        CreatedDate DATETIME DEFAULT GETDATE()
    );
    PRINT '用户表 TJYHB 创建成功';
END
ELSE
BEGIN
    PRINT '用户表 TJYHB 已存在';
END
GO

-- 创建统计配置表 TJTJPZB
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TJTJPZB' AND xtype='U')
BEGIN
    CREATE TABLE TJTJPZB (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        ProcedureName NVARCHAR(100) NOT NULL,
        DisplayName NVARCHAR(200) NOT NULL,
        Description NVARCHAR(500),
        CreatedDate DATETIME DEFAULT GETDATE()
    );
    PRINT '统计配置表 TJTJPZB 创建成功';
END
ELSE
BEGIN
    PRINT '统计配置表 TJTJPZB 已存在';
END
GO

-- 插入默认管理员账号（如果不存在）
IF NOT EXISTS (SELECT * FROM TJYHB WHERE Username = 'lhbdb')
BEGIN
    INSERT INTO TJYHB (Username, Password, CanViewStats, IsLocked)
    VALUES ('lhbdb', '241023', 1, 0);
    PRINT '管理员账号 lhbdb 创建成功';
END
ELSE
BEGIN
    PRINT '管理员账号 lhbdb 已存在';
END
GO

-- 插入普通用户账号（如果不存在）
IF NOT EXISTS (SELECT * FROM TJYHB WHERE Username = 'user')
BEGIN
    INSERT INTO TJYHB (Username, Password, CanViewStats, IsLocked)
    VALUES ('user', 'user123', 1, 0);
    PRINT '普通用户账号 user 创建成功';
END
ELSE
BEGIN
    PRINT '普通用户账号 user 已存在';
END
GO

-- 插入只读用户账号（如果不存在）
IF NOT EXISTS (SELECT * FROM TJYHB WHERE Username = 'readonly')
BEGIN
    INSERT INTO TJYHB (Username, Password, CanViewStats, IsLocked)
    VALUES ('readonly', 'readonly123', 1, 0);
    PRINT '只读用户账号 readonly 创建成功';
END
ELSE
BEGIN
    PRINT '只读用户账号 readonly 已存在';
END
GO

-- 插入统计配置示例数据
IF NOT EXISTS (SELECT * FROM TJTJPZB WHERE ProcedureName = 'usp_GetDoctorWorkload')
BEGIN
    INSERT INTO TJTJPZB (ProcedureName, DisplayName, Description)
    VALUES ('usp_GetDoctorWorkload', '医生工作量统计', '统计医生的检查工作量');
    PRINT '统计配置 usp_GetDoctorWorkload 创建成功';
END
GO

IF NOT EXISTS (SELECT * FROM TJTJPZB WHERE ProcedureName = 'usp_GetExamStats')
BEGIN
    INSERT INTO TJTJPZB (ProcedureName, DisplayName, Description)
    VALUES ('usp_GetExamStats', '检查统计', '统计各科室检查数量');
    PRINT '统计配置 usp_GetExamStats 创建成功';
END
GO

-- 查询所有用户
SELECT '用户列表' AS Info;
SELECT UserID, Username, CanViewStats, IsLocked, CreatedDate FROM TJYHB;
GO

-- 查询所有统计配置
SELECT '统计配置列表' AS Info;
SELECT ID, ProcedureName, DisplayName, Description FROM TJTJPZB;
GO

PRINT '数据脚本执行完成！';