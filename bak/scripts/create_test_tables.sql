-- 创建测试业务表（用于开发测试）
-- 如果您使用的是真实医院数据库，这些表应该已存在

-- 1. 检查任务表
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EXAM_TASK')
BEGIN
    CREATE TABLE EXAM_TASK (
        ID INT PRIMARY KEY,
        SYSTEM_SOURCE_NO VARCHAR(50),
        EXAM_CATEGORY_NAME VARCHAR(100),
        TECHNICIAN_NAME VARCHAR(100),
        EXAM_TASK_CREATE_TIME DATETIME,
        IS_DEL INT DEFAULT 0
    )
    PRINT 'EXAM_TASK表已创建'
END

-- 2. 检查报告表
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EXAM_REPORT')
BEGIN
    CREATE TABLE EXAM_REPORT (
        ID INT PRIMARY KEY,
        TASK_ID INT,
        REPORTER_NAME VARCHAR(100),
        REVIEWER_NAME VARCHAR(100),
        REPORT_RESULT VARCHAR(500),
        REPORT_TIME DATETIME
    )
    PRINT 'EXAM_REPORT表已创建'
END

-- 3. 检查任务信息表
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'EXAM_TASK_INFO')
BEGIN
    CREATE TABLE EXAM_TASK_INFO (
        ID INT PRIMARY KEY,
        TASK_ID INT,
        PATIENT_TYPE VARCHAR(50),
        SYSTEM_SOURCE_NO VARCHAR(50)
    )
    PRINT 'EXAM_TASK_INFO表已创建'
END

-- 4. 检查医生表
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DOCTOR_INFO')
BEGIN
    CREATE TABLE DOCTOR_INFO (
        ID INT PRIMARY KEY,
        DOCTOR_CODE VARCHAR(50),
        DOCTOR_NAME NVARCHAR(100),
        IS_REPORTER INT DEFAULT 0,
        IS_REVIEWER INT DEFAULT 0,
        IS_ACTIVE INT DEFAULT 1,
        SYSTEM_TYPE VARCHAR(50)
    )
    -- 插入测试医生数据
    INSERT INTO DOCTOR_INFO (ID, DOCTOR_CODE, DOCTOR_NAME, IS_REPORTER, IS_REVIEWER, IS_ACTIVE, SYSTEM_TYPE)
    VALUES
    (1, 'D001', '张医生', 1, 0, 1, NULL),
    (2, 'D002', '李医生', 1, 0, 1, NULL),
    (3, 'D003', '王医生', 0, 1, 1, NULL),
    (4, 'D004', '赵医生', 0, 1, 1, NULL)
    PRINT 'DOCTOR_INFO表已创建并插入测试数据'
END

-- 5. 检查科室映射表
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DEPT_CATEGORY_MAPPING')
BEGIN
    CREATE TABLE DEPT_CATEGORY_MAPPING (
        ID INT PRIMARY KEY,
        DepartmentName VARCHAR(100),
        CategoryName VARCHAR(100)
    )
    -- 插入测试数据
    INSERT INTO DEPT_CATEGORY_MAPPING (ID, DepartmentName, CategoryName)
    VALUES
    (1, '消化内镜(总)', '消化内镜'),
    (2, '呼吸内镜科', '呼吸内镜'),
    (3, '放射科', '放射'),
    (4, '超声科', '超声')
    PRINT 'DEPT_CATEGORY_MAPPING表已创建并插入测试数据'
END

-- 6. 检查系统配置表
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SYS_CONFIG')
BEGIN
    CREATE TABLE SYS_CONFIG (
        ID INT PRIMARY KEY,
        CONFIG_KEY VARCHAR(100),
        CONFIG_VALUE NVARCHAR(500)
    )
    -- 插入测试数据
    INSERT INTO SYS_CONFIG (ID, CONFIG_KEY, CONFIG_VALUE)
    VALUES (1, 'HOSPITAL_NAME', '示例医院')
    PRINT 'SYS_CONFIG表已创建并插入测试数据'
END

PRINT '所有测试表创建完成！'
