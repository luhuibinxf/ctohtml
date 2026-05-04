-- 配置中心数据库表结构（添加 tjfx_ 前缀）
-- 更新日期：2026-04-25

-- ==============================================
-- 1. 配置表
-- ==============================================
CREATE TABLE IF NOT EXISTS tjfx_SYS_CONFIG (
    CONFIG_ID INT IDENTITY(1,1) PRIMARY KEY,
    CONFIG_KEY NVARCHAR(100) NOT NULL UNIQUE,
    CONFIG_VALUE NVARCHAR(MAX) NOT NULL,
    CONFIG_TYPE NVARCHAR(50) NOT NULL,
    DESCRIPTION NVARCHAR(255),
    CREATE_TIME DATETIME DEFAULT GETDATE(),
    UPDATE_TIME DATETIME DEFAULT GETDATE(),
    IS_DEL BIT DEFAULT 0
);

-- ==============================================
-- 2. 用户角色表
-- ==============================================
CREATE TABLE IF NOT EXISTS tjfx_SYS_ROLE (
    ROLE_ID INT IDENTITY(1,1) PRIMARY KEY,
    ROLE_NAME NVARCHAR(50) NOT NULL UNIQUE,
    DESCRIPTION NVARCHAR(255),
    CREATE_TIME DATETIME DEFAULT GETDATE(),
    UPDATE_TIME DATETIME DEFAULT GETDATE(),
    IS_DEL BIT DEFAULT 0
);

-- ==============================================
-- 3. 用户权限表
-- ==============================================
CREATE TABLE IF NOT EXISTS tjfx_SYS_PERMISSION (
    PERMISSION_ID INT IDENTITY(1,1) PRIMARY KEY,
    PERMISSION_NAME NVARCHAR(50) NOT NULL UNIQUE,
    PERMISSION_CODE NVARCHAR(50) NOT NULL UNIQUE,
    DESCRIPTION NVARCHAR(255),
    CREATE_TIME DATETIME DEFAULT GETDATE(),
    UPDATE_TIME DATETIME DEFAULT GETDATE(),
    IS_DEL BIT DEFAULT 0
);

-- ==============================================
-- 4. 角色权限关联表
-- ==============================================
CREATE TABLE IF NOT EXISTS tjfx_SYS_ROLE_PERMISSION (
    ROLE_PERMISSION_ID INT IDENTITY(1,1) PRIMARY KEY,
    ROLE_ID INT NOT NULL,
    PERMISSION_ID INT NOT NULL,
    CREATE_TIME DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ROLE_ID) REFERENCES tjfx_SYS_ROLE(ROLE_ID),
    FOREIGN KEY (PERMISSION_ID) REFERENCES tjfx_SYS_PERMISSION(PERMISSION_ID),
    UNIQUE(ROLE_ID, PERMISSION_ID)
);

-- ==============================================
-- 5. 用户角色关联表
-- ==============================================
CREATE TABLE IF NOT EXISTS tjfx_SYS_USER_ROLE (
    USER_ROLE_ID INT IDENTITY(1,1) PRIMARY KEY,
    YHID INT NOT NULL,
    ROLE_ID INT NOT NULL,
    CREATE_TIME DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (YHID) REFERENCES TJYHB(YHID),
    FOREIGN KEY (ROLE_ID) REFERENCES tjfx_SYS_ROLE(ROLE_ID),
    UNIQUE(YHID, ROLE_ID)
);

-- ==============================================
-- 6. 配置中心初始化数据
-- ==============================================

-- 插入默认角色
INSERT INTO tjfx_SYS_ROLE (ROLE_NAME, DESCRIPTION) VALUES
('超级管理员', '拥有所有权限'),
('管理员', '拥有管理权限'),
('普通用户', '拥有基本操作权限');

