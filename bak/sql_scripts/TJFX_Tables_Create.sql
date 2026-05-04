-- =============================================
-- 统计分析系统 - 数据库表结构优化脚本
-- 创建日期: 2026-05-01
-- 说明: 创建优化后所需的所有表
-- =============================================

USE [WiNEX_PACS_Schema_Full]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- 1. TJFX_QUERY_CONFIG - 查询配置表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TJFX_QUERY_CONFIG]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[TJFX_QUERY_CONFIG](
        [ID] [INT] IDENTITY(1,1) NOT NULL,
        [QUERY_NAME] [NVARCHAR](100) NOT NULL,
        [QUERY_TYPE] [NVARCHAR](50) NOT NULL,
        [QUERY_SQL] [NVARCHAR](MAX) NOT NULL,
        [PARAMS_MAPPING] [NVARCHAR](MAX) NULL,
        [PERMISSION_REQUIRED] [INT] NOT NULL DEFAULT 0,
        [IS_ACTIVE] [BIT] NOT NULL DEFAULT 1,
        [DESCRIPTION] [NVARCHAR](255) NULL,
        [CREATED_BY] [NVARCHAR](50) NULL,
        [CREATED_TIME] [DATETIME] NOT NULL DEFAULT GETDATE(),
        [UPDATED_BY] [NVARCHAR](50) NULL,
        [UPDATED_TIME] [DATETIME] NULL,
        CONSTRAINT [PK_TJFX_QUERY_CONFIG] PRIMARY KEY CLUSTERED ([ID] ASC)
    ) ON [PRIMARY]
    PRINT '✅ 表 TJFX_QUERY_CONFIG 创建成功'
END
ELSE
    PRINT 'ℹ️ 表 TJFX_QUERY_CONFIG 已存在'
GO

-- =============================================
-- 2. TJFX_CODE_MAPPING - 编码映射表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TJFX_CODE_MAPPING]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[TJFX_CODE_MAPPING](
        [ID] [INT] IDENTITY(1,1) NOT NULL,
        [CATEGORY] [NVARCHAR](50) NOT NULL,
        [SOURCE_VALUE] [NVARCHAR](100) NOT NULL,
        [TARGET_VALUE] [NVARCHAR](100) NOT NULL,
        [SORT_ORDER] [INT] NOT NULL DEFAULT 0,
        [IS_ACTIVE] [BIT] NOT NULL DEFAULT 1,
        [DESCRIPTION] [NVARCHAR](255) NULL,
        [CREATED_TIME] [DATETIME] NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_TJFX_CODE_MAPPING] PRIMARY KEY CLUSTERED ([ID] ASC)
    ) ON [PRIMARY]
    
    CREATE UNIQUE INDEX [UX_TJFX_CODE_MAPPING_CATEGORY_SOURCE] 
        ON [dbo].[TJFX_CODE_MAPPING]([CATEGORY], [SOURCE_VALUE])
    PRINT '✅ 表 TJFX_CODE_MAPPING 创建成功'
END
ELSE
    PRINT 'ℹ️ 表 TJFX_CODE_MAPPING 已存在'
GO

-- =============================================
-- 3. TJFX_MENU_CONFIG - 菜单配置表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TJFX_MENU_CONFIG]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[TJFX_MENU_CONFIG](
        [ID] [INT] IDENTITY(1,1) NOT NULL,
        [MENU_NAME] [NVARCHAR](100) NOT NULL,
        [MENU_URL] [NVARCHAR](200) NULL,
        [PARENT_ID] [INT] NULL,
        [PERMISSION_LEVEL] [INT] NOT NULL DEFAULT 0,
        [SORT_ORDER] [INT] NOT NULL DEFAULT 0,
        [IS_ACTIVE] [BIT] NOT NULL DEFAULT 1,
        [ICON] [NVARCHAR](50) NULL,
        CONSTRAINT [PK_TJFX_MENU_CONFIG] PRIMARY KEY CLUSTERED ([ID] ASC)
    ) ON [PRIMARY]
    PRINT '✅ 表 TJFX_MENU_CONFIG 创建成功'
END
ELSE
    PRINT 'ℹ️ 表 TJFX_MENU_CONFIG 已存在'
GO

-- =============================================
-- 4. TJFX_DB_CONFIG - 多数据库配置表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TJFX_DB_CONFIG]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[TJFX_DB_CONFIG](
        [ID] [INT] IDENTITY(1,1) NOT NULL,
        [CONFIG_NAME] [NVARCHAR](100) NOT NULL,
        [SERVER] [NVARCHAR](200) NOT NULL,
        [DATABASE] [NVARCHAR](100) NOT NULL,
        [USERNAME] [NVARCHAR](50) NOT NULL,
        [PASSWORD] [NVARCHAR](256) NOT NULL,
        [IS_DEFAULT] [BIT] NOT NULL DEFAULT 0,
        [IS_ACTIVE] [BIT] NOT NULL DEFAULT 1,
        [CREATED_TIME] [DATETIME] NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_TJFX_DB_CONFIG] PRIMARY KEY CLUSTERED ([ID] ASC)
    ) ON [PRIMARY]
    PRINT '✅ 表 TJFX_DB_CONFIG 创建成功'
