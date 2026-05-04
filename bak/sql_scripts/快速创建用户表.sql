-- 简化版数据脚本 - 快速创建用户表
-- 连接信息: 127.0.0.1,1433 / sa / P@ssw0rd / WiNEX_PACS

USE WiNEX_PACS;
GO

-- 创建用户表
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TJYHB' AND xtype='U')
    CREATE TABLE TJYHB (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) UNIQUE NOT NULL,
        Password NVARCHAR(100) NOT NULL,
        CanViewStats BIT DEFAULT 1,
        IsLocked BIT DEFAULT 0,
        CreatedDate DATETIME DEFAULT GETDATE()
    );
GO

-- 创建统计配置表
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TJTJPZB' AND xtype='U')
    CREATE TABLE TJTJPZB (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        ProcedureName NVARCHAR(100) NOT NULL,
        DisplayName NVARCHAR(200) NOT NULL,
        Description NVARCHAR(500),
        CreatedDate DATETIME DEFAULT GETDATE()
    );
GO

-- 插入默认用户（使用 MERGE 避免重复）
MERGE INTO TJYHB AS target
USING (SELECT 'lhbdb' AS Username, '241023' AS Password) AS source
ON target.Username = source.Username
WHEN NOT MATCHED THEN
    INSERT (Username, Password, CanViewStats, IsLocked)
    VALUES (source.Username, source.Password, 1, 0);
GO

MERGE INTO TJYHB AS target
USING (SELECT 'user' AS Username, 'user123' AS Password) AS source
ON target.Username = source.Username
WHEN NOT MATCHED THEN
    INSERT (Username, Password, CanViewStats, IsLocked)
    VALUES (source.Username, source.Password, 1, 0);
GO

MERGE INTO TJYHB AS target
USING (SELECT 'readonly' AS Username, 'readonly123' AS Password) AS source
ON target.Username = source.Username
WHEN NOT MATCHED THEN
    INSERT (Username, Password, CanViewStats, IsLocked)
    VALUES (source.Username, source.Password, 1, 0);
GO

PRINT '数据脚本执行完成！';
SELECT * FROM TJYHB;