-- =============================================
-- 统计分析系统数据库初始化脚本
-- =============================================

-- 创建用户表 TJYHB
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TJYHB' AND xtype='U')
BEGIN
    CREATE TABLE TJYHB (
        ID INT PRIMARY KEY,                    -- 用户ID
        YHM VARCHAR(50) NOT NULL UNIQUE,       -- 用户名 (YongHuMing)
        YKL VARCHAR(100) NOT NULL,             -- 用户密码 (YongHuMiMa/KL)
        QX INT DEFAULT 0,                      -- 权限 (QuanXian): 0=普通用户, 1=管理员
        SFY INT DEFAULT 1                      -- 是否启用 (ShiFouYong): 0=禁用, 1=启用
    )
    PRINT '表 TJYHB 创建成功'
END
ELSE
BEGIN
    PRINT '表 TJYHB 已存在'
END
GO

-- 插入默认管理员用户
IF NOT EXISTS (SELECT * FROM TJYHB WHERE YHM = 'lhbdb')
BEGIN
    INSERT INTO TJYHB (ID, YHM, YKL, QX, SFY)
    VALUES (1, 'lhbdb', '241023', 1, 1)
    PRINT '默认管理员用户 lhbdb 创建成功'
END
ELSE
BEGIN
    PRINT '默认管理员用户 lhbdb 已存在'
END
GO

-- 创建每日分析统计视图 (如果需要)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='VW_DailyAnalysis' AND xtype='V')
BEGIN
    CREATE VIEW VW_DailyAnalysis AS
    SELECT 
        'RIS' AS 系统,
        '张医生' AS 报告医生,
        '赵医生' AS 审核医生,
        '李技师' AS 技师,
        '放射科' AS 执行科室,
        'CT' AS 检查类型,
        '门诊' AS 病人类型,
        '阳性' AS 结果状态,
        100 AS 任务数量,
        25 AS 阳性数量,
        75 AS 阴性数量,
        25.0 AS 阳性率
    UNION ALL
    SELECT 
        'RIS' AS 系统,
        '李医生' AS 报告医生,
        '王医生' AS 审核医生,
        '张技师' AS 技师,
        '超声科' AS 执行科室,
        '超声' AS 检查类型,
        '住院' AS 病人类型,
        '阴性' AS 结果状态,
        80 AS 任务数量,
        16 AS 阳性数量,
        64 AS 阴性数量,
        20.0 AS 阳性率
    PRINT '视图 VW_DailyAnalysis 创建成功'
END
GO

PRINT '数据库初始化完成'