END
ELSE
    PRINT 'ℹ️ 表 TJFX_DB_CONFIG 已存在'
GO

-- =============================================
-- 5. TJFX_ACCESS_TOKEN - 无密码登录令牌表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TJFX_ACCESS_TOKEN]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[TJFX_ACCESS_TOKEN](
        [ID] [INT] IDENTITY(1,1) NOT NULL,
        [TOKEN] [VARCHAR](64) NOT NULL,
        [USER_ID] [INT] NOT NULL,
        [EXPIRE_TIME] [DATETIME] NOT NULL,
        [IS_USED] [BIT] NOT NULL DEFAULT 0,
        [CREATED_TIME] [DATETIME] NOT NULL DEFAULT GETDATE(),
        [USED_TIME] [DATETIME] NULL,
        CONSTRAINT [PK_TJFX_ACCESS_TOKEN] PRIMARY KEY CLUSTERED ([ID] ASC)
    ) ON [PRIMARY]
    
    CREATE UNIQUE INDEX [UX_TJFX_ACCESS_TOKEN_TOKEN] 
        ON [dbo].[TJFX_ACCESS_TOKEN]([TOKEN])
    PRINT '✅ 表 TJFX_ACCESS_TOKEN 创建成功'
END
ELSE
    PRINT 'ℹ️ 表 TJFX_ACCESS_TOKEN 已存在'
GO

-- =============================================
-- 6. TJFX_ROLE - 角色表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TJFX_ROLE]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[TJFX_ROLE](
        [ID] [INT] IDENTITY(1,1) NOT NULL,
        [ROLE_NAME] [NVARCHAR](50) NOT NULL,
        [PERMISSIONS] [NVARCHAR](MAX) NULL,
        [DESCRIPTION] [NVARCHAR](255) NULL,
        [IS_ACTIVE] [BIT] NOT NULL DEFAULT 1,
        [CREATED_TIME] [DATETIME] NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_TJFX_ROLE] PRIMARY KEY CLUSTERED ([ID] ASC)
    ) ON [PRIMARY]
    PRINT '✅ 表 TJFX_ROLE 创建成功'
END
ELSE
    PRINT 'ℹ️ 表 TJFX_ROLE 已存在'
GO

-- =============================================
-- 7. TJFX_USER_ROLE - 用户角色映射表
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TJFX_USER_ROLE]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[TJFX_USER_ROLE](
        [USER_ID] [INT] NOT NULL,
        [ROLE_ID] [INT] NOT NULL,
        [CREATED_TIME] [DATETIME] NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_TJFX_USER_ROLE] PRIMARY KEY CLUSTERED ([USER_ID] ASC, [ROLE_ID] ASC)
    ) ON [PRIMARY]
    PRINT '✅ 表 TJFX_USER_ROLE 创建成功'
END
ELSE
    PRINT 'ℹ️ 表 TJFX_USER_ROLE 已存在'
GO

-- =============================================
-- 8. 修改 TJYHB - 用户表优化
-- =============================================
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TJYHB]') AND type in (N'U'))
BEGIN
    -- 检查并添加 SALT 字段
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[TJYHB]') AND name = 'SALT')
    BEGIN
        ALTER TABLE [dbo].[TJYHB] ADD [SALT] [VARCHAR](64) NULL
        PRINT '✅ 字段 TJYHB.SALT 添加成功'
    END
    ELSE
        PRINT 'ℹ️ 字段 TJYHB.SALT 已存在'
    
    -- 检查并添加 LAST_LOGIN_TIME 字段
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[TJYHB]') AND name = 'LAST_LOGIN_TIME')
    BEGIN
        ALTER TABLE [dbo].[TJYHB] ADD [LAST_LOGIN_TIME] [DATETIME] NULL
        PRINT '✅ 字段 TJYHB.LAST_LOGIN_TIME 添加成功'
    END
    ELSE
        PRINT 'ℹ️ 字段 TJYHB.LAST_LOGIN_TIME 已存在'
    
    -- 检查并添加 LAST_LOGIN_IP 字段
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[TJYHB]') AND name = 'LAST_LOGIN_IP')
    BEGIN
        ALTER TABLE [dbo].[TJYHB] ADD [LAST_LOGIN_IP] [VARCHAR](50) NULL
        PRINT '✅ 字段 TJYHB.LAST_LOGIN_IP 添加成功'
    END
    ELSE
        PRINT 'ℹ️ 字段 TJYHB.LAST_LOGIN_IP 已存在'
    
    -- 检查并调整 YKL 字段长度
    DECLARE @CurrentYKLSize INT
    SELECT @CurrentYKLSize = max_length 
    FROM sys.columns 
    WHERE object_id = OBJECT_ID(N'[dbo].[TJYHB]') AND name = 'YKL'
    
    IF @CurrentYKLSize < 256
    BEGIN
        ALTER TABLE [dbo].[TJYHB] ALTER COLUMN [YKL] [VARCHAR](256) NOT NULL
        PRINT '✅ 字段 TJYHB.YKL 长度调整成功'
    END
    ELSE
        PRINT 'ℹ️ 字段 TJYHB.YKL 长度无需调整'
END
GO

PRINT '============================================='
PRINT '🎉 所有表结构处理完成！'
PRINT '============================================='
GO
