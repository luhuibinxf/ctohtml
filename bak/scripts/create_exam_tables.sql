-- ========================================
-- HIS/RIS业务表创建脚本
-- 创建时间: 2026-04-29
-- 说明: 创建统计系统所需的HIS业务表（测试用）
-- ========================================

-- 1. 检查任务表 EXAM_TASK
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EXAM_TASK]') AND type in (N'U'))
BEGIN
    CREATE TABLE EXAM_TASK (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        SYSTEM_SOURCE_NO NVARCHAR(50) NOT NULL,      -- 系统标识
        EXAM_CATEGORY_NAME NVARCHAR(100),            -- 检查类别名称
        TECHNICIAN_NAME NVARCHAR(100),                -- 技师姓名
        EXAM_TASK_CREATE_TIME DATETIME DEFAULT GETDATE(), -- 任务创建时间
        EXAM_TASK_STATUS NVARCHAR(50) DEFAULT '进行中', -- 任务状态
        IS_DEL INT DEFAULT 0                          -- 是否删除
    );
    PRINT 'EXAM_TASK表创建成功'
END
ELSE
BEGIN
    PRINT 'EXAM_TASK表已存在'
END
GO

-- 2. 检查报告表 EXAM_REPORT
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EXAM_REPORT]') AND type in (N'U'))
BEGIN
    CREATE TABLE EXAM_REPORT (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        TASK_ID INT,                                  -- 关联任务ID
        REPORTER_NAME NVARCHAR(100),                  -- 报告医生姓名
        REVIEWER_NAME NVARCHAR(100),                  -- 审核医生姓名
        REPORT_RESULT NVARCHAR(500),                  -- 报告结果
        REPORT_TIME DATETIME DEFAULT GETDATE(),        -- 报告时间
        IS_DEL INT DEFAULT 0                          -- 是否删除
    );
    PRINT 'EXAM_REPORT表创建成功'
END
ELSE
BEGIN
    PRINT 'EXAM_REPORT表已存在'
END
GO

-- 3. 任务信息表 EXAM_TASK_INFO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EXAM_TASK_INFO]') AND type in (N'U'))
BEGIN
    CREATE TABLE EXAM_TASK_INFO (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        TASK_ID INT,                                  -- 关联任务ID
        PATIENT_TYPE NVARCHAR(50),                    -- 病人类型（门诊/住院/急诊）
        SYSTEM_SOURCE_NO NVARCHAR(50),                -- 系统标识
        IS_DEL INT DEFAULT 0                          -- 是否删除
    );
    PRINT 'EXAM_TASK_INFO表创建成功'
END
ELSE
BEGIN
    PRINT 'EXAM_TASK_INFO表已存在'
END
GO

-- 4. 插入测试数据
IF NOT EXISTS (SELECT * FROM EXAM_TASK)
BEGIN
    INSERT INTO EXAM_TASK (SYSTEM_SOURCE_NO, EXAM_CATEGORY_NAME, TECHNICIAN_NAME, EXAM_TASK_CREATE_TIME, EXAM_TASK_STATUS)
    VALUES
    ('RIS', 'CT', '李技师', DATEADD(day, -5, GETDATE()), '已完成'),
    ('RIS', 'CT', '李技师', DATEADD(day, -4, GETDATE()), '已完成'),
    ('RIS', 'CT', '张技师', DATEADD(day, -3, GETDATE()), '已完成'),
    ('RIS', '核磁共振', '王技师', DATEADD(day, -2, GETDATE()), '进行中'),
    ('UIS', '超声', '赵技师', DATEADD(day, -1, GETDATE()), '已完成'),
    ('UIS', '彩超', '钱技师', GETDATE(), '进行中'),
    ('EIS', '胃镜', '孙技师', DATEADD(day, -2, GETDATE()), '已完成'),
    ('EIS', '肠镜', '周技师', DATEADD(day, -1, GETDATE()), '已完成'),
    ('RIS', '普放', '吴技师', DATEADD(day, -3, GETDATE()), '已完成'),
    ('RIS', 'CT', '郑技师', GETDATE(), '进行中');
    PRINT 'EXAM_TASK测试数据插入成功'
END
GO

IF NOT EXISTS (SELECT * FROM EXAM_REPORT)
BEGIN
    INSERT INTO EXAM_REPORT (TASK_ID, REPORTER_NAME, REVIEWER_NAME, REPORT_RESULT, REPORT_TIME)
    VALUES
    (1, '张医生', '王医生', '未见异常', DATEADD(day, -5, GETDATE())),
    (2, '张医生', '王医生', '未见异常', DATEADD(day, -4, GETDATE())),
    (3, '李医生', '赵医生', '肺炎', DATEADD(day, -3, GETDATE())),
    (5, '周医生', '吴医生', '正常', DATEADD(day, -1, GETDATE())),
    (7, '郑医生', '陈医生', '胃炎', DATEADD(day, -2, GETDATE())),
    (8, '郑医生', '陈医生', '未见异常', DATEADD(day, -1, GETDATE())),
    (9, '杨医生', '黄医生', '骨折', DATEADD(day, -3, GETDATE()));
    PRINT 'EXAM_REPORT测试数据插入成功'
END
GO

PRINT 'HIS业务表创建完成！'
