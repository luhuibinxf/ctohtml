-- 创建用户表 TJYHB
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TJYHB' AND xtype='U')
CREATE TABLE TJYHB (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    CanViewStats BIT DEFAULT 1,
    IsLocked BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- 添加默认管理员用户
IF NOT EXISTS (SELECT * FROM TJYHB WHERE Username='lhbdb')
INSERT INTO TJYHB (Username, Password, CanViewStats, IsLocked)
VALUES ('lhbdb', '241023', 1, 0);

-- 添加默认普通用户
IF NOT EXISTS (SELECT * FROM TJYHB WHERE Username='user')
INSERT INTO TJYHB (Username, Password, CanViewStats, IsLocked)
VALUES ('user', 'user123', 1, 0);

-- 添加默认只读用户
IF NOT EXISTS (SELECT * FROM TJYHB WHERE Username='readonly')
INSERT INTO TJYHB (Username, Password, CanViewStats, IsLocked)
VALUES ('readonly', 'readonly123', 0, 0);

-- 添加默认锁定用户
IF NOT EXISTS (SELECT * FROM TJYHB WHERE Username='locked')
INSERT INTO TJYHB (Username, Password, CanViewStats, IsLocked)
VALUES ('locked', 'locked123', 1, 1);

-- 创建存储过程：获取用户信息
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserInfo]') AND type in (N'P', N'PC'))
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetUserInfo]
    @Username NVARCHAR(50)
AS
BEGIN
    SELECT * FROM TJYHB WHERE Username = @Username
END
';

-- 创建存储过程：添加用户
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddUser]') AND type in (N'P', N'PC'))
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[AddUser]
    @Username NVARCHAR(50),
    @Password NVARCHAR(100),
    @CanViewStats BIT,
    @IsLocked BIT
AS
BEGIN
    INSERT INTO TJYHB (Username, Password, CanViewStats, IsLocked)
    VALUES (@Username, @Password, @CanViewStats, @IsLocked)
END
';

-- 创建存储过程：更新用户
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateUser]') AND type in (N'P', N'PC'))
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[UpdateUser]
    @UserID INT,
    @Password NVARCHAR(100),
    @CanViewStats BIT,
    @IsLocked BIT
AS
BEGIN
    UPDATE TJYHB
    SET Password = @Password,
        CanViewStats = @CanViewStats,
        IsLocked = @IsLocked
    WHERE UserID = @UserID
END
';

-- 创建存储过程：删除用户
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteUser]') AND type in (N'P', N'PC'))
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[DeleteUser]
    @UserID INT
AS
BEGIN
    DELETE FROM TJYHB WHERE UserID = @UserID
END
';

-- 创建存储过程：获取所有用户
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllUsers]') AND type in (N'P', N'PC'))
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[GetAllUsers]
AS
BEGIN
    SELECT * FROM TJYHB
END
';

-- 创建存储过程：锁定/解锁用户
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ToggleUserLock]') AND type in (N'P', N'PC'))
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[ToggleUserLock]
    @UserID INT
AS
BEGIN
    UPDATE TJYHB
    SET IsLocked = CASE WHEN IsLocked = 1 THEN 0 ELSE 1 END
    WHERE UserID = @UserID
END
';

-- 查看创建结果
SELECT '用户表创建成功' AS Result;
SELECT * FROM TJYHB;