-- 插入默认权限
INSERT INTO tjfx_SYS_PERMISSION (PERMISSION_NAME, PERMISSION_CODE, DESCRIPTION) VALUES
('用户管理', 'USER_MANAGE', '添加、编辑、删除用户'),
('权限管理', 'PERMISSION_MANAGE', '管理角色和权限'),
('参数配置', 'PARAM_CONFIG', '配置系统参数'),
('每日分析', 'DAILY_ANALYSIS', '查看每日统计分析'),
('存储配置', 'STORAGE_CONFIG', '配置存储策略');

-- 关联角色权限
INSERT INTO tjfx_SYS_ROLE_PERMISSION (ROLE_ID, PERMISSION_ID) VALUES
(1, 1), (1, 2), (1, 3), (1, 4), (1, 5), -- 超级管理员
(2, 1), (2, 3), (2, 4), (2, 5),         -- 管理员
(3, 4);                                 -- 普通用户

-- 插入默认配置
INSERT INTO tjfx_SYS_CONFIG (CONFIG_KEY, CONFIG_VALUE, CONFIG_TYPE, DESCRIPTION) VALUES
('DB_CONNECTION', 'Data Source=localhost;Initial Catalog=WiNEX_PACS;Integrated Security=True;', 'DATABASE', '数据库连接字符串'),
('SYSTEM_NAME', '医疗影像系统', 'SYSTEM', '系统名称'),
('DEFAULT_ROLE', '3', 'SYSTEM', '默认用户角色ID'),
('MAX_UPLOAD_SIZE', '104857600', 'SYSTEM', '最大上传文件大小（字节）');

-- ==============================================
-- 7. 配置管理存储过程
-- ==============================================

-- 获取配置
CREATE PROCEDURE [dbo].[GetConfig]
    @ConfigKey NVARCHAR(100)
AS
BEGIN
    SELECT CONFIG_VALUE FROM tjfx_SYS_CONFIG WHERE CONFIG_KEY = @ConfigKey AND IS_DEL = 0;
END;

-- 设置配置
CREATE PROCEDURE [dbo].[SetConfig]
    @ConfigKey NVARCHAR(100),
    @ConfigValue NVARCHAR(MAX),
    @ConfigType NVARCHAR(50),
    @Description NVARCHAR(255)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM tjfx_SYS_CONFIG WHERE CONFIG_KEY = @ConfigKey AND IS_DEL = 0)
    BEGIN
        UPDATE tjfx_SYS_CONFIG 
        SET CONFIG_VALUE = @ConfigValue, 
            CONFIG_TYPE = @ConfigType, 
            DESCRIPTION = @Description, 
            UPDATE_TIME = GETDATE()
        WHERE CONFIG_KEY = @ConfigKey AND IS_DEL = 0;
    END
    ELSE
    BEGIN
        INSERT INTO tjfx_SYS_CONFIG (CONFIG_KEY, CONFIG_VALUE, CONFIG_TYPE, DESCRIPTION)
        VALUES (@ConfigKey, @ConfigValue, @ConfigType, @Description);
    END;
END;

-- 获取用户角色
CREATE PROCEDURE [dbo].[GetUserRole]
    @YHID INT
AS
BEGIN
    SELECT r.ROLE_ID, r.ROLE_NAME 
    FROM tjfx_SYS_USER_ROLE ur
    JOIN tjfx_SYS_ROLE r ON ur.ROLE_ID = r.ROLE_ID
    WHERE ur.YHID = @YHID AND r.IS_DEL = 0;
END;

-- 获取角色权限
CREATE PROCEDURE [dbo].[GetRolePermissions]
    @RoleID INT
AS
BEGIN
    SELECT p.PERMISSION_ID, p.PERMISSION_NAME, p.PERMISSION_CODE 
    FROM tjfx_SYS_ROLE_PERMISSION rp
    JOIN tjfx_SYS_PERMISSION p ON rp.PERMISSION_ID = p.PERMISSION_ID
    WHERE rp.ROLE_ID = @RoleID AND p.IS_DEL = 0;
END;