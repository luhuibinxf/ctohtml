-- 创建医生信息表
CREATE TABLE IF NOT EXISTS DOCTOR_INFO (
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

-- 创建报告医生查询存储过程
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
      AND (@System IS NULL OR @System = '' OR SYSTEM_TYPE = @System)
    ORDER BY DOCTOR_NAME;
END;

-- 创建审核医生查询存储过程
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
      AND (@System IS NULL OR @System = '' OR SYSTEM_TYPE = @System)
    ORDER BY DOCTOR_NAME;
END;

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
GO